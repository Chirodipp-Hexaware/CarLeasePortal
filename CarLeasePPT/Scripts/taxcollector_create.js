window.utc_aft = "";
var taxAssessorId = -1;
$(document).ready(function () {
    window.utc_aft = $("#aft").data("token");
    $("#TaxAssessorSelect2").on("change",
        function () {
            if (null !== $(this).val()) {
                taxAssessorId = $(this).val();
                $("#TaxAssessorId").val(taxAssessorId);
            }
        });
    $("#TaxAssessorSelect2").select2({
        "ajax": {
            "url": "/api/TaxAssessorData/Select2",
            "type": "POST",
            "datatype": "json",
            "delay": 250,
            "data": function (params) {
                return {
                    term: params.term || "",
                    page: params.page || 1
                }
            },
            "headers": {
                'RequestVerificationToken': window.utc_aft
            },
            "theme": "bootstrap4"
        },
        "minimumResultsForSearch": 10
    });
});