#region

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CarLeasePPT.Utility;

#endregion

namespace CarLeasePPT.DataAccessLayer
{
    public static class PersonSecurityEngine
    {
        #region Public Methods and Operators

        public static async Task<IReadOnlyCollection<string>> GetPasswordSecurityHashListAsync(int personId,
            int reuseCount)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var topNthSecurityResets = await
                        e.PersonSecurities.Where(
                                p => p.PersonId.Equals(personId) &&
                                     p.PersonSecurityTypeId.Equals(SecurityMethod.Password))
                            .OrderByDescending(p => p.PersonSecurityId)
                            .Take(reuseCount)
                            .Select(p => p.SecurityValue)
                            .ToListAsync();
                    return topNthSecurityResets.AsReadOnly();
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return new List<string>().AsReadOnly();
            }
        }

        public static async Task<PersonSecurityRecord> GetSecurityRecord(int personId)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var psr =
                        await e.PersonSecurities.Where(
                                p => p.PersonId.Equals(personId) &&
                                     p.PersonSecurityTypeId.Equals(SecurityMethod.Password))
                            .OrderByDescending(p => p.ExpirationDateTime)
                            .Take(1)
                            .Select(p => new PersonSecurityRecord
                            {
                                PersonId = p.PersonId,
                                PersonSecurityId = p.PersonSecurityId,
                                EffectiveDateTime = p.EffectiveDateTime,
                                ExpirationDateTime = p.ExpirationDateTime,
                                SecurityValue = p.SecurityValue
                            }).SingleAsync();
                    return psr;
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return null;
            }
        }

        public static async Task<bool> SaveRecoveryCodesAsync(int personId,
            IEnumerable<string> recoveryTokenHashCollection)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var existingTokens = await
                        e.PersonSecurities.Where(
                                p =>
                                    p.PersonId.Equals(personId) &&
                                    p.PersonSecurityTypeId.Equals(SecurityMethod.RecoveryCode))
                            .Select(p => p.PersonSecurityId)
                            .ToListAsync();
                    foreach (var t in existingTokens)
                    {
                        var psr = e.PersonSecurities.Find(t);
                        if (psr == null) continue;
                        e.PersonSecurities.Remove(psr);
                    }

                    foreach (var t in recoveryTokenHashCollection)
                        e.PersonSecurities.Add(new PersonSecurity
                        {
                            SecurityValue = t,
                            EffectiveDateTime = DateTime.Now,
                            ExpirationDateTime = DateTime.MaxValue,
                            ModifiedByPersonId = personId,
                            ModifiedOnDateTime = DateTime.Now,
                            PersonId = personId,
                            PersonSecurityTypeId = SecurityMethod.RecoveryCode
                        });
                    await e.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return false;
            }
        }

        public static async Task<bool> UpdatePasswordAsync(int personId, string hash, int expirePeriod)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var n = DateTime.Now;
                    var dbQuery = e.PersonSecurities.Where(p => p.PersonId.Equals(personId));
                    dbQuery = dbQuery.Where(p => p.PersonSecurityTypeId.Equals(SecurityMethod.Password));
                    dbQuery = dbQuery.Where(p => p.EffectiveDateTime <= n && p.ExpirationDateTime >= n);
                    var psr = await dbQuery.ToListAsync();
                    foreach (var p in psr)
                    {
                        p.ExpirationDateTime = DateTime.Now;
                        p.ModifiedByPersonId = personId;
                        p.ModifiedOnDateTime = DateTime.Now;
                        e.Entry(p).State = EntityState.Modified;
                    }

                    var passwordExpiration =
                        expirePeriod.Equals(0) ? DateTime.MaxValue : DateTime.Now.AddDays(expirePeriod);
                    e.PersonSecurities.Add(new PersonSecurity
                    {
                        SecurityValue = hash,
                        EffectiveDateTime = DateTime.Now,
                        ExpirationDateTime = passwordExpiration,
                        CreatedByPersonId = personId,
                        CreatedOnDateTime = DateTime.Now,
                        ModifiedByPersonId = personId,
                        ModifiedOnDateTime = DateTime.Now,
                        PersonId = personId,
                        PersonSecurityTypeId = SecurityMethod.Password
                    });

                    await e.SaveChangesAsync();

                    // Unlock user
                    await PersonEngine.UnlockUser(personId, personId);
                    // Clear authentication failures, if any
                    await AuthenticationFailureEngine.ClearAuthenticationFailureRecordAsync(personId);
                    // Override blocks, if any
                    await AuthenticationFailureEngine.ClearBlockRecordAsync(personId, personId);

                    return true;
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return false;
            }
        }

        #endregion
    }
}