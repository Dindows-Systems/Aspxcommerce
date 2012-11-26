<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AttributesSetManage.ascx.cs"
    Inherits="Modules_AspxAttributesManagement_AttributesSetManage" %>

<script type="text/javascript">
    //<![CDATA[
    var lblAttributeSetInfo = "<%= lblAttributeSetInfo.ClientID %>";    
    //]]>
</script>

<!-- Grid -->
<div id="divAttribSetGrid">
    <td class="cssClassBodyContentBox">
        <div class="cssClassCommonBox Curve">
            <div class="cssClassHeader">
                <h2>
                    <asp:Label ID="lblAttrSetsGridHeading" runat="server" Text="Manage Attribute Sets"></asp:Label>
                </h2>
                <div class="cssClassHeaderRight">
                    <div class="cssClassButtonWrapper">
                        <p>
                            <button type="button" id="btnAddNewSet">
                                <span><span>Add New Set</span></span></button>
                        </p>
                        <div class="cssClassClear">
                        </div>
                    </div>
                </div>
                <div class="cssClassClear">
                </div>
            </div>
            <div class="cssClassGridWrapper">
                <div class="cssClassGridWrapperContent">
                    <div class="cssClassSearchPanel cssClassFormWrapper">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <label class="cssClassLabel">
                                        Attribute Set Name:</label>
                                    <input type="text" id="txtSearchAttributeSetName" class="cssClassTextBoxSmall" />
                                </td>
                                <td>
                                    <label class="cssClassLabel">
                                        IsActive:</label>
                                    <select id="ddlIsActive" class="cssClassDropDown">
                                        <option value="">-- All -- </option>
                                        <option value="True">Yes </option>
                                        <option value="False">No </option>
                                    </select>
                                </td>
                                <td>
                                    <label class="cssClassLabel">
                                        Used In System:</label>
                                    <select id="ddlUserInSystem" class="cssClassDropDown">
                                        <option value="">--All--</option>
                                        <option value="True">Yes</option>
                                        <option value="False">No</option>
                                    </select>
                                </td>
                                <td>
                                    <div class="cssClassButtonWrapper cssClassPaddingNone">
                                        <p>
                                            <button type="button" onclick="AttributeSetManage.SearchAttributeSetName()">
                                                <span><span>Search</span></span></button>
                                        </p>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="loading">
                        <img id="ajaxAttributeSetMgmtImage" src=""  alt="loading...." title="loadin...."/>
                    </div>
                    <div class="log">
                    </div>
                    <table id="gdvAttributeSet" width="100%" border="0" cellpadding="0" cellspacing="0">
                    </table>
                </div>
            </div>
        </div>
</div>
<!-- End of Grid -->
<div id="divAttribSetAddForm" style="display:none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblAttrSetsFormHeading" runat="server" Text="Add New Attribute Set"></asp:Label>
            </h2>
        </div>
        <div class="cssClassFormWrapper">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="cssClassPadding tdpadding">
                <tr>
                    <td>
                        <asp:Label ID="lblAttributeSetName" runat="server" Text="Name:" CssClass="cssClassLabel"></asp:Label>
                        <span class="cssClassRequired">*</span>
                    </td>
                    <td class="cssClassTableRightCol">
                        <input type="text" class="cssClassNormalTextBox" name="" id="txtAttributeSetName"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblType" runat="server" Text="Based On:" CssClass="cssClassLabel"></asp:Label>
                        <span class="cssClassRequired">*</span>
                    </td>
                    <td class="cssClassTableRightCol">
                        <select class="cssClassDropDown" name="" id="ddlAttributeSet">
                        </select>
                    </td>
                </tr>
            </table>
        </div>
        <div class="cssClassButtonWrapper">
            <p>
                <button id="btnBackAdd" type="button">
                    <span><span>Back</span></span>
                </button>
            </p>
            <p>
                <button id="btnSaveAttributeSet" type="button">
                    <span><span>Save</span></span>
                </button>
            </p>
        </div>
        <div class="cssClassClear">
        </div>
    </div>
</div>
<div id="divAttribSetEditForm" style="display:none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblAttributeSetInfo" runat="server" CssClass="cssClassLabel"></asp:Label>
            </h2>
        </div>
        <div class="cssClassFormWrapper">
            <table cellspacing="0" cellpadding="0" border="0" width="100%" class="cssClassPadding">
                <tr>
                    <td width="50%">
                        <h3>
                            <asp:Label ID="lblAttributeNameTitle" runat="server" Text="Edit Set Name" CssClass="cssClassLabel"></asp:Label>
                        </h3>
                        <table cellspacing="0" cellpadding="0" border="0" width="100%" class="cssClassPadding">
                            <tr>
                                <td>
                                    <asp:Label ID="lblAttributeSetNameTitle" runat="server" Text="Name:" CssClass="cssClassLabel"></asp:Label>
                                    <span class="cssClassRequired">*</span>
                                </td>
                                <td class="cssClassTableRightCol">
                                    <input type="text" class="cssClassNormalTextBox" name="" id="txtOldAttributeSetName"/>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="50%">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <h3>
                                        <asp:Label ID="lblGroups" runat="server" Text="Groups" CssClass="cssClassLabel"></asp:Label>
                                    </h3>
                                </td>
                                <td>
                                    <div class="cssClassButtonWrapper cssClassPaddingNone">
                                        <p>
                                            <button type="button" id="btnAddNewGroup">
                                                <span><span>Add New Group</span></span></button>
                                        </p>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <div class="contextMenu" id="myMenu1">
                                        <ul>
                                            <li id="rename" class="cssClassSeparator">
                                                <img runat="server" id="imgRename" src="" alt="Rename" title="Rename" />
                                                <b>Rename</b></li>
                                            <li id="delete" class="cssClassSeparator">
                                                <img runat="server" id="imgDelete" src="" alt="Delete" title="Delete" />
                                                <b>Delete</b></li>
                                            <li id="remove" class="cssClassSeparator">
                                                <img runat="server" id="imgRemove" src="" alt="Remove" title="Remove" />
                                                <b>Remove</b></li>
                                        </ul>
                                    </div>
                                    <div id="dvTree" style="float: left;">
                                    </div>
                                    <%--<div>
                                                                                                            <a href="#" id="aCollapse" onclick="$('#tree').tree('closeNode', $('#tree').find('li'));">
                                                                                                                Collapse all</a> | <a href="#" id="aExpand" onclick="$('#tree').tree('openNode', $('#tree').find('li'));">
                                                                                                                    Expand all</a>
                                                                                                        </div>--%>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div class="cssClassButtonWrapper">
            <p>
                <button id="btnBackEdit" type="button">
                    <span><span>Back</span></span>
                </button>
            </p>
            <p>
                <button class="btnResetEdit" type="button">
                    <span><span>Reset</span></span>
                </button>
            </p>
            <p>
                <button class="btnDeleteAttributeSet" type="button" style="display:none">
                    <span><span>Delete</span></span>
                </button>
            </p>
            <p>
                <button class="btnUpdateAttributeSet" type="button">
                    <span><span>Save</span></span>
                </button>
            </p>
        </div>
    </div>
</div>
