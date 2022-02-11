var table;
window.utc_aft = "";
$(document).ready(function () {
    window.utc_aft = $("#aft").data("token");
    table = $("#bill_search_table").DataTable({
        "processing": true,
        "serverSide": true,
        "searching": false,
        "saveState": true,
        "ordering": false,
        "stateSaveParams": function (settings, data) {
            data.search.ln = $("#LeaseNumber").val();
            data.search.tan = $("#TaxAccountNumber").val();
            data.search.bn = $("#BillNumber").val();
            data.search.vin = $("#Vin").val();
        },
        "stateLoadParams": function (settings, data) {
            $("#LeaseNumber").val(data.search.ln);
            $("#TaxAccountNumber").val(data.search.tan);
            $("#BillNumber").val(data.search.bn);
            $("#Vin").val(data.search.vin);
        },
        "deferLoading": 0,
        "ajax": {
            "url": "/api/TaxBillData",
            "type": "POST",
            "data": function (data) {
                data.search.ln = $("#LeaseNumber").val();
                data.search.tan = $("#TaxAccountNumber").val();
                data.search.bn = $("#BillNumber").val();
                data.search.vin = $("#Vin").val();
            },
            headers: {
                'RequestVerificationToken': window.utc_aft
            }
        },
        "columns": [
            {
                "className": "td-nowrap",
                "data": "BillNumber",
                "orderable": false,
                "render":
                    function (data, type, row, meta) {
                        return '<a href="/TaxBill/Detail/' +
                            row.TaxBillId +
                            '">' + data +'</a>';
                    }
            },
            {
                "className": "td-nowrap text-center",
                "data": "TaxAssessorState",
                "orderable": false
            },
            {
                "className": "td-nowrap text-center",
                "data": "TaxYear",
                "orderable": false
            },
            {
                "className": "td-nowrap",
                "data": "CollectorAccountNumber",
                "orderable": false
            },
            {
                "className": "td-nowrap text-center",
                "data": "DueDate",
                "render":
                    function (data) {
                        if (null !== data) {
                            return new Date(Date.parse(data)).toLocaleDateString();
                        }
                        return "";
                    },
                "orderable": false
            },
            {
                "className": "td-nowrap text-center",
                "data": "CompleteDate",
                "render":
                    function (data) {
                        if (null !== data) {
                            return new Date(Date.parse(data)).toLocaleDateString();
                        }
                        return "";
                    },
                "orderable": false
            },
            {
                "className": "td-nowrap text-right",
                "data": "TotalAmountOwed",
                "orderable": false,
                "render":
                    function(data) {
                        if (null !== data && "undefined" !== typeof data) {
                            return data.toFixed(2);
                        }
                        return data;
                    }
            },
            {
                "className": "td-nowrap text-center",
                "data": "IsCompleteString",
                "orderable": false
            }
        ]
    });
    $("#taxbill_search_table_clear").on("click",
        function () {
            table.state.clear();
            window.location.reload();
        });
    $("#taxbill_search_table_run").on("click",
        function () {
            table.draw();
        });
});