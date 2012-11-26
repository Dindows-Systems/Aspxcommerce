<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EditBanner.ascx.cs" Inherits="Modules_Sage_Banner_EditBanner" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<script type="text/javascript">
    //<![CDATA[
    var baseUrl = '<%=modulePath %>';
    var editorID = '<%= _imageEditor.ClientID %>';
    jQuery(function() {

        jQuery('#' + editorID).Jcrop({
            onChange: showCoords,
            onSelect: showCoords
        });
    });
    function showCoords(c) {
        var tdX = document.getElementById('tdX');
        var tdY = document.getElementById('tdY');
        var tdWidth = document.getElementById('tdWidth');
        var tdHeight = document.getElementById('tdHeight');
        tdX.innerHTML = c.x;
        tdY.innerHTML = c.y;
        tdWidth.innerHTML = c.w;
        tdHeight.innerHTML = c.h;
        var xField = document.getElementById('<%= _xField.ClientID %>');
        var yField = document.getElementById('<%= _yField.ClientID %>');
        var widthField = document.getElementById('<%= _widthField.ClientID %>');
        var heightField = document.getElementById('<%= _heightField.ClientID %>');
        xField.value = c.x;
        yField.value = c.y;
        widthField.value = c.w;
        heightField.value = c.h;

    }
    $(document).ready(function() {
        //    $('#<%=imbCancel.ClientID%>').bind("click", function() {
        //            $('#<%=divEditBannerImage.ClientID%>').hide();
        //            $('#<%=divbannerImageContainer.ClientID%>').show();
        //        });

        $('#imbAddBanner').bind("click", function() {
            ShowPopUp('divAddBanner');
        });
        $('#fade').bind("click", function() {
            $('#fade,#divAddBanner').fadeOut();

        });
        $('#imbAddHtmlContent').bind("click", function() {
            ClearHTMLForm();
            $('#<%=divHtmlBannerContainer.ClientID%>').hide();
            $('#<%=divEditWrapper.ClientID%>').show();
        });
        //        $('#imbCancelEditor').bind("click", function() {
        //            $('#<%=divHtmlBannerContainer.ClientID%>').show();
        //            $('#<%=divEditWrapper.ClientID%>').hide();
        //        });
        $('#imbAddNewImage').bind("click", function() {
            ClearImageForm();
            $('#<%=divbannerImageContainer.ClientID%>').hide();
            $('#<%=divEditBannerImage.ClientID%>').show();
        });
        $('#lblReturnBack').bind("click", function() {
            $('#<%=pnlBannercontainer.ClientID%>').hide();
            $('#<%=pnlBannerList.ClientID%>').show();
        });
    });
    function ClearImageForm() {
        $("#<%= txtCaption.ClientID %>").val('');
        $("#<%= txtReadButtonText.ClientID %>").val('');
        $("#<%= txtBannerDescriptionToBeShown.ClientID %>").val('');
        $("#<%= imgEditBannerImageImage.ClientID %>").hide();
    }
    function ClearHTMLForm() {
        $("#<%= txtBody.ClientID %>").val('');
        $("#<%= imgEditNavImage.ClientID %>").hide();
    }

    function ShowPopUp(popupid) {
        $('#' + popupid).fadeIn();
        var popuptopmargin = ($('#' + popupid).height() + 10) / 2;
        var popupleftmargin = ($('#' + popupid).width() + 10) / 2;
        $('#' + popupid).css({
            'margin-top': -popuptopmargin,
            'margin-left': -popupleftmargin
        });
    }
    function GetRadioButtonListSelectedValue(radioButtonList) {
        for (var i = 0; i < radioButtonList.rows.length; ++i) {

            if (radioButtonList.rows[i].cells[0].firstChild.checked) {
                $('#trddlPagesLoad').show();
                $('#trtxtWebUrl').hide();

            }
            else {
                $('#trddlPagesLoad').hide();
                $('#trtxtWebUrl').show();
            }

        }

    }
    $('#txtWebUrl').live("change", function() {
        var DemoUrl = $(this).val();
        if ($(this).val().length > 0) {
            if (!$(this).val().match(/^http/)) {
                $(this).val('http://' + DemoUrl);
            }
        }
    });


    //]]> 
