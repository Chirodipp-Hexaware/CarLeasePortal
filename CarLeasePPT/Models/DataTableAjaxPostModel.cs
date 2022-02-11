using System.Collections.Generic;

// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable CollectionNeverUpdated.Global

namespace CarLeasePPT.Models
{
    //Start - JSon class sent from Datatables
    public class DataTableAjaxPostModel
    {
        #region Public Properties

        public List<Column> columns { get; set; }

        // properties are not capital due to json mapping
        public int draw { get; set; }
        public int length { get; set; }
        public List<Order> order { get; set; }
        public Search search { get; set; }
        public int start { get; set; }

        #endregion
    }

    public class Column
    {
        #region Public Properties

        public string data { get; set; }
        public string name { get; set; }
        public bool orderable { get; set; }
        public Search search { get; set; }
        public bool searchable { get; set; }

        #endregion
    }

    public class Search
    {
        #region Public Properties

        public string bn { get; set; }
        public string lfn { get; set; }
        public string lln { get; set; }
        public string ln { get; set; }
        public string nn { get; set; }
        public int pl { get; set; }

        public string pn { get; set; }
        public string record_code { get; set; }
        public string record_name { get; set; }
        public string regex { get; set; }
        public bool show_excluded { get; set; }
        public bool show_info { get; set; }
        public string tan { get; set; }

        public string tcn { get; set; }
        public string value { get; set; }
        public string vin { get; set; }
        public int wis { get; set; }
        public int wit { get; set; }

        #endregion
    }


    public class Order
    {
        #region Public Properties

        public int column { get; set; }
        public string dir { get; set; }

        #endregion
    }

    // End- JSon class sent from Datatables
}