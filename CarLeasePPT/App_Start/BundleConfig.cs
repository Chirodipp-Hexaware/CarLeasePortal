using System.Web.Optimization;

namespace CarLeasePPT
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/js-cookie/js.cookie-2.1.4.min.js",
                "~/DataTables/datatables.js",
                "~/Scripts/select2.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/Content/custom.css",
                      "~/DataTables/datatables.css",
                      "~/Content/css/select2.min.css",
                      "~/Content/select2-bootstrap4.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/common").Include(
                "~/Scripts/common.js"));

            bundles.Add(new ScriptBundle("~/bundles/user").Include(
                "~/Scripts/user.js"));

            bundles.Add(new ScriptBundle("~/bundles/car").Include(
                "~/Scripts/car.js"));

            bundles.Add(new ScriptBundle("~/bundles/home").Include(
                "~/Scripts/home.js"));

            bundles.Add(new ScriptBundle("~/bundles/auditlog").Include(
                "~/Scripts/auditlog.js"));

            bundles.Add(new ScriptBundle("~/bundles/taxbill").Include(
                "~/Scripts/taxbill.js"));

            bundles.Add(new ScriptBundle("~/bundles/taxbill/create").Include(
                "~/Scripts/taxbill_create.js"));

            bundles.Add(new ScriptBundle("~/bundles/workitemactivity").Include(
                "~/Scripts/workitemactivity_create.js"));

            bundles.Add(new ScriptBundle("~/bundles/addattachment").Include(
                "~/Scripts/add_attachment.js"));

            bundles.Add(new ScriptBundle("~/bundles/taxassessor").Include(
                "~/Scripts/taxassessor.js"));

            bundles.Add(new ScriptBundle("~/bundles/taxcollector").Include(
                "~/Scripts/taxcollector.js"));

            bundles.Add(new ScriptBundle("~/bundles/taxcollector/create").Include(
                "~/Scripts/taxcollector_create.js"));
        }
    }
}
