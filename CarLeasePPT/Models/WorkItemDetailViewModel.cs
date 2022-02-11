using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CarLeasePPT.DataAccessLayer;
using CarLeasePPT.Utility;

namespace CarLeasePPT.Models
{
    public class WorkItemDetailViewModel
    {
        #region Public Properties

        [Display(ResourceType = typeof(Resources), Name = "WorkItem_Common_CompanyName_DisplayName")]
        public string CompanyName { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "WorkItem_Detail_CreatedByPersonFullName_DisplayName")]
        public string CreatedByPersonFullName { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "WorkItem_Detail_CreatedOnDateTime_DisplayName")]
        [DisplayFormat(DataFormatString = "dddd MMMM, d yyyy @ hh:mm tt")]
        [DataType(DataType.Date)]
        public DateTime CreatedOnDateTime { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "WorkItem_Common_DueDate_DisplayName")]
        [DisplayFormat(DataFormatString = "M/d/yyyy")]
        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "WorkItem_Common_GarageAddress_DisplayName")]
        public string GarageAddress { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "WorkItem_Common_GarageCity_DisplayName")]
        public string GarageCity { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "WorkItem_Common_GarageCounty_DisplayName")]
        public string GarageCounty { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "WorkItem_Common_GarageState_DisplayName")]
        public string GarageState { get; set; }

        public bool IsEditable { get; set; }

        public bool IsUrgent { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "WorkItem_Common_LeaseNumber_DisplayName")]
        public string LeaseNumber { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "WorkItem_Common_LesseeFirstName_DisplayName")]
        public string LesseeFirstName { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "WorkItem_Common_LesseeLastName_DisplayName")]
        public string LesseeLastName { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "WorkItem_Detail_ModifiedByPersonFullName_DisplayName")]
        public string ModifiedByPersonFullName { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "WorkItem_Detail_ModifiedOnDateTime_DisplayName")]
        [DisplayFormat(DataFormatString = "dddd MMMM, d yyyy @ hh:mm tt")]
        [DataType(DataType.Date)]
        public DateTime? ModifiedOnDateTime { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "WorkItem_Common_Narrative_DisplayName")]
        public string Narrative { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "WorkItem_Common_NoticeDate_DisplayName")]
        [DisplayFormat(DataFormatString = "M/d/yyyy")]
        [DataType(DataType.Date)]
        public DateTime? NoticeDate { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "WorkItem_Common_NoticeNumber_DisplayName")]
        public string NoticeNumber { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "WorkItem_Common_PlateNumber_DisplayName")]
        public string PlateNumber { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "WorkItem_Common_TaxBill_DisplayName")]
        public string TaxBill { get; set; }

        public int? TaxBillId { get; set; }
        public int? TaxCollectorId { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "WorkItem_Common_TaxCollectorName_DisplayName")]
        public string TaxCollectorName { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "WorkItem_Common_TaxYear_DisplayName")]
        public string TaxYear { get; set; }

        public int? CarLeaseId { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "WorkItem_Common_CarMake_DisplayName")]
        public string CarMake { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "WorkItem_Common_Vin_Displayname")]
        public string Vin { get; set; }

        public IEnumerable<WorkItemActivityRecord> WorkItemActivityRecords { get; set; }

        public IEnumerable<WorkItemAttachmentRecord> WorkItemAttachmentRecords { get; set; }
        public int WorkItemId { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "WorkItem_Detail_WorkItemType_DisplayName")]
        public string WorkItemType { get; set; }

        #endregion
    }
}