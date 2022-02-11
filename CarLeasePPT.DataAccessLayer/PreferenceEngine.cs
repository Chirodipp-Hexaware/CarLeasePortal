#region

using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CarLeasePPT.Utility;

#endregion

namespace CarLeasePPT.DataAccessLayer
{
    public static class PreferenceEngine
    {
        #region Methods

        private static async Task<Preference> GetGlobal(string preferenceName)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var query = e.Preferences.Where(p => !p.PersonId.HasValue
                                                         && p.PreferenceName.Equals(preferenceName));
                    return await query.SingleOrDefaultAsync();
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return null;
            }
        }

        private static async Task<Preference> GetPerson(int personId, string preferenceName)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var query = e.Preferences.Where(p => p.PersonId.HasValue
                                                         && p.PersonId.Value.Equals(personId)
                                                         && p.PreferenceName.Equals(preferenceName));
                    return await query.SingleOrDefaultAsync();
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return null;
            }
        }

        private static async Task<bool> SetGlobal(string preferenceName, int value)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var query = e.Preferences.Where(p => p.PreferenceName.Equals(preferenceName)
                                                         && !p.PersonId.HasValue);
                    var pref = await query.SingleOrDefaultAsync();
                    if (pref == null)
                    {
                        pref = new Preference
                        {
                            PreferenceName = preferenceName,
                            ValueInt = value
                        };
                        e.Preferences.Add(pref);
                    }
                    else
                    {
                        pref.ValueInt = value;
                        e.Entry(pref).State = EntityState.Modified;
                    }

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

        private static async Task<bool> SetGlobal(string preferenceName, string value)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var query = e.Preferences.Where(p => p.PreferenceName.Equals(preferenceName)
                                                         && !p.PersonId.HasValue);
                    var pref = await query.SingleOrDefaultAsync();
                    if (pref == null)
                    {
                        pref = new Preference
                        {
                            PreferenceName = preferenceName,
                            ValueString = value
                        };
                        e.Preferences.Add(pref);
                    }
                    else
                    {
                        pref.ValueString = value;
                        e.Entry(pref).State = EntityState.Modified;
                    }

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

        private static async Task<bool> SetGlobal(string preferenceName, bool value)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var query = e.Preferences.Where(p => p.PreferenceName.Equals(preferenceName)
                                                         && !p.PersonId.HasValue);
                    var pref = await query.SingleOrDefaultAsync();
                    if (pref == null)
                    {
                        pref = new Preference
                        {
                            PreferenceName = preferenceName,
                            ValueBool = value
                        };
                        e.Preferences.Add(pref);
                    }
                    else
                    {
                        pref.ValueBool = value;
                        e.Entry(pref).State = EntityState.Modified;
                    }

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

        private static async Task<bool> SetGlobal(string preferenceName, decimal value)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var query = e.Preferences.Where(p => p.PreferenceName.Equals(preferenceName)
                                                         && !p.PersonId.HasValue);
                    var pref = await query.SingleOrDefaultAsync();
                    if (pref == null)
                    {
                        pref = new Preference
                        {
                            PreferenceName = preferenceName,
                            ValueDecimal = value
                        };
                        e.Preferences.Add(pref);
                    }
                    else
                    {
                        pref.ValueDecimal = value;
                        e.Entry(pref).State = EntityState.Modified;
                    }

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

        private static async Task<bool> SetPerson(int personId, string preferenceName,
            int value)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var query = e.Preferences.Where(p => p.PreferenceName.Equals(preferenceName)
                                                         && p.PersonId.HasValue &&
                                                         p.PersonId.Value.Equals(personId));
                    var pref = await query.SingleOrDefaultAsync();
                    if (pref == null)
                    {
                        pref = new Preference
                        {
                            PersonId = personId,
                            PreferenceName = preferenceName,
                            ValueInt = value
                        };
                        e.Preferences.Add(pref);
                    }
                    else
                    {
                        pref.ValueInt = value;
                        e.Entry(pref).State = EntityState.Modified;
                    }

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

        private static async Task<bool> SetPerson(int personId, string preferenceName,
            string value)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var query = e.Preferences.Where(p => p.PreferenceName.Equals(preferenceName)
                                                         && p.PersonId.HasValue &&
                                                         p.PersonId.Value.Equals(personId));
                    var pref = await query.SingleOrDefaultAsync();
                    if (pref == null)
                    {
                        pref = new Preference
                        {
                            PersonId = personId,
                            PreferenceName = preferenceName,
                            ValueString = value
                        };
                        e.Preferences.Add(pref);
                    }
                    else
                    {
                        pref.ValueString = value;
                        e.Entry(pref).State = EntityState.Modified;
                    }

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

        private static async Task<bool> SetPerson(int personId, string preferenceName,
            bool value)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var query = e.Preferences.Where(p => p.PreferenceName.Equals(preferenceName)
                                                         && p.PersonId.HasValue &&
                                                         p.PersonId.Value.Equals(personId));
                    var pref = await query.SingleOrDefaultAsync();
                    if (pref == null)
                    {
                        pref = new Preference
                        {
                            PersonId = personId,
                            PreferenceName = preferenceName,
                            ValueBool = value
                        };
                        e.Preferences.Add(pref);
                    }
                    else
                    {
                        pref.ValueBool = value;
                        e.Entry(pref).State = EntityState.Modified;
                    }

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

        private static async Task<bool> SetPerson(int personId, string preferenceName,
            decimal value)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var query = e.Preferences.Where(p => p.PreferenceName.Equals(preferenceName)
                                                         && p.PersonId.HasValue &&
                                                         p.PersonId.Value.Equals(personId));
                    var pref = await query.SingleOrDefaultAsync();
                    if (pref == null)
                    {
                        pref = new Preference
                        {
                            PersonId = personId,
                            PreferenceName = preferenceName,
                            ValueDecimal = value
                        };
                        e.Preferences.Add(pref);
                    }
                    else
                    {
                        pref.ValueDecimal = value;
                        e.Entry(pref).State = EntityState.Modified;
                    }

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

        #endregion

        public static class Global
        {
            #region Constants

            private const string UseWindowsAuthenticationPreferenceName = "USE_ACTIVE_DIRECTORY";

            #endregion

            #region Public Methods and Operators

            public static async Task<bool> UseActiveDirectory()
            {
                try
                {
                    var pref = await GetGlobal(UseWindowsAuthenticationPreferenceName);
                    return pref?.ValueBool ?? false;
                }
                catch (Exception exception)
                {
                    Log.Instance.Error(exception);
                    return false;
                }
            }

            public static async Task<bool> UseActiveDirectory(int value)
            {
                try
                {
                    return await SetGlobal(UseWindowsAuthenticationPreferenceName, value);
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
}