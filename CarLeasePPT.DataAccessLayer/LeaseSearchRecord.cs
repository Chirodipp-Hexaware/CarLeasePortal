namespace CarLeasePPT.DataAccessLayer
{
    public class LeaseSearchRecord
    {
        #region Public Properties

        public string LeaseNumber { get; set; }
        public string LesseeFirstName { get; set; }
        public string LesseeFullName => $"{LesseeLastName}, {LesseeFirstName}";
        public string LesseeLastName { get; set; }
        public string ModelYear { get; set; }
        public int CarLeaseId { get; set; }
        public string CarMake { get; set; }
        public int CarMasterId { get; set; }
        public string CarModel { get; set; }

        // ReSharper disable once InconsistentNaming
        public string VIN { get; set; }

        #endregion
    }
}