var GigsController = function (attendanceService) {
    var button;

    var init = function (container) {
        $(container).on("click", ".js-toggle-attendance", toggleAttendance);
    };

    var toggleAttendance = function (event) {
        button = $(event.target);

        var gigId = button.attr("data-gig-id");
        if (button.hasClass("btn-default"))
            attendanceService.createAttendance(gigId, done, fail);
        else
            attendanceService.deleteAttendance(gigId, done, fail);
    };

    var done = function () {
        var text = (button.text() == "Will Go!") ? "Going?" : "Will Go!";
        button.toggleClass("btn-info").toggleClass("btn-default").text(text);
    };

    var fail = function () {
        alert("Something is wrong!");
    };

    return {
        init: init
    }
}(AttendanceService);