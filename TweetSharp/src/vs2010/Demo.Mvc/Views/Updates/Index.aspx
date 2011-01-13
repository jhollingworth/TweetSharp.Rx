<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Updates
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script language="javascript" type="text/javascript">
    var username = '<%=ViewData["username"] %>';
    </script>
    <script src="../../Scripts/updates.js" type="text/javascript"></script>
     <% Html.BeginForm(); %>
    <h3 id="title">Updates for <%=ViewData["username"] %></h3>   
    <div id="results"></div>
    <% Html.EndForm(); %>
</asp:Content>

