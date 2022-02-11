using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using NLog;
using CarLeasePPT.DataAccessLayer;
using CarLeasePPT.Models;
using CarLeasePPT.Utility;

namespace CarLeasePPT.Controllers
{
    public class WorkItemDataController : ApiController
    {
        #region Properties

        private static WorkItemListData DefaultData => new WorkItemListData
        {
            draw = 0,
            data = new List<WorkItemListRecord>(),
            recordsFiltered = 0,
            recordsTotal = 0
        };

        private static Select2Data DefaultSelect2Data => new Select2Data();

        #endregion

        #region Public Methods and Operators

        [HttpPost]
        public async Task<WorkItemListData> Post(DataTableAjaxPostModel model)
        {
            try
            {
                var request = HttpContext.Current.Request;
                if (!request.IsAuthenticated) return DefaultData;

                // Validate CRSF Antiforgery Token
                var tokenheaders = request.Headers.GetValues("RequestVerificationToken")?.FirstOrDefault() ??
                                   string.Empty;
                if (string.IsNullOrWhiteSpace(tokenheaders)) return DefaultData;
                var tokens = tokenheaders.Split(':');
                if (!tokens.Length.Equals(2)) return DefaultData;
                try
                {
                    AntiForgery.Validate(tokens[0].Trim(), tokens[1].Trim());
                }
                catch
                {
                    var personName = HttpContext.Current.User.Identity.Name ?? "Unknown";
                    var personId = HttpContext.Current.User.Identity.GetUserId();

                    var logEvent = new LogEventInfo(LogLevel.Warn, "AuditLog", "Invalid WebAPI Access Attempt (CRSF)");
                    logEvent.Properties.Add("PersonId", personId);
                    logEvent.Properties.Add("FullName", personName);
                    Audit.Instance.Log(logEvent);
                    return DefaultData;
                }

                // Hand off data request to the parse/pull method to communicate with the data layer
                var returnData = await ParseAndPullAsync(model);
                // Return the data for json conversion and passback to calling application

                foreach (var w in returnData.data)
                {
                    w.IsUrgentString = w.IsUrgent
                        ? Resources.WorkItem_Common_IsUrgent_True
                        : Resources.WorkItem_Common_IsUrgent_False;
                    w.NoticeNumber = w.NoticeNumber.AsNullIfWhiteSpace() ??
                                     Resources.WorkItem_Index_NoticeNumber_Default_Value;
                    if (w.LastUpdated.HasValue && w.LastUpdated.Value.Equals(DateTime.MinValue))
                        w.LastUpdated = null;
                }

                return new WorkItemListData
                {
                    draw = model.draw,
                    data = returnData.data.ToList(),
                    recordsTotal = returnData.recordsTotal,
                    recordsFiltered = returnData.recordsFiltered
                };
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return DefaultData;
            }
        }

        [HttpPost]
        public async Task<Select2Data> Select2(Select2AjaxPostModel model)
        {
            try
            {
                var request = HttpContext.Current.Request;
                if (!request.IsAuthenticated) return DefaultSelect2Data;

                // Validate CRSF Antiforgery Token
                var tokenheaders = request.Headers.GetValues("RequestVerificationToken")?.FirstOrDefault() ??
                                   string.Empty;
                if (string.IsNullOrWhiteSpace(tokenheaders)) return DefaultSelect2Data;
                var tokens = tokenheaders.Split(':');
                if (!tokens.Length.Equals(2)) return DefaultSelect2Data;
                try
                {
                    AntiForgery.Validate(tokens[0].Trim(), tokens[1].Trim());
                }
                catch
                {
                    var personName = HttpContext.Current.User.Identity.Name ?? "Unknown";
                    var personId = HttpContext.Current.User.Identity.GetUserId();

                    var logEvent = new LogEventInfo(LogLevel.Warn, "AuditLog", "Invalid WebAPI Access Attempt (CRSF)");
                    logEvent.Properties.Add("PersonId", personId);
                    logEvent.Properties.Add("FullName", personName);
                    Audit.Instance.Log(logEvent);
                    return DefaultSelect2Data;
                }

                var entityRecords =
                    await CarEngine.GetSelect2CollectionAsync(model.term ?? string.Empty, model.page);
                return entityRecords;
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return DefaultSelect2Data;
            }
        }

        #endregion

        #region Methods

        private static async Task<WorkItemListData> ParseAndPullAsync(DataTableAjaxPostModel model)
        {
            // Grab the search and pagination values
            var take = model.length;
            var skip = model.start;
            var vin = model.search?.vin;
            var nn = model.search?.nn;
            var tcn = model.search?.tcn;
            var wis = model.search?.wis ?? -1;
            var wit = model.search?.wit ?? -1;
            var pl = model.search?.pl ?? -1;
            var ln = model.search?.ln;

            // Set the default sort data
            var sortBy = string.Empty;
            var sortDir = true;

            // Parse the order data for sorting information
            if (model.order != null)
            {
                var col = model.columns[model.order[0].column];
                sortBy = col.name ?? col.data;
                sortDir = model.order[0].dir.ToLower().Equals("asc");
            }

            var result =
                await WorkItemEngine.GetCollectionAsync(take, skip, sortBy, sortDir, wit, wis, pl, nn, tcn, vin, ln);
            // If the result is null, return the "default" data object
            return result ?? DefaultData;
        }

        #endregion
    }
}