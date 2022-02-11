using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CarLeasePPT.Utility;

namespace CarLeasePPT.DataAccessLayer
{
    public static class AuditLogEngine
    {
        #region Public Methods and Operators

        public static async Task<AuditLogDetailRecord> GetAsync(int auditLogId)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var record = await e.AuditLogs.FindAsync(auditLogId);
                    if (record == null) return null;
                    var auditLogRecord = new AuditLogDetailRecord
                    {
                        Action = record.Action,
                        IsReviewed = record.IsReviewed,
                        LogLevel = record.Level,
                        AuditLogId = record.AuditLogId,
                        RecordId = record.RecordId,
                        Logged = record.Logged,
                        Message = record.Message,
                        Controller = record.Controller,
                        PersonFullName = record.FullName,
                        RemoteAddress = record.RemoteAddress,
                        Url = record.Url,
                        ReviewedOnDateTime = record.ReviewedOnDateTime,
                        PersonId = record.PersonId
                    };
                    if (record.ReviewedByPersonId.HasValue)
                        auditLogRecord.ReviewedByFullName = record.PersonReviewedBy.FullName;
                    return auditLogRecord;
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return null;
            }
        }

        public static async Task<AuditLogListData> GetCollectionAsync(int take, int skip, bool showReviewed = false,
            bool showInfo = false)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var query = e.AuditLogs.Where(p => true);
                    query = query.OrderByDescending(a => a.AuditLogId);
                    if (!showReviewed) query = query.Where(a => !a.IsReviewed);
                    if (!showInfo) query = query.Where(a => !a.Level.Equals("Info"));

                    var count = await query.CountAsync();
                    var recordList = await query.Skip(skip).Take(take).ToListAsync();

                    var totalResultsCount = e.AuditLogs.Count();

                    var auditLogSearchRecords = recordList.Select(a => new AuditLogListRecord
                    {
                        Action = a.Action,
                        AuditLogId = a.AuditLogId,
                        Controller = a.Controller,
                        IsReviewed = a.IsReviewed,
                        IsReviewedString = a.IsReviewed ? "Yes" : "No",
                        Logged = a.Logged,
                        LogLevel = a.Level,
                        Message = a.Message,
                        PersonFullName = a.FullName
                    }).ToList();

                    return new AuditLogListData
                    {
                        data = auditLogSearchRecords,
                        recordsFiltered = count,
                        recordsTotal = totalResultsCount
                    };
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return new AuditLogListData
                {
                    data = new List<AuditLogListRecord>(),
                    recordsFiltered = 0,
                    recordsTotal = 0
                };
            }
        }

        public static async Task<IReadOnlyCollection<AuditLogReportRecord>> GetReportRecords(DateTime startDate,
            DateTime endDate, bool includeInfo, bool includeReviewed)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var query = e.AuditLogs.Where(a => a.Logged <= endDate && a.Logged >= startDate);
                    if (!includeInfo)
                        query = query.Where(a => !a.Level.Equals("Info"));
                    if (!includeReviewed)
                        query = query.Where(a => !a.IsReviewed);

                    query = query.OrderByDescending(a => a.Logged);

                    var auditLogRecords = await query.Select(a => new AuditLogReportRecord
                    {
                        Action = a.Action,
                        Controller = a.Controller,
                        IsReviewed = a.IsReviewed ? "Yes" : "No",
                        Logged = a.Logged,
                        LogLevel = a.Level,
                        Message = a.Message,
                        PersonFullName = a.FullName,
                        RecordId = a.RecordId,
                        RemoteAddress = a.RemoteAddress,
                        ReviewedByFullName = a.IsReviewed ? a.PersonReviewedBy.FullName : string.Empty,
                        ReviewedOnDateTime = a.ReviewedOnDateTime,
                        Url = a.Url
                    }).ToListAsync();
                    return auditLogRecords.AsReadOnly();
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return new List<AuditLogReportRecord>().AsReadOnly();
            }
        }

        public static async Task<bool> MarkIsReviewed(int personId)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    // Locate the person record.
                    var personRecord = await e.People.FindAsync(personId);
                    if (personRecord == null) return false;
                    // Ensure the user is authorized to use AuditLog.
                    if (!personRecord.PersonRoles.Any(role => role.Role.RoleCode.Equals("AUDIT_LOG"))) return false;
                    // Update all unreviewed records.
                    await e.Database.ExecuteSqlCommandAsync(
                        "UPDATE AuditLog SET IsReviewed = 1, ReviewedByPersonId = @p0, ReviewedOnDateTime = @p1 WHERE IsReviewed = 0;",
                        new SqlParameter("@p0", personId),
                        new SqlParameter("@p1", DateTime.Now));
                    return true;
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return false;
            }
        }

        public static async Task<bool> MarkIsReviewed(int auditLogId, int personId)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    // Locate the person record.
                    var personRecord = await e.People.FindAsync(personId);
                    if (personRecord == null) return false;
                    // Ensure the user is authorized to use AuditLog.
                    if (!personRecord.PersonRoles.Any(role => role.Role.RoleCode.Equals("AUDIT_LOG"))) return false;
                    // Update the unreviewed record.
                    await e.Database.ExecuteSqlCommandAsync(
                        "UPDATE AuditLog SET IsReviewed = 1, ReviewedByPersonId = @p0, ReviewedOnDateTime = @p1 WHERE AuditLogId = @p2 AND IsReviewed = 0;",
                        new SqlParameter("@p0", personId),
                        new SqlParameter("@p1", DateTime.Now),
                        new SqlParameter("@p2", auditLogId));
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