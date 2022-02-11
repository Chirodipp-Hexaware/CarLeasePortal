window.utc_aft = "";
$(document).ready(function () {
    window.utc_aft = $("#aft").data("token");
    $("#car_table").DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": "/api/CarData",
            "type": "POST",
            headers: {
                'RequestVerificationToken': window.utc_aft
            }
        },
        "order": [[0, "asc"]],
        "columns": [
            {
                "className": "td-nowrap",
                "data": "Vin"
            },
            {
                "className": "td-nowrap",
                "data": "CarMake",
                "orderable": false
            },
            {
                "className": "td-nowrap",
                "data": "CarModel",
                "orderable": false
            },
            {
                "className": "td-nowrap",
                "data": "ModelYear",
                "orderable": false
            },
            {
                "className": "td-nowrap action",
                "data": "CarMasterId",
                "orderable": false,
                "render":
                    function (data, type, row, meta) {
                        return '<a class="btn btn-secondary btn-sm" href="/Car/Detail/' +
                            data +
                            '">View</a> <a class="btn btn-secondary btn-sm" href="/Car/Edit/' +
                            data +
                            '">Edit</a>';
                    }
            }
        ]
    });
});