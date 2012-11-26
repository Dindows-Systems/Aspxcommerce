<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SimpleSearch.ascx.cs"
    Inherits="Modules_AspxGeneralSearch_SimpleSearch" %>

<script type="text/javascript">
    //<![CDATA[

    //]]>
</script>

 <div class="cssClassSageSearchWrapper">
 <%--<div class="cssClassFormWrapper">--%>
    <ul>
        <li> <select id="ddlSimpleSearchCategory" class="cssClassDropDown">
        <option value=""></option>
                    </select></li>
        <li>
            <input type="text" id="txtSimpleSearchText" class="cssClassSageSearchBox" /></li>
        <li>
            <input type="button" id="btnSimpleSearch" class="cssClassSageSearchButton" value="Go" /></li>
        <li>  <a href="#" id="lnkAdvanceSearch" class="cssClassAdvanceSearch"> Advanced Search</a></li>
    </ul>
    <%--<a href="#" id="lnkAdvanceSearch" class="cssClassAdvanceSearch">Go For Advanced Search</a>--%>
</div>
