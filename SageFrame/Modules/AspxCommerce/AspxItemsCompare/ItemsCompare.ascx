<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItemsCompare.ascx.cs"
    Inherits="Modules_AspxCompareItems_ItemsCompare" %>

<script type="text/javascript" language="javascript">
	//<![CDATA[
	    var enableCompareItem='<%=EnableCompareItems %>';
	//]]>
</script>

<div id="divCompareItems" class="cssClassCommonSideBox">
    <h2 id="h2compareitems">
   <label><span class='cssClassCompareItem'>My Compared Items []</span></label>     
    </h2>
    <div class="cssClassCommonSideBoxTable">
        <table width="100%" border="0" cellspacing="0" cellpadding="0" id="tblCompareItemList">
            <tbody>
                <tr><td></td></tr>
            </tbody>
        </table>
        <div class="cssClassButtonWrapper" id="compareItemBottons" style="display:none">
            <button type="button" onclick="ItemsCompare.ClearAll();">
                <span><span>Clear All</span></span></button>
            <button type="button" id="btncompare">
                <span><span>Compare</span></span></button>
        </div>
    </div>
</div>


