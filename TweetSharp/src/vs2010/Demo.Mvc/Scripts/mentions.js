$(function() {
    $("#navMentions").addClass("current");

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
    $.getJSON("/Twitter/GetMentions",
          { },
          function(response) {
              $("#results").empty();
              if (response.ResponseCode == 1) {
                  $("#results").html("<div class='error'>" + response.Message + "</div>");
              } else {
                $("#results").html(CreateTimelineView(response.Data));
              }
          });
}
