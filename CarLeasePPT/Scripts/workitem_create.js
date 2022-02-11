$(document).ready(function () {
    $("#WorkItemTypeId").on("change",
        function () {
            if (null !== $(this).val()) {
                var i = $(this).val();
                if (i === "3") {
                    $("#notice-panel").not("hidden").removeClass("hidden");
                } else {
                    $("#notice-panel").not("hidden").addClass("hidden");
                }
            };
        });
});