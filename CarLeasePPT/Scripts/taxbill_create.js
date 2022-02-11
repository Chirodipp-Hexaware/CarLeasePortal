$(document).ready(function () {
    if ($("#TaxBillId").val() !== "") {
        $("#TaxCollectorName").prop("readonly", "readonly");
        $("#TaxYear").prop("readonly", "readonly");
    }
    if ($("#CarLeaseId").val() !== "") {
        $("#Vin").prop("readonly", "readonly");
        $("#LeaseNumber").prop("readonly", "readonly");
        $("#LesseeFirstName").prop("readonly", "readonly");
        $("#LesseeLastName").prop("readonly", "readonly");
    }
});