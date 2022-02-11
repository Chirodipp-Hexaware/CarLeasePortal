using System.Collections.Generic;

namespace CarLeasePPT.DataAccessLayer
{
    public class CarMasterRecord
    {
        #region Public Properties

        public string ModelYear { get; set; }
        public IEnumerable<CarLeaseRecord> CarLeaseRecords { get; set; }
        public string CarMake { get; set; }
        public int CarMasterId { get; set; }
        public string CarModel { get; set; }
        public string Vin { get; set; }

        #endregion
    }
}