</script>
<asp:Panel ID="pnlBannercontainer" Style="display: none;" runat="server" CssClass="cssClassFormWrapper"
    Width="100%">
  <cc1:TabContainer ID="SageBannerTabcontainer" runat="server" ActiveTabIndex="0" Width="100%">
    <cc1:TabPanel ID="tpSageBanner" runat="server">
      <HeaderTemplate>
        <asp:Label ID="lblBannerImage" runat="server" CssClass="cssClassFormLabel" Text='Banner Image' />
      </HeaderTemplate>
      <ContentTemplate>
        <p class="sfNote">
          <asp:Label ID="lblAddBanner" runat="server" Text=' In this section, you can add and manage Banner image.' />
        </p>
        <div id="divbannerImageContainer" runat="server">
          <div class="cssClassButtonWrapper sftype1">
            <label id="imbAddNewImage">
            
            <img id="imbAddnewbannerImage" alt="" src="<%=modulePath %>images/add.png" />
             Add Banner Image</label>
          </div>
          <div id="dvGrid" class="cssClassGridWrapper">
            <asp:GridView ID="gdvBannerImages" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            EmptyDataText="..........No Data Found.........." GridLines="None" OnPageIndexChanged="gdvBannerImages_PageIndexChanged"
                            OnRowCommand="gdvBannerImages_RowCommand" OnRowDataBound="gdvBannerImages_RowDataBound"
                            OnRowDeleting="gdvBannerImages_RowDeleting" OnRowEditing="gdvBannerImages_RowEditing"
                            Width="100%" OnPageIndexChanging="gdvBannerImages_PageIndexChanging" PageSize="6">
              <Columns>
               <asp:TemplateField HeaderText="S.N">
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1 %>
                    </ItemTemplate>
                    <HeaderStyle CssClass="cssCheckBoxHeader" />
                </asp:TemplateField>
              <asp:TemplateField>
                <ItemTemplate>
                  <asp:LinkButton ID="lnkImageEdit" runat="server" CommandArgument='<%# Eval("ImageID") %>'
                                            CommandName="Editimage" Text="Crop"></asp:LinkButton>
                </ItemTemplate>
                <HeaderTemplate> Crop Image </HeaderTemplate>
              </asp:TemplateField>
              <asp:TemplateField>
                <ItemTemplate>
                  <asp:Panel ID="pnlImage" runat="server">
                    <img id="bannerimgGrd" alt="image" height="50" src='<%#ResolveUrl("~/Modules/Sage_Banner/images/CroppedImages/"+Eval("ImagePath")) %>'
                                                width="50" /></asp:Panel>
                </ItemTemplate>
                <HeaderTemplate> Image </HeaderTemplate>
              </asp:TemplateField>
               <asp:TemplateField meta:resourcekey="TemplateFieldResource1" Visible="false">
                                    <ItemTemplate>
                                        <div>
                                            <asp:ImageButton ID="imgListUp" runat="server" CausesValidation="False" CommandArgument='<%# Eval("ImageID") %>'
                                                CommandName="SortUp" ImageUrl='<%# GetTemplateImageUrl("imgup.png", true) %>'
                                                ToolTip="Move Up" />
                                        </div>
                                        <div>
                                            <asp:ImageButton ID="imgListDown" runat="server" CausesValidation="False" CommandArgument='<%# Eval("ImageID") %>'
                                                CommandName="SortDown" ImageUrl='<%# GetTemplateImageUrl("imgdown.png", true) %>'
                                                ToolTip="Move Down"  />
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="cssClassColumnDelete" />
                                </asp:TemplateField>

              <asp:TemplateField>
                <ItemTemplate>
                  <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" CommandArgument='<%# Eval("ImageID") %>'
                                            CommandName="Edit" ImageUrl='<%# GetTemplateImageUrl("imgedit.png", true) %>' />
                </ItemTemplate>
                <HeaderStyle CssClass="cssClassColumnDelete" VerticalAlign="Top" />
                <ItemStyle VerticalAlign="Top" />
              </asp:TemplateField>
              
              <asp:TemplateField>
                <ItemTemplate>
                  <asp:ImageButton ID="imdDelete" runat="server" CausesValidation="False" CommandArgument='<%# Eval("ImageID") %>'
                                            CommandName="Delete" ImageUrl='<%# GetTemplateImageUrl("imgdelete.png", true) %>' />
                </ItemTemplate>
                <HeaderStyle CssClass="cssClassColumnDelete" VerticalAlign="Top" />
                <ItemStyle VerticalAlign="Top" />
              </asp:TemplateField>
              </Columns>
              <AlternatingRowStyle CssClass="cssClassAlternativeEven" />
              <PagerStyle CssClass="cssClassPageNumber" />
              <RowStyle CssClass="cssClassAlternativeOdd" />             
              <HeaderStyle CssClass="cssClassHeadingOne" />
            </asp:GridView>
          </div>
        </div>
        <div id="divEditBannerImage" runat="server">
          <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
              <td width="150"><asp:Label ID="lblReadMorePageType" runat="server" CssClass="cssClassFormLabel">Read Button Text</asp:Label></td>
              <td width="30"> : </td>
              <td><asp:RadioButtonList ID="rdbReadMorePageType" runat="server" CssClass="cssClassRadioButton"
                                    onclick="GetRadioButtonListSelectedValue(this);" RepeatDirection="Horizontal">
                  <asp:ListItem Value="0" Selected="True">Page</asp:ListItem>
                  <asp:ListItem Value="1">Web Url</asp:ListItem>
                </asp:RadioButtonList></td>
            </tr>
            <tr id="trddlPagesLoad">
              <td><asp:Label ID="lblReadMorePages" runat="server" CssClass="cssClassFormLabel">Redirect To:</asp:Label></td>
              <td width="30"> : </td>
              <td><asp:DropDownList ID="ddlPagesLoad" runat="server" CssClass="cssClassDropDown"> </asp:DropDownList></td>
            </tr>
            <tr id="trtxtWebUrl" style="display: none;">
              <td><asp:Label ID="lblWebUrl" runat="server" CssClass="cssClassFormLabel">Web Link</asp:Label></td>
              <td width="30"> : </td>
              <td><asp:TextBox ID="txtWebUrl" runat="server" CssClass="cssClassNormalTextBox"></asp:TextBox></td>
            </tr>
            <tr>
              <td><asp:Label ID="lblChooseFile" runat="server" CssClass="cssClassFormLabel">Choose Image</asp:Label></td>
              <td width="30"> : </td>
              <td><asp:FileUpload ID="fuFileUpload" runat="server" /></td>
            </tr>
            <tr>
              <td></td>
              <td></td>
              <td><asp:Image ID="imgEditBannerImageImage" runat="server" Visible="False" 
                      CssClass="sfBannerimage" /></td>
            </tr>
            <tr style="display: none">
              <td><asp:Label ID="lblCaptionDetail" runat="server" CssClass="cssClassFormLabel">Caption</asp:Label></td>
              <td width="30"> : </td>
              <td><asp:TextBox ID="txtCaption" runat="server" CssClass="cssClassNormalTextBox" TextMode="MultiLine"></asp:TextBox></td>
            </tr>
            <tr>
              <td><asp:Label ID="lblReadButtonText" runat="server" CssClass="cssClassFormLabel">Read Button Text</asp:Label></td>
              <td width="30"> : </td>
              <td><asp:TextBox ID="txtReadButtonText" runat="server" CssClass="cssClassNormalTextBox"></asp:TextBox></td>
            </tr>
            <tr>
              <td valign="top"><asp:Label ID="lblBannerDescriptionToBeShow" runat="server" Text="Banner Description:"
                                    CssClass="cssClassFormLabel"></asp:Label></td>
              <td width="30"> : </td>
              <td valign="top">
                  <FCKeditorV2:FCKeditor ID="txtBannerDescriptionToBeShown" 
                      runat="server" Height="350px" Width="100%" 
                                   ></FCKeditorV2:FCKeditor>
                <asp:CustomValidator ID="cvBannerDesc" runat="server" ErrorMessage="Description required."
                                    OnServerValidate="cvFckDescription_ServerValidate" ValidationGroup="text" /></td>
            </tr>
            <tr>
              <td></td>
              <td></td>
              <td><div class="cssClassButtonWrapper sfMarginnone">
                  <asp:ImageButton ID="imbSave" runat="server" ValidationGroup="text" OnClick="imbSave_Click" />
                  <asp:Label ID="lblSave" runat="server" Text="Save" AssociatedControlID="imbSave"></asp:Label>
                  <asp:ImageButton ID="imbCancel" runat="server" OnClick="imbCancel_Click" />
                  <asp:Label ID="lblCancel" runat="server" Text="Cancel" AssociatedControlID="imbCancel"></asp:Label>
                </div></td>
            </tr>
          </table>
        </div>
      </ContentTemplate>
    </cc1:TabPanel>
    <cc1:TabPanel ID="tpSageBannerHTML" runat="server">
      <HeaderTemplate> HTML Content </HeaderTemplate>
      <ContentTemplate>
        <p class="sfNote">
          <asp:Label ID="lblHTMLContentAdd" runat="server" Text='In this section, you can add and manage Banner HTML Content.' />
        </p>
        <div id="divHtmlBannerContainer" runat="server">
          <div class="cssClassButtonWrapper sftype1">
            <label id="imbAddHtmlContent" class="sfAdd"> Add HTML Content</label>
          </div>
          <div id="divHTMLContent" runat="server" class="sfGridwrapper">
            <asp:GridView ID="gdvHTMLContent" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            EmptyDataText="..........No Data Found.........." GridLines="None" Width="100%"
                            OnRowCommand="gdvHTMLContent_RowCommand" PageSize="3" OnPageIndexChanging="gdvHTMLContent_PageIndexChanging">
              <Columns>
               <asp:TemplateField HeaderText="S.N">
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1 %>
                    </ItemTemplate>
                    <HeaderStyle CssClass="cssCheckBoxHeader" />
                </asp:TemplateField>
              <asp:TemplateField>
                <HeaderTemplate> BannerID </HeaderTemplate>
                <ItemTemplate>
                  <asp:Label ID="lblImageID" runat="server" Font-Bold="true" Text='<%# Eval("ImageID")%>' />
                </ItemTemplate>
              </asp:TemplateField>
              <asp:TemplateField>
                <HeaderTemplate> HTML Content </HeaderTemplate>
                <ItemTemplate>
                  <asp:Label ID="lblHTMLBodyText" runat="server" Font-Bold="true" Text='<%# Eval("HTMLBodyText")%>' />
                </ItemTemplate>
              </asp:TemplateField>
              <asp:TemplateField HeaderText="Edit">
                <ItemTemplate>
                  <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" CommandArgument='<%# Eval("ImageID") %>'
                                            CommandName="EditHTML" ImageUrl='<%# GetTemplateImageUrl("imgedit.png", true) %>' />
                </ItemTemplate>
                <HeaderStyle CssClass="cssClassColumnDelete" VerticalAlign="Top" />
                <ItemStyle VerticalAlign="Top" />
              </asp:TemplateField>
              <asp:TemplateField HeaderText="Delete">
                <ItemTemplate>
                  <asp:ImageButton ID="imdDelete" runat="server" CausesValidation="False" CommandArgument='<%# Eval("ImageID") %>'
                                            CommandName="DeleteHTML" ImageUrl='<%# GetTemplateImageUrl("imgdelete.png", true) %>' />
                </ItemTemplate>
                <HeaderStyle CssClass="cssClassColumnDelete" VerticalAlign="Top" />
                <ItemStyle VerticalAlign="Top" />
              </asp:TemplateField>
              </Columns>
              <PagerStyle CssClass="cssClassPageNumber" />
              <RowStyle CssClass="cssClassAlternativeOdd" />
              <AlternatingRowStyle CssClass="cssClassAlternativeEven" />           
               <HeaderStyle CssClass="cssClassHeadingOne" />
            </asp:GridView>
          </div>
        </div>
        <div id="divEditWrapper" runat="server">
          <div class="cssClassFormWrapper">
            <table cellspacing="0" width="100%" cellpadding="0" border="0" class="editorborder">
              <tr>
                <td><table cellspacing="0" cellpadding="0" width="100%" border="0" id="tblTextEditor"
                                        runat="server">
                    <tr>
                      <td><FCKeditorV2:FCKeditor ID="txtBody" runat="server" Height="350px"> </FCKeditorV2:FCKeditor></td>
                    </tr>
                  </table></td>
              </tr>
              <tr>
                <td style="display: none;"><asp:Label ID="lblNavigationImage" runat="server" Text="Navigation Image" CssClass="cssClassFormLabel"></asp:Label>
                  <asp:FileUpload ID="fluBannerNavigationImage" runat="server" CssClass="cssClassNormalFileUpload" />
                  <br />
                  <asp:Image ID="imgEditNavImage" runat="server" Visible="false" /></td>
              </tr>
            </table>
            <div class="cssClassButtonWrapper">
              <asp:ImageButton ID="imbSaveEditorContent" runat="server" ValidationGroup="save"
                                OnClick="imbSaveEditorContent_Click" />
              <asp:Label ID="lblSaveEditorContent" runat="server" Text="Save" AssociatedControlID="imbSaveEditorContent"></asp:Label>
              <asp:ImageButton ID="imgCancelHtmlContent" runat="server" OnClick="imgCancelHtmlContent_Click" />
              <asp:Label ID="lblcancelHtml" runat="server" Text="Cancel" AssociatedControlID="imgCancelHtmlContent"></asp:Label>
            </div>
          </div>
        </div>
      </ContentTemplate>
    </cc1:TabPanel>
  </cc1:TabContainer>
  <div class="sftype1 sfMargintop">
    <label id="lblReturnBack" class="cssClassFormLabel">
    <img alt="" src="<%=modulePath %>images/btnback.png" />
     Back</label>
  </div>
  </asp:Panel>
