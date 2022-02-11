using System;
using System.Collections.Generic;

namespace CarLeasePPT.DataAccessLayer
{
    public class WorkItemRecord
    {
        #region Public Properties

        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }

        public string CreatedByPersonFullName { get; set; }
        public int CreatedByPersonId { get; set; }
        public DateTime CreatedOnDateTime { get; set; }

        public DateTime? DueDate { get; set; }
        public string GarageAddress { get; set; }
        public string GarageCity { get; set; }
        public string GarageCounty { get; set; }
        public string GarageState { get; set; }
        public bool IsUrgent { get; set; }
        public string LeaseNumber { get; set; }
        public string LesseeFirstName { get; set; }
        public string LesseeLastName { get; set; }
        public string ModifiedByPersonFullName { get; set; }
        public int? ModifiedByPersonId { get; set; }
        public DateTime? ModifiedOnDateTime { get; set; }
        public string Narrative { get; set; }
        public DateTime? NoticeDate { get; set; }
        public string NoticeNumber { get; set; }
        public string PlateNumber { get; set; }
        public int? TaxBillId { get; set; }
        public string TaxBillNumber { get; set; }
        public int? TaxBillCarLeaseId { get; set; }
        public int? TaxCollectorId { get; set; }
        public string TaxCollectorName { get; set; }
        public string TaxYear { get; set; }
        public int? CarLeaseId { get; set; }
        public string CarMake { get; set; }
        public int? CarMasterId { get; set; }
        public string Vin { get; set; }
        public IEnumerable<WorkItemActivityRecord> WorkItemActivityRecords { get; set; }

        public IEnumerable<WorkItemAttachmentRecord> WorkItemAttachmentRecords { get; set; }
        public int WorkItemId { get; set; }
        public int WorkItemStatusId { get; set; }
        public string WorkItemStatusName { get; set; }
        public int WorkItemTypeId { get; set; }
        public string WorkItemTypeName { get; set; }

        #endregion
    }
}