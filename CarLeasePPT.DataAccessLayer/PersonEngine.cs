#region

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LinqKit;
using NLog;
using CarLeasePPT.Utility;

#endregion

namespace CarLeasePPT.DataAccessLayer
{
    [SuppressMessage("ReSharper", "CommentTypo")]
    public static partial class PersonEngine
    {
        #region Public Properties

        public static string InternalUserEmailDomain => ConfigurationManager.AppSettings["InternalUserEmailDomain"];

        #endregion

        #region Public Methods and Operators

        public static async Task<int> CreateAsync(int modifiedByPersonId, string fullName, string preferredName,
            string emailAddress)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var p = new Person
                    {
                        FullName = fullName,
                        PreferredName = preferredName,
                        EffectiveDateTime = DateTime.Now,
                        EmailAddress = emailAddress,
                        ModifiedByPersonId = modifiedByPersonId,
                        ModifiedOnDateTime = DateTime.Now
                    };
                    e.People.Add(p);
                    await e.SaveChangesAsync();

                    // Log new user creation to AuditLog
                    var m = await e.People.FindAsync(modifiedByPersonId);
                    var modifiedByPersonName = m?.FullName ?? "Unknown";
                    var logEvent = new LogEventInfo(LogLevel.Info, "AuditLog",
                        $"Created user identified by {emailAddress}.");
                    logEvent.Properties.Add("PersonId", modifiedByPersonId);
                    logEvent.Properties.Add("FullName", modifiedByPersonName);
                    Audit.Instance.Log(logEvent);