<asp:Panel ID="pnlBannerList" runat="server" meta:resourcekey="pnlPageListResource1"
    CssClass="cssClassFormWrapper">
  <table cellpadding="0" cellspacing="0" width="100%">
    <tr>
      <td width="20%"><asp:Label ID="lblBannerName" runat="server" CssClass="cssClassFormLabel">Banner Name</asp:Label></td>
      <td width="30"> : </td>
      <td><asp:TextBox ID="txtBannerName" runat="server" CssClass="cssClassNormalTextBox"></asp:TextBox>
      <asp:RequiredFieldValidator ID="rfvtxtBannerName" runat="server" ControlToValidate="txtBannerName"
                SetFocusOnError="true" ValidationGroup="bannername" ErrorMessage="*"
                CssClass="cssClasssNormalRed" Display="Dynamic"></asp:RequiredFieldValidator></td>
      <td><div class="cssClassButtonWrapper">
          <asp:ImageButton ID="imbSaveBanner" runat="server" CssClass="sfAdd" ValidationGroup="bannername" OnClick="imbSaveBanner_Click" />
          <asp:Label ID="lblSaveBanner" runat="server" Text="Add" AssociatedControlID="imbSaveBanner"></asp:Label>
        </div></td>
    </tr>
    <tr style="display: none">
      <td><asp:Label ID="lblBannerDescription" runat="server" CssClass="cssClassFormLabel">Description</asp:Label></td>
      <td><asp:TextBox ID="txtBannerDescription" runat="server" CssClass="cssClassNormalTextBox" TextMode="MultiLine"></asp:TextBox></td>
    </tr>
  </table>
  <div class="sfGridwrapper">
    <asp:GridView ID="gdvBannerList" runat="server" AutoGenerateColumns="False" GridLines="None"
            Width="100%" meta:resourcekey="gdvPageListResource1" OnRowDataBound="gdvBannerList_RowDataBound" OnRowCommand="gdvBannerList_RowCommand"
            OnPageIndexChanging="gdvBannerList_PageIndexChanging" PageSize="6">
      <Columns>
       <asp:TemplateField HeaderText="S.N">
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1 %>
                    </ItemTemplate>
                    <HeaderStyle CssClass="cssCheckBoxHeader" />
                </asp:TemplateField>
      <asp:TemplateField HeaderText="Banner Name" meta:resourcekey="TemplateFieldResource48">
        <ItemTemplate>
       
          <asp:HiddenField ID="hdnBannerID" runat="server" Value='<%# Eval("BannerID") %>' />
          <asp:HiddenField ID="hdnBannerName" runat="server" Value='<%# Eval("BannerName") %>' />
          <asp:LinkButton ID="lnkBannerName" runat="server" Value='<%# Eval("BannerName") %>'
                            Text='<%# Eval("BannerName")%>' CommandArgument='<%# Eval("BannerID")%>' CommandName="BannerEdit"
                            meta:resourcekey="lnkPageNameResource1"></asp:LinkButton>
        </ItemTemplate>
      </asp:TemplateField>
     
      <asp:TemplateField HeaderText="Edit">
        <ItemTemplate>
          <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" CommandArgument='<%# Eval("BannerID")%>' CommandName="BannerEdit" ImageUrl='<%# GetTemplateImageUrl("imgedit.png", true) %>' />
        </ItemTemplate>
        <HeaderStyle CssClass="cssClassColumnDelete" VerticalAlign="Top" />
        <ItemStyle VerticalAlign="Top" />
      </asp:TemplateField>
      <asp:TemplateField HeaderText="Delete">
        <ItemTemplate>
          <asp:ImageButton ID="imbDeletePage" ImageUrl='<%# GetTemplateImageUrl("imgdelete.png", true) %>'
                            runat="server" CommandName="BannerDelete" AlternateText="Delete" CommandArgument='<%# Eval("BannerID") %>'/>
        </ItemTemplate>
        <HeaderStyle CssClass="cssClassColumnDelete" />
      </asp:TemplateField>
      </Columns>
      <AlternatingRowStyle CssClass="cssClassAlternativeEven" />
      <RowStyle CssClass="cssClassAlternativeOdd" />
       <HeaderStyle CssClass="cssClassHeadingOne" />
    </asp:GridView>
  </div>
  </asp:Panel>
