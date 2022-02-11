// ReSharper disable InconsistentNaming
// ReSharper disable UnusedAutoPropertyAccessor.Global

using System.Collections.Generic;
using Newtonsoft.Json;

namespace CarLeasePPT.DataAccessLayer
{
    public class Select2Data
    {
        #region Constructors and Destructors

        public Select2Data()
        {
            results = new List<Select2Record>();
            pagination = new Select2Pagination
            {
                more = false
            };
        }

        #endregion

        #region Public Properties

        public Select2Pagination pagination { get; set; }
        public IEnumerable<Select2Record> results { get; set; }

        #endregion
    }

    public class Select2Record
    {
        #region Public Properties

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<Select2Record> children { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? id { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string text { get; set; }

        #endregion
    }

    public class Select2Pagination
    {
        #region Public Properties

        public bool more { get; set; }

        #endregion
    }
}