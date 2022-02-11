#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

#endregion

namespace CarLeasePPT.Helpers
{
    public static class ListItemActiveHelper
    {
        #region Public Methods and Operators

        public static string ApplyClassMatchAction(ViewContext viewContext, string actionName, string controllerName,
            string className)
        {
            var baseReturn = $"class=\"{className}\"";
            var activeReturn = $"class=\"{className} active\"";
            try
            {
                return viewContext.RouteData.Values["Action"].ToString().Equals(actionName) &&
                       viewContext.RouteData.Values["Controller"].ToString().Equals(controllerName)
                    ? activeReturn
                    : baseReturn;
            }
            catch (Exception)
            {
                return baseReturn;
            }
        }

        public static string ApplyClassMatchAction(ViewContext viewContext, string actionName, string className)
        {
            var baseReturn = $"class=\"{className}\"";
            var activeReturn = $"class=\"{className} active\"";
            try
            {
                return viewContext.RouteData.Values["Action"].ToString().Equals(actionName)
                    ? activeReturn
                    : baseReturn;
            }
            catch (Exception)
            {
                return baseReturn;
            }
        }

        public static string ApplyClassMatchAction(ViewContext viewContext, IEnumerable<string> actionList,
            string className)
        {
            var baseReturn = $"class=\"{className}\"";
            var activeReturn = $"class=\"{className} active\"";
            try
            {
                return actionList.Contains(viewContext.RouteData.Values["Action"].ToString())
                    ? activeReturn
                    : baseReturn;
            }
            catch (Exception)
            {
                return baseReturn;
            }
        }

        public static string ApplyClassMatchArea(ViewContext viewContext, string areaName, string className)
        {
            var baseReturn = $"class=\"{className}\"";
            var activeReturn = $"class=\"{className} active\"";
            try
            {
                if (viewContext.RouteData.DataTokens.ContainsKey("Area"))
                    return viewContext.RouteData.DataTokens["Area"].ToString().Equals(areaName)
                        ? activeReturn
                        : baseReturn;
                return baseReturn;
            }
            catch (Exception)
            {
                return baseReturn;
            }
        }

        public static string ApplyClassMatchAreaAndController(ViewContext viewContext, string areaName,
            string controllerName, string className)
        {
            var baseReturn = $"class=\"{className}\"";
            var activeReturn = $"class=\"{className} active\"";
            try
            {
                if (!viewContext.RouteData.Values["Controller"].ToString().Equals(controllerName)) return baseReturn;
                if (areaName == null && !viewContext.RouteData.DataTokens.ContainsKey("Area")) return activeReturn;
                if (!viewContext.RouteData.DataTokens.ContainsKey("Area")) return baseReturn;
                return viewContext.RouteData.DataTokens["Area"].Equals(areaName) ? activeReturn : baseReturn;
            }
            catch (Exception)
            {
                return baseReturn;
            }
        }

        public static string ApplyClassMatchController(ViewContext viewContext, string valueName, string className)
        {
            var baseReturn = $"class=\"{className}\"";
            var activeReturn = $"class=\"{className} active\"";
            try
            {
                return viewContext.RouteData.Values["Controller"].ToString().Equals(valueName)
                    ? activeReturn
                    : baseReturn;
            }
            catch (Exception)
            {
                return baseReturn;
            }
        }

        public static string ApplyClassMatchController(ViewContext viewContext, IEnumerable<string> valueStrings,
            string className)
        {
            var baseReturn = $"class=\"{className}\"";
            var activeReturn = $"class=\"{className} active\"";
            try
            {
                var c = viewContext.RouteData.Values["Controller"].ToString();
                return valueStrings.Contains(c) ? activeReturn : baseReturn;
            }
            catch (Exception)
            {
                return baseReturn;
            }
        }

        #endregion
    }
}