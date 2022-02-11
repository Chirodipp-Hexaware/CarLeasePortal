using System;
using System.Collections.ObjectModel;
using System.Configuration;

namespace CarLeasePPT.Utility
{
    public static class PasswordSecurityPolicy
    {
        #region Public Properties

        public static int AlphaRequiredCount =>
            int.TryParse(ConfigurationManager.AppSettings["PasswordAlphaRequiredCount"], out var t) ? t : 0;

        public static int ExpirePeriod =>
            int.TryParse(ConfigurationManager.AppSettings["PasswordExpirePeriod"], out var t) ? t : 0;

        public static int LowerCaseCount =>
            int.TryParse(ConfigurationManager.AppSettings["PasswordLowerCaseCount"], out var t) ? t : 0;

        public static int MaximumLength =>
            int.TryParse(ConfigurationManager.AppSettings["PasswordMaximumLength"], out var t) ? t : 0;

        public static int MinimumLength =>
            int.TryParse(ConfigurationManager.AppSettings["PasswordMinimumLength"], out var t) ? t : 10;

        public static int NumericRequiredCount =>
            int.TryParse(ConfigurationManager.AppSettings["PasswordNumericRequiredCount"], out var t) ? t : 0;

        public static int RepeatedCharacterCount =>
            int.TryParse(ConfigurationManager.AppSettings["PasswordRepeatedCharacterCount"], out var t) ? t : 0;

        public static int ReuseInterval =>
            int.TryParse(ConfigurationManager.AppSettings["PasswordReuseInterval"], out var t) ? t : 0;

        public static string SpecialCharacterList => ConfigurationManager.AppSettings["PasswordSpecialCharacterList"] ??
                                                     @" !""$%&'()*+,-./:;<=>?@[\]^_`{|}~";

        public static int SpecialRequiredCount =>
            int.TryParse(ConfigurationManager.AppSettings["PasswordSpecialRequiredCount"], out var t) ? t : 0;

        public static int UpperCaseCount =>
            int.TryParse(ConfigurationManager.AppSettings["PasswordUpperCaseCount"], out var t) ? t : 0;

        public static int WarnPeriod =>
            int.TryParse(ConfigurationManager.AppSettings["PasswordWarnPeriod"], out var t) ? t : 0;

        #endregion

        #region Public Methods and Operators

        public static Collection<string> GetPasswordPolicyList()
        {
            var policyCollection = new Collection<string>();
            try
            {
                if (!AlphaRequiredCount.Equals(0))
                    policyCollection.Add(string.Format(Resources.Settings_Password_PasswordPolicy_AlphaCount_Message,
                        AlphaRequiredCount));

                if (!LowerCaseCount.Equals(0))
                    policyCollection.Add(string.Format(
                        Resources.Settings_Password_PasswordPolicy_LowerAlphaCount_Message,
                        LowerCaseCount));

                if (!UpperCaseCount.Equals(0))
                    policyCollection.Add(string.Format(
                        Resources.Settings_Password_PasswordPolicy_UpperAlphaCount_Message,
                        UpperCaseCount));

                if (!NumericRequiredCount.Equals(0))
                    policyCollection.Add(string.Format(Resources.Settings_Password_PasswordPolicy_NumericCount_Message,
                        NumericRequiredCount));

                if (!SpecialRequiredCount.Equals(0) && !SpecialCharacterList.Length.Equals(0))
                    policyCollection.Add(string.Format(
                        Resources.Settings_Password_PasswordPolicy_SpecialCharacterCount_Message,
                        SpecialRequiredCount, SpecialCharacterList));

                if (!RepeatedCharacterCount.Equals(0))
                    policyCollection.Add(string.Format(
                        Resources.Settings_Password_PasswordPolicy_RepeatCharacterCount_Message,
                        RepeatedCharacterCount));

                if (!MinimumLength.Equals(0))
                    policyCollection.Add(string.Format(Resources.Settings_Password_PasswordPolicy_LengthMinimum_Message,
                        MinimumLength));

                if (!MaximumLength.Equals(0))
                    policyCollection.Add(string.Format(Resources.Settings_Password_PasswordPolicy_LengthMaximum_Message,
                        MaximumLength));

                if (!ReuseInterval.Equals(0))
                    policyCollection.Add(
                        string.Format(Resources.Settings_Password_PasswordPolicy_PasswordReuseCount_Message,
                            ReuseInterval));

                if (!ExpirePeriod.Equals(0))
                    policyCollection.Add(
                        string.Format(Resources.Settings_Password_PasswordPolicy_ExpirePeriod_Message,
                            ExpirePeriod));
                return policyCollection;
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return policyCollection;
            }
        }

        #endregion
    }
}