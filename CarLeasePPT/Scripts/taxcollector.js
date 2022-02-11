window.utc_aft = "";
var table;
var showDeleted = false;
$(document).ready(function () {

    window.utc_aft = $("#aft").data("token");
    $("#show_deleted").on("change", function () {
        showDeleted = this.checked;
        table.column(4).visible(this.checked);
        table.draw();
    });
    table = $("#taxcollector_table").DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": "/api/TaxCollectorData",
            "type": "POST",
            "data": function (d) {
                d.search.show_excluded = showDeleted;
            },
            headers: {
                'RequestVerificationToken': window.utc_aft
            }
        },
        "stateSave": true,
        "stateSaveParams": function (settings, data) {
            data.search.show_excluded = showDeleted;
        },
        "stateLoadParams": function (settings, data) {
            $("#show_deleted").prop("checked", data.search.show_excluded);
            showDeleted = data.search.show_excluded;
        },
        "order": [[0, "asc"]],
        "columns": [
            {
                "className": "td-nowrap",
                "data": "StateAbbreviation"
            },
            {
                "className": "td-nowrap",
                "data": "TaxCollectorName"
            },
            {
                "className": "td-nowrap",
                "data": "VendorCode"
            },
            {
                "className": "td-nowrap",
                "data": "TaxAssessorName",
                "orderable": false
            },
            {
                "className": "td-nowrap",
                "data": "IsDeletedString",
                "orderable": false
            },
            {
                "className": "td-nowrap action",
                "data": "TaxCollectorId",
                "orderable": false,
                "render":
                    function (d, t, r) {
                        var response = '<a class="btn btn-secondary btn-sm" href="/TaxCollector/Detail/' + d + '">View</a>';
                        if (!r.IsDeleted) {
                            response += ' <a class="btn btn-secondary btn-sm" href="/TaxCollector/Edit/' + d + '">Edit</a>';
                            response += ' <a class="btn btn-secondary btn-sm" href="/TaxCollector/Delete/' + d + '">Delete</a>';
                        }
                        return response;
                    }
            }
        ]
    });
    table.column(4).visible(showDeleted);
});