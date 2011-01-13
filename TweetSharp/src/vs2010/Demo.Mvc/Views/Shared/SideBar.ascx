<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="TweetSharpMvc.Security"%>
<% if (UserManager.HasCredentials)
   { %>
<script language="javascript" type="text/javascript">
    $(function() {
        $("#logout").click(
        function() { window.location = "/Account/Logout"; });
    });
</script>
<div id="navcontainer">
<ul id="navlist">
<li id="active"><a href="/" id="navHomepage">Homepage</a></li>
<li><a href="/Updates" id="navUpdates">Updates</a></li>
<li><a href="/Mentions" id="navMentions">Mentions</a></li>
<li><a href="/Followers" id="navFollowers">Followers</a></li>
</ul>
</div>
<div id="loginInfo">
    <div>Logged in as:</div><div class="username"><%= UserManager.Username%></div>
    <div>(<a href="#" id="logout">logout</a>)</div>
</div>
<div id="credits">
<div>Written with <a href="http://code.google.com/p/tweetsharp/" target="_blank">TweetSharp</a></div>
</div>
<% } %>