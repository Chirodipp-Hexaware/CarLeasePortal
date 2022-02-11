window.utc_aft = "";
var table;
var showDeleted = false;
$(document).ready(function () {

    window.utc_aft = $("#aft").data("token");
    $("#show_deleted").on("change", function () {
        showDeleted = this.checked;
        table.column(3).visible(this.checked);
        table.draw();
    });
    table = $("#taxassessor_table").DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": "/api/TaxAssessorData",
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
                "data": "TaxAssessorName"
            },
            {
                "className": "td-nowrap",
                "data": "County",
                "orderable": false
            },
            {
                "className": "td-nowrap",
                "data": "IsDeletedString",
                "orderable": false
            },
            {
                "className": "td-nowrap action",
                "data": "TaxAssessorId",
                "orderable": false,
                "render":
                    function (d, t, r) {
                        var response = '<a class="btn btn-secondary btn-sm" href="/TaxAssessor/Detail/' + d + '">View</a>';
                        if (!r.IsDeleted) {
                            response += ' <a class="btn btn-secondary btn-sm" href="/TaxAssessor/Edit/' + d + '">Edit</a>';
                            response += ' <a class="btn btn-secondary btn-sm" href="/TaxAssessor/Delete/' + d + '">Delete</a>';
                        }
                        return response;
                    }
            }
        ]
    });
    table.column(3).visible(showDeleted);
});