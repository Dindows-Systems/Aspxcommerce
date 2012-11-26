<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PopularTags.ascx.cs" Inherits="Modules_AspxPopularTags_PopularTags" %>

<script type="text/javascript">
    //<![CDATA[
    var popularTagsCount = '<%=NoOfPopTags %>';
  //]]>
</script>

<div class="cssClassCommonSideBox">
    <h2>
        <asp:Label ID="lblPopularTagsTitle" runat="server" Text="Popular Tags" CssClass="cssClassPopularTags"></asp:Label>
    </h2>
    <div id="divPopularTags" class="cssClassPopularTags">
    </div>
    <div class="cssClassClear">
    </div>
    <div id="divViewAllTags" class="cssClassViewAllTags">
    </div>
    <div class="cssClassClear">
    </div>
</div>
