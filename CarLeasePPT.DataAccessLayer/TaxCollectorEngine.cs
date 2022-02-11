using LinqKit;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CarLeasePPT.Utility;

namespace CarLeasePPT.DataAccessLayer
{
    public static class TaxCollectorEngine
    {
        #region Public Methods and Operators

        public static async Task<int> CreateAsync(int modifiedByPersonId, int taxAssessorId, string taxCollectorName, string vendorCode)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var taxCollector = new TaxCollector
                    {
                        CreatedOnDateTime = DateTime.Now,
                        CreatedByPersonId = modifiedByPersonId,
                        TaxCollectorName = taxCollectorName,
                        TaxAssessorId = taxAssessorId,
                        VendorCode = vendorCode
                    };

                    e.TaxCollectors.Add(taxCollector);
                    await e.SaveChangesAsync();

                    // Log creation to AuditLog
                    var m = await e.People.FindAsync(modifiedByPersonId);
                    var modifiedByPersonName = m?.FullName ?? "Unknown";
                    var logEvent = new LogEventInfo(LogLevel.Info, "AuditLog",
                        $"Created tax collector {taxCollectorName} ({vendorCode}).");
                    logEvent.Properties.Add("PersonId", modifiedByPersonId);
                    logEvent.Properties.Add("FullName", modifiedByPersonName);
                    Audit.Instance.Log(logEvent);

                    return taxCollector.TaxAssessorId;
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return -1;
            }
        }

        public static async Task<bool> DeleteAsync(int taxCollectorId, int modifiedByPersonId)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var taxCollector = e.TaxCollectors.Find(taxCollectorId);
                    if (taxCollector == null) return false;

                    taxCollector.ModifiedByPersonId = modifiedByPersonId;
                    taxCollector.ModifiedOnDateTime = DateTime.Now;
                    taxCollector.DeletedOnDateTime = DateTime.Now;
                    taxCollector.IsDeleted = true;
                    taxCollector.VendorCode = null;

                    e.Entry(taxCollector).State = EntityState.Modified;
                    await e.SaveChangesAsync();

                    // Log new user creation to AuditLog
                    var m = await e.People.FindAsync(modifiedByPersonId);
                    var modifiedByPersonName = m?.FullName ?? "Unknown";
                    var logEvent = new LogEventInfo(LogLevel.Info, "AuditLog",
                        $"Deleted tax collector {taxCollector.TaxCollectorName} ({taxCollector.VendorCode}).");
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


        public static async Task<bool> EditAsync(int taxCollectorId, int modifiedByPersonId, string taxCollectorName, string vendorCode)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var taxCollector = await e.TaxCollectors.FindAsync(taxCollectorId);
                    if (taxCollector == null) return false;

                    taxCollector.TaxCollectorName = taxCollectorName;
                    taxCollector.VendorCode = vendorCode;
                    taxCollector.ModifiedByPersonId = modifiedByPersonId;
                    taxCollector.ModifiedOnDateTime = DateTime.Now;
                    e.Entry(taxCollector).State = EntityState.Modified;
                    await e.SaveChangesAsync();

                    // Log edit to AuditLog
                    var m = await e.People.FindAsync(modifiedByPersonId);
                    var modifiedByPersonName = m?.FullName ?? "Unknown";
                    var logEvent = new LogEventInfo(LogLevel.Info, "AuditLog",
                        $"Edited tax collector {taxCollectorName} ({vendorCode}).");
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

        public static async Task<bool> ExistsAsync(int taxAssessorId, string taxCollectorName)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var exists =
                        await e.TaxCollectors.Where(t =>
                                t.TaxAssessorId.Equals(taxAssessorId) &&
                                t.TaxCollectorName.Equals(taxCollectorName) &&
                                !t.IsDeleted)
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

        public static async Task<bool> ExistsAsync(int taxCollectorId, int taxAssessorId, string taxCollectorName)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var exists =
                        await e.TaxCollectors.Where(t =>
                                !t.TaxCollectorId.Equals(taxCollectorId) &&
                                t.TaxAssessorId.Equals(taxAssessorId) &&
                                t.TaxCollectorName.Equals(taxCollectorName) &&
                                !t.IsDeleted)
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

        public static async Task<bool> VendorCodeExistsAsync(string vendorCode)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var exists = await e.TaxCollectors.Where(t => t.VendorCode.Equals(vendorCode) && !t.IsDeleted).AnyAsync();
                    return exists;
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return true;
            }
        }

        public static async Task<bool> VendorCodeExistsAsync(int taxCollectorId, string vendorCode)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var exists = await e.TaxCollectors.Where(t => !t.TaxCollectorId.Equals(taxCollectorId) && t.VendorCode.Equals(vendorCode) && !t.IsDeleted).AnyAsync();
                    return exists;
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return true;
            }
        }


        public static async Task<TaxCollectorRecord> GetAsync(int id)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var record = await e.TaxCollectors.FindAsync(id);
                    if (record == null) return null;

                    var taxAssessorRecord = new TaxCollectorRecord
                    {
                        County = record.TaxAssessor.County,
                        IsDeleted = record.IsDeleted,
                        StateAbbreviation = record.TaxAssessor.StateAbbreviation,
                        TaxAssessorId = record.TaxAssessorId,
                        TaxAssessorName = record.TaxAssessor.TaxAssessorName,
                        TaxCollectorId = record.TaxCollectorId,
                        TaxCollectorName = record.TaxCollectorName,
                        VendorCode = record.VendorCode,
                    };
                    return taxAssessorRecord;
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return null;
            }
        }


        public static async Task<TaxCollectorListData> GetCollectionAsync(string searchBy, int take, int skip,
            string sortBy,
            bool sortDir, bool includeDeleted = false)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var p = PredicateWhere(searchBy);

                    var query = e.TaxCollectors.AsExpandable().Where(p);
                    if (!includeDeleted) query = query.Where(taxCollector => !taxCollector.IsDeleted);

                    switch (sortBy)
                    {
                        case "StateAbbreviation":
                            query = sortDir
                                ? query.OrderBy(v => v.TaxAssessor.StateAbbreviation)
                                : query.OrderByDescending(v => v.TaxAssessor.StateAbbreviation);
                            break;
                        case "TaxCollectorName":
                            query = sortDir
                                ? query.OrderBy(v => v.TaxCollectorName)
                                : query.OrderByDescending(v => v.TaxCollectorName);
                            break;
                        case "VendorCode":
                            query = sortDir
                                ? query.OrderBy(v => v.VendorCode)
                                : query.OrderByDescending(v => v.VendorCode);
                            break;
                        default:
                            query = query.OrderBy(v => v.TaxAssessor.StateAbbreviation).ThenBy(v => v.TaxCollectorName);
                            break;
                    }

                    var count = await query.CountAsync();
                    var recordList = await query.Skip(skip).Take(take).ToListAsync();

                    var totalResultsCount = e.TaxCollectors.Count(t => !t.IsDeleted);

                    var assessorListRecords = recordList.Select(collector => new TaxCollectorListRecord
                    {
                        StateAbbreviation = collector.TaxAssessor.StateAbbreviation,
                        TaxAssessorName = collector.TaxAssessor.TaxAssessorName,
                        IsDeleted = collector.IsDeleted,
                        TaxCollectorName = collector.TaxCollectorName,
                        TaxCollectorId = collector.TaxCollectorId,
                        VendorCode = collector.VendorCode,
                    }).ToList();

                    return new TaxCollectorListData
                    {
                        data = assessorListRecords,
                        recordsFiltered = count,
                        recordsTotal = totalResultsCount
                    };
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return new TaxCollectorListData
                {
                    data = new List<TaxCollectorListRecord>(),
                    recordsFiltered = 0,
                    recordsTotal = 0
                };
            }
        }

        #endregion

        #region Methods

        private static Expression<Func<TaxCollector, bool>> PredicateWhere(string searchValue)
        {
            var p = PredicateBuilder.New<TaxCollector>(true);
            if (string.IsNullOrWhiteSpace(searchValue)) return p;
            var searchTerms = searchValue.Split(' ').ToList().ConvertAll(x => x.ToLower());
            p = p.Or(s => searchTerms.Any(value => s.TaxCollectorName.ToLower().Contains(value)));
            p = p.Or(s => searchTerms.Any(value => s.VendorCode.ToLower().Contains(value)));
            p = p.Or(s => searchTerms.Any(value => s.TaxAssessor.TaxAssessorName.Contains(value)));
            return p;
        }

        #endregion
    }
}