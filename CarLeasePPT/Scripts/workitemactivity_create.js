$(document).ready(function () {
    var h = $("#ActivityTimeHour").val();
    var m = $("#ActivityTimeMinute").val();
    var t = $("#ActivityMeridian").val();

    $("#hour").text((h % 12).toString().padStart(2,"0"));
    $("#minute").text(m.padStart(2, "0"));
    $("#meridian").text(t);
});