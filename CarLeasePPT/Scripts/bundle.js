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
var table;
var showReviewed = false;
var showInfo = false;
window.utc_aft = "";
$(document).ready(function () {

    window.utc_aft = $("#aft").data("token");
    $("#show_reviewed").on("change", function () {
        showReviewed = this.checked;
        table.column(6).visible(this.checked);
        table.draw();
    });
    $("#show_info").on("change",
        function () {
            showInfo = this.checked;
            table.draw();
        });

    table = $("#auditlog_table").DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": "/api/AuditLogData",
            "type": "POST",
            "data": function (d) {
                d.search.show_excluded = showReviewed;
                d.search.show_info = showInfo;
            },
            headers: {
                'RequestVerificationToken': window.utc_aft
            }
        },
        "stateSave": true,
        "stateSaveParams": function (settings, data) {
            data.search.show_excluded = showReviewed;
            data.search.show_info = showInfo;
        },
        "stateLoadParams": function (settings, data) {
            $("#show_reviewed").prop("checked", data.search.show_excluded);
            $("#show_info").prop("checked", data.search.show_info);
            showReviewed = data.search.show_excluded;
            showInfo = data.search.show_info;
        },
        "ordering": false,
        "searching": false,
        "createdRow": function (r, d) {
            if (d.LogLevel === "Warn") {
                $(r).addClass("warning-row");
            } else if (d.LogLevel === "Error") {
                $(r).addClass("danger-row");
            }
        },
        "columns": [
            {
                "className": "td-nowrap",
                "data": "Logged",
                "render":
                    function (data) {
                        if (null !== data) {
                            return new Date(Date.parse(data)).toLocaleString();
                        }
                        return "";
                    }
            },
            {
                "className": "td-nowrap",
                "data": "LogLevel"
            },
            {
                "className": "td-nowrap",
                "data": "PersonFullName"
            },
            {
                "data": "Message"
            },
            {
                "className": "td-nowrap",
                "data": "Controller"
            },
            {
                "className": "td-nowrap",
                "data": "Action"
            },
            {
                "className": "td-nowrap",
                "data": "IsReviewedString"
            },
            {
                "className": "td-nowrap action",
                "data": "AuditLogId",
                "render":
                    function (d, t, r) {
                        var response = '<a class="btn btn-secondary btn-sm" href="/AuditLog/Detail/' +
                            d +
                            '">View</a>';
                        if (!r.IsReviewed) {
                            response += ' <a class="btn btn-secondary btn-sm" href="/AuditLog/MarkReviewed/' + d + '">Mark</a>';
                        }
                        return response;
                    }
            }
        ]
    });
    table.column(6).visible(showReviewed);
});
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
window.utc_aft = "";
var table;
var showDeleted = false;
$(document).ready(function () {

    window.utc_aft = $("#aft").data("token");
    $("#show_deleted").on("change", function () {
        showDeleted = this.checked;
        table.column(3).visible(this.checked);
        table.draw();
    });
    table = $("#taxassessor_table").DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": "/api/TaxAssessorData",
            "type": "POST",
            "data": function (d) {
                d.search.show_excluded = showDeleted;
            },
            headers: {
                'RequestVerificationToken': window.utc_aft
            }
        },
        "stateSave": true,
        "stateSaveParams": function (settings, data) {
            data.search.show_excluded = showDeleted;
        },
        "stateLoadParams": function (settings, data) {
            $("#show_deleted").prop("checked", data.search.show_excluded);
            showDeleted = data.search.show_excluded;
        },
        "order": [[0, "asc"]],
        "columns": [
            {
                "className": "td-nowrap",
                "data": "StateAbbreviation"
            },
            {
                "className": "td-nowrap",
                "data": "TaxAssessorName"
            },
            {
                "className": "td-nowrap",
                "data": "County",
                "orderable": false
            },
            {
                "className": "td-nowrap",
                "data": "IsDeletedString",
                "orderable": false
            },
            {
                "className": "td-nowrap action",
                "data": "TaxAssessorId",
                "orderable": false,
                "render":
                    function (d, t, r) {
                        var response = '<a class="btn btn-secondary btn-sm" href="/TaxAssessor/Detail/' + d + '">View</a>';
                        if (!r.IsDeleted) {
                            response += ' <a class="btn btn-secondary btn-sm" href="/TaxAssessor/Edit/' + d + '">Edit</a>';
                            response += ' <a class="btn btn-secondary btn-sm" href="/TaxAssessor/Delete/' + d + '">Delete</a>';
                        }
                        return response;
                    }
            }
        ]
    });
    table.column(3).visible(showDeleted);
});
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
window.utc_aft = "";
var table;
var showDeleted = false;
$(document).ready(function () {

    window.utc_aft = $("#aft").data("token");
    $("#show_deleted").on("change", function () {
        showDeleted = this.checked;
        table.column(4).visible(this.checked);
        table.draw();
    });
    table = $("#taxcollector_table").DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": "/api/TaxCollectorData",
            "type": "POST",
            "data": function (d) {
                d.search.show_excluded = showDeleted;
            },
            headers: {
                'RequestVerificationToken': window.utc_aft
            }
        },
        "stateSave": true,
        "stateSaveParams": function (settings, data) {
            data.search.show_excluded = showDeleted;
        },
        "stateLoadParams": function (settings, data) {
            $("#show_deleted").prop("checked", data.search.show_excluded);
            showDeleted = data.search.show_excluded;
        },
        "order": [[0, "asc"]],
        "columns": [
            {
                "className": "td-nowrap",
                "data": "StateAbbreviation"
            },
            {
                "className": "td-nowrap",
                "data": "TaxCollectorName"
            },
            {
                "className": "td-nowrap",
                "data": "VendorCode"
            },
            {
                "className": "td-nowrap",
                "data": "TaxAssessorName",
                "orderable": false
            },
            {
                "className": "td-nowrap",
                "data": "IsDeletedString",
                "orderable": false
            },
            {
                "className": "td-nowrap action",
                "data": "TaxCollectorId",
                "orderable": false,
                "render":
                    function (d, t, r) {
                        var response = '<a class="btn btn-secondary btn-sm" href="/TaxCollector/Detail/' + d + '">View</a>';
                        if (!r.IsDeleted) {
                            response += ' <a class="btn btn-secondary btn-sm" href="/TaxCollector/Edit/' + d + '">Edit</a>';
                            response += ' <a class="btn btn-secondary btn-sm" href="/TaxCollector/Delete/' + d + '">Delete</a>';
                        }
                        return response;
                    }
            }
        ]
    });
    table.column(4).visible(showDeleted);
});
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
window.utc_personId = 0;
window.utc_aft = "";
var table;
var showExpired = false;
$(document).ready(function () {

    window.utc_personId = $("#k2so").data("person");
    window.utc_aft = $("#aft").data("token");
    $("#show_expired").on("change",
        function () {
            showExpired = this.checked;
            table.draw();
        });

    table = $("#person_table").DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": "/api/UserData",
            "type": "POST",
            "data": function (d) {
                d.search.show_excluded = showExpired;
            },
            headers: {
                'RequestVerificationToken': window.utc_aft
            }
        },
        "stateSave": true,
        "stateSaveParams": function (settings, data) {
            data.search.show_excluded = showExpired;
        },
        "stateLoadParams": function (settings, data) {
            $("#show_expired").prop("checked", data.search.show_excluded);
            showExpired = data.search.show_excluded;
        },
        "order": [[0, "asc"]],
        "columns": [
            {
                "className": "td-nowrap",
                "data": "PersonStatus"
            },
            {
                "className": "td-nowrap",
                "data": "FullName"
            },
            {
                "className": "td-nowrap",
                "data": "PreferredName"
            },
            {
                "className": "td-nowrap",
                "data": "EmailAddress"
            },
            {
                "className": "td-nowrap action",
                "data": "PersonId",
                "orderable": false,
                "render":
                    function (d, t, r) {
                        var response = '<a class="btn btn-secondary btn-sm" href="/User/Detail/' + d + '">View</a>';
                        if (!r.IsExpired) {
                            response += ' <a class="btn btn-secondary btn-sm" href="/User/Edit/' + d + '">Edit</a>';
                            if (window.utc_personId !== d) {
                                response += ' <a class="btn btn-secondary btn-sm" href="/User/Delete/' + d + '">Delete</a>';
                            }
                        }
                        return response;
                    }
            }
        ]
    });
});
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
var table;
window.utc_aft = "";
$(document).ready(function () {
    window.utc_aft = $("#aft").data("token");
    table = $("#workitem_search_table").DataTable({
        "processing": true,
        "serverSide": true,
        "searching": false,
        "saveState": true,
        "stateSaveParams": function (settings, data) {
            data.search.ln = $("#LeaseNumber").val();
            data.search.nn = $("#NoticeNumber").val();
            data.search.vin = $("#Vin").val();
            data.search.tcn = $("#TaxCollectorName").val();
            data.search.wit = $("#WorkItemTypeId").val();
            data.search.wis = $("#WorkItemStatusId").val();
            data.search.pl = $("#PriorityLevel").val();
        },
        "stateLoadParams": function (settings, data) {
            $("#LeaseNumber").val(data.search.ln);
            $("#NoticeNumber").val(data.search.nn);
            $("#Vin").val(data.search.vin);
            $("#TaxCollectorName").val(data.search.tcn);
            $("#WorkItemTypeId").val(data.search.wit);
            $("#WorkItemStatusId").val(data.search.wis);
            $("#PriorityLevel").val(data.search.pl);
        },
        "ajax": {
            "url": "/api/WorkItemData",
            "type": "POST",
            "data": function (data) {
                data.search.ln = $("#LeaseNumber").val();
                data.search.nn = $("#NoticeNumber").val();
                data.search.vin = $("#Vin").val();
                data.search.tcn = $("#TaxCollectorName").val();
                data.search.wit = $("#WorkItemTypeId").val();
                data.search.wis = $("#WorkItemStatusId").val();
                data.search.pl = $("#PriorityLevel").val();
            },
            headers: {
                'RequestVerificationToken': window.utc_aft
            }
        },
        "createdRow": function (r, d) {
            if (d.IsUrgent === true) {
                $(r).addClass("danger-row");
            }
        },
        "order":[[3,"desc"]],
        "columns": [
            {
                "className": "td-nowrap",
                "data": "NoticeNumber",
                "render":
                    function (data, type, row, meta) {
                        return '<a href="/workitem/Detail/' +
                            row.WorkItemId +
                            '">' + data + "</a>";
                    }
            },
            {
                "className": "td-nowrap",
                "data": "WorkItemTypeName"
            },
            {
                "className": "td-nowrap",
                "data": "WorkItemStatusName",
                "orderable": false
            },
            {
                "className": "td-nowrap text-center",
                "data": "IsUrgentString"
            },
            {
                "className": "td-nowrap",
                "data": "TaxCollectorName"
            },
            {
                "className": "td-nowrap text-center",
                "data": "CreateDateTime",
                "render":
                    function (data) {
                        if (null !== data) {
                            return new Date(Date.parse(data)).toLocaleDateString();
                        }
                        return "";
                    }
            },
            {
                "className": "td-nowrap text-center",
                "data": "LastUpdated",
                "render":
                    function (data) {
                        if (null !== data) {
                            return new Date(Date.parse(data)).toLocaleDateString();
                        }
                        return "";
                    },
                "orderable": false
            }
        ]
    });
    $("#workitem_search_table_clear").on("click",
        function () {
            table.state.clear();
            window.location.reload();
        });
    $("#workitem_search_table_run").on("click",
        function () {
            table.draw();
        });
});
$(document).ready(function () {
    var h = $("#ActivityTimeHour").val();
    var m = $("#ActivityTimeMinute").val();
    var t = $("#ActivityMeridian").val();

    $("#hour").text((h % 12).toString().padStart(2,"0"));
    $("#minute").text(m.padStart(2, "0"));
    $("#meridian").text(t);
});