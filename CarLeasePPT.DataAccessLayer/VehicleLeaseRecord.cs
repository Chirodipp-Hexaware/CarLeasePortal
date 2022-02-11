using System;
using System.Collections.Generic;

namespace CarLeasePPT.DataAccessLayer
{
    public class CarLeaseRecord
    {
        #region Public Properties

        public DateTime? ActualTerminationDate { get; set; }
        public string AssetNumber { get; set; }

        public string AssetStatusName { get; set; }
        public string BranchCode { get; set; }
        public DateTime CommencementDate { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public IEnumerable<CustomerAddressRecord> CustomerAddressRecords { get; set; }
        public string ExemptionCode { get; set; }

        public IEnumerable<LeaseAttachmentRecord> LeaseAttachmentRecords { get; set; }
        public string LeaseNumber { get; set; }
        public string LeaseStatusName { get; set; }
        public string LesseeFirstLastName => $"{LesseeFirstName} {LesseeLastName}";
        public string LesseeFirstName { get; set; }

        public string LesseeLastFirstName => $"{LesseeLastName}, {LesseeFirstName}";
        public string LesseeLastName { get; set; }
        public string ModelYear { get; set; }
        public decimal? OriginalCost { get; set; }
        public string PayoffSource { get; set; }

        public IEnumerable<PhysicalAddressRecord> PhysicalAddressRecords { get; set; }
        public string PlateNumber { get; set; }
        public string ProductUseCode { get; set; }
        public string RegistrationState { get; set; }
        public DateTime? SaleDate { get; set; }
        public decimal? SalesPrice { get; set; }
        public DateTime ScheduledTerminationDate { get; set; }
        public IEnumerable<TaxBillCarLeaseRecord> TaxBillCarLeaseRecords { get; set; }
        public string TaxRecoveryCode { get; set; }
        public string TerminationCode { get; set; }
        public int CarLeaseId { get; set; }

        public string CarMake { get; set; }
        public int CarMasterId { get; set; }
        public string CarModel { get; set; }
        public string Vin { get; set; }

        #endregion
    }
}