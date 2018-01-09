var GigsController = function () {
    var button;

    var init = function () {
        $(".js-toggle-attendance").click(toggleAttendance);
    };

    var toggleAttendance = function (event) {
        button = $(event.target);
        if (button.hasClass("btn-default")) {
            $.post("/api/attendances", { gigId: button.attr("data-gig-id") })
                .done(done)
                .fail(fail);
        } else {
            $.ajax({
                url: "/api/attendances/" + button.attr("data-gig-id"),
                method: "DELETE"
            })
                .done(done)
                .fail(fail);
        }
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
}();