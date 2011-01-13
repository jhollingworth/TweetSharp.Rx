<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Followers
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../Scripts/followers.js" type="text/javascript"></script>
     <% Html.BeginForm(); %>
    <h3 id="title">Followers for <%=TweetSharpMvc.Security.UserManager.Username %></h3>   
    <div id="results"></div>
    <% Html.EndForm(); %>
</asp:Content>
