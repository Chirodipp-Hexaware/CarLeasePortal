var table;
var showReviewed = false;
var showInfo = false;
window.utc_aft = "";
$(document).ready(function () {

    window.utc_aft = $("#aft").data("token");
    $("#show_reviewed").on("change", function () {
        showReviewed = this.checked;
        table.column(6).visible(this.checked);
        table.draw();
    });
    $("#show_info").on("change",
        function () {
            showInfo = this.checked;
            table.draw();
        });

    table = $("#auditlog_table").DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": "/api/AuditLogData",
            "type": "POST",
            "data": function (d) {
                d.search.show_excluded = showReviewed;
                d.search.show_info = showInfo;
            },
            headers: {
                'RequestVerificationToken': window.utc_aft
            }
        },
        "stateSave": true,
        "stateSaveParams": function (settings, data) {
            data.search.show_excluded = showReviewed;
            data.search.show_info = showInfo;
        },
        "stateLoadParams": function (settings, data) {
            $("#show_reviewed").prop("checked", data.search.show_excluded);
            $("#show_info").prop("checked", data.search.show_info);
            showReviewed = data.search.show_excluded;
            showInfo = data.search.show_info;
        },
        "ordering": false,
        "searching": false,
        "createdRow": function (r, d) {
            if (d.LogLevel === "Warn") {
                $(r).addClass("warning-row");
            } else if (d.LogLevel === "Error") {
                $(r).addClass("danger-row");
            }
        },
        "columns": [
            {
                "className": "td-nowrap",
                "data": "Logged",
                "render":
                    function (data) {
                        if (null !== data) {
                            return new Date(Date.parse(data)).toLocaleString();
                        }
                        return "";
                    }
            },
            {
                "className": "td-nowrap",
                "data": "LogLevel"
            },
            {
                "className": "td-nowrap",
                "data": "PersonFullName"
            },
            {
                "data": "Message"
            },
            {
                "className": "td-nowrap",
                "data": "Controller"
            },
            {
                "className": "td-nowrap",
                "data": "Action"
            },
            {
                "className": "td-nowrap",
                "data": "IsReviewedString"
            },
            {
                "className": "td-nowrap action",
                "data": "AuditLogId",
                "render":
                    function (d, t, r) {
                        var response = '<a class="btn btn-secondary btn-sm" href="/AuditLog/Detail/' +
                            d +
                            '">View</a>';
                        if (!r.IsReviewed) {
                            response += ' <a class="btn btn-secondary btn-sm" href="/AuditLog/MarkReviewed/' + d + '">Mark</a>';
                        }
                        return response;
                    }
            }
        ]
    });
    table.column(6).visible(showReviewed);
});