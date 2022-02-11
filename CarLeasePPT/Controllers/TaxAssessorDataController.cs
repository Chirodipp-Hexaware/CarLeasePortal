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
    public class TaxAssessorDataController : ApiController
    {
        #region Properties

        private static TaxAssessorListData DefaultData => new TaxAssessorListData
        {
            draw = 0,
            data = new List<TaxAssessorListRecord>(),
            recordsFiltered = 0,
            recordsTotal = 0
        };

        private static Select2Data DefaultSelect2Data => new Select2Data();

        #endregion

        #region Public Methods and Operators

        [HttpPost]
        public async Task<TaxAssessorListData> Post(DataTableAjaxPostModel model)
        {
            try
            {
                var request = HttpContext.Current.Request;
                if (!request.IsAuthenticated || !User.IsInRole("ASSESSOR_MANAGEMENT")) return DefaultData;

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
                foreach (var v in returnData.data)
                    v.IsDeletedString =
                        v.IsDeleted
                            ? Resources.Common_Yes_Message
                            : Resources.Common_No_Message;
                // Return the data for json conversion and passback to calling application
                return new TaxAssessorListData
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
                if (!request.IsAuthenticated || !User.IsInRole("COLLECTOR_MANAGEMENT")) return DefaultSelect2Data;

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

                var taxAssessorReturnData = await TaxAssessorEngine.GetSelect2CollectionAsync(model.term, model.page);

                return taxAssessorReturnData;
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return DefaultSelect2Data;
            }
        }

        #endregion

        #region Methods

        private static async Task<TaxAssessorListData> ParseAndPullAsync(DataTableAjaxPostModel model)
        {
            // Grab the search and pagination values
            var searchBy = model.search?.value;
            var includeDeleted = model.search?.show_excluded ?? false;
            var take = model.length;
            var skip = model.start;

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

            // Call the LocationEngine to pull the appropriate data (including search, sort, paging, and showing excluded records)
            var result =
                await TaxAssessorEngine.GetCollectionAsync(searchBy, take, skip, sortBy, sortDir, includeDeleted);
            // If the result is null, return the "default" data object
            return result ?? DefaultData;
        }

        #endregion
    }
}