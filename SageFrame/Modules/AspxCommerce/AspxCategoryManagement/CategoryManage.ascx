<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoryManage.ascx.cs"
    Inherits="Modules_AspxCategoryManagement_CategoryManage" %>
<script type="text/javascript">
//<![CDATA[
    var categoryLargeThumbImageSetting = '<%=CategoryLargeThumbImage %>';
    var categoryMediumThumbImageSetting = '<%=CategoryMediumThumbImage %>';
    var categorySmallThumbImageSetting = '<%=CategorySmallThumbImage %>';
    var categoryTitleLabel = '<%=lblCategoryTitle.ClientID %>';

//]]>
</script>

<div>
    <input type="hidden" id="hdnCatNameTxtBox" /></div>
<div>
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="cssClassTableLeftCol">
                <div class="cssClassSideBox">
                    <div class="cssClassSideBoxNavi Curve">
                        <h2>
                            Available Categories</h2>
                        <%--<div>
                    <a href="#" id="aCollapse" onclick="$('#categoryTree').tree('closeNode', $('#categoryTree').find('li'));">
                      Collapse all</a> | <a href="#" id="aExpand" onclick="$('#categoryTree').tree('openNode', $('#categoryTree').find('li'));">
                        Expand all</a>
                  </div>--%>
                        <div id="CategoryTree_Container">
                        </div>
                    </div>
                </div>
            </td>
            <td>
                <div>
                    <div class="cssClassCommonBox Curve">
                        <div class="cssClassHeader">
                            <h2>
                               <%--Categories (ID: <span id="lblCategoryID">0</span>)--%>
                                <asp:Label ID="lblCategoryTitle" runat="server" Text="Category (ID: "></asp:Label><span id="lblCategoryID">0</span>
                                <asp:Label ID="lblclosing" runat="server" Text=" )"></asp:Label>
                            </h2>
                            <div class="cssClassHeaderRight">
                                <div class="cssClassButtonWrapper ">
                                    <p>
                                        <button type="button" class="" onclick="categoryMgmt.AddCategory()"><span><span>Add Category</span></span></button>
                                    </p>
                                    <p>
                                        <button type="button" class="" onclick="categoryMgmt.AddSubCategory()"><span><span>Add Sub Category</span></span></button>
                                    </p>
                                    <div class="cssClassClear">
                                    </div>
                                </div>
                            </div>
                            <div class="cssClassClear">
                            </div>
                        </div>
                        <div class="cssClassTabPanelTable">
                            <input type="hidden" id="CagetoryMgt_categoryID" value="0" />
                            <input type="hidden" id="CagetoryMgt_parentCagetoryID" value="0" />
                            <div id="dynForm" class="cssClassFormWrapper">
                            </div>
                        </div>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <div class="cssClassClear">
    </div>
</div>