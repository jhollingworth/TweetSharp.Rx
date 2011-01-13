<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master"  Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Account Page
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
    <p>You are currently not logged in</p>
    <%
        Html.BeginForm(); %>
<label for="username">User name: </label><input type="text" id="username" name="username" />
<label for="password">Password: </label><input type="password" id="password"  name="password" />
<input type="submit" value="Login" />
<% Html.EndForm(); %>
    </div>
    </asp:Content>
