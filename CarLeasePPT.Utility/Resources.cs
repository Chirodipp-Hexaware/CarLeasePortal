namespace CarLeasePPT.Utility
{
    // ReSharper disable InconsistentNaming
    public static class Resources
    {
        #region Public Properties

        public static string AccessDenied_AuthenticationError_Message =>
            $"The application is unable to validate your credentials at this time. Please contact your {Application_Name} portal administrator.";

        public static string AccessDenied_AuthenticationError_Title => "Access Denied: Authentication Error";

        public static string AccessDenied_BlockedPerson_Expiration_Message =>
            "This user has been restricted until {0:f} {1} (GMT{0:zzz}).";

        public static string AccessDenied_BlockedPerson_Message =>
            $"Access to this application has been restricted for this user. This is a protective security measure applied when significant unauthorized access attempts occur. If you believe this action to be in error, please contact your  {Application_Name}  portal administrator.";

        public static string AccessDenied_BlockedPerson_Title => "Access Denied: User Restricted";

        public static string Account_InvalidToken_Message =>
            "The authentication token could not be validated. The token may be expired, invalid, or previously redeemed. If you are unable to log in to your account, please request a new token.";

        public static string Account_InvalidToken_Title => "Invalid Authentication Token";

        public static string Account_Login_CredentialsExpiredStatus_Message =>
            "The provided credentials are expired. Please reset your password to regain access to your account. If you believe you have received this message in error, please contact your administrator.";

        public static string Account_Login_CredentialsLockAction_Message =>
            "Your account has been locked due to a series of failed login attempts. Please reset your password to regain access to your account. If you believe you have received this message in error, please contact your administrator.";

        public static string Account_Login_CredentialsLockedStatus_Message =>
            "The provided credentials are locked. Please reset your password to regain access to your account. If you believe you have received this message in error, please contact your administrator.";

        public static string Account_Login_CredentialsPendingLock_Message =>
            "Warning: You have had a series of consecutive, unsuccessful login attempts. An additional failed attempt will lock your account.";

        public static string Account_Login_ForgotSignIn_Link => "Forgot Your Password?";

        public static string Account_Login_InvalidCredentials_Message =>
            "The provided credentials could not be validated.";

        public static string Account_Login_Link => "Sign In";

        public static string Account_Login_Message =>
            "This site is intended for use by Authorized Users only. Any attempt to deny access to, hack into and/or deface this site will result in criminal prosecution under local, state, federal and international law. If you have reached this website in error, please remove yourself by typing the correct URL name of the website intended. We reserve the right to monitor access to/from this website in accordance with Hexaware policies.";

        public static string Account_Login_Password_DisplayName => "Password";
        public static string Account_Login_SignIn_Link => "Sign In";
        public static string Account_Login_Submit_DisplayName => "Sign In";
        public static string Account_Login_Title => "Sign In";

        public static string Account_NewUser_Email_Body =>
            $"Hello {{0}},\r\n\r\nYou have been set up with a user account on {Application_Name}. Before logging in to your account, you will need to set a password. You may do so at {{1}}. This single-use URL will expire in {{2}} minutes but may be re-requested at any time through the password reset function.\r\n\r\nIf you experience difficulties in accessing your account, please contact your Hexaware service team.";

        public static string Account_NewUser_Email_Subject => $"{Application_Name} New User Welcome";


        public static string Account_PasswordReset_Email_Body =>
            "Hello {0},\r\n\r\nYour password reset token is {1}. This single-use token will expire in {2} minutes but may be re-requested at any time. If you did not request the token or believe this to have been sent in error, please contact your Hexaware service team.";

        public static string Account_PasswordReset_Email_Subject => $"{Application_Name} Password Reset Token Request";
        public static string Account_PasswordReset_EmailAddress_DisplayName => "Email Address";

        public static string Account_PasswordReset_RememberedPassword_Message => "Remembered Your Password?";
        public static string Account_PasswordReset_ReturnToSignIn_Link => "Return to Sign In";
        public static string Account_PasswordReset_Submit_DisplayName => "Request Password Reset";

        public static string Account_PasswordReset_Title => "Request Password Reset";
        public static string Account_PasswordReset_Username_DisplayName => "Username";

        public static string Account_RedeemToken_CodeGenerationError_Message =>
            "A verification code could not be generated.";

        public static string Account_RedeemToken_Message =>
            "As an added security measure, a password reset requires a secondary authentication factor. Please select the secondary authentication method below.";

        public static string Account_RedeemToken_Submit_DisplayName => "Verify Me";
        public static string Account_RedeemToken_Title => "Redeem Token";

        public static string Account_SessionTerminated_Message =>
            $"Your {Application_Name} portal session was terminated due to a new login from another device or browser.";

        public static string Account_SessionTerminated_Title => "Session Terminated";

        public static string Account_SetPassword_Message => "Please enter and confirm your new password.";
        public static string Account_SetPassword_PasswordSecurityPolicy_Heading => "Password Security Policy";
        public static string Account_SetPassword_Submit_DisplayName => "Change Password";

        public static string Account_SetPassword_Title => "Change Password";
        public static string Account_SetPasswordSuccess_Message => "You have successfully changed your password.";

        public static string Account_SetPasswordSuccess_Title => "Password Changed Successfully";

        public static string Account_TokenRequest_Email_Body =>
            "Hello {0},\r\n\r\nYour authentication token is {1}. This single-use token will expire in {2} minutes but may be re-requested at any time. If you did not request the token or believe this to have been sent in error, please contact your Hexaware service team.";

        public static string Account_TokenRequest_Email_Subject => $"{Application_Name} Authentication Token Request";

        public static string Account_TokenRequested_Message =>
            "If it is determined that you are a valid user of this system, you will receive an email shortly with appropriate instructions.";

        public static string Account_TokenRequested_ResetTokenRequested_Title => "Password Reset Token Requested";

        public static string Account_TokenRequested_Response_Message =>
            $"If it is determined that you are a valid user of this system, you will receive an email shortly with appropriate instructions. If you do not receive an email or continue to experience problems logging in, please contact your {Application_Name} portal administrator for assistance.";

        public static string Account_TokenRequested_Title => "Password Reset Token Requested";

        public static string Account_TokenRequested_WhiteList_Message =>
            "Note: Occasionally, the email may be interpreted as unsolicited email. If you have not received the email after a short period of time, please check your spam folder.";

        public static string AuditLog_BreadCrumb => "Audit Log";
        public static string AuditLog_Detail_Action_DisplayName => "MVC Action";
        public static string AuditLog_Detail_Action_Heading => "Action Data";
        public static string AuditLog_Detail_BreadCrumb => Common_Detail_BreadCrumb;
        public static string AuditLog_Detail_Controller_DisplayName => "MVC Controller";
        public static string AuditLog_Detail_Logged_DisplayName => "Logged";
        public static string AuditLog_Detail_LogLevel_DisplayName => "Level";
        public static string AuditLog_Detail_MarkReviewed_Link => "Mark Reviewed";
        public static string AuditLog_Detail_Message_DisplayName => "User Action";
        public static string AuditLog_Detail_PersonFullName_DisplayNane => "Full Name";
        public static string AuditLog_Detail_RecordId_DisplayName => "Record Identifier";
        public static string AuditLog_Detail_RemoteAddress_DisplayName => "Remote Address";
        public static string AuditLog_Detail_Review_Heading => "Reviewer";
        public static string AuditLog_Detail_ReviewedByPersonName_DisplayName => "Reviewed By";
        public static string AuditLog_Detail_ReviewedOnDateTime_DisplayName => "Marked Reviewed On";
        public static string AuditLog_Detail_Title => "Audit Log Event Detail";
        public static string AuditLog_Detail_Url_DisplayName => "URL";
        public static string AuditLog_Detail_User_Heading => "User Data";
        public static string AuditLog_Index_Action_TableHeader => "Action";
        public static string AuditLog_Index_Controller_TableHeader => "Controller";
        public static string AuditLog_Index_IsReviewed_TableHeader => "Reviewed";
        public static string AuditLog_Index_Logged_TableHeader => "Logged";
        public static string AuditLog_Index_LogLevel_TableHeader => "Level";
        public static string AuditLog_Index_MarkAllReviewed_Link => "Mark All Reviewed";
        public static string AuditLog_Index_Message_TableHeader => "Event Type";
        public static string AuditLog_Index_PersonFullName_TableHeader => "Person";
        public static string AuditLog_Index_ShowInfo_DisplayName => "Show Info Events";
        public static string AuditLog_Index_ShowReviewed_DisplayName => "Show Reviewed";
        public static string AuditLog_Index_Title => "Audit Log";

        public static string AuditLog_MarkAllReviewedFailed_Message =>
            "The audit log could not be updated and records were not marked as reviewed.";

        public static string AuditLog_MarkAllReviewedFailed_Title => "Audit Log: Mark All Reviewed Failed";
        public static string AuditLog_MarkReviewedFailed_BreadCrumb => "Mark Reviewed Failed";

        public static string AuditLog_MarkReviewedFailed_Message =>
            "The audit log could not be updated and the record was not marked as reviewed.";

        public static string AuditLog_MarkReviewedFailed_Title => "Audit Log: Mark Reviewed Failed";
        public static string AuditLog_RunReport_BreadCrumb => "Run Report";
        public static string AuditLog_RunReport_EndDate_DisplayName => "End Date";

        public static string AuditLog_RunReport_EndDate_Validation_Message =>
            $"{AuditLog_RunReport_EndDate_DisplayName} must be after {AuditLog_RunReport_StartDate_DisplayName}";

        public static string AuditLog_RunReport_IncludeInfo_DisplayName => "Include Information Events";
        public static string AuditLog_RunReport_IncludeReviewed_DisplayName => "Include Reviewed Events";
        public static string AuditLog_RunReport_NoRecords_Message => "No records matched the reporting criteria.";
        public static string AuditLog_RunReport_StartDate_DisplayName => "Start Date";

        public static string AuditLog_RunReport_StartDate_Validation_Message =>
            $"{AuditLog_RunReport_StartDate_DisplayName} must be before {AuditLog_RunReport_EndDate_DisplayName}";

        public static string AuditLog_RunReport_Submit_DipslayName => "Run Report";
        public static string AuditLog_RunReport_Title => "Run Report";
        public static string Common_Actions_TableHeader => "Actions";
        public static string Common_Application_Title => $"{Application_Name} Portal";
        public static string Common_Attachments_Heading => "Attachments";
        public static string Common_AuditLog_Link => "Audit Log";
        public static string Common_CancelButton_DisplayName => "Cancel";
        public static string Common_Copyright_Message => "© {0} Hexaware. All rights reserved.";
        public static string Common_Create_BreadCrumb => "Create";
        public static string Common_Dashboard_Link => "Dashboard";
        public static string Common_Delete_BreadCrumb => "Delete";
        public static string Common_Delete_Link => "Delete";
        public static string Common_Detail_BreadCrumb => "Detail";
        public static string Common_Edit_BreadCrumb => "Edit";
        public static string Common_Edit_Link => "Edit";
        public static string Common_EmailAddress_DisplayName => "Email Address";
        public static string Common_Lease_Link => "Leases";
        public static string Common_LeaseNumber_DisplayName => "Lease Number";
        public static string Common_No_Message => "No";
        public static string Common_No_Records_Message => "No records matched your selection.";
        public static string Common_NoAssignedRoles_Message => "No Assigned Roles";

        public static string Common_PasswordInvalid_Message =>
            "The password does not match your password on file. Please try again.";

        public static string Common_Reporting_Link => "Reporting";
        public static string Common_ReturnTo_Link => "Return to {0}";
        public static string Common_Settings_Menu => "Settings";
        public static string Common_SignOut_Link => "Sign Out";
        public static string Common_TaxBills_Link => "Tax Bills";
        public static string Common_TaxAssessor_Link => "Tax Assessors";
        public static string Common_TaxCollector_Link => "Tax Collectors";
        public static string Common_User_Link => "Manage Users";

        public static string Common_Validation_FieldLengthMaximumError_Message =>
            "{0} must be no more than {1:N0} character(s)";

        public static string Common_Validation_FieldLengthMinimumError_Message =>
            "{0} must be no less than {1:N0} character(s)";

        public static string Common_Validation_Required_Message => "{0} is required.";
        public static string Common_View_Link => "View";
        public static string Common_Vin_DisplayName => "VIN";
        public static string Common_WorkItem_Create_Link => "Create Work Item";
        public static string Common_WorkItem_Link => "Work Items";
        public static string Common_Yes_Message => "Yes";

        public static string Error_FiveHundred_Lead =>
            "Uh oh. The action couldn't be completed due to an application error.";

        public static string Error_FiveHundred_Message =>
            $"If you continue to experience difficulties, please contact your {Application_Name} portal client administrator.";

        public static string Error_FiveHundred_Title => "500: An Application Error Occurred";
        public static string Error_FourOhFour_Lead => "Uh oh. The page you attempted to reach doesn't exist.";

        public static string Error_FourOhFour_Message =>
            $"If you believe this to be in error, please contact your {Application_Name} portal client administrator.";

        public static string Error_FourOhFour_Title => "404: Page Not Found";

        public static string Helper_ValidatePasswordChange_BlockedPassword_Message =>
            "The password you selected is one of the 10,000 most common passwords. Please use a more secure password or pass phrase.";

        public static string Helper_ValidatePasswordReset_RequiredValidation_Message =>
            "Username or email address is required.";

        public static string Home_Index_BillNumber_DisplayName => "Bill Number";
        public static string Home_Index_LeaseNumber_DisplayName => "Lease Number";
        public static string Home_Index_LeaseNumber_TableHeader => "Lease #";
        public static string Home_Index_LesseeFirstName_DisplayName => "Lessee First Name";
        public static string Home_Index_LesseeFullName_TableHeader => "Lessee";
        public static string Home_Index_LesseeLastName_DisplayName => "Lessee Last Name";
        public static string Home_Index_ModelYear_TableHeader => "Year";

        public static string Home_Index_PasswordExpiresToday_Message =>
            "Your password expires in {0} hour{1}. Please change your password immediately.";

        public static string Home_Index_PasswordExpiresWarning_Message =>
            "Your password expires in {0} day{1} and {2} hour{3}. Please consider changing your password soon.";

        public static string Home_Index_PlateNumber_DisplayName => "Plate";
        public static string Home_Index_TaxAccountNumber_DisplayName => "Tax Account";
        public static string Home_Index_Title => $"{Common_Application_Title} Dashboard";

        public static string Home_Index_UnworkedWorkItem_Message => "There {0} {1:N0} unworked work item{2}";
        public static string Home_Index_CarMake_TableHeader => "Make";
        public static string Home_Index_CarModel_TableHeader => "Model";
        public static string Home_Index_Vin_DisplayName => "Full or Partial VIN";
        public static string Home_Index_Vin_TableHeader => "VIN";
        public static string Lease_Bill_BreadCrumb => "Tax Bill";
        public static string Lease_BreadCrumb => "Car Leases";
        public static string Lease_Common_BackToList_Link => "Back to List";
        public static string Lease_Common_Lease_Heading => "Lessee Information";

        public static string Lease_Common_LeaseDates_Heading => "Lease Dates";
        public static string Lease_Common_Car_Heading => "Car Data";
        public static string Lease_Detail_AcccountNumber_TableHeader => "Account #";
        public static string Lease_Detail_AccountHistory_Heading => "Account History";
        public static string Lease_Detail_AccountHistory_NoHistory_Message => "No Account History Found";
        public static string Lease_Detail_ActualTerminationDate_DisplayName => "Actual Termination Date";
        public static string Lease_Detail_Address_TableHeader => "Address";
        public static string Lease_Detail_AssetNumber_DisplayName => "Asset Number";
        public static string Lease_Detail_AssetStatusName_DisplayName => "Asset Status";
        public static string Lease_Detail_BillNumber_TableHeader => "Bill #";
        public static string Lease_Detail_BranchCode_DisplayName => "Branch Code";
        public static string Lease_Detail_BreadCrumb => Common_Detail_BreadCrumb;
        public static string Lease_Detail_CityStateZip_TableHeader => "City/State/Zip";
        public static string Lease_Detail_CommencementDate_DisplayName => "Commencement Date";
        public static string Lease_Detail_CompanyCode_DisplayName => "Company Code";
        public static string Lease_Detail_County_TableHeader => "County";
        public static string Lease_Detail_CreatedOn_TableHeader => "Uploaded On";
        public static string Lease_Detail_CustomerAddress_DisplayName => "Customer Address";
        public static string Lease_Detail_CustomerAddressHistory_Heading => "Customer Address History";


        public static string Lease_Detail_DocumentName_TableHeader => "Document Name";
        public static string Lease_Detail_DocumentType_TableHeader => "Document Type";
        public static string Lease_Detail_DueDate_TableHeader => "Due Date";
        public static string Lease_Detail_ExemptionCode_DisplayName => "Exemption Code";
        public static string Lease_Detail_LeaseStatusName_DisplayName => "Lease Status";
        public static string Lease_Detail_LesseeFullName_DisplayName => "Lessee Name";
        public static string Lease_Detail_ModelYear_DisplayName => "Year";
        public static string Lease_Detail_OriginalCost_DisplayName => "Original Cost";
        public static string Lease_Detail_PaidDate_TableHeader => "Paid Date";
        public static string Lease_Detail_PayoffSource_DisplayName => "Payoff Source";
        public static string Lease_Detail_Period_TableHeader => "Period";
        public static string Lease_Detail_PhysicalAddress_DisplayName => "Physical Address";
        public static string Lease_Detail_PhysicalAddressHistory_Heading => "Physical Address History";
        public static string Lease_Detail_PlateNumber_DisplayName => "Plate Number";
        public static string Lease_Detail_ProductUseCode_DisplayName => "Product Use Code";
        public static string Lease_Detail_RegistrationState_DisplayName => "Registration State";
        public static string Lease_Detail_SaleDate_DisplayName => "Sale Date";
        public static string Lease_Detail_SalesPrice_DisplayName => "Sales Price";
        public static string Lease_Detail_ScheduledTerminationDate_DisplayName => "Scheduled Termination Date";
        public static string Lease_Detail_SourceSystem_DisplayName => "Source System";
        public static string Lease_Detail_TaxCollector_TableHeader => "Tax Collector";
        public static string Lease_Detail_TaxRecoveryCode_DisplayName => "Tax Recovery Code";
        public static string Lease_Detail_TaxYear_TableHeader => "Year";
        public static string Lease_Detail_TerminationCode_DisplayName => "Termination Code";
        public static string Lease_Detail_Title => "Lease Detail";
        public static string Lease_Detail_TotalAmount_TableHeader => "Total Amount";
        public static string Lease_Detail_CarMake_DisplayName => "Make";
        public static string Lease_Detail_CarModel_DisplayName => "Model";
        public static string Lease_Detail_YearMakeModel_DisplayName => "Year/Make/Model";
        public static string Lease_Index_Title => "Car Leases";
        public static string Settings_Account_BreadCrumb => "Account Details";
        public static string Settings_MyAccount_AccountInformation_Title => "Account Information";
        public static string Settings_MyAccount_EmailAddress_DisplayName => "Email Address";
        public static string Settings_MyAccount_Link => "Account";
        public static string Settings_MyAccount_Roles_Title => "Account Roles";
        public static string Settings_MyAccount_Title => "Account Details";
        public static string Settings_MyAccount_Username_DisplayName => "Username";
        public static string Settings_MyProfile_FullName_DisplayName => "Full Name";
        public static string Settings_MyProfile_Link => "Profile";
        public static string Settings_MyProfile_PreferredName_DisplayName => "Preferred Name";
        public static string Settings_MyProfile_Profile_Heading => "Profile";
        public static string Settings_MyProfile_Title => "Account Profile";

        public static string Settings_MyProfile_UnableToUpdateError_Message =>
            "Unable to update your profile at this time.";

        public static string Settings_MyProfile_UpdateProfileButton_Title => "Update Profile";
        public static string Settings_Password_ChangePasswordButton_Title => "Change Password";
        public static string Settings_Password_ConfirmPassword_DisplayName => "New Password Confirmation";

        public static string Settings_Password_CurrentPassword_DisplayName => "Current Password";
        public static string Settings_Password_EffectiveDateTime_DisplayName => "Last Changed";
        public static string Settings_Password_ExpirationDateTime_DisplayName => "Will Expire";
        public static string Settings_Password_Link => "Password";
        public static string Settings_Password_NewPassword_DisplayName => "New Password";

        public static string Settings_Password_NewPassword_MatchValidation_Message =>
            "New Password and Confirmation of New Password must match.";

        public static string Settings_Password_NewPassword_Validation_AlphaCount_Message =>
            "{0} must contain at least {1:N0} alpha (letter) character(s).";

        public static string Settings_Password_NewPassword_Validation_LowerAlphaCount_Message =>
            "{0} must contain at least {1:N0} lowercase alpha (letter) character(s).";

        public static string Settings_Password_NewPassword_Validation_NumericCount_Message =>
            "{0} must contain at least {1:N0} numeric character(s).";

        public static string Settings_Password_NewPassword_Validation_PasswordReuseCount_Message =>
            "{0} matches one of the last {1:N0} passwords used.";

        public static string Settings_Password_NewPassword_Validation_RepeatCharacterCount_Message =>
            "{0} repeats a single character consecutively more than {1:N0} time(s).";

        public static string Settings_Password_NewPassword_Validation_SpecialCharacterCount_Message =>
            "{0} must contain at least {1:N0} special character(s).";

        public static string Settings_Password_NewPassword_Validation_UpperAlphaCount_Message =>
            "{0} must contain at least {1:N0} uppercase alpha (letter) character(s).";

        public static string Settings_Password_PasswordChangedSuccessfully_Message =>
            "You successfully changed your password.";

        public static string Settings_Password_PasswordCouldNotBeChanged_Message =>
            "Your password could not be changed.";

        public static string Settings_Password_PasswordInformation_Title => "Password Information";

        public static string Settings_Password_PasswordPolicy_AlphaCount_Message =>
            "Must contain at least {0:N0} alpha (letter) character(s)";

        public static string Settings_Password_PasswordPolicy_DoesNotExpire_Message => "Password does not expire.";

        public static string Settings_Password_PasswordPolicy_ExpirePeriod_Message =>
            "Must be changed at least every {0:N0} days.";

        public static string Settings_Password_PasswordPolicy_LengthMaximum_Message =>
            "Must be no more than {0:N0} character(s).";

        public static string Settings_Password_PasswordPolicy_LengthMinimum_Message =>
            "Must be at least {0:N0} character(s).";

        public static string Settings_Password_PasswordPolicy_LowerAlphaCount_Message =>
            "Must contain at least {0:N0} lowercase alpha (letter) character(s).";

        public static string Settings_Password_PasswordPolicy_NumericCount_Message =>
            "Must contain at least {0:N0} numeric character(s).";

        public static string Settings_Password_PasswordPolicy_PasswordReuseCount_Message =>
            "Must not be one of the last {0:N0} passwords used.";

        public static string Settings_Password_PasswordPolicy_RepeatCharacterCount_Message =>
            "May not repeat a character consecutively more than {0:N0} times.";

        public static string Settings_Password_PasswordPolicy_SpecialCharacterCount_Message =>
            "Must contain at least {0:N0} special character(s) from the following list: {1}.";

        public static string Settings_Password_PasswordPolicy_UpperAlphaCount_Message =>
            "Must contain at least {0:N0} uppercase alpha (letter) character(s).";

        public static string Settings_Password_PasswordSecurityPolicy_Heading => "Password Security Policy";
        public static string Settings_Password_Title => "Your Password";
        public static string TaxBill_Allocation_Amount_TableHeader => "Amount";
        public static string TaxBill_Allocation_Assessment_Heading => "Assessment Detail";
        public static string TaxBill_Allocation_BreadCrumb => "Allocation";
        public static string TaxBill_Allocation_Breakout_Heading => "Tax Bill Breakout";
        public static string TaxBill_Allocation_Component_TableHeader => "Component";

        public static string TaxBill_Allocation_Title => "Tax Bill Car Allocation";
        public static string TaxBill_Allocation_ViewLeaseDetail_Link => "View Lease Detail";
        public static string TaxBill_Allocation_ViewTaxBillDetail_Link => "View Tax Bill Detail";

        public static string TaxBill_AssetSummary_AssetSummary_Heading => "Asset Summary";
        public static string TaxBill_AssetSummary_DateComplete_DisplayName => "Date Complete";
        public static string TaxBill_AssetSummary_Link => "View Lease Asset Summary";
        public static string TaxBill_AssetSummary_TaxBillSummary_Heading => "Tax Bill Summary";

        public static string TaxBill_AssetSummary_Title => "Asset Summary";
        public static string TaxBill_AssetSummary_CarDescription_DisplayName => "Car Description";


        public static string TaxBill_BreadCrumb => "Tax Bill";
        public static string TaxBill_Common_ApExportDate_DisplayName => "A/P Export Date";
        public static string TaxBill_Common_AssessedValue_DisplayName => "Assessed Value";
        public static string TaxBill_Common_Assessor_DisplayName => "Tax Assessor";
        public static string TaxBill_Common_BaseTax_DisplayName => "Base Tax";
        public static string TaxBill_Common_BillNumber_DisplayName => "Bill Number";
        public static string TaxBill_Common_CollectorAccountNumber_DisplayName => "Collector Account";
        public static string TaxBill_Common_Company_DisplayName => "Company";
        public static string TaxBill_Common_CompletedDate_DisplayName => "Completed Date";
        public static string TaxBill_Common_DecalRegistrationFee_DisplayName => "Decal";
        public static string TaxBill_Common_Discount_DisplayName => "Discount";
        public static string TaxBill_Common_DiscountDueDate_DisplayName => "Discount Due Date";
        public static string TaxBill_Common_DueDate_DisplayName => "Due Date";
        public static string TaxBill_Common_Interest_DisplayName => "Interest";
        public static string TaxBill_Common_InvoiceDate_DisplayName => "Invoice Date";
        public static string TaxBill_Common_PaidDate_DisplayName => "Paid Date";
        public static string TaxBill_Common_Penalty_DisplayName => "Penalty";
        public static string TaxBill_Common_PptraCredit_DisplayName => "PPTRA Credit";
        public static string TaxBill_Common_ReceivedDate_DisplayName => "Received Date";
        public static string TaxBill_Common_TaxCollector_DisplayName => "Tax Collector";
        public static string TaxBill_Common_TaxPeriod_DisplayName => "Tax Period";
        public static string TaxBill_Common_TaxYear_DisplayName => "Tax Year";
        public static string TaxBill_Common_TotalAmountOwed_DisplayName => "Total";
        public static string TaxBill_Detail_APExportDate_DisplayName => "AP Export Date";
        public static string TaxBill_Detail_Assessment_TableHeader => "Assessment";
        public static string TaxBill_Detail_AssessorState_DisplayName => "Tax Assessor State";
        public static string TaxBill_Detail_BreadCrumb => Common_Detail_BreadCrumb;
        public static string TaxBill_Detail_CreatedOn_TableHeader => "Uploaded On";
        public static string TaxBill_Detail_Decal_TableHeader => "Decal";
        public static string TaxBill_Detail_Discount_TableHeader => "Discount";
        public static string TaxBill_Detail_DiscountDueDate_DisplayName => "Discount Due Date";
        public static string TaxBill_Detail_DocumentName_TableHeader => "Document Name";
        public static string TaxBill_Detail_DocumentType_TableHeader => "Document Type";
        public static string TaxBill_Detail_DueDate_DisplayName => "Due Date";
        public static string TaxBill_Detail_Interest_TableHeader => "Interest";
        public static string TaxBill_Detail_InvoiceDate_DisplayName => "Invoice Date";
        public static string TaxBill_Detail_LeaseAllocation_Heading => "Lease Allocation";
        public static string TaxBill_Detail_MilestoneDates_Heading => "Milestone Dates";
        public static string TaxBill_Detail_MonthsTaxed_DisplayName => "Months Taxed";
        public static string TaxBill_Detail_Penalty_TableHeader => "Penalty";
        public static string TaxBill_Detail_PptraCredit_TableHeader => "PPTRA Cr";
        public static string TaxBill_Detail_ReceivedDate_DisplayName => "Received Date";
        public static string TaxBill_Detail_Tax_TableHeader => "Tax";
        public static string TaxBill_Detail_TaxAuthorities_Heading => "Tax Authorities";
        public static string TaxBill_Detail_TaxPeriodEnd_DisplayName => "Tax Period End";
        public static string TaxBill_Detail_TaxPeriodStart_DisplayName => "Tax Period Start";
        public static string TaxBill_Detail_Title => "Tax Bill Detail";
        public static string TaxBill_Detail_Total_TableHeader => "Total";
        public static string TaxBill_Detail_TotalAmountOwed_DisplayName => "Total Amount Owed";
        public static string TaxBill_Detail_TotalTaxableValue_DisplayName => "Total Taxable Value";
        public static string TaxBill_Detail_Vin_TableHeader => "VIN";
        public static string TaxBill_Index_BillNumber_DisplayName => "Bill Number";
        public static string TaxBill_Index_BillNumber_TableHeader => "Bill #";
        public static string TaxBill_Index_CollectorAccountNumber_TableHeader => "Account #";
        public static string TaxBill_Index_DateComplete_TableHeader => "Date Complete";
        public static string TaxBill_Index_DueDate_TableHeader => "Due Date";

        public static string TaxBill_Index_IsComplete_TableHeader => "Complete?";
        public static string TaxBill_Index_LeaseNumber_DisplayName => "Lease Number";
        public static string TaxBill_Index_TaxAccountNumber_DisplayName => "Tax Account Number";
        public static string TaxBill_Index_TaxAssessorState_TableHeader => "State";
        public static string TaxBill_Index_TaxCollectorName_TableHeader => "Collector";
        public static string TaxBill_Index_TaxYear_TableHeader => "Tax Year";

        public static string TaxBill_Index_Title => "Tax Bills";
        public static string TaxBill_Index_TotalAmountOwed_TableHeader => "Bill Amount";
        public static string TaxBill_Index_Vin_DisplayName => "Full or Partial VIN";
        public static string User_BreadCrumb => "Users";
        public static string User_Common_AssignedRoles_Title => "Assigned Roles";
        public static string User_Common_AvailableRoles_Title => "Available Roles";
        public static string User_Common_BackToList_Link => "Back to List";
        public static string User_Common_Details_Heading => "User Details";
        public static string User_Common_EffectiveDate => "Effective Date";
        public static string User_Common_EmailAddress_DisplayName => "Email Address";
        public static string User_Common_EmailExists_Message => "A user with this email address already exists.";
        public static string User_Common_ExpirationDate => "Expiration Date";
        public static string User_Common_ExpirationDate_DisplayName => "Expiration Date";
        public static string User_Common_FullName_DisplayName => "Full Name";
        public static string User_Common_PreferredName_DisplayName => "Preferred Name";
        public static string User_Common_Roles_Heading => "User Roles";
        public static string User_Common_RolesAssigned_DisplayName => "Assigned";
        public static string User_Common_RolesAvailable_DisplayName => "Available";
        public static string User_Common_Status_Active_Message => "Active";
        public static string User_Common_Status_DisplayName => "Status";
        public static string User_Common_Status_Expired_Message => "Expired";
        public static string User_Common_Status_Heading => "User is {0}";
        public static string User_Common_Status_Locked_Message => "Locked";
        public static string User_Create_BreadCrumb => Common_Create_BreadCrumb;

        public static string User_Create_PasswordResetFailure_Message =>
            "The user was created but a password reset could not be generated.";

        public static string User_Create_RoleSaveFailure_Message => "The user's roles could not be added.";
        public static string User_Create_SaveFailure_Message => "The user could not be created.";
        public static string User_Create_Submit_DisplayName => "Create User";
        public static string User_Create_Title => "Create User";
        public static string User_Delete_BreadCrumb => Common_Delete_BreadCrumb;
        public static string User_Delete_Cancel_DisplayName => "No, do not delete {0}";

        public static string User_Delete_Message =>
            $"<strong>Warning!</strong> You are about to permanently delete {{0}} ({{1}}) from the {Application_Name} portal. Are you sure?";

        public static string User_Delete_Submit_DisplayName => "Yes, permanently delete {0}";
        public static string User_Delete_Title => "Delete User";
        public static string User_Detail_BreadCrumb => Common_Detail_BreadCrumb;
        public static string User_Detail_Title => "User Details";
        public static string User_Edit_BreadCrumb => Common_Edit_BreadCrumb;
        public static string User_Edit_RolesNotAdded_Message => "The user's roles could not be added.";
        public static string User_Edit_RolesNotRemoved_Message => "The user's roles could not be removed.";
        public static string User_Edit_SaveFailure_Message => "The user could not be updated.";
        public static string User_Edit_Submit_DisplayName => "Save Changes";
        public static string User_Edit_Title => "Edit User";
        public static string User_Index_Create_Link => "Create User";
        public static string User_Index_ShowExpired_DisplayName => "Show Expired";
        public static string User_Index_Title => "User List";
        public static string User_Unlock_Link => "Unlock Account";
        public static string Car_BreadCrumb => "Cars";
        public static string Car_Index_ModelYear_TableHeader => "Year";
        public static string Car_Index_Title => "Car Management";
        public static string Car_Index_CarMake_TableHeader => "Make";
        public static string Car_Index_CarModel_TableHeader => "Model";
        public static string Car_Index_VIN_TableHeader => "VIN";
        public static string WorkItem_AddAttachment_BreadCrumb => "Add Attachment";
        public static string WorkItem_AddAttachment_Heading => "Upload Work Item Attachment(s)";

        public static string WorkItem_AddAttachment_Message =>
            "Select and add one or more PDF attachments for this work item. If you do not have any attachments to upload, you may navigate away from this page.";

        public static string WorkItem_AddAttachment_Submit_DisplayName => "Upload Attachment(s)";
        public static string WorkItem_AddAttachment_Title => "Add Work Item Attachments";
        public static string WorkItem_AssignToClient_BreadCrumb => "Assign To Client Failed";
        public static string WorkItem_AssignToClient_Detail_Link => "Return to Work Item";
        public static string WorkItem_AssignToClient_Message => "The work item could not be assigned to the client.";

        public static string WorkItem_AssignToClient_Title => "Assign to Client Failed";

        public static string WorkItem_BreadCrumb => "Work Items";

        public static string WorkItem_Common_AddAttachment_SaveError_Message => "The file '{0}' could not be saved.";
        public static string WorkItem_Common_AddAttachments_DisplayName => "Add Attachments";
        public static string WorkItem_Common_BackToList_Link => "Back to List";
        public static string WorkItem_Common_CompanyName_DisplayName => "Company Name";
        public static string WorkItem_Common_DueDate_DisplayName => "Due Date";
        public static string WorkItem_Common_GarageAddress_DisplayName => "Garage Address";
        public static string WorkItem_Common_GarageCity_DisplayName => "Garage City";
        public static string WorkItem_Common_GarageCounty_DisplayName => "Garage County";
        public static string WorkItem_Common_GarageState_DisplayName => "Garage State";
        public static string WorkItem_Common_IsPriviate_True => "Private Activity";

        public static string WorkItem_Common_IsUrgent_False => "Normal";
        public static string WorkItem_Common_IsUrgent_True => "High";
        public static string WorkItem_Common_LeaseLessee_Heading => "Lease/Lessee Information";

        public static string WorkItem_Common_LeaseNumber_DisplayName => "Lease Number";
        public static string WorkItem_Common_LesseeFirstName_DisplayName => "Lessee First Name";
        public static string WorkItem_Common_LesseeLastName_DisplayName => "Lessee Last Name";
        public static string WorkItem_Common_Narrative_DisplayName => "Work Item Narrative";

        public static string WorkItem_Common_Narrative_Heading => "Work Item Narrative";
        public static string WorkItem_Common_NoticeDate_DisplayName => "Notice Date";
        public static string WorkItem_Common_NoticeDetails_Heading => "Notice Details";
        public static string WorkItem_Common_NoticeNumber_DisplayName => "Reference Number";
        public static string WorkItem_Common_PlateNumber_DisplayName => "Plate Number";
        public static string WorkItem_Common_PriorityLevel_Message => "{0} Priority {1}";
        public static string WorkItem_Common_TaxAuthority_Heading => "Tax Authority Information";

        public static string WorkItem_Common_TaxBill_DisplayName => "Tax Bill";
        public static string WorkItem_Common_TaxCollectorName_DisplayName => "Tax Collector";
        public static string WorkItem_Common_TaxYear_DisplayName => "Tax Year";
        public static string WorkItem_Common_UploadedFiles_DisplayName => "Choose File(s)";

        public static string WorkItem_Common_UploadFiles_Validation_PdfsOnly_Message =>
            "Please upload one or more PDF files";

        public static string WorkItem_Common_Car_Heading => "Car Information";
        public static string WorkItem_Common_CarMake_DisplayName => "Car Make";

        public static string WorkItem_Common_Vin_Displayname => "VIN";
        public static string WorkItem_Create_BreadCrumb => Common_Create_BreadCrumb;
        public static string WorkItem_Create_FromLease_Message => "This work item is being created from lease '{0}'.";

        public static string WorkItem_Create_FromTaxBill_Message =>
            "This work item is being created from tax bill '{0}'.";

        public static string WorkItem_Create_FromTaxBillCarLease_Message =>
            "This work item is being created from tax bill '{0}' in reference to lease '{1}'.";

        public static string WorkItem_Create_IsUrgent_DisplayName => "High Priority";
        public static string WorkItem_Create_SaveError_Message => "The work item could not be created.";

        public static string WorkItem_Create_Submit_DisplayName => "Create Work Item";
        public static string Workitem_Create_Title => "Create Work Item";
        public static string WorkItem_Create_WorkItemTypeId_DisplayName => "Work Item Type";
        public static string WorkItem_Detail_Activities_Heading => "Work Item Activities";
        public static string WorkItem_Detail_AssignToClient_Link => "Assign to Client";
        public static string WorkItem_Detail_BreadCrumb => Common_Detail_BreadCrumb;
        public static string WorkItem_Detail_CreateActivity_Link => "Add Activity";
        public static string WorkItem_Detail_Information_Heading => "Work Item Information";
        public static string WorkItem_Detail_CreatedByPersonFullName_DisplayName => "Created By";
        public static string WorkItem_Detail_CreatedOnDateTime_DisplayName => "Created On";
        public static string WorkItem_Detail_IsUrgent_DisplayName => "Priority Level";
        public static string WorkItem_Detail_ModifiedByPersonFullName_DisplayName => "Modified By";
        public static string WorkItem_Detail_ModifiedOnDateTime_DisplayName => "Modified On";
        public static string WorkItem_Detail_ResolveWorkItem_Link => "Resolve Work Item";
        public static string WorkItem_Detail_Title => "Work Item Detail";

        public static string WorkItem_Detail_CarLessee_Heading => "Car & Lessee";
        public static string WorkItem_Detail_WorkItemType_DisplayName => "Work Item Type";
        public static string WorkItem_Edit_BreadCrumb => "Edit Work Item";
        public static string WorkItem_Edit_FromLease_Message => "This work item was created from lease '{0}'.";

        public static string WorkItem_Edit_FromTaxBill_Message =>
            "This work item was created from tax bill '{0}'.";

        public static string WorkItem_Edit_FromTaxBillCarLease_Message =>
            "This work item was created from tax bill '{0}' in reference to lease '{1}'.";

        public static string WorkItem_Edit_Submit_DisplayName => "Update Work Item";
        public static string WorkItem_Index_Basics_Heading => "Work Item";
        public static string WorkItem_Index_Garage_Heading => "Garage Location";
        public static string WorkItem_Index_NoticeNumber_Default_Value => "Not Specified";
        public static string WorkItem_Index_PriorityLevel_AllTypes_Value => "All Priorities";
        public static string WorkItem_Index_PriorityLevel_DisplayName => "Priority Level";
        public static string WorkItem_Index_PriorityLevel_Normal_Value => "Normal Work Items";
        public static string WorkItem_Index_PriorityLevel_Priority_Value => "Priority Work Items";
        public static string WorkItem_Index_Submit_DisplayName => "Create Work Item";
        public static string WorkItem_Index_Title => "Work Items";

        public static string WorkItem_Index_WorkItemStatusId_AllStatuses_Value => "All Active Statuses";
        public static string WorkItem_Index_WorkItemStatusId_DisplayName => "Work Item Status";
        public static string WorkItem_Index_WorkItemTypeId_AllTypes_Value => "All Types";
        public static string WorkItem_Index_WorkItemTypeId_DisplayName => "Work Item Type";
        public static string WorkItemActivity_AddAttachment_BreadCrumb => "Add Attachment";
        public static string WorkItemActivity_AddAttachment_Heading => "Upload Work Item Activity Attachment(s)";

        public static string WorkItemActivity_AddAttachment_Message =>
            "Select and add one or more PDF attachments for this work item activity. If you do not have any attachments to upload, you may navigate away from this page";

        public static string WorkItemActivity_AddAttachment_Submit_DisplayName => "Upload Attachment(s)";
        public static string WorkItemActivity_AddAttachment_Title => "Add Work Item Activity Attachments";
        public static string WorkItemActivity_Common_ActivityDate_DisplayName => "Activity Date";
        public static string WorkItemActivity_Common_ActivityTime_DisplayName => "Activity Time";
        public static string WorkItemActivity_Common_IsPrivate_DisplayName => "Private Activity";
        public static string WorkItemActivity_Common_Narrative_DisplayName => "Activity Narrative";
        public static string WorkItemActivity_Common_Narrative_Heading => "Activity Narrative";

        public static string WorkItemActivity_Common_WorkItemActivity_Heading => "Work Item Activity Details";
        public static string WorkItemActivity_Common_WorkItemActivityTypeId_DisplayName => "Activity Type";

        public static string WorkItemActivity_Create_AddAttachments_DisplayName => "Add Attachments";
        public static string WorkItemActivity_Create_AddAttachments_Heading => "Attachments";
        public static string WorkItemActivity_Create_BreadCrumb => "Create Activity";

        public static string WorkItemActivity_Create_MarkResolved_DisplayName => "Mark as Resolved";

        public static string WorkItemActivity_Create_SaveFailure_Message =>
            "The work item activity could not be created";

        public static string WorkItemActivity_Create_Submit_DisplayName => "Create Activity";

        public static string WorkItemActivity_Create_WillResolve_Message =>
            "This activity will mark the work item as resolved.";

        public static string WorkItemActivity_Detail_BreadCrumb => "Work Item Activity";
        public static string WorkItemActivity_Edit_BreadCrumb => "Edit Activity";

        public static string WorkItemActivity_Edit_SaveFailure_Message => "The work item activity could not be updated";
        public static string WorkItemActivity_Edit_Submit_DisplayName => "Update Activity";

        public static string WorkItemEngine_AssignToClientAsync_Narrative_Message =>
            "Work item was assigned to client for further processing.";

        public static string TaxAssessor_Index_Title => "Tax Assessors";
        public static string TaxAssessor_Index_ShowDeleted_DisplayName => "Show Deleted";
        public static string TaxAssessor_Index_Create_Link => "Create Tax Assessor";

        public static string TaxAssessor_Common_StateAbbreviation_TableHeader => "State";
        public static string TaxAssessor_Common_TaxAssessorName_TableHeader => "Tax Assessor";
        public static string TaxAssessor_Common_County_TableHeader => "County";
        public static string TaxAssessor_Common_IsDeleted_TableHeader => "Deleted?";
        public static string TaxAssessor_BreadCrumb => "Tax Assessors";

        public static string TaxAssessor_Common_County_DisplayName => "County";
        public static string TaxAssessor_Common_StateAbbreviation_DisplayName => "State";
        public static string TaxAssessor_Common_TaxAssessorName_DisplayName => "Tax Assessor Name";
        public static string TaxAssessor_Create_Title => "Create Tax Assessor";
        public static string TaxAssessor_Create_BreadCrumb => Common_Create_BreadCrumb;

        public static string TaxAssessor_Create_Submit_DisplayName => "Create Tax Assessor";

        public static string TaxAssessor_Common_Details_Heading => "Tax Assessor Details";
        public static string TaxAssessor_Common_Exists_Message => "A tax assessor with this combination of information already exists.";
        public static string TaxAssessor_Create_SaveFailure_Message => "The tax assessor could not be created.";
        public static string TaxAssessor_Detail_Title => "Tax Assessor";
        public static string TaxAssessor_Detail_BreadCrumb => "Tax Assessor Detail";

        public static string TaxAssessor_Detail_IsDeleted_Message => "Tax Assessor Not Active (Deleted)";

        public static string TaxAssessor_Common_BackToList_Link => "Back to List";

        public static string TaxAssessor_Edit_Submit_DisplayName => "Update Tax Assessor";
        public static string TaxAssessor_Edit_Title => "Edit Tax Assessor";
        public static string TaxAssessor_Edit_BreadCrumb => "Edit Tax Assessor";
        public static string TaxAssessor_Edit_SaveFailure_Message => "The tax assessor could not be updated.";
        public static string TaxAssessor_Delete_ConfirmDelete_DisplayName => "I Confirm";
        public static string TaxAssessor_Delete_ConfirmDelete_Message => "<strong>Warning!</strong> You are about to permanently delete {0} ({1}). Are you sure?";
        public static string TaxAssessor_Delete_Title => "Delete Tax Assessor";
        public static string TaxAssessor_Delete_BreadCrumb => "Delete Tax Assessor";
        public static string TaxAssessor_Delete_Submit_DisplayName => "Delete Tax Assessor";
        public static string TaxAssessor_Delete_ConfirmDelete_Validation_Message => "You must confirm you wish to delete this tax assessor.";
        public static string TaxAssessor_Delete_SaveFailure_Message => "The tax assessor could not be deleted.";


        public static string TaxCollector_Index_Title => "Tax Collectors";
        public static string TaxCollector_Index_ShowDeleted_DisplayName => "Show Deleted";
        public static string TaxCollector_Index_Create_Link => "Create Tax Collector";

        public static string TaxCollector_Common_VendorCode_TableHeader => "Vendor Code";
        public static string TaxCollector_Common_TaxCollectorName_TableHeader => "Tax Collector";
        public static string TaxCollector_Common_TaxAssessor_TableHeader => "Tax Assessor";
        public static string TaxCollector_Common_IsDeleted_TableHeader => "Deleted?";
        public static string TaxCollector_Common_StateAbbreviation_DisplayName => "State";
        public static string TaxCollector_Common_County_DisplayName => "County";
        public static string TaxCollector_BreadCrumb => "Tax Collectors";

        public static string TaxCollector_Common_VendorCode_DisplayName => "Vendor Code";
        public static string TaxCollector_Common_TaxAssessorName_DisplayName => "Tax Assessor";
        public static string TaxCollector_Common_TaxCollectorName_DisplayName => "Tax Collector Name";
        public static string TaxCollector_Create_Title => "Create Tax Collector";
        public static string TaxCollector_Create_BreadCrumb => Common_Create_BreadCrumb;

        public static string TaxCollector_Create_Submit_DisplayName => "Create Tax Collector";

        public static string TaxCollector_Common_Details_Heading => "Tax Collector Details";
        public static string TaxCollector_Common_Exists_Message => "A tax collector with this combination of information already exists.";
        public static string TaxCollector_Common_VendorCode_Exists_Message => "A tax collector with this vendor code already exists.";
        public static string TaxCollector_Create_SaveFailure_Message => "The tax collector could not be created.";
        public static string TaxCollector_Detail_Title => "Tax Collector";
        public static string TaxCollector_Detail_BreadCrumb => "Tax Collector Detail";

        public static string TaxCollector_Detail_IsDeleted_Message => "Tax Collector Not Active (Deleted)";

        public static string TaxCollector_Common_BackToList_Link => "Back to List";

        public static string TaxCollector_Edit_Submit_DisplayName => "Update Tax Collector";
        public static string TaxCollector_Edit_Title => "Edit Tax Collector";
        public static string TaxCollector_Edit_BreadCrumb => "Edit Tax Collector";
        public static string TaxCollector_Edit_SaveFailure_Message => "The tax collector could not be updated.";
        public static string TaxCollector_Delete_ConfirmDelete_DisplayName => "I Confirm";
        public static string TaxCollector_Delete_ConfirmDelete_Message => "<strong>Warning!</strong> You are about to permanently delete {0} ({1}). Are you sure?";
        public static string TaxCollector_Delete_Title => "Delete Tax Collector";
        public static string TaxCollector_Delete_BreadCrumb => "Delete Tax Collector";
        public static string TaxCollector_Delete_Submit_DisplayName => "Delete Tax Collector";
        public static string TaxCollector_Delete_ConfirmDelete_Validation_Message => "You must confirm you wish to delete this tax collector.";
        public static string TaxCollector_Delete_SaveFailure_Message => "The tax collector could not be deleted.";


        #endregion

        #region Properties

        private static string Application_Name => "Hexaware Property Tax";

        #endregion
    }
}