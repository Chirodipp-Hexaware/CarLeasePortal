
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedAutoPropertyAccessor.Global

using System.Collections.Generic;

namespace CarLeasePPT.DataAccessLayer
{
    public class TaxAssessorListData
    {
        public IList<TaxAssessorListRecord> data { get; set; }
        public int draw { get; set; }
        public int recordsFiltered { get; set; }
        public int recordsTotal { get; set; }
    }
}
