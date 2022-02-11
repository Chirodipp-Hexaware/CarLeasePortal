using System;

namespace CarLeasePPT.Spreadsheet
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EpPlusNumberFormat : Attribute
    {
        #region Constructors and Destructors

        public EpPlusNumberFormat(string format)
        {
            Format = format;
        }

        #endregion

        #region Public Properties

        public string Format { get; }

        #endregion
    }
}