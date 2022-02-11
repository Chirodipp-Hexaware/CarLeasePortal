
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedAutoPropertyAccessor.Global

using System.Collections.Generic;

namespace CarLeasePPT.DataAccessLayer
{
    public class TaxCollectorListData
    {
        public IList<TaxCollectorListRecord> data { get; set; }
        public int draw { get; set; }
        public int recordsFiltered { get; set; }
        public int recordsTotal { get; set; }
    }
}
