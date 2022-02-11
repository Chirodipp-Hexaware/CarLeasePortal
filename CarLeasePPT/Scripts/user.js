window.utc_personId = 0;
window.utc_aft = "";
var table;
var showExpired = false;
$(document).ready(function () {

    window.utc_personId = $("#k2so").data("person");
    window.utc_aft = $("#aft").data("token");
    $("#show_expired").on("change",
        function () {
            showExpired = this.checked;
            table.draw();
        });

    table = $("#person_table").DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": "/api/UserData",
            "type": "POST",
            "data": function (d) {
                d.search.show_excluded = showExpired;
            },
            headers: {
                'RequestVerificationToken': window.utc_aft
            }
        },
        "stateSave": true,
        "stateSaveParams": function (settings, data) {
            data.search.show_excluded = showExpired;
        },
        "stateLoadParams": function (settings, data) {
            $("#show_expired").prop("checked", data.search.show_excluded);
            showExpired = data.search.show_excluded;
        },
        "order": [[0, "asc"]],
        "columns": [
            {
                "className": "td-nowrap",
                "data": "PersonStatus"
            },
            {
                "className": "td-nowrap",
                "data": "FullName"
            },
            {
                "className": "td-nowrap",
                "data": "PreferredName"
            },
            {
                "className": "td-nowrap",
                "data": "EmailAddress"
            },
            {
                "className": "td-nowrap action",
                "data": "PersonId",
                "orderable": false,
                "render":
                    function (d, t, r) {
                        var response = '<a class="btn btn-secondary btn-sm" href="/User/Detail/' + d + '">View</a>';
                        if (!r.IsExpired) {
                            response += ' <a class="btn btn-secondary btn-sm" href="/User/Edit/' + d + '">Edit</a>';
                            if (window.utc_personId !== d) {
                                response += ' <a class="btn btn-secondary btn-sm" href="/User/Delete/' + d + '">Delete</a>';
                            }
                        }
                        return response;
                    }
            }
        ]
    });
});