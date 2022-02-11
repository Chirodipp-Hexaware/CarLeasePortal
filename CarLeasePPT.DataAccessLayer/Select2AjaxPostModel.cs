// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable CollectionNeverUpdated.Global

namespace CarLeasePPT.DataAccessLayer
{
    public class Select2AjaxPostModel
    {
        #region Constructors and Destructors

        public Select2AjaxPostModel()
        {
            term = "";
            page = 1;
        }

        #endregion

        #region Public Properties

        public int page { get; set; }

        // properties are not capital due to json mapping
        public string term { get; set; }

        #endregion
    }
}