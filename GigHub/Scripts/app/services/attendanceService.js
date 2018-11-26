var AttendanceService = function () {
    var createAttendance = function (gigId, done, fail) {
        $.post("/api/attendances", { gigId: gigId })
            .done(
                done
                //function () {button.removeClass("btn-default").addClass("btn-info").text("Going");}
            )
            .fail(
                fail
                //function () { alert("Something Failed in Going!"); }
            );
    };
    var deleteAttendance = function (gigId, done, fail) {
        $.ajax({
                url: "/api/attendances/" + gigId,
                method: "DELETE"
            })
            .done(done)
            .fail(fail);
    };

    return {
        createAttendance: createAttendance,
        deleteAttendance: deleteAttendance
    }
}();