<div id="divAddBanner" class="sfPopup sfFormwrapper" style="display: none"> <span class="sfPopupclose"></span> </div>
<div id="divImageEditor" runat="server" style="display: none;" class="sfFormwrapper">
  <div class="cssClassFormHeading">
    <asp:Label ID="lblCropArea" runat="server" CssClass="cssClassFormLabel"> Drag on the image area to crop the image:</asp:Label>
  </div>
  <div>
    <asp:Image runat="server" ID="_imageEditor" />
  </div>
  <table>
    <tr>
      <td> x:   <tr>
      <td> x: </td>
      <td id="tdX"> - </td>
      <td> y: </td>
      <td id="tdY"> - </td>
      <td> width: </td>
      <td id="tdWidth"> - </td>
      <td> height: </td>
      <td id="tdHeight"> - </td>
    </tr>
  </table>
  <div class="cssClassButtonWrapper">
    <asp:ImageButton runat="server" ID="_cropCommand" OnClick="_cropCommand_Click" />
    <asp:Label ID="lblSaveCrop" runat="server" Text="Save" AssociatedControlID="_cropCommand"></asp:Label>
    <asp:ImageButton ID="imbCancelImageEdit" runat="server" OnClick="imbCancelImageEdit_Click" />
    <asp:Label ID="lblCancelImageEdit" runat="server" Text="Cancel" AssociatedControlID="imbCancelImageEdit"></asp:Label>
  </div>
  <input type="hidden" runat="server" id="_xField" />
  <input type="hidden" runat="server" id="_yField" />
  <input type="hidden" runat="server" id="_widthField" />
  <input type="hidden" runat="server" id="_heightField" />
</div>
