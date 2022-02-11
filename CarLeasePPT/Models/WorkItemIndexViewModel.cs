using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CarLeasePPT.Utility;

namespace CarLeasePPT.Models
{
    public class WorkItemIndexViewModel
    {
        #region Constructors and Destructors

        public WorkItemIndexViewModel()
        {
            WorkItemTypeId = -1;
            WorkItemStatusId = -1;
            PriorityLevel = -1;
        }

        #endregion

        #region Public Properties

        [Display(ResourceType = typeof(Resources), Name = "WorkItem_Common_LeaseNumber_DisplayName")]
        public string LeaseNumber { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "WorkItem_Common_NoticeNumber_DisplayName")]
        public string NoticeNumber { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "WorkItem_Index_PriorityLevel_DisplayName")]
        public int PriorityLevel { get; set; }

        public IEnumerable<SelectListItem> PriorityLevels { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "WorkItem_Common_TaxCollectorName_DisplayName")]
        public string TaxCollectorName { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "WorkItem_Common_Vin_Displayname")]
        public string Vin { get; set; }

        public IEnumerable<SelectListItem> WorkItemStatuses { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "WorkItem_Index_WorkItemStatusId_DisplayName")]
        public int WorkItemStatusId { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "WorkItem_Index_WorkItemTypeId_DisplayName")]
        public int WorkItemTypeId { get; set; }

        public IEnumerable<SelectListItem> WorkItemTypes { get; set; }

        #endregion
    }
}