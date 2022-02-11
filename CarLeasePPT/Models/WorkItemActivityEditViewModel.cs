using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CarLeasePPT.Utility;

namespace CarLeasePPT.Models
{
    public class WorkItemActivityEditViewModel
    {
        #region Public Properties

        [Required]
        [Display(ResourceType = typeof(Resources), Name = "WorkItemActivity_Common_ActivityDate_DisplayName")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime ActivityDateTime { get; set; }

        [Required]
        [Display(ResourceType = typeof(Resources), Name = "WorkItemActivity_Common_ActivityTime_DisplayName")]
        public int ActivityTimeHour { get; set; }

        public SelectList ActivityTimeHours { get; set; }

        [Required]
        [Display(ResourceType = typeof(Resources), Name = "WorkItemActivity_Common_ActivityTime_DisplayName")]
        public int ActivityTimeMinute { get; set; }

        public SelectList ActivityTimeMinutes { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "WorkItemActivity_Common_IsPrivate_DisplayName")]
        public bool IsPrivate { get; set; }

        [Required]
        [MaxLength(4000)]
        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(Resources), Name = "WorkItemActivity_Common_Narrative_DisplayName")]
        public string Narrative { get; set; }

        public int WorkItemActivityId { get; set; }

        [Required]
        [Display(ResourceType = typeof(Resources), Name = "WorkItemActivity_Common_WorkItemActivityTypeId_DisplayName")]
        public int WorkItemActivityTypeId { get; set; }

        public Dictionary<int, string> WorkItemActivityTypes { get; set; }
        public int WorkItemId { get; set; }

        #endregion
    }
}