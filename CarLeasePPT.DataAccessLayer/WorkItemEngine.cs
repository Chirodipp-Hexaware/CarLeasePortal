using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using CarLeasePPT.Utility;

namespace CarLeasePPT.DataAccessLayer
{
    public static class WorkItemEngine
    {
        #region Enums

        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private enum WorkItemActivityTypeEnum
        {
            Call = 1,
            Email,
            Fax,
            Letter,
            Memorandum,
            Other,
            MergedWorkItem
        }

        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private enum WorkItemStatusEnum
        {
            Unworked = 1,
            Merged,
            Working,
            WaitingForClient,
            PendingResolved,
            Resolved,
            Cancelled
        }

        #endregion

        #region Properties

        private static int WorkItemCacheDuration =>
            int.TryParse(ConfigurationManager.AppSettings["WorkItemCacheDuration"], out var wicd) ? wicd : 60;

        private static string WorkItemCacheKey => ConfigurationManager.AppSettings["WorkItemCacheKey"] ??
                                                  "hexa_default_5802beff286b48c9964667facc914778";

        #endregion

        #region Public Methods and Operators

        public static async Task<bool> AddActivityAttachmentAsync(int workItemId, int workItemActivityId, int personId,
            string attachmentName, Guid fileStreamId)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var a = new WorkItemAttachment
                    {
                        AttachmentName = attachmentName,
                        CreatedByPersonId = personId,
                        CreatedOnDateTime = DateTime.Now,
                        DocumentFileStoreStreamId = fileStreamId,
                        WorkItemActivityId = workItemActivityId,
                        WorkItemId = workItemId
                    };
                    e.WorkItemAttachments.Add(a);
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

        public static async Task<bool> AddAttachmentAsync(int workItemId, int personId, string attachmentName,
            Guid fileStreamId)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var a = new WorkItemAttachment
                    {
                        AttachmentName = attachmentName,
                        CreatedByPersonId = personId,
                        CreatedOnDateTime = DateTime.Now,
                        DocumentFileStoreStreamId = fileStreamId,
                        WorkItemId = workItemId
                    };
                    e.WorkItemAttachments.Add(a);
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


        public static async Task<bool> AssignToClientAsync(int id, int actionByPersonId)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var w = await e.WorkItems.FindAsync(id);
                    if (w == null) return false;
                    if (!w.WorkItemStatusId.Equals((int) WorkItemStatusEnum.Working)) return false;
                    w.WorkItemStatusId = (int) WorkItemStatusEnum.WaitingForClient;
                    var a = new WorkItemActivity
                    {
                        ActivityDateTime = DateTime.Now,
                        CreatedByPersonId = actionByPersonId,
                        CreatedOnDateTime = DateTime.Now,
                        Narrative = Resources.WorkItemEngine_AssignToClientAsync_Narrative_Message,
                        WorkItemActivityId = (int) WorkItemActivityTypeEnum.Other
                    };
                    w.WorkItemActivities.Add(a);
                    e.Entry(w).State = EntityState.Modified;
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

