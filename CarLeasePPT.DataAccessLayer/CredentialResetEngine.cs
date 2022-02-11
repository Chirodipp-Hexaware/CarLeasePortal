using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CarLeasePPT.Utility;

namespace CarLeasePPT.DataAccessLayer
{
    public static class CredentialResetEngine
    {
        #region Public Methods and Operators

        public static async Task<bool> CreatePersonSecurityResetAsync(int personId, string tokenHash, int tokenLife,
            bool skipMultifactor = false)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var ints = await e.PersonSecurityResets.Where(t => t.PersonId.Equals(personId))
                        .Select(t => t.PersonSecurityResetId).ToListAsync();
                    foreach (var p in ints)
                    {
                        var personSecurityReset = await e.PersonSecurityResets.FindAsync(p);
                        if (personSecurityReset != null)
                            e.PersonSecurityResets.Remove(personSecurityReset);
                    }

                    e.PersonSecurityResets.Add(new PersonSecurityReset
                    {
                        PersonId = personId,
                        ResetExpiration = DateTime.Now.AddMinutes(tokenLife),
                        ResetToken = tokenHash,
                        ResetTokenClaimed = false,
                        RequiresSecondary = skipMultifactor
                    });
                    await e.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return false;
            }
        }

        public static async Task DeletePersonSecurityResetsAsync()
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var ints = await e.PersonSecurityResets
                        .Where(t => t.ResetExpiration <= DateTime.Now || t.ResetTokenClaimed)
                        .Select(t => t.PersonSecurityResetId).ToListAsync();
                    foreach (var p in ints)
                    {
                        var personSecurityReset = await e.PersonSecurityResets.FindAsync(p);
                        if (personSecurityReset != null)
                            e.PersonSecurityResets.Remove(personSecurityReset);
                    }

                    await e.SaveChangesAsync();
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
            }
        }

        public static async Task<IReadOnlyCollection<PersonSecurityResetRecord>>
            GetValidPersonSecurityResetCollectionAsync()
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var otl = await e.PersonSecurityResets
                        .Where(p => !p.ResetTokenClaimed && p.ResetExpiration > DateTime.Now)
                        .Select(p => new PersonSecurityResetRecord
                        {
                            PersonId = p.PersonId,
                            ResetExpiration = p.ResetExpiration,
                            PersonSecurityResetId = p.PersonSecurityResetId,
                            RequiresSecondary = p.RequiresSecondary,
                            ResetToken = p.ResetToken
                        })
                        .ToListAsync();
                    return otl.AsReadOnly();
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return new List<PersonSecurityResetRecord>().AsReadOnly();
            }
        }

        public static async Task<int> FindPersonBySecurityResetTokenAsync(int tokenId)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var t = await e.PersonSecurityResets.FindAsync(tokenId);
                    if (t == null) return -1;
                    return t.PersonId; ;
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return -1;
            }
        }

        public static async Task RedeemPersonSecurityResetTokensAsync(int personId)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var tokens = await e.PersonSecurityResets.Where(t => t.PersonId.Equals(personId)).ToListAsync();
                    foreach (var t in tokens)
                    {
                        t.ResetTokenClaimed = true;
                        e.Entry(t).State = EntityState.Modified;
                    }
                    await e.SaveChangesAsync();
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
            }
        }

        public static async Task<RedeemedToken> RedeemPersonSecurityResetAsync(int tokenId)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var t = await e.PersonSecurityResets.FindAsync(tokenId);
                    if (t == null) return new RedeemedToken { PersonId = -1, BypassTwoFactor = false };
                    t.ResetTokenClaimed = true;
                    e.Entry(t).State = EntityState.Modified;
                    await e.SaveChangesAsync();
                    return new RedeemedToken { PersonId = t.PersonId, BypassTwoFactor = t.RequiresSecondary };
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return new RedeemedToken { PersonId = -1, BypassTwoFactor = false };
            }
        }

        #endregion
    }
}