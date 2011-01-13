<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="TweetSharp.Model"%>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
TweetSharpMVC Demo
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../Scripts/homepage.js" type="text/javascript"></script>
 
    <% Html.BeginForm(); %>
    <h3>What are you doing now?</h3>   
    <textarea id="statusUpdate" name="statusUpdate" cols="50" rows="5" style="width:100%;height:60px;"></textarea>
    <span id="charlimitinfo" style=" float:right; color:red; font-size:11px;" >Maximum 140 characters</span>
    <input id="updateMyStatus" type="button" value="Update" />  
    <div id="lastUpdate"></div>
    <div id="results"></div>
  
    <% Html.EndForm(); %>
    </asp:Content>
