$(document).ready(function () {
    $("input[name='show-profile']").click(function () {
        $("#profile-form").show();
        $("#show-profile").hide();
        $("#profile-display").hide();
    });

    $(function () {
        $(".focus:input").focus();
    });
    $(".datatable_auto").DataTable();

    $(".select2").select2({
        minimumResultsForSearch: 10,
        theme: "bootstrap4"
    });

    $("input[name='reset-password']").click(function () {
        $("#change-password-card").find("input[type=password]").val("");
    });

});