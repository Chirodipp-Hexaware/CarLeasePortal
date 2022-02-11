var table;
window.utc_aft = "";
$(document).ready(function () {
    window.utc_aft = $("#aft").data("token");
    table = $("#workitem_search_table").DataTable({
        "processing": true,
        "serverSide": true,
        "searching": false,
        "saveState": true,
        "stateSaveParams": function (settings, data) {
            data.search.ln = $("#LeaseNumber").val();
            data.search.nn = $("#NoticeNumber").val();
            data.search.vin = $("#Vin").val();
            data.search.tcn = $("#TaxCollectorName").val();
            data.search.wit = $("#WorkItemTypeId").val();
            data.search.wis = $("#WorkItemStatusId").val();
            data.search.pl = $("#PriorityLevel").val();
        },
        "stateLoadParams": function (settings, data) {
            $("#LeaseNumber").val(data.search.ln);
            $("#NoticeNumber").val(data.search.nn);
            $("#Vin").val(data.search.vin);
            $("#TaxCollectorName").val(data.search.tcn);
            $("#WorkItemTypeId").val(data.search.wit);
            $("#WorkItemStatusId").val(data.search.wis);
            $("#PriorityLevel").val(data.search.pl);
        },
        "ajax": {
            "url": "/api/WorkItemData",
            "type": "POST",
            "data": function (data) {
                data.search.ln = $("#LeaseNumber").val();
                data.search.nn = $("#NoticeNumber").val();
                data.search.vin = $("#Vin").val();
                data.search.tcn = $("#TaxCollectorName").val();
                data.search.wit = $("#WorkItemTypeId").val();
                data.search.wis = $("#WorkItemStatusId").val();
                data.search.pl = $("#PriorityLevel").val();
            },
            headers: {
                'RequestVerificationToken': window.utc_aft
            }
        },
        "createdRow": function (r, d) {
            if (d.IsUrgent === true) {
                $(r).addClass("danger-row");
            }
        },
        "order":[[3,"desc"]],
        "columns": [
            {
                "className": "td-nowrap",
                "data": "NoticeNumber",
                "render":
                    function (data, type, row, meta) {
                        return '<a href="/workitem/Detail/' +
                            row.WorkItemId +
                            '">' + data + "</a>";
                    }
            },
            {
                "className": "td-nowrap",
                "data": "WorkItemTypeName"
            },
            {
                "className": "td-nowrap",
                "data": "WorkItemStatusName",
                "orderable": false
            },
            {
                "className": "td-nowrap text-center",
                "data": "IsUrgentString"
            },
            {
                "className": "td-nowrap",
                "data": "TaxCollectorName"
            },
            {
                "className": "td-nowrap text-center",
                "data": "CreateDateTime",
                "render":
                    function (data) {
                        if (null !== data) {
                            return new Date(Date.parse(data)).toLocaleDateString();
                        }
                        return "";
                    }
            },
            {
                "className": "td-nowrap text-center",
                "data": "LastUpdated",
                "render":
                    function (data) {
                        if (null !== data) {
                            return new Date(Date.parse(data)).toLocaleDateString();
                        }
                        return "";
                    },
                "orderable": false
            }
        ]
    });
    $("#workitem_search_table_clear").on("click",
        function () {
            table.state.clear();
            window.location.reload();
        });
    $("#workitem_search_table_run").on("click",
        function () {
            table.draw();
        });
});