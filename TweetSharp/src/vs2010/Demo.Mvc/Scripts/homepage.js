
$(function() {
    $("#navHomepage").addClass("current");
    $("#updateMyStatus").click(function() { UpdateStatus(); });
    $("#statusUpdate").keyup(function() { limitChars('statusUpdate', 140, 'charlimitinfo'); });

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

function GetTimeline(callback) {
    $("#results").html(loadingHTML);
    $.getJSON("/Twitter/GetTimeline",
          {},
          function(response) {
              $("#results").empty();
              if (response.ResponseCode == 1) {
                  $("#results").html("<div class='error'>" + response.Message + "</div>");
              } else {
                  $("#results").html(CreateTimelineView(response.Data));
              }
              if (callback != null) callback();
           });
}

function UpdateStatus() {
    var toUpdate = $('#statusUpdate').val();
    $('#updateMyStatus').attr("disabled", true);
    $.getJSON("/Twitter/UpdateStatus",
    { statusUpdate: toUpdate },
          function(response) {
              if (response.ResponseCode == 1) {
                  $("#results").html("<div class='error'>" + response.Message + "</div>");
                  $("#updateMyStatus").attr("disabled", false);
              } else {
                  $("#lastUpdate").empty();
                  $('<div>' + response.Data.Text + '</div>').appendTo('#lastUpdate');
                  $('#statusUpdate').val('');

                  //Update timeline and callback to enable Update button again
                  GetTimeline(function() { $("#updateMyStatus").attr("disabled", false) });
              }
          });
}
