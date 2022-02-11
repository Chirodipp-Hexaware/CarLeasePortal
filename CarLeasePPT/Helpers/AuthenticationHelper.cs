using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using CarLeasePPT.DataAccessLayer;
using CarLeasePPT.EmailClient;
using CarLeasePPT.Encryption;
using CarLeasePPT.Models;
using CarLeasePPT.Utility;
// ReSharper disable StringLiteralTypo

// ReSharper disable IdentifierTypo

namespace CarLeasePPT.Helpers
{
    public static partial class AuthenticationHelper
    {
        #region Properties

        private static byte[] BlockCookieEncryptionKey =>
            Convert.FromBase64String(ConfigurationManager.AppSettings["BlockCookieEncryptionKey"] ?? "");

        private static string BlockCookieName => ConfigurationManager.AppSettings["BlockCookieName"] ??
                                                 "hexa_default_84d48701aba3477e88e1cab81508fc62";

        private static int TokenDuration =>
            int.TryParse(ConfigurationManager.AppSettings["TokenDuration"], out var tokenDuration) ? tokenDuration : 15;

        #endregion

        #region Public Methods and Operators

        public static async Task<int> CheckPasswordResetTokenAsync(string token)
        {
            try
            {
                var validTokenCollection = await CredentialResetEngine.GetValidPersonSecurityResetCollectionAsync();
                foreach (var t in validTokenCollection)
                {
                    if (!Pbkdf2.Verify(token, t.ResetToken)) continue;
                    return t.PersonSecurityResetId;
                }

                return -1;
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return -1;
            }
        }

        public static BlockedPersonViewModel GetBlockCookie(HttpContext context)
        {
            try
            {
                var cookie = context.Request.Cookies[BlockCookieName];
                if (cookie == null) return new BlockedPersonViewModel();
                var encryptedData = cookie.Value;
                var decryptedData = AesGcm.SimpleDecrypt(encryptedData, BlockCookieEncryptionKey);
                return (BlockedPersonViewModel) JsonConvert.DeserializeObject(decryptedData,
                    typeof(BlockedPersonViewModel));
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return new BlockedPersonViewModel();
            }
        }

        public static DateTime GetPasswordExpiration(IAuthenticationManager authenticationManager)
        {
            try
            {
                var principal = authenticationManager.User;
                var identifier = principal.Claims.AsEnumerable()?
                    .SingleOrDefault(t => t.Type.Equals(CustomClaimTypes.PasswordExpiration))?.Value;
                return DateTime.TryParse(identifier, out var d) ? d : DateTime.MaxValue;
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return DateTime.MaxValue;
            }
        }

        public static int GetPersonIdentifier(IAuthenticationManager authenticationManager)
        {
            try
            {
                var principal = authenticationManager.User;
                var identifier = principal.Claims.AsEnumerable()?
                    .SingleOrDefault(t => t.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
                if (identifier == null) return -1;
                return int.Parse(identifier);
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return -1;
            }
        }

        public static string GetPersonSession(IAuthenticationManager authenticationManager)
        {
            try
            {
                var principal = authenticationManager.User;
                var identifier = principal.Claims.AsEnumerable()?
                    .SingleOrDefault(t => t.Type.Equals(CustomClaimTypes.SessionToken))?.Value;
                return identifier ?? string.Empty;
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return string.Empty;
            }
        }

        public static async Task<bool> NewUserResetAsync(int personId, string actionUrl)
        {
            try
            {
                var personRecord = await PersonEngine.GetAsync(personId);
                if (personRecord == null) return false;
                var userToken = RandomGeneration.GenerateRandomToken();
                var tokenizedUrl = $"{actionUrl}/{userToken}";

                if (string.IsNullOrWhiteSpace(userToken)) return false;
                var hashedToken = Pbkdf2.Hash(userToken);
                if (string.IsNullOrWhiteSpace(hashedToken)) return false;

                var createTokenResult =
                    await CredentialResetEngine.CreatePersonSecurityResetAsync(personRecord.PersonId, hashedToken,
                        TokenDuration);
                if (!createTokenResult) return false;

                var emailBody = string.Format(Resources.Account_NewUser_Email_Body, personRecord.PreferredName,
                    tokenizedUrl, TokenDuration);
                Sender.Send(personRecord.PreferredName, personRecord.EmailAddress,
                    Resources.Account_NewUser_Email_Subject, emailBody);
                return true;
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return false;
            }
        }

        public static async Task<IReadOnlyCollection<string>> PasswordMeetsMinimumRequirementsAsync(int personId,
            string password)
        {
            var failureList = new List<string>();
            try
            {
                if (!PasswordSecurityPolicy.AlphaRequiredCount.Equals(0) &&
                    password.Count(char.IsLetter) < PasswordSecurityPolicy.AlphaRequiredCount)
                    failureList.Add(string.Format(Resources.Settings_Password_NewPassword_Validation_AlphaCount_Message,
                        Resources.Settings_Password_NewPassword_DisplayName,
                        PasswordSecurityPolicy.AlphaRequiredCount));

                if (!PasswordSecurityPolicy.LowerCaseCount.Equals(0) &&
                    password.Count(char.IsLower) < PasswordSecurityPolicy.LowerCaseCount)
                    failureList.Add(string.Format(
                        Resources.Settings_Password_NewPassword_Validation_LowerAlphaCount_Message,
                        Resources.Settings_Password_NewPassword_DisplayName,
                        PasswordSecurityPolicy.LowerCaseCount));

                if (!PasswordSecurityPolicy.UpperCaseCount.Equals(0) &&
                    password.Count(char.IsUpper) < PasswordSecurityPolicy.UpperCaseCount)
                    failureList.Add(string.Format(
                        Resources.Settings_Password_NewPassword_Validation_UpperAlphaCount_Message,
                        Resources.Settings_Password_NewPassword_DisplayName,
                        PasswordSecurityPolicy.UpperCaseCount));

                if (!PasswordSecurityPolicy.NumericRequiredCount.Equals(0) &&
                    password.Count(char.IsDigit) < PasswordSecurityPolicy.NumericRequiredCount)
                    failureList.Add(string.Format(
                        Resources.Settings_Password_NewPassword_Validation_NumericCount_Message,
                        Resources.Settings_Password_NewPassword_DisplayName,
                        PasswordSecurityPolicy.NumericRequiredCount));
                var specialCharacterList =
                    PasswordSecurityPolicy.SpecialCharacterList.ToCharArray().ToList().AsReadOnly();
                if (!PasswordSecurityPolicy.SpecialRequiredCount.Equals(0) &&
                    !PasswordSecurityPolicy.SpecialCharacterList.Length.Equals(0) &&
                    password.Count(c => specialCharacterList.Contains(c)) < PasswordSecurityPolicy.SpecialRequiredCount)
                    failureList.Add(string.Format(
                        Resources.Settings_Password_NewPassword_Validation_SpecialCharacterCount_Message,
                        Resources.Settings_Password_NewPassword_DisplayName,
                        PasswordSecurityPolicy.SpecialRequiredCount));

                //Password cannot have more than N repeating characters
                if (!PasswordSecurityPolicy.RepeatedCharacterCount.Equals(0))
                {
                    var regExPattern = $"(.)\\1{{{PasswordSecurityPolicy.RepeatedCharacterCount},}}";
                    var matchList = Regex.Matches(password, regExPattern);
                    if (!matchList.Count.Equals(0))
                        failureList.Add(
                            string.Format(
                                Resources.Settings_Password_NewPassword_Validation_RepeatCharacterCount_Message,
                                Resources.Settings_Password_NewPassword_DisplayName,
                                PasswordSecurityPolicy.RepeatedCharacterCount));
                }

                //Password Minimum Length
                if (!PasswordSecurityPolicy.MinimumLength.Equals(0) &&
                    password.Length < PasswordSecurityPolicy.MinimumLength)
                    failureList.Add(string.Format(Resources.Common_Validation_FieldLengthMinimumError_Message,
                        Resources.Settings_Password_NewPassword_DisplayName,
                        PasswordSecurityPolicy.MinimumLength));
                //Password Maximum Length
                if (!PasswordSecurityPolicy.MaximumLength.Equals(0) &&
                    password.Length > PasswordSecurityPolicy.MaximumLength)
                    failureList.Add(string.Format(Resources.Common_Validation_FieldLengthMaximumError_Message,
                        Resources.Settings_Password_NewPassword_DisplayName,
                        PasswordSecurityPolicy.MaximumLength));

                if (!PasswordSecurityPolicy.ReuseInterval.Equals(0) &&
                    await IsPasswordReused(personId, password, PasswordSecurityPolicy.ReuseInterval))
                    failureList.Add(string.Format(
                        Resources.Settings_Password_NewPassword_Validation_PasswordReuseCount_Message,
                        Resources.Settings_Password_NewPassword_DisplayName,
                        PasswordSecurityPolicy.ReuseInterval));

                return failureList.AsReadOnly();
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return new List<string>();
            }
        }

        public static async Task ResetPassword(string emailAddress, string actionUrl)
        {
            try
            {
                // Get person id
                var personRecord = await PersonEngine.GetAsync(emailAddress);
                if (personRecord == null) return;
                var userToken = RandomGeneration.GenerateRandomToken();
                var tokenizedUrl = $"{actionUrl}/{userToken}";

                if (string.IsNullOrWhiteSpace(userToken)) return;
                var hashedToken = Pbkdf2.Hash(userToken);
                if (string.IsNullOrWhiteSpace(hashedToken)) return;

                var createTokenResult =
                    await CredentialResetEngine.CreatePersonSecurityResetAsync(personRecord.PersonId, hashedToken,
                        TokenDuration);
                if (!createTokenResult) return;
                var emailBody = string.Format(Resources.Account_PasswordReset_Email_Body, personRecord.PreferredName,
                    tokenizedUrl, TokenDuration);
                Sender.Send(personRecord.PreferredName, personRecord.EmailAddress,
                    Resources.Account_PasswordReset_Email_Subject, emailBody);
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
            }
        }

        public static void SetBlockCookie(HttpContext context, BlockedPersonViewModel model)
        {
            try
            {
                var jsonData = JsonConvert.SerializeObject(model);
                context.Response.Cookies.Add(new HttpCookie(BlockCookieName)
                {
                    Name = BlockCookieName,
                    HttpOnly = true,
                    Secure = true,
                    Value = Convert.ToBase64String(AesGcm.SimpleEncrypt(
                        Encoding.UTF8.GetBytes(jsonData), BlockCookieEncryptionKey))
                });
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
            }
        }

        public static void SimulateRequestToken()
        {
            try
            {
                var randomToken = RandomGeneration.GenerateRandomToken();
                Pbkdf2.Hash(randomToken);
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
            }
        }

        public static async Task<bool> UpdatePasswordAsync(int personId, string password)
        {
            try
            {
                var expirePeriod = PasswordSecurityPolicy.ExpirePeriod;
                var passwordHash = Pbkdf2.Hash(password);
                return await PersonSecurityEngine.UpdatePasswordAsync(personId, passwordHash, expirePeriod);
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return false;
            }
        }

        public static void UpdatePasswordExpirationClaim(IAuthenticationManager authenticationManager,
            DateTime passwordExpiration)
        {
            try
            {
                var claimsIdentity = new ClaimsIdentity(authenticationManager.User.Identity);

                claimsIdentity.RemoveClaim(claimsIdentity.FindFirst(CustomClaimTypes.PasswordExpiration));
                claimsIdentity.AddClaim(new Claim(CustomClaimTypes.PasswordExpiration, $"{passwordExpiration:s}"));

                authenticationManager.AuthenticationResponseGrant =
                    new AuthenticationResponseGrant(
                        new ClaimsPrincipal(claimsIdentity),
                        new AuthenticationProperties {IsPersistent = true}
                    );
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
            }
        }

        #endregion

        #region Methods

        private static async Task<bool> IsPasswordReused(int personId, string password, int reuseCount)
        {
            try
            {
                var topNthSecurityResets =
                    await PersonSecurityEngine.GetPasswordSecurityHashListAsync(personId, reuseCount);
                return topNthSecurityResets.Any() && topNthSecurityResets.Any(s => Pbkdf2.Verify(password, s));
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
                return false;
            }
        }

        #endregion
    }
}