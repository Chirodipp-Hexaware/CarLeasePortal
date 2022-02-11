//https://gist.github.com/laktek/758269
//https://gist.github.com/jtarchie
$.isBlank = function (object) {
    return (
        ($.isPlainObject(object) && $.isEmptyObject(object)) ||
            ($.isArray(object) && object.length === 0) ||
            (typeof (object) == "string" && $.trim(object) === "") ||
            (!object)
    );
};

$(document).ready(function () {
    $("input:file").change(function () {
        if ($.isBlank($(this).val())) {
            $("input[type='submit']").attr("disabled", "disabled");
        } else {
            $("input[type='submit']").removeAttr("disabled");
        }
    });
});