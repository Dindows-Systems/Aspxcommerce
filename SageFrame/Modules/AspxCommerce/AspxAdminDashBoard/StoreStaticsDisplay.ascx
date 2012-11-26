<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StoreStaticsDisplay.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxAdminDashBoard_StoreStaticsDisplay" %>

<script type="text/javascript">
    //<![CDATA[

    //]]>
</script>

<div class="cssClassSelectChart">
    <label>
        <b>Select Range:</b></label>
    <select id="ddlRange" class="cssClassDropDown">
        <option value="24h">Show Today's Report</option>
        <option value="7d">Last Week</option>
        <option value="1m">Current Month</option>
        <option value="1y">Year To Date</option>
    </select><br />
    <br />
<div id="divChartType">
    <label id="lbla">
        <b>Select Chart Type:</b></label>
    <select id="ddlChartType" class="cssClassDropDown">
        <option value="1">Bar</option>
        <option value="2">Pie</option>
        <option value="3">Line</option>
    </select></div></div>
<div id="div24hours" style="display:none">
</div>
<div id="divLW" style="display:none">
</div>
<div id="divCM" style="display:none">
</div>
<div id="divYear" style="display:none">
</div>
