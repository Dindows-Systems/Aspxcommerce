<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EditsPolling.ascx.cs"
    Inherits="Modules_Polling_EditsPolling" %>

<script type="text/javascript">
    var Arr = new Array();
    var cID = "";
    var rid = 1;
    var RowID = '<%=rindex %>';
    $(function() {

        var radiobtnid = '#' + '<%=gvListPoll.ClientID %>';
        var checks = $(radiobtnid + ' input[type="radio"]');
        $(checks).bind("click", function() {
            $(checks).attr("checked", false);
            $(this).attr("checked", true);
        });
    });
    function showRowIndexs() {
        var i = 0;
        $("#tableChoices tr td.tdLabel").each(function() {
            i++;
            $(this).html("Choice " + i);
        });
    }

    function addFormField() {
        var ctrlID = $("#<%= hidRowIndex.ClientID %>").val();
        if (RowID == null || RowID == '') {
            rid++;
            $("#tableChoices").append("<tr class='row' id='pRow" + rid + "'><td width='200' class='tdLabel'>Choice:</td><td><input class='text' type='text' name='txtChoice" + rid + "' id='txtChoice" + rid + "'> <a href='#' onClick='removeFormField(\"#pRow" + rid + "\"); return false;'>Remove</a></td></tr>");
            showRowIndexs(rid);
        }
        else {
            RowID++;
            $("#tableChoices").append("<tr class='row' id='pRow" + RowID + "'><td width='200' class='tdLabel'>Choice:</td><td><input class='text' type='text' name='txtChoice" + RowID + "' id='txtChoice" + RowID + "'> <a href='#' onClick='removeFormField(\"#pRow" + RowID + "\"); return false;'>Remove</a></td></tr>");
            showRowIndexs(RowID);
        }
    }

    function removeFormField(id) {
        cID = $("input:hidden", id).val();
        $(id).remove();
        if (cID > 0) //call the Page method using JQuery ajax 
        {
            Arr.push(cID);
        }
        showRowIndexs();
    }
    $('#imbAddNewImage').live("click", function() {
        $('#txtChoice0').val('');
        $('#txtChoice1').val('');
        $("#tableChoices").find("tr:gt(1)").remove();
        $('#<%=divform.ClientID %>').attr("style", "display:block");
        $('#<%=divManipulateData.ClientID %>').attr("style", "display:none");
    });

    function DeleteChoice() {
        rid = null;
        RowID = null;
        $.each(Arr, function(index, value) {
            var Choiceid = value;
            DeleteChoiceField(Choiceid);
        });
    }
    function DeleteChoiceField(id) {
        $.ajax(
            {
                type: "POST",
                url: PollingEditServicePath + "Services/PollingWebService.asmx/DeletePollChoice",
                data: JSON2.stringify({ cID: parseInt(id) }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false
            });
    }
        

</script>

<h3>
    Add/Edit Poll</h3>
<br />
<div runat="server" id="divMsg" class="mInfo" visible="false">
</div>
<div id="divManipulateData" runat="server">
    <div class="cssClassButtonWrapper">
        <p>
            <button id="imbAddNewImage" type="button">
                <span>Add new poll</span>
            </button>
        </p>
    </div>
    <div class="cssClassGridWrapper">
        <asp:GridView ID="gvListPoll" runat="server" AutoGenerateColumns="false" EmptyDataText="No record to display"
            AllowPaging="true" PageSize="10" Width="100%" OnPageIndexChanging="gvListPoll_PageIndexChanging"
            OnRowCommand="gvListPoll_RowCommand" OnRowDeleting="gvListPoll_RowDeleting" OnRowEditing="gvListPoll_RowEditing">
            <Columns>
                <asp:TemplateField HeaderText="IsVisible" HeaderStyle-CssClass="cssClassColumnCheckBox"
                    meta:resourcekey="TemplateFieldResource1">
                    <ItemTemplate>
                        <asp:HiddenField ID="hdfPollID" runat="server" Value='<%# Eval("PollID") %>' />
                        <asp:RadioButton ID="rdbActive" runat="server" CausesValidation="False" Checked='<%# Convert.ToBoolean(Eval("Active")) %>' />
                    </ItemTemplate>
                    <HeaderStyle VerticalAlign="Top" />
                    <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Question">
                    <ItemTemplate>
                        <asp:Label ID="lblQuestion" runat="server" Text='<%#Eval("Question") %>'> ></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ChoiceCount">
                    <ItemTemplate>
                        <asp:Label ID="lblChoiceCount" runat="server" Text='<%#Eval("ChoiceCount") %>'> ></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="TotalVotes">
                    <ItemTemplate>
                        <asp:Label ID="lblVoteCount" runat="server" Text='<%#Eval("TotalVotes") %>'> ></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Edit">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" CommandArgument='<%#Eval("PollID")%>'
                            CommandName="Edit" ImageUrl='<%# GetTemplateImageUrl("imgedit.png", true) %>' />
                    </ItemTemplate>
                    <HeaderStyle CssClass="cssClassColumnDelete" VerticalAlign="Top" />
                    <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete">
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgDelete" runat="server" CausesValidation="False" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"PollID") %>'
                            CommandName="Delete" ToolTip="Delete" ImageUrl='<%# GetTemplateImageUrl("imgdelete.png", true) %>' />
                    </ItemTemplate>
                    <HeaderStyle CssClass="cssClassColumnDelete" VerticalAlign="Top" />
                    <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>
            </Columns>
            <HeaderStyle CssClass="cssClassHeadingOne" />
            <RowStyle CssClass="cssClassAlternativeOdd" />
            <AlternatingRowStyle CssClass="cssClassAlternativeEven" />
        </asp:GridView>
    </div>
    <div id="divVisibleButton" class="cssClassButtonWrapper" runat="server">
        <asp:ImageButton ID="btnSaveIsVisible" runat="server" Style="height: 16px" OnClick="btnSaveIsVisible_Click" />
        <asp:Label ID="lblSaveIsVisible" runat="server" Text="Save" AssociatedControlID="btnSaveIsVisible"
            CssClass="cssClassHtmlViewCursor"></asp:Label>
    </div>
</div>
<div id="divform" runat="server" class="cssClassFormWrapper" style="display: none;">
    <table id="tblQtn" width="100%">
        <tr>
            <td>
                <asp:Label runat="server" ID="lblQuestion" CssClass="cssClassFormLabel" AssociatedControlID="txtQuestion"
                    Text="Question" />
            </td>
            <td>
                :
            </td>
            <td>
                <asp:TextBox ID="txtQuestion" runat="server" CssClass="cssClassNormalTextBox" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="<img align='absmiddle' src='../Modules/Polling/images/warn.gif' /> Required"
                    ControlToValidate="txtQuestion" ValidationGroup="body" SetFocusOnError="true" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server" ID="lblBlock" AssociatedControlID="rdoCookie" CssClass="cssClassFormLabel"
                    Text="Block repeated voting by" />
            </td>
            <td>
                :
            </td>
            <td>
                <asp:RadioButton CssClass="cssClassRadioButton cssClassFormLabel" Checked="true"
                    GroupName="block" ID="rdoCookie" runat="server" Text=" Cookie(safest)" />
                <asp:RadioButton CssClass="cssClassRadioButton cssClassFormLabel" GroupName="block"
                    ID="rdoIP" runat="server" Text=" IP Address" ToolTip="This may cause problems for multiple voters on same network" />
                <asp:RadioButton CssClass="cssClassRadioButton cssClassFormLabel" GroupName="block"
                    ID="rdoNone" runat="server" Text=" Don't block" />
            </td>
        </tr>
    </table>
    <div runat="server" id="divChoices">
        <table id="tableChoices" width="100%">
            <tr id="pRow0">
                <td width="200" class="tdLabel">
                    Choice 1
                </td>
                <td>
                    <input type="text" class="text" id="txtChoice0" name="txtChoice0" />
                    <a onclick="removeFormField('#pRow0'); return false;" href="#">Remove</a>
                </td>
            </tr>
            <tr id="pRow1">
                <td width="200" class="tdLabel">
                    Choice 2
                </td>
                <td>
                    <input type="text" class="text" id="txtChoice1" name="txtChoice1" />
                    <a onclick="removeFormField('#pRow1'); return false;" href="#">Remove</a>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField runat="server" ID="hidRowIndex" Value="2" />
    <!-- by default there will be two inputs available to insert choices -->
    <asp:HiddenField ID="hidPollID" runat="server" />
    <div class="cssClassButtonWrapper">
        <p>
            <button type="button" onclick="addFormField();return false;">
                <span>Add a new choice</span>
            </button>
        </p>
    </div>
    <div class="cssClassButtonWrapper">
        <asp:ImageButton ID="btnSave" runat="server" ValidationGroup="body" Style="height: 16px"
            OnClick="btnSave_Click" OnClientClick="return DeleteChoice()" />
        <asp:Label ID="lblSave" runat="server" Text="Save" AssociatedControlID="btnSave"
            CssClass="cssClassHtmlViewCursor"></asp:Label>
        <asp:ImageButton ID="btnCancel" runat="server" Style="height: 16px" OnClick="btnCancel_Click" />
        <asp:Label ID="lblSaveCancel" runat="server" Text="Cancel" AssociatedControlID="btnCancel"
            CssClass="cssClassHtmlViewCursor"></asp:Label>
    </div>
</div>
