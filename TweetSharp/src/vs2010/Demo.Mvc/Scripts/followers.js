$(function() {
    $("#navFollowers").addClass("current");

    $.ajaxSetup({ cache: false });

    $("#updateMyStatus").ajaxComplete(function(ev, request, settings) {
        // Block 401 unauthorized by redirect to /Account
        // substring hack is to get around ASP.NET forms authentication auto redirect to a 200 
        if (request.status == 401 || request.responseText.substring(4, 1) == "401") {
            window.location = "/Account";
        }
    });
    
    // Load initial timeline
    GetTimeline();
});

function GetTimeline() {
    $("#results").html(loadingHTML);
    $.getJSON("/Twitter/GetFollowers",
          { },
          function(response) {
              $("#results").empty();
              if (response.ResponseCode == 1) {
                  $("#results").html("<div class='error'>" + response.Message + "</div>");
              } else {
                    $("#results").html(CreateUsersView(response.Data));
              }
          });
}

function CreateUsersView(data) {
    var toReturn = '';
    $.each(data, function(i, user) {
        toReturn += '<div class="twit">'
                    + '<img src="' + user.ProfileImageUrl + '" class="twitPic" />'
                    + '<div class="twitText">'
                        + '<a href="/Updates/' + user.ScreenName + '">@' + user.ScreenName + '</a>'
                        + ' ( ' + user.Name + ')'
                    + '</div>'
                    + '<div class="twitText">' + user.Description + '</div>'
                    + '</div>';
    });
    return toReturn;
}