$(document).ready(function () {
    function showPasswordForm() {
        $("#change-password-card").show();
        $("#change-password-button").hide();
        Cookies.set("change-password", true, { secure: true });
    }

    $("#change-password-button").click(function () {
        showPasswordForm();
    });

    if ($("#change-password-button").length !== 0) {
        if (Cookies.get("change-password") !== undefined &&
            Cookies.get("change-password") === "true") {
            showPasswordForm();
        };
    };

    $("input[name='reset-password']").click(function () {
        $("#change-password-card").find("input[type=password]").val("");
        $("#change-password-card").hide();
        $("#change-password-button").show();
        if (Cookies.get("change-password") !== undefined) {
            Cookies.remove("change-password");
        };
    });
});