                    return p.PersonId;
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return -1;
            }
        }

        public static async Task DeleteAsync(int personId, int modifiedByPersonId)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var p = e.People.Find(personId);
                    if (p == null) return;
                    var emailAddress = p.EmailAddress;

                    p.ModifiedByPersonId = modifiedByPersonId;
                    p.ModifiedOnDateTime = DateTime.Now;
                    p.ExpirationDateTime = DateTime.Now;
                    e.Entry(p).State = EntityState.Modified;
                    await e.SaveChangesAsync();

                    await RemovePersonRoleRecordsAsync(p.PersonId, modifiedByPersonId);

                    // Log new user creation to AuditLog
                    var m = await e.People.FindAsync(modifiedByPersonId);
                    var modifiedByPersonName = m?.FullName ?? "Unknown";
                    var logEvent = new LogEventInfo(LogLevel.Info, "AuditLog",
                        $"Deleted user identified by {emailAddress}.");
                    logEvent.Properties.Add("PersonId", modifiedByPersonId);
                    logEvent.Properties.Add("FullName", modifiedByPersonName);
                    Audit.Instance.Log(logEvent);
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
            }
        }

        public static async Task<bool> ExistsEmailAsync(string emailAddress)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var exists =
                        await e.People.Where(
                                p => p.EmailAddress.Equals(emailAddress) && !p.IsExpired)
                            .AnyAsync();
                    return exists;
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return true;
            }
        }

        public static async Task<bool> ExistsEmailAsync(int personId, string emailAddress)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var exists =
                        await e.People.Where(
                                p =>
                                    !p.PersonId.Equals(personId) &&
                                    p.EmailAddress.Equals(emailAddress) && !p.IsExpired)
                            .AnyAsync();
                    return exists;
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return true;
            }
        }

        public static Dictionary<int, string> GetAllSessions()
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var dictionary = e.PersonSessions.ToDictionary(t => t.PersonId, t => t.Token);
                    return dictionary;
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return new Dictionary<int, string>();
            }
        }

        public static async Task<PersonRecord> GetAsync(int personId)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var record = await e.People.FindAsync(personId);
                    if (record == null) return null;
                    var personRecord = new PersonRecord
                    {
                        EmailAddress = record.EmailAddress,
                        EffectiveDateTime = record.EffectiveDateTime,
                        ExpirationDateTime = record.ExpirationDateTime,
                        FullName = record.FullName,
                        IsExpired = record.IsExpired,
                        IsLocked = record.IsLocked,
                        ModifiedByPersonId = record.ModifiedByPersonId,
                        ModifiedOnDateTime = record.ModifiedOnDateTime,
                        PersonId = record.PersonId,
                        PreferredName = record.PreferredName
                    };
                    return personRecord;
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return null;
            }
        }

        public static async Task<PersonRecord> GetAsync(string emailAddress)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var record = await e.People.SingleOrDefaultAsync(l => l.EmailAddress.Equals(emailAddress) && !l.IsExpired);
                    if (record == null) return null;
                    var personRecord = new PersonRecord
                    {
                        EmailAddress = record.EmailAddress,
                        EffectiveDateTime = record.EffectiveDateTime,
                        ExpirationDateTime = record.ExpirationDateTime,
                        FullName = record.FullName,
                        IsExpired = record.IsExpired,
                        IsLocked = record.IsLocked,
                        ModifiedByPersonId = record.ModifiedByPersonId,
                        ModifiedOnDateTime = record.ModifiedOnDateTime,
                        PersonId = record.PersonId,
                        PreferredName = record.PreferredName
                    };
                    return personRecord;
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return null;
            }
        }


        public static async Task<UserListData> GetCollectionAsync(string searchBy, int take, int skip, string sortBy,
            bool sortDir, bool includeExpired = false)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var p = PredicateWhere(searchBy);

                    var query = e.People.AsExpandable().Where(p);
                    if (!includeExpired) query = query.Where(person => !person.IsExpired);

                    switch (sortBy)
                    {
                        case "EmailAddress":
                            query = sortDir
                                ? query.OrderBy(v => v.EmailAddress)
                                : query.OrderByDescending(v => v.EmailAddress);
                            break;
                        case "FullName":
                            query = sortDir
                                ? query.OrderBy(v => v.FullName)
                                : query.OrderByDescending(v => v.FullName);
                            break;
                        case "PreferredName":
                            query = sortDir
                                ? query.OrderBy(v => v.PreferredName)
                                : query.OrderByDescending(v => v.PreferredName);
                            break;
                        default:
                            query = query.OrderBy(v => v.EmailAddress);
                            break;
                    }

                    var count = await query.CountAsync();
                    var recordList = await query.Skip(skip).Take(take).ToListAsync();

                    var totalResultsCount = e.People.Count();

                    var people = recordList.Select(pr => new UserRecord
                    {
                        EmailAddress = pr.EmailAddress,
                        FullName = pr.FullName,
                        PersonId = pr.PersonId,
                        PreferredName = pr.PreferredName,
                        IsExpired = pr.IsExpired,
                        IsLocked = pr.IsLocked
                    }).ToList();

                    return new UserListData
                    {
                        data = people,
                        recordsFiltered = count,
                        recordsTotal = totalResultsCount
                    };
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return new UserListData
                {
                    data = new List<UserRecord>(),
                    recordsFiltered = 0,
                    recordsTotal = 0
                };
            }
        }

        public static async Task<PersonAuthenticationRecord> GetPersonAuthenticationRecordAsync(string emailAddress)
        {
            try
            {
                var n = DateTime.Now;
                using (var entities = new HexaEntities())
                {
                    var dbQuery = entities.People.Where(q => q.EmailAddress.Equals(emailAddress) && !q.IsExpired);
                    var viewRecordList =
                        await dbQuery.SelectMany(
                            q => q.PersonSecurities.Where(p => p.PersonSecurityTypeId.Equals(SecurityMethod.Password)),
                            (person, security) => new PersonAuthenticationRecord
                            {
                                PersonId = person.PersonId,
                                SecurityValue = security.SecurityValue,
                                FullName = person.FullName,
                                IsLocked = person.IsLocked,
                                PersonSecurityEffectiveDateTime = security.EffectiveDateTime,
                                PersonSecurityExpirationDateTime = security.ExpirationDateTime,
                                IsExpired = security.ExpirationDateTime <= n
                            }).ToArrayAsync();

                    // Check to see if any records matched the query
                    if (!viewRecordList.Any()) return null;

                    var passwordExpirePeriod = PasswordSecurityPolicy.ExpirePeriod;
                    // Update each PersonAuthenticationRecord record to mark the record as expired. This is a two-part check.
                    foreach (var r in viewRecordList)
                    {
                        // If the security record is expired, mark it as expired
                        r.IsExpired = DateTime.Now > r.PersonSecurityExpirationDateTime;
                        // If not expired, check to see if the password policy has a password expiration policy other than zero
                        if (!r.IsExpired && !passwordExpirePeriod.Equals(0))
                            r.IsExpired = DateTime.Now >
                                          r.PersonSecurityEffectiveDateTime.AddDays(passwordExpirePeriod);
                    }

                    // Ordering by descending effective date on the security record, take the first record and return it.
                    var personAuthRecord =
                        viewRecordList.OrderByDescending(p => p.PersonSecurityEffectiveDateTime).Take(1).Single();
                    return personAuthRecord;
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return null;
            }
        }

        public static async Task<IsBlockedResult> GetPersonBlockResultAsync(int personId)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var n = DateTime.Now;
                    var personBlock = await e.PersonBlocks.Where(b => b.PersonId.Equals(personId)
                                                                      && b.EffectiveDateTime <= n &&
                                                                      b.ExpirationDateTime >= n && !b.IsOverridden
                    ).OrderByDescending(b => b.ExpirationDateTime).Take(1).SingleOrDefaultAsync();
                    if (personBlock != null) return new IsBlockedResult(personBlock.ExpirationDateTime);
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
            }

            return new IsBlockedResult();
        }

        public static async Task<IReadOnlyCollection<int>> GetPersonRoleIdsAsync(int personId)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var currentDateTime = DateTime.Now;
                    var person = await e.People.FindAsync(personId);
                    if (person == null) return new List<int>().AsReadOnly();
                    var personRolesList =
                        person.PersonRoles.Where(
                                r => r.EffectiveDateTime <= currentDateTime && r.ExpirationDateTime >= currentDateTime)
                            .Select(r => r.RoleId)
                            .ToList();
                    return personRolesList.AsReadOnly();
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return new List<int>().AsReadOnly();
            }
        }


        public static async Task<IReadOnlyCollection<RoleRecord>> GetPersonRoleRecordsAsync(int personId)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var currentDateTime = DateTime.Now;
                    var person = await e.People.FindAsync(personId);
                    if (person == null) return new List<RoleRecord>().AsReadOnly();
                    var personRolesList =
                        person.PersonRoles.Where(
                                r => r.EffectiveDateTime <= currentDateTime && r.ExpirationDateTime >= currentDateTime)
                            .OrderBy(r => r.Role.SequenceId)
                            .Select(r => new RoleRecord
                            {
                                RoleCode = r.Role.RoleCode,
                                RoleId = r.RoleId,
                                RoleName = r.Role.RoleName,
                                SequenceId = r.Role.SequenceId,
                                InternalUseOnly = r.Role.InternalUseOnly
                            })
                            .ToList();
                    return personRolesList.AsReadOnly();
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return new List<RoleRecord>().AsReadOnly();
            }
        }

        public static async Task<IReadOnlyCollection<RoleRecord>> GetRoleRecordsAsync(int personId)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var currentDateTime = DateTime.Now;
                    var person = await e.People.FindAsync(personId);
                    if (person == null) return new List<RoleRecord>().AsReadOnly();
                    var personRolesList =
                        person.PersonRoles.Where(
                                r => r.EffectiveDateTime <= currentDateTime && r.ExpirationDateTime >= currentDateTime)
                            .Select(r => new RoleRecord
                            {
                                RoleCode = r.Role.RoleCode,
                                RoleId = r.Role.RoleId,
                                RoleName = r.Role.RoleName,
                                InternalUseOnly = r.Role.InternalUseOnly
                            })
                            .ToList();
                    return personRolesList.AsReadOnly();
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return new List<RoleRecord>().AsReadOnly();
            }
        }


        public static async Task<bool> RemovePersonRoleRecordsAsync(IEnumerable<int> roleIdEnumerable, int personId,
            int modifiedByPersonId)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var personRoleList =
                        e.PersonRoles.Where(role => roleIdEnumerable.Contains(role.RoleId) &&
                                                    role.ExpirationDateTime > DateTime.Now &&
                                                    role.PersonId.Equals(personId));
                    foreach (var role in personRoleList)
                    {
                        role.ExpirationDateTime = DateTime.Now;
                        role.ModifiedByPersonId = modifiedByPersonId;
                        role.ModifiedOnDateTime = DateTime.Now;
                    }

                    await e.SaveChangesAsync();

                    // Log new user creation to AuditLog
                    var p = await e.People.FindAsync(personId);
                    var m = await e.People.FindAsync(modifiedByPersonId);
                    var modifiedByPersonName = m?.FullName ?? "Unknown";
                    var emailAddress = p?.EmailAddress ?? "Unknown";
                    var logEvent = new LogEventInfo(LogLevel.Info, "AuditLog",
                        $"Removed user roles from user identified by {emailAddress}.");
                    logEvent.Properties.Add("PersonId", modifiedByPersonId);
                    logEvent.Properties.Add("FullName", modifiedByPersonName);
                    Audit.Instance.Log(logEvent);

                    return true;
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return false;
            }
        }

        public static async Task SessionCreateAsync(int personId, string token)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var t = await e.PersonSessions.FindAsync(personId);
                    if (t == null)
                    {
                        e.PersonSessions.Add(new PersonSession
                        {
                            PersonId = personId,
                            Token = token
                        });
                    }
                    else
                    {
                        t.Token = token;
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

        public static async Task<bool> SessionDeleteAsync(int personId)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var t = await e.PersonSessions.FindAsync(personId);
                    if (t == null) return true;
                    e.Entry(t).State = EntityState.Deleted;
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


        public static async Task<bool> SetPersonRoleRecordsAsync(IEnumerable<int> roleIdEnumerable, int personId,
            int modifiedByPersonId)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var person = await e.People.FindAsync(personId);
                    if (person == null) return false;

                    var roleList = roleIdEnumerable.ToArray();

                    var isInternalUser = person.EmailAddress.EndsWith(InternalUserEmailDomain);
                    if (!isInternalUser)
                    {
                        // If the user is not internal, restrict the roles that may be applied to the non-internal user
                        var externalRoleIds = await e.Roles.Where(r => !r.InternalUseOnly).Select(r => r.RoleId)
                            .ToArrayAsync();
                        // Reset the role list to just the roles that are permissable
                        roleList = roleList.Intersect(externalRoleIds).ToArray();
                    }

                    foreach (var i in roleList)
                        e.PersonRoles.Add(new PersonRole
                        {
                            EffectiveDateTime = DateTime.Now,
                            ExpirationDateTime = DateTime.MaxValue,
                            ModifiedByPersonId = modifiedByPersonId,
                            ModifiedOnDateTime = DateTime.Now,
                            PersonId = personId,
                            RoleId = i
                        });

                    await e.SaveChangesAsync();

                    // Log new user creation to AuditLog
                    var p = await e.People.FindAsync(personId);
                    var m = await e.People.FindAsync(modifiedByPersonId);
                    var modifiedByPersonName = m?.FullName ?? "Unknown";
                    var emailAddress = p?.EmailAddress ?? "Unknown";
                    var logEvent = new LogEventInfo(LogLevel.Info, "AuditLog",
                        $"Added user roles to user identified by {emailAddress}.");
                    logEvent.Properties.Add("PersonId", modifiedByPersonId);
                    logEvent.Properties.Add("FullName", modifiedByPersonName);
                    Audit.Instance.Log(logEvent);

                    return true;
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return false;
            }
        }

        public static async Task UnlockUser(int personId, int modifiedByPersonId)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var p = await e.People.FindAsync(personId);
                    if (p == null || p.IsExpired || !p.IsLocked) return;
                    p.IsLocked = false;
                    p.ModifiedByPersonId = modifiedByPersonId;
                    p.ModifiedOnDateTime = DateTime.Now;
                    e.Entry(p).State = EntityState.Modified;
                    await e.SaveChangesAsync();
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
            }
        }


        public static async Task<bool> UpdatePersonProfile(int personId, string preferredName)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var p = await e.People.FindAsync(personId);
                    if (p == null) return false;
                    if (string.Equals(p.PreferredName, preferredName)) return true;
                    var currentPreferredName = p.PreferredName;
                    p.PreferredName = preferredName;
                    p.ModifiedByPersonId = personId;
                    p.ModifiedOnDateTime = DateTime.Now;
                    e.Entry(p).State = EntityState.Modified;
                    await e.SaveChangesAsync();

                    // Log new user creation to AuditLog
                    var logEvent = new LogEventInfo(LogLevel.Info, "AuditLog",
                        $"User updated preferred name from '{currentPreferredName}' to '{preferredName}'.");
                    logEvent.Properties.Add("PersonId", personId);
                    logEvent.Properties.Add("FullName", p.FullName);
                    Audit.Instance.Log(logEvent);

                    return true;
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return false;
            }
        }


        public static async Task<bool> UpdatePersonRecordAsync(int modifiedByPersonId, int personId, string fullName,
            string preferredName, string emailAddress)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var person = await e.People.FindAsync(personId);
                    if (person == null) return false;
                    if (person.FullName.Equals(fullName) && person.EmailAddress.Equals(emailAddress) &&
                        person.PreferredName.Equals(preferredName)) return true;

                    if (!person.EmailAddress.Equals(emailAddress))
                    {
                        // If email address changes and email address is not internal, remove all internal roles from user
                        var isInternalUser = emailAddress.EndsWith(InternalUserEmailDomain);
                        if (isInternalUser)
                        {
                            var b = await RemovePersonRoleRecordsAsync(personId, modifiedByPersonId, true);
                            // If removal of internal-only roles is unsuccessful, return false
                            if (!b) return false;
                        }
                    }

                    person.FullName = fullName;
                    person.PreferredName = preferredName;
                    person.EmailAddress = emailAddress;
                    person.ModifiedByPersonId = modifiedByPersonId;
                    person.ModifiedOnDateTime = DateTime.Now;
                    e.Entry(person).State = EntityState.Modified;
                    await e.SaveChangesAsync();

                    // Log new user creation to AuditLog
                    var m = await e.People.FindAsync(modifiedByPersonId);
                    var modifiedByPersonName = m?.FullName ?? "Unknown";
                    var logEvent = new LogEventInfo(LogLevel.Info, "AuditLog",
                        $"Updated user identified by {person.EmailAddress}.");
                    logEvent.Properties.Add("PersonId", modifiedByPersonId);
                    logEvent.Properties.Add("FullName", modifiedByPersonName);
                    Audit.Instance.Log(logEvent);
                }

                return true;
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return false;
            }
        }

        #endregion

        #region Methods

        private static Expression<Func<Person, bool>> PredicateWhere(string searchValue)
        {
            var p = PredicateBuilder.New<Person>(true);
            if (string.IsNullOrWhiteSpace(searchValue)) return p;
            var searchTerms = searchValue.Split(' ').ToList().ConvertAll(x => x.ToLower());
            p = p.Or(s => searchTerms.Any(value => s.EmailAddress.ToLower().Contains(value)));
            p = p.Or(s => searchTerms.Any(value => s.FullName.ToLower().Contains(value)));
            p = p.Or(s => searchTerms.Any(value => s.PreferredName.Contains(value)));
            return p;
        }

        private static async Task<bool> RemovePersonRoleRecordsAsync(int personId, int modifiedByPersonId,
            bool internalUseOnly = false)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var personRoleList = e.PersonRoles.Where(role =>
                        role.PersonId.Equals(personId) && role.ExpirationDateTime > DateTime.Now);
                    if (internalUseOnly) personRoleList = personRoleList.Where(role => role.Role.InternalUseOnly);
                    foreach (var role in personRoleList)
                    {
                        role.ExpirationDateTime = DateTime.Now;
                        role.ModifiedByPersonId = modifiedByPersonId;
                        role.ModifiedOnDateTime = DateTime.Now;
                    }

                    await e.SaveChangesAsync();

                    // Log new user creation to AuditLog
                    var p = await e.People.FindAsync(personId);
                    var m = await e.People.FindAsync(modifiedByPersonId);
                    var modifiedByPersonName = m?.FullName ?? "Unknown";
                    var emailAddress = p?.EmailAddress ?? "Unknown";
                    var logEvent = new LogEventInfo(LogLevel.Info, "AuditLog",
                        $"Removed user roles from user identified by {emailAddress}.");
                    logEvent.Properties.Add("PersonId", modifiedByPersonId);
                    logEvent.Properties.Add("FullName", modifiedByPersonName);
                    Audit.Instance.Log(logEvent);

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