        public static async Task<int> CreateActivityAsync(int workItemId, int personId, int workItemActivityTypeId,
            DateTime activityDateTime, string narrative, bool isPrivate, bool resolve = false)
        {
            try
            {
                var n = DateTime.Now;
                using (var e = new HexaEntities())
                {
                    var wi = await e.WorkItems.FindAsync(workItemId);
                    if (wi == null) return -1;

                    wi.WorkItemStatusId =
                        resolve ? (int) WorkItemStatusEnum.Resolved : (int) WorkItemStatusEnum.Working;
                    e.Entry(wi).State = EntityState.Modified;

                    var a = new WorkItemActivity
                    {
                        ActivityDateTime = activityDateTime,
                        CreatedByPersonId = personId,
                        CreatedOnDateTime = n,
                        IsPrivate = isPrivate,
                        Narrative = narrative,
                        WorkItemId = workItemId,
                        WorkItemActivityTypeId = workItemActivityTypeId
                    };

                    e.WorkItemActivities.Add(a);
                    await e.SaveChangesAsync();
                    return a.WorkItemActivityId;
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return -1;
            }
        }

        public static async Task<int> CreateAsync(string companyName, int createdByPersonId, string garageAddress,
            string garageCity, string garageCounty, string garageState, bool isUrgent, string leaseNumber,
            string lesseeFirstname, string lesseeLastName, string narrative, string plateNumber, int? taxBillId,
            int? taxCollectorId, string taxCollectorName, string taxYear, int? carLeaseId, string carMake,
            string vin, int workItemTypeId, DateTime? dueDate, DateTime? noticeDate, string noticeNumber,
            int? taxBillCarLeaseId)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var n = DateTime.Now;
                    var w = new WorkItem
                    {
                        CompanyName = companyName.SanitizeHtml().Truncate(100).AsNullIfWhiteSpace(),
                        CreatedByPersonId = createdByPersonId,
                        CreatedOnDateTime = n,
                        DueDate = dueDate,
                        GarageAddress = garageAddress.SanitizeHtml().Truncate(100).AsNullIfWhiteSpace(),
                        GarageCity = garageCity.SanitizeHtml().Truncate(60).AsNullIfWhiteSpace(),
                        GarageCounty = garageCounty.SanitizeHtml().Truncate(60).AsNullIfWhiteSpace(),
                        GarageState = garageState.SanitizeHtml().Truncate(2).AsNullIfWhiteSpace(),
                        IsUrgent = isUrgent,
                        LeaseNumber = leaseNumber.SanitizeHtml().Truncate(50).AsNullIfWhiteSpace(),
                        LesseeFirstName = lesseeFirstname.SanitizeHtml().Truncate(18).AsNullIfWhiteSpace(),
                        LesseeLastName = lesseeLastName.SanitizeHtml().Truncate(40).AsNullIfWhiteSpace(),
                        Narrative = narrative.SanitizeHtml(),
                        NoticeDate = noticeDate,
                        NoticeNumber = noticeNumber.SanitizeHtml().Truncate(50).AsNullIfWhiteSpace(),
                        PlateNumber = plateNumber.SanitizeHtml().Truncate(10).AsNullIfWhiteSpace(),
                        TaxBillId = taxBillId,
                        TaxBillCarLeaseId = taxBillCarLeaseId,
                        TaxCollectorId = taxCollectorId,
                        TaxCollectorName = taxCollectorName.SanitizeHtml().Truncate(200).AsNullIfWhiteSpace(),
                        TaxYear = taxYear,
                        CarLeaseId = carLeaseId,
                        CarMake = carMake.SanitizeHtml().Truncate(100).AsNullIfWhiteSpace(),
                        Vin = vin.SanitizeHtml().Truncate(50).AsNullIfWhiteSpace(),
                        WorkItemStatusId = (int) WorkItemStatusEnum.Unworked,
                        WorkItemTypeId = workItemTypeId
                    };
                    e.WorkItems.Add(w);
                    await e.SaveChangesAsync();
                    var id = w.WorkItemId;
                    return id;
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return -1;
            }
        }

        public static async Task<WorkItemActivityRecord> GetActivityAsync(int id)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var a = await e.WorkItemActivities.FindAsync(id);
                    if (a == null) return null;

