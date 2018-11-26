var GigsController = function (attendanceService) {
    var button;
    var done = function () {
        //if its going make it going? other wise leave it going.
        var text = (button.text() == "Going") ? "Going" : "Going";

        button.toggleClass("btn-info").toggleClass("btn-default").text(text);
    };
    var fail = function () {
        alert("Something Failed in not Going?");
    };

    var init = function (container) {
        //dom: document object model works only for elements currently in dom;
        //$(".js-toggle-attendance").click(toggleAttendance);
        $(container).on("click", ".js-toggle-attendance", toggleAttendance);//with this well only have one isntance of toggle attendance in memory

    };

    var toggleAttendance = function (e) {
        button = $(e.target);

        var gigId = button.attr("data-gig-id");

        if (button.hasClass("btn-default"))
            attendanceService.createAttendance(gigId, done, fail);
        else
            attendanceService.deleteAttendance(gigId, done, fail);
    };


    

    return {
        init: init
    }
}(AttendanceService);