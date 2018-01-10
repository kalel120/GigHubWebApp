var FollowingService = function () {
    var followArtist = function (artistId, doneFollowing, failedFollowing) {
        $.post("/api/following", { artistId: artistId })
            .done(doneFollowing)
            .fail(failedFollowing);
    };

    var unfollowArtist = function (artistId, doneFollowing, failedFollowing) {
        $.ajax({
            url: "/api/following/" + artistId,
            method: "DELETE"
        })
            .done(doneFollowing)
            .fail(failedFollowing);
    };

    return {
        followArtist: followArtist,
        unfollowArtist: unfollowArtist
    }
}();