                    return new WorkItemActivityRecord
                    {
                        ActivityDateTime = a.ActivityDateTime,
                        CreatedByPersonId = a.CreatedByPersonId,
                        CreatedByPersonName = a.PersonCreatedBy.FullName,
                        CreatedOnDateTime = a.CreatedOnDateTime,
                        DeletedByPersonId = a.DeletedByPersonId,
                        DeletedByPersonName = a?.PersonDeletedBy.FullName ?? string.Empty,
                        DeletedOnDateTime = a.DeletedOnDateTime,
                        IsDeleted = a.IsDeleted,
                        IsPrivate = a.IsPrivate,
                        ModifiedOnDateTime = a.ModifiedOnDateTime,
                        Narrative = a.Narrative,
                        WorkItemActivityId = a.WorkItemActivityId,
                        WorkItemActivityType = a.WorkItemActivityType.WorkItemActivityTypeName,
                        WorkItemActivityTypeId = a.WorkItemActivityTypeId,
                        WorkItemId = a.WorkItemId
                    };
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return null;
            }
        }

        public static async Task<WorkItemRecord> GetAsync(int workItemId)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var w = await e.WorkItems.FindAsync(workItemId);
                    if (w == null) return null;
                    var wir = new WorkItemRecord
                    {
                        CompanyCode = w.CompanyCode,
                        CompanyName = string.IsNullOrWhiteSpace(w.CompanyCode)
                            ? w.CompanyName
                            : w.Company?.CompanyName ?? string.Empty,
                        CreatedByPersonFullName = w.PersonCreatedBy.FullName,
                        CreatedByPersonId = w.CreatedByPersonId,
                        CreatedOnDateTime = w.CreatedOnDateTime,
                        DueDate = w.DueDate,
                        GarageAddress = w.GarageAddress,
                        GarageCity = w.GarageCity,
                        GarageCounty = w.GarageCounty,
                        GarageState = w.GarageState,
                        IsUrgent = w.IsUrgent,
                        LeaseNumber = w.CarLeaseId.HasValue
                            ? w.CarLease?.LeaseNumber ?? string.Empty
                            : w.LeaseNumber,
                        LesseeFirstName = w.CarLeaseId.HasValue
                            ? w.CarLease?.LesseeFirstName ?? string.Empty
                            : w.LesseeFirstName,
                        LesseeLastName = w.CarLeaseId.HasValue
                            ? w.CarLease?.LesseeLastName ?? string.Empty
                            : w.LesseeLastName,
                        ModifiedByPersonFullName = w.PersonModifiedBy?.FullName ?? string.Empty,
                        ModifiedByPersonId = w.ModifiedByPersonId,
                        ModifiedOnDateTime = w.ModifiedOnDateTime,
                        Narrative = w.Narrative,
                        NoticeDate = w.NoticeDate,
                        NoticeNumber = w.NoticeNumber,
                        PlateNumber = w.PlateNumber,
                        TaxBillId = w.TaxBillId,
                        TaxBillNumber = w.TaxBill?.BillNumber ?? "No Bill Number",
                        TaxBillCarLeaseId = w.TaxBillCarLeaseId,
                        TaxCollectorId = w.TaxCollectorId,
                        TaxCollectorName = w.TaxCollectorId.HasValue
                            ? w.TaxCollector?.TaxCollectorName ?? string.Empty
                            : w.TaxCollectorName,
                        TaxYear = w.TaxYear,
                        CarLeaseId = w.CarLeaseId,
                        CarMake = w.CarMake,
                        CarMasterId = w.CarMasterId,
                        Vin = w.CarLeaseId.HasValue ? w.CarLease?.Vin ?? string.Empty : w.Vin,
                        WorkItemId = w.WorkItemId,
                        WorkItemStatusId = w.WorkItemStatusId,
                        WorkItemStatusName = w.WorkItemStatus.WorkItemStatusName,
                        WorkItemTypeId = w.WorkItemTypeId,
                        WorkItemTypeName = w.WorkItemType.WorkItemTypeName,
                        WorkItemAttachmentRecords = w.WorkItemAttachments
                            .Where(a => !a.IsDeleted && !a.WorkItemActivityId.HasValue).Select(a =>
                                new WorkItemAttachmentRecord
                                {
                                    AttachmentDescription = a.AttachmentDescription,
                                    AttachmentName = a.AttachmentName,
                                    CreatedByPersonFullName = a.PersonCreatedBy.FullName,
                                    CreatedOnDateTime = a.CreatedOnDateTime,
                                    WorkItemAttachmentId = a.WorkItemAttachmentId
                                }).ToList(),
                        WorkItemActivityRecords = w.WorkItemActivities.Where(a => !a.IsDeleted)
                            .OrderByDescending(a => a.ActivityDateTime).ThenByDescending(a => a.WorkItemActivityId)
                            .Select(a => new WorkItemActivityRecord
                            {
                                CreatedByPersonId = a.CreatedByPersonId,
                                Narrative = a.Narrative,
                                ActivityDateTime = a.ActivityDateTime,
                                WorkItemActivityId = a.WorkItemActivityId,
                                CreatedByPersonName = a.PersonCreatedBy.FullName,
                                IsPrivate = a.IsPrivate,
                                WorkItemActivityType = a.WorkItemActivityType.WorkItemActivityTypeName,
                                WorkItemActivityAttachmentRecords = a.WorkItemAttachments.Where(q => !q.IsDeleted)
                                    .Select(q => new WorkItemAttachmentRecord
                                    {
                                        AttachmentDescription = q.AttachmentDescription,
                                        AttachmentName = q.AttachmentName,
                                        CreatedByPersonFullName = q.PersonCreatedBy.FullName,
                                        CreatedOnDateTime = q.CreatedOnDateTime,
                                        WorkItemAttachmentId = q.WorkItemAttachmentId
                                    }).ToList()
                            }).ToList()
                    };
                    return wir;
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return null;
            }
        }

        public static async Task<FileRecord> GetAttachmentAsync(int id)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var a = await e.WorkItemAttachments.FindAsync(id);
                    if (a?.DocumentFileStoreStreamId == null) return null;
                    var g = a.DocumentFileStoreStreamId.Value;

                    var d = await e.ViewDocumentFileStores.SingleOrDefaultAsync(v => v.stream_id.Equals(g));
                    if (d == null) return null;
                    return new FileRecord
                    {
                        Bytes = d.file_stream,
                        FileName = d.name
                    };
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return null;
            }
        }

        public static async Task<WorkItemListData> GetCollectionAsync(int take, int skip, string sortBy, bool sortDir,
            int wit, int wis, int pl, string nn, string tcn, string vin, string ln)
        {
            var workItemStatusExcludeByDefault = new List<int>
            {
                (int) WorkItemStatusEnum.Cancelled,
                (int) WorkItemStatusEnum.Merged,
                (int) WorkItemStatusEnum.Resolved
            };

            try
            {
                using (var e = new HexaEntities())
                {
                    var query = e.WorkItems.Where(w => true);

                    if (!wit.Equals(-1)) query = query.Where(w => w.WorkItemTypeId.Equals(wit));

                    query = !wis.Equals(-1)
                        ? query.Where(w => w.WorkItemStatusId.Equals(wis))
                        : query.Where(w => !workItemStatusExcludeByDefault.Contains(w.WorkItemStatusId));

                    if (!nn.IsNullOrWhiteSpace())
                        query = query.Where(w => w.NoticeNumber.Contains(nn));
                    if (!tcn.IsNullOrWhiteSpace())
                        query = query.Where(w => w.TaxCollectorName.Contains(tcn));

                    if (!vin.IsNullOrWhiteSpace())
                        query = query.Where(w => w.Vin.Contains(vin));

                    if (!ln.IsNullOrWhiteSpace())
                        query = query.Where(w => w.LeaseNumber.Contains(ln));

                    switch (pl)
                    {
                        case 1:
                            query = query.Where(w => w.IsUrgent);
                            break;
                        case 0:
                            query = query.Where(w => !w.IsUrgent);
                            break;
                    }

                    switch (sortBy)
                    {
                        case "WorkItemTypeName":
                            query = sortDir
                                ? query.OrderBy(v => v.WorkItemTypeId)
                                : query.OrderByDescending(v => v.WorkItemTypeId);
                            break;
                        case "IsUrgentString":
                            query = sortDir
                                ? query.OrderBy(v => v.IsUrgent)
                                : query.OrderByDescending(v => v.IsUrgent);
                            break;
                        case "NoticeNumber":
                            query = sortDir
                                ? query.OrderBy(v => v.NoticeNumber)
                                : query.OrderByDescending(v => v.NoticeNumber);
                            break;
                        case "CreateDateTime":
                            query = sortDir
                                ? query.OrderBy(v => v.CreatedOnDateTime)
                                : query.OrderByDescending(v => v.CreatedOnDateTime);
                            break;
                        case "TaxCollectorName":
                            query = sortDir
                                ? query.OrderBy(v => v.TaxCollectorName)
                                : query.OrderByDescending(v => v.TaxCollectorName);
                            break;
                        default:
                            query = query.OrderBy(a => a.IsUrgent).ThenByDescending(a => a.CreatedOnDateTime);
                            break;
                    }


                    var count = await query.CountAsync();
                    var recordList = await query.Skip(() => skip).Take(() => take).ToListAsync();

                    var totalResultsCount = e.WorkItems.Count();

                    var workItemListRecords = recordList.Select(workItem => new WorkItemListRecord
                    {
                        TaxCollectorName = workItem.TaxCollectorName,
                        WorkItemId = workItem.WorkItemId,
                        NoticeNumber = workItem.NoticeNumber,
                        IsUrgent = workItem.IsUrgent,
                        CreateDateTime = workItem.CreatedOnDateTime,
                        WorkItemTypeName = workItem.WorkItemType.WorkItemTypeName,
                        WorkItemStatusName = workItem.WorkItemStatus.WorkItemStatusName,
                        LastUpdated = workItem.WorkItemActivities.OrderByDescending(q => q.ActivityDateTime)
                            .Select(q => q.ActivityDateTime).FirstOrDefault()
                    }).ToList();

                    return new WorkItemListData
                    {
                        data = workItemListRecords,
                        recordsFiltered = count,
                        recordsTotal = totalResultsCount
                    };
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return new WorkItemListData
                {
                    data = new List<WorkItemListRecord>(),
                    recordsFiltered = 0,
                    recordsTotal = 0
                };
            }
        }

        public static async Task<int> GetUnworkedItemCount(bool refresh = false)
        {
            // TODO: Compartmentalize this
            try
            {
                if (Caching.GetCachedData(WorkItemCacheKey) is int unworkedCount && !refresh) return unworkedCount;
                unworkedCount = await GetUnworkedCount();
                Caching.SetCachedData(WorkItemCacheKey, unworkedCount, WorkItemCacheDuration);
                return unworkedCount;
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return 0;
            }
        }

        public static async Task<Dictionary<int, string>> GetWorkItemActivityTypesAsync()
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var x = await e.WorkItemActivityTypes.ToDictionaryAsync(w => w.WorkItemActivityTypeId,
                        w => w.WorkItemActivityTypeName);
                    return x;
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return new Dictionary<int, string>();
            }
        }

        public static async Task<int?> GetWorkItemIdAsync(int workItemActivityId)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var a = await e.WorkItemActivities.FindAsync(workItemActivityId);
                    return a?.WorkItemId;
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return null;
            }
        }

        public static async Task<Dictionary<int, string>> GetWorkItemStatusesAsync()
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var x = await e.WorkItemStatus.OrderBy(w => w.WorkItemStatusId)
                        .ToDictionaryAsync(w => w.WorkItemStatusId, w => w.WorkItemStatusName);
                    return x;
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return new Dictionary<int, string>();
            }
        }

        public static async Task<Dictionary<int, string>> GetWorkItemTypesAsync()
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var x = await e.WorkItemTypes.ToDictionaryAsync(w => w.WorkItemTypeId, w => w.WorkItemTypeName);
                    return x;
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return new Dictionary<int, string>();
            }
        }

        public static async Task<bool> IsEditableAsync(int workItemId)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var w = await e.WorkItems.FindAsync(workItemId);
                    if (w == null) return false;
                    switch (w.WorkItemStatusId)
                    {
                        case (int) WorkItemStatusEnum.Unworked:
                        case (int) WorkItemStatusEnum.Working:
                        case (int) WorkItemStatusEnum.WaitingForClient:
                        case (int) WorkItemStatusEnum.PendingResolved:
                            return true;
                        default:
                            return false;
                    }
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return false;
            }
        }

        public static async Task<bool> UpdateAsync(int workItemId, string companyName, int modifiedByPersonId,
            string garageAddress,
            string garageCity, string garageCounty, string garageState, bool isUrgent, string leaseNumber,
            string lesseeFirstname, string lesseeLastName, string narrative, string plateNumber,
            string taxCollectorName, string taxYear, string carMake,
            string vin, int workItemTypeId, DateTime? dueDate, DateTime? noticeDate, string noticeNumber)
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var wi = await e.WorkItems.FindAsync(workItemId);
                    if (wi == null) return false;
                    var n = DateTime.Now;

                    if (!wi.CarLeaseId.HasValue)
                    {
                        wi.LeaseNumber = leaseNumber.SanitizeHtml().Truncate(50).AsNullIfWhiteSpace();
                        wi.LesseeFirstName = lesseeFirstname.SanitizeHtml().Truncate(18).AsNullIfWhiteSpace();
                        wi.LesseeLastName = lesseeLastName.SanitizeHtml().Truncate(40).AsNullIfWhiteSpace();
                        wi.PlateNumber = plateNumber.SanitizeHtml().Truncate(10).AsNullIfWhiteSpace();
                        wi.Vin = vin.SanitizeHtml().Truncate(50).AsNullIfWhiteSpace();
                    }

                    if (!wi.TaxBillId.HasValue)
                    {
                        wi.TaxCollectorName = taxCollectorName.SanitizeHtml().Truncate(200).AsNullIfWhiteSpace();
                        wi.TaxYear = taxYear;
                    }

                    wi.CompanyName = companyName.SanitizeHtml().Truncate(100).AsNullIfWhiteSpace();
                    wi.DueDate = dueDate;
                    wi.GarageAddress = garageAddress.SanitizeHtml().Truncate(100).AsNullIfWhiteSpace();
                    wi.GarageCity = garageCity.SanitizeHtml().Truncate(60).AsNullIfWhiteSpace();
                    wi.GarageCounty = garageCounty.SanitizeHtml().Truncate(60).AsNullIfWhiteSpace();
                    wi.GarageState = garageState.SanitizeHtml().Truncate(2).AsNullIfWhiteSpace();
                    wi.IsUrgent = isUrgent;
                    wi.Narrative = narrative.SanitizeHtml();
                    wi.NoticeDate = noticeDate;
                    wi.NoticeNumber = noticeNumber.SanitizeHtml().Truncate(50).AsNullIfWhiteSpace();
                    wi.CarMake = carMake.SanitizeHtml().Truncate(100).AsNullIfWhiteSpace();
                    wi.WorkItemTypeId = workItemTypeId;

                    wi.ModifiedOnDateTime = n;
                    wi.ModifiedByPersonId = modifiedByPersonId;

                    e.Entry(wi).State = EntityState.Modified;
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

        #region Methods

        private static async Task<int> GetUnworkedCount()
        {
            try
            {
                using (var e = new HexaEntities())
                {
                    var unworkedWorkItemCount = await e.WorkItems.CountAsync(w => w.WorkItemStatusId.Equals(1));
                    return unworkedWorkItemCount;
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return 0;
            }
        }

        #endregion
    }
}