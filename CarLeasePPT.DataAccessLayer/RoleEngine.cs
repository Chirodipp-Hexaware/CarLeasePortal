using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CarLeasePPT.Utility;

namespace CarLeasePPT.DataAccessLayer
{
    public static class RoleEngine
    {
        #region Public Methods and Operators

        public static async Task<IReadOnlyCollection<RoleRecord>> GetCollectionAsync(bool includeInternal = true)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var query = e.Roles.Where(p => true);
                    if (!includeInternal) query = query.Where(r => !r.InternalUseOnly);

                    var roleIdList = await
                        query.OrderBy(r => r.SequenceId).ThenBy(r => r.RoleName).Select(r => new RoleRecord
                        {
                            RoleCode = r.RoleCode,
                            RoleId = r.RoleId,
                            RoleName = r.RoleName,
                            SequenceId = r.SequenceId,
                            InternalUseOnly = r.InternalUseOnly
                        }).ToListAsync();
                    return roleIdList.AsReadOnly();
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return new List<RoleRecord>().AsReadOnly();
            }
        }

        #endregion
    }
}