var table;
window.utc_aft = "";
$(document).ready(function () {
    window.utc_aft = $("#aft").data("token");
    table = $("#lease_search_table").DataTable({
        "processing": true,
        "serverSide": true,
        "searching": false,
        "saveState": true,
        "ordering": false,
        "stateSaveParams": function (settings, data) {
            data.search.pn = $("#PlateNumber").val();
            data.search.ln = $("#LeaseNumber").val();
            data.search.tan = $("#TaxAccountNumber").val();
            data.search.bn = $("#BillNumber").val();
            data.search.vin = $("#VIN").val();
            data.search.lln = $("#LesseeLastName").val();
            data.search.lfn = $("#LesseeFirstName").val();
        },
        "stateLoadParams": function (settings, data) {
            $("#PlateNumber").val(data.search.pn);
            $("#LeaseNumber").val(data.search.ln);
            $("#TaxAccountNumber").val(data.search.tan);
            $("#BillNumber").val(data.search.bn);
            $("#VIN").val(data.search.vin);
            $("#LesseeLastName").val(data.search.lln);
            $("#LesseeFirstName").val(data.search.lfn);
        },
        "deferLoading": 0,
        "ajax": {
            "url": "/api/LeaseData",
            "type": "POST",
            "data": function (data) {
                data.search.pn = $("#PlateNumber").val();
                data.search.ln = $("#LeaseNumber").val();
                data.search.tan = $("#TaxAccountNumber").val();
                data.search.bn = $("#BillNumber").val();
                data.search.vin = $("#VIN").val();
                data.search.lln = $("#LesseeLastName").val();
                data.search.lfn = $("#LesseeFirstName").val();
            },
            headers: {
                'RequestVerificationToken': window.utc_aft
            }
        },
        "columns": [
            {
                "className": "td-nowrap",
                "data": "LeaseNumber",
                "render":
                    function (data, type, row, meta) {
                        return '<a href="/Lease/Detail/' +
                            row.CarLeaseId +
                            '">' + data + '</a>';
                    }
            },
            {
                "className": "td-nowrap",
                "data": "VIN"
            },
            {
                "className": "td-nowrap",
                "data": "LesseeFullName"
            },
            {
                "className": "td-nowrap",
                "data": "CarMake"
            },
            {
                "className": "td-nowrap",
                "data": "CarModel"
            },
            {
                "className": "td-nowrap text-center",
                "data": "ModelYear"
            }
        ]
    });
    $("#lease_search_table_clear").on("click",
        function () {
            table.state.clear();
            window.location.reload();
        });
    $("#lease_search_table_run").on("click",
        function () {
            table.draw();
        });
});