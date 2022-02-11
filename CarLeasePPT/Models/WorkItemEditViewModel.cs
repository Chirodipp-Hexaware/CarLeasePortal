using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CarLeasePPT.Utility;

namespace CarLeasePPT.Models
{
    public class WorkItemEditViewModel
    {
        #region Public Properties

        [Display(ResourceType = typeof(Resources), Name = "WorkItem_Common_CompanyName_DisplayName")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "Common_Validation_FieldLengthMaximumError_Message")]
        public string CompanyName { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "WorkItem_Common_DueDate_DisplayName")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "WorkItem_Common_GarageAddress_DisplayName")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "Common_Validation_FieldLengthMaximumError_Message")]
        public string GarageAddress { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "WorkItem_Common_GarageCity_DisplayName")]
        [MaxLength(60, ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "Common_Validation_FieldLengthMaximumError_Message")]
        public string GarageCity { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "WorkItem_Common_GarageCounty_DisplayName")]
        [MaxLength(60, ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "Common_Validation_FieldLengthMaximumError_Message")]
        public string GarageCounty { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "WorkItem_Common_GarageState_DisplayName")]
        [MaxLength(2, ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "Common_Validation_FieldLengthMaximumError_Message")]
        public string GarageState { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "WorkItem_Create_IsUrgent_DisplayName")]
        public bool IsUrgent { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "WorkItem_Common_LeaseNumber_DisplayName")]
        [MaxLength(50, ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "Common_Validation_FieldLengthMaximumError_Message")]
        public string LeaseNumber { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "WorkItem_Common_LesseeFirstName_DisplayName")]
        [MaxLength(18, ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "Common_Validation_FieldLengthMaximumError_Message")]
        public string LesseeFirstName { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "WorkItem_Common_LesseeLastName_DisplayName")]
        [MaxLength(40, ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "Common_Validation_FieldLengthMaximumError_Message")]
        public string LesseeLastName { get; set; }

        [Required]
        [MaxLength(4000)]
        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(Resources), Name = "WorkItem_Common_Narrative_DisplayName")]
        public string Narrative { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "WorkItem_Common_NoticeDate_DisplayName")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? NoticeDate { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "WorkItem_Common_NoticeNumber_DisplayName")]
        [MaxLength(50, ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "Common_Validation_FieldLengthMaximumError_Message")]
        public string NoticeNumber { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "WorkItem_Common_PlateNumber_DisplayName")]
        [MaxLength(10, ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "Common_Validation_FieldLengthMaximumError_Message")]
        public string PlateNumber { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "WorkItem_Common_TaxBill_DisplayName")]
        public string TaxBill { get; set; }

        public int? TaxBillId { get; set; }

        public string TaxBillNumber { get; set; }
        public int? TaxBillCarLeaseId { get; set; }
        public int? TaxCollectorId { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "WorkItem_Common_TaxCollectorName_DisplayName")]
        [MaxLength(200, ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "Common_Validation_FieldLengthMaximumError_Message")]
        public string TaxCollectorName { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "WorkItem_Common_TaxYear_DisplayName")]
        [Range(2000, 2050)]
        public string TaxYear { get; set; }

        public int? CarLeaseId { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "WorkItem_Common_CarMake_DisplayName")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "Common_Validation_FieldLengthMaximumError_Message")]
        public string CarMake { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "WorkItem_Common_Vin_Displayname")]
        [MaxLength(50, ErrorMessageResourceType = typeof(Resources),
            ErrorMessageResourceName = "Common_Validation_FieldLengthMaximumError_Message")]
        public string Vin { get; set; }

        public int WorkItemId { get; set; }

        [DisplayName("Work Item Type")]
        [Display(ResourceType = typeof(Resources), Name = "WorkItem_Create_WorkItemTypeId_DisplayName")]
        [Required]
        public int WorkItemTypeId { get; set; }

        public Dictionary<int, string> WorkItemTypes { get; set; }

        #endregion
    }
}