var GigDetailsController = function(followingService) {
    var followButton;

    var init = function() {
        $(".js-toggle-followArtist").click(toggleFollowing);
    };

    var toggleFollowing = function(event) {
        followButton = $(event.target);

        var artistId = followButton.attr("data-artist-id");

        if (followButton.hasClass("btn-default"))
            followingService.followArtist(artistId, doneFollowing, failedFollowing);
        else
            followingService.unfollowArtist(artistId, doneFollowing, failedFollowing);
    };

    var doneFollowing = function() {
        var text = (followButton.text() == "Follow?") ? "Following" : "Follow?";
        followButton.toggleClass("btn-info").toggleClass("btn-default").text(text);
    };

    var failedFollowing = function() {
        alert("Something is wrong!");
    };


    return {
        init: init
    }

}(FollowingService);