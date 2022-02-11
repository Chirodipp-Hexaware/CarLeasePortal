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
    public static class TaxAssessorEngine
    {
        #region Public Methods and Operators

        public static async Task<int> CreateAsync(int modifiedByPersonId, string taxAssessorName,
            string stateAbbreviation, string county)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var ta = new TaxAssessor
                    {
                        CreatedOnDateTime = DateTime.Now,
                        CreatedByPersonId = modifiedByPersonId,
                        StateAbbreviation = stateAbbreviation,
                        TaxAssessorName = taxAssessorName,
                        //TaxPeriodEnd = taxPeriodEnd,
                        //TaxPeriodStart = taxPeriodStart,
                        //AssessmentDate = assessmentDate,
                        County = county.AsNullIfWhiteSpace()
                    };

                    e.TaxAssessors.Add(ta);
                    await e.SaveChangesAsync();

                    // Log creation to AuditLog
                    var m = await e.People.FindAsync(modifiedByPersonId);
                    var modifiedByPersonName = m?.FullName ?? "Unknown";
                    var logEvent = new LogEventInfo(LogLevel.Info, "AuditLog",
                        $"Created tax assessor {taxAssessorName} ({stateAbbreviation}).");
                    logEvent.Properties.Add("PersonId", modifiedByPersonId);
                    logEvent.Properties.Add("FullName", modifiedByPersonName);
                    Audit.Instance.Log(logEvent);

                    return ta.TaxAssessorId;
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return -1;
            }
        }

        public static async Task<bool> DeleteAsync(int taxAssessorId, int modifiedByPersonId)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var taxAssessor = e.TaxAssessors.Find(taxAssessorId);
                    if (taxAssessor == null) return false;

                    taxAssessor.ModifiedByPersonId = modifiedByPersonId;
                    taxAssessor.ModifiedOnDateTime = DateTime.Now;
                    taxAssessor.DeletedOnDateTime = DateTime.Now;
                    taxAssessor.IsDeleted = true;

                    e.Entry(taxAssessor).State = EntityState.Modified;
                    await e.SaveChangesAsync();

                    // Log new user creation to AuditLog
                    var m = await e.People.FindAsync(modifiedByPersonId);
                    var modifiedByPersonName = m?.FullName ?? "Unknown";
                    var logEvent = new LogEventInfo(LogLevel.Info, "AuditLog",
                        $"Deleted tax assessor {taxAssessor.TaxAssessorName} ({taxAssessor.StateAbbreviation}).");
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


        public static async Task<bool> EditAsync(int taxAssessorId, int modifiedByPersonId, string taxAssessorName,
            string stateAbbreviation, string county)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var taxAssessor = await e.TaxAssessors.FindAsync(taxAssessorId);
                    if (taxAssessor == null) return false;

                    taxAssessor.TaxAssessorName = taxAssessorName;
                    taxAssessor.StateAbbreviation = stateAbbreviation;
                    taxAssessor.County = county.AsNullIfWhiteSpace();
                    //taxAssessor.AssessmentDate = assessmentDate;
                    //taxAssessor.TaxPeriodStart = taxPeriodStart;
                    //taxAssessor.TaxPeriodEnd = taxPeriodEnd;
                    taxAssessor.ModifiedByPersonId = modifiedByPersonId;
                    taxAssessor.ModifiedOnDateTime = DateTime.Now;
                    e.Entry(taxAssessor).State = EntityState.Modified;
                    await e.SaveChangesAsync();

                    // Log edit to AuditLog
                    var m = await e.People.FindAsync(modifiedByPersonId);
                    var modifiedByPersonName = m?.FullName ?? "Unknown";
                    var logEvent = new LogEventInfo(LogLevel.Info, "AuditLog",
                        $"Edited tax assessor {taxAssessorName} ({stateAbbreviation}).");
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

        public static async Task<bool> ExistsAsync(string taxAssessorName, string stateAbbreviation, string county)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var countyClean = county.AsNullIfWhiteSpace();
                    var exists =
                        await e.TaxAssessors.Where(t =>
                                t.TaxAssessorName.Equals(taxAssessorName) &&
                                t.StateAbbreviation.Equals(stateAbbreviation) && t.County.Equals(countyClean) &&
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

        public static async Task<bool> ExistsAsync(int taxAssessorId, string taxAssessorName, string stateAbbreviation,
            string county)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var countyClean = county.AsNullIfWhiteSpace();
                    var exists =
                        await e.TaxAssessors.Where(t =>
                                !t.TaxAssessorId.Equals(taxAssessorId) && t.TaxAssessorName.Equals(taxAssessorName) &&
                                t.StateAbbreviation.Equals(stateAbbreviation) && t.County.Equals(countyClean) &&
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


        public static async Task<TaxAssessorRecord> GetAsync(int id)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var record = await e.TaxAssessors.FindAsync(id);
                    if (record == null) return null;

                    var taxAssessorRecord = new TaxAssessorRecord
                    {
                        AssessmentDate = record.AssessmentDate,
                        County = record.County,
                        IsDeleted = record.IsDeleted,
                        StateAbbreviation = record.StateAbbreviation,
                        TaxAssessorId = record.TaxAssessorId,
                        TaxAssessorName = record.TaxAssessorName,
                        TaxPeriodEnd = record.TaxPeriodEnd,
                        TaxPeriodStart = record.TaxPeriodStart
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


        public static async Task<TaxAssessorListData> GetCollectionAsync(string searchBy, int take, int skip,
            string sortBy,
            bool sortDir, bool includeDeleted = false)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var p = PredicateWhere(searchBy);

                    var query = e.TaxAssessors.AsExpandable().Where(p);
                    if (!includeDeleted) query = query.Where(assessor => !assessor.IsDeleted);

                    switch (sortBy)
                    {
                        case "TaxAssessorName":
                            query = sortDir
                                ? query.OrderBy(v => v.TaxAssessorName)
                                : query.OrderByDescending(v => v.TaxAssessorName);
                            break;
                        case "StateAbbreviation":
                            query = sortDir
                                ? query.OrderBy(v => v.StateAbbreviation)
                                : query.OrderByDescending(v => v.StateAbbreviation);
                            break;
                        default:
                            query = query.OrderBy(v => v.StateAbbreviation).ThenBy(v => v.TaxAssessorName);
                            break;
                    }

                    var count = await query.CountAsync();
                    var recordList = await query.Skip(skip).Take(take).ToListAsync();

                    var totalResultsCount = e.TaxAssessors.Count(t => !t.IsDeleted);

                    var taxAssessorListRecords = recordList.Select(pr => new TaxAssessorListRecord
                    {
                        StateAbbreviation = pr.StateAbbreviation,
                        TaxAssessorName = pr.TaxAssessorName,
                        IsDeleted = pr.IsDeleted,
                        County = pr.County,
                        TaxAssessorId = pr.TaxAssessorId
                    }).ToList();

                    return new TaxAssessorListData
                    {
                        data = taxAssessorListRecords,
                        recordsFiltered = count,
                        recordsTotal = totalResultsCount
                    };
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return new TaxAssessorListData
                {
                    data = new List<TaxAssessorListRecord>(),
                    recordsFiltered = 0,
                    recordsTotal = 0
                };
            }
        }

        public static async Task<Select2Data> GetSelect2CollectionAsync(string term, int page)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    const int take = 20;
                    var skip = (page - 1) * take;
                    var w = PredicateWhere(term);
                    var baseQuery = e.TaxAssessors
                        .AsExpandable()
                        .Where(a => !a.IsDeleted)
                        .Where(w)
                        .OrderBy(a => a.StateAbbreviation)
                        .ThenBy(a => a.TaxAssessorName);

                    var query = baseQuery
                        .Skip(skip)
                        .Take(take);

                    var assessorData = await query.ToListAsync();

                    var select2Data = assessorData.Select(r => new Select2Record
                    {
                        id = r.TaxAssessorId,
                        text = $"{r.TaxAssessorName} [{r.StateAbbreviation}{(r.County.IsNullOrWhiteSpace() ? string.Empty : $" - {r.County}")}]"
                    }).ToList();

                    var totalCount = await baseQuery.CountAsync();

                    return new Select2Data
                    {
                        results = select2Data,
                        pagination = new Select2Pagination
                        {
                            more = page * take < totalCount
                        }
                    };
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return new Select2Data();
            }
        }

        #endregion

        #region Methods

        private static Expression<Func<TaxAssessor, bool>> PredicateWhere(string searchValue)
        {
            var p = PredicateBuilder.New<TaxAssessor>(true);
            if (string.IsNullOrWhiteSpace(searchValue)) return p;
            var searchTerms = searchValue.Split(' ').ToList().ConvertAll(x => x.ToLower());
            p = p.Or(s => searchTerms.Any(value => s.TaxAssessorName.ToLower().Contains(value)));
            p = p.Or(s => searchTerms.Any(value => s.County.ToLower().Contains(value)));
            p = p.Or(s => searchTerms.Any(value => s.StateAbbreviation.Contains(value)));
            return p;
        }

        #endregion
    }
}