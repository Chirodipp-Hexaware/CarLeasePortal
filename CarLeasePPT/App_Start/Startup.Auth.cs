using System;
using System.Configuration;
using System.Web.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Owin;
using CarLeasePPT;

[assembly: OwinStartup(typeof(Startup))]
namespace CarLeasePPT
{
    public partial class Startup
    {
        private static int ApplicationCookieDuration => int.TryParse(ConfigurationManager.AppSettings["ApplicationCookieDuration"], out var cookieDuration) ? cookieDuration : 15;
        private static string ApplicationCookieName => ConfigurationManager.AppSettings["ApplicationCookieName"] ?? "hexa_default_127d478d535044058c68f0a12f43bcfc";
        private static string AntiForgeryCookieName => ConfigurationManager.AppSettings["AntiForgeryCookieName"] ?? "hexa_default_9bf53f12ffcf4b3bbe1513a07b7ed12b";
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Login"),
                CookieSecure = CookieSecureOption.Always,
                SlidingExpiration = true,
                ExpireTimeSpan = TimeSpan.FromMinutes(ApplicationCookieDuration),
                CookieName = ApplicationCookieName,
                AuthenticationMode = AuthenticationMode.Active,
            });
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));
            //app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);
            AntiForgeryConfig.RequireSsl = true;
            AntiForgeryConfig.CookieName = AntiForgeryCookieName;
        }
    }
}