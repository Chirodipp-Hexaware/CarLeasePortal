using System.Collections.Generic;

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace CarLeasePPT.DataAccessLayer
{
    public class CarListData
    {
        #region Public Properties

        public IList<CarMasterRecord> data { get; set; }
        public int draw { get; set; }
        public int recordsFiltered { get; set; }
        public int recordsTotal { get; set; }

        #endregion
    }
}