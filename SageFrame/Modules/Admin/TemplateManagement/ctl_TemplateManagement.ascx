<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctl_TemplateManagement.ascx.cs"
    Inherits="Modules_Admin_TemplateManagement_ctl_TemplateManagement" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script type="text/javascript">

 $(document).ready(function(){ 
    $('div.divTemplate img,div.divTemplateActive img').live("click",function(){
    GetImageList($(this).attr("alt")); 
    });
     $('div.cssClassTemplateControls span.preview').live("click",function(){
    GetImageList($(this).find("a").attr("href"));    
   return false;
    });    
    $('span.closePopUp').bind("click",function(){
    $('#fade,#divMessagePopUp').fadeOut();
    
    });
     $('#fade').click(function() {
            $('#fade,#divMessagePopUp').fadeOut();
            return false;
        });
        
        $('#divThumbnails a').live("click",function(){
        var imagePath=$(this).attr("path");
        $("#imgMain").attr("src",imagePath).fadeIn();       
        
        });
        
    });

 function ShowPopUp(popupid) {

        $('#' + popupid).fadeIn();
        $('body').append('<div id="fade"></div>');
        $('#fade').css({ 'filter': 'alpha(opacity=80)' }).fadeIn();
        var popuptopmargin = ($('#' + popupid).height() + 10) / 2;
        var popupleftmargin = ($('#' + popupid).width() + 10) / 2;
        $('#' + popupid).css({
            'margin-top': -popuptopmargin,
            'margin-left': -popupleftmargin
        });
       
    }
    
    function InitializeImage()
    {
        $("#imgMain").attr("src","");
        var imageList = $('#divThumbnails a');
        var count = 0;
        $.each(imageList, function(index, item) {
            if (count == 0) {
                $("#imgMain").attr("src", $(this).attr("path"));
            }
            count++;
            
        });     
    
    }
    
    function GetImageList(directory)
    {
     $('#divThumbnails').html('');
      $.get(TemplateMgrVar+'script.aspx?dir='+directory+'', {}, function(data) {
      $('#divThumbnails').append(data);
      InitializeImage();
      ShowPopUp("divMessagePopUp");
                });  
    }

</script>

<div id="fade">
</div>
<div id="divMessagePopUp" class="popupbox">
    <div class="cssClassFileManagerPopUP" style="text-align: center">
        <span class="closePopUp">
            <img src='<%=TemplateURL %>' id="imgMessage" /></span> <span class="cssClassMessage cssClassLabel">
            </span>
        <div id="divImageView">
            <img id="imgMain" alt="screenshot" />
        </div>
        <div id="divThumbnails">
        </div>
    </div>
</div>
<h2 class="cssClassFormHeading">
    <asp:Label ID="lblTemplateManagement" runat="server" Text="Template Management" meta:resourcekey="lblTemplateManagementResource1"></asp:Label>
</h2>
<asp:Panel ID="pnlTemplateList" runat="server" meta:resourcekey="pnlTemplateListResource1">
    <div class="cssClassButtonWrapper">
        <asp:ImageButton ID="imgAddTemplate" runat="server" AlternateText="Add Template"
            OnClick="imgAddTemplate_Click" meta:resourcekey="imgAddTemplateResource1" />
        <asp:Label ID="lblAddTemplate" runat="server" AssociatedControlID="imgAddTemplate"
            Text="Add Template" meta:resourcekey="lblAddTemplateResource1"></asp:Label>
    </div>
    <div class="cssClassTemplateSelection">
        <asp:Repeater ID="rptrTemplates" runat="server" OnItemDataBound="rptrTemplates_ItemDataBound"
            OnItemCommand="rptrTemplates_ItemCommand">
            <ItemTemplate>
                <asp:Label ID="lblTemplateTitle" runat="server" Style="display: none" Text='<%# Eval("TemplateTitle")%>'></asp:Label>
                <div class="divTemplate Curve" id="divMain" runat="server">
                    <div class="divTemplateInside Curve">
                        <div class="cssClassTemplateImage">
                            <a href="#">
                                <img src='<%#Eval("ThumbNail")%>' alt='<%# Eval("TemplateTitle")%>' width="148px"
                                    height="98px" />
                            </a>
                        </div>
                        <div class="cssClassTemplateDetails">
                            <ul>
                                <li><span class="title">
                                    <%# Eval("TemplateTitle")%></span> </li>
                                <li><span class="author">By:<a href='<%#Eval("AuthorURL")%>'><%#Eval("Author")%></a></span>
                                </li>
                            </ul>
                        </div>
                        <div class="cssClassTemplateDesc">
                            <span class="description">
                                <%#Eval("Description")%></span>
                        </div>
                        <div class="cssClassTemplateControls">
                            <span class="activate">
                                <asp:LinkButton ID="lnkBtnActivate" CommandName="Activate" CommandArgument='<%#Eval("TemplateID")%>'
                                    runat="server">Activate</asp:LinkButton>
                            </span><span class="preview"><a href="<%# Eval("TemplateTitle")%>">Preview</a></span>
                            <span class="delete">
                                <asp:LinkButton ID="lnkBtnDelete" CommandName="DeleteTemplate" CommandArgument='<%# Eval("TemplateTitle") + "_" + Eval("TemplateID") %>'
                                    runat="server">Delete</asp:LinkButton>
                            </span>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <div class="cssClassClear">
        </div>
    </div>
</asp:Panel>
<asp:Panel ID="pnlTemplate" runat="server" meta:resourcekey="pnlTemplateResource1">
    <div class="cssClassFormWrapper">
        <h2 class="cssClassFormHeading">
            <asp:Label ID="lblInstallTemplate" runat="server" Text="Install new template" meta:resourcekey="lblInstallTemplateResource1"></asp:Label>
        </h2>
        <div>
            <p>
                &nbsp;
            </p>
            <p>
                <asp:Label ID="lblTemplateHelp" runat="server" Text="The template zip file should have css folder, images folder"
                    meta:resourcekey="lblTemplateHelpResource1"></asp:Label>
            </p>
        </div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td width="20%">
                    <asp:Label ID="lblBrowseTemplateFile" runat="server" Text="Browse template file :"
                        CssClass="cssClassFormLabel" meta:resourcekey="lblBrowseTemplateFileResource1"></asp:Label>
                </td>
                <td width="20%">
                    <asp:FileUpload ID="fileTemplateZip" runat="server" CssClass="cssClassNormalFileUpload"
                        meta:resourcekey="fileTemplateZipResource1" />
                </td>
                <td>
                    <asp:Label ID="lblTemplateFileHelp" runat="server" Text="(zip)" meta:resourcekey="lblTemplateFileHelpResource1"></asp:Label>
                </td>
            </tr>
            <tr>
                <td width="20%">
                    <asp:Label ID="lblAuthorName" runat="server" Text="Template Author:" CssClass="cssClassFormLabel"></asp:Label>
                </td>
                <td width="20%">
                    <asp:TextBox ID="txtAuthor" runat="server" CssClass="cssClassNormalTextBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="20%">
                    <asp:Label ID="lblAuthorURL" runat="server" Text="Author URL:" CssClass="cssClassFormLabel"></asp:Label>
                </td>
                <td width="20%">
                    <asp:TextBox ID="txtAuthorURL" runat="server" CssClass="cssClassNormalTextBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="20%">
                    <asp:Label ID="Label1" runat="server" Text="Template Description:" CssClass="cssClassFormLabel"></asp:Label>
                </td>
                <td width="20%">
                    <asp:TextBox ID="txtTemplateDesc" runat="server" TextMode="MultiLine" CssClass="cssClassTextBox"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div class="cssClassButtonWrapper">
        <asp:ImageButton ID="imgInstall" runat="server" AlternateText="Install" ToolTip="Install the template"
            OnClick="imgInstall_Click" ImageUrl='<%# GetTemplateImageUrl("imginstall.png", true) %>'
            meta:resourcekey="imgInstallResource1" />
        <asp:Label ID="lblInstall" runat="server" Text="Install" AssociatedControlID="imgInstall"
            meta:resourcekey="lblInstallResource1"></asp:Label>
        <asp:ImageButton ID="imgCancel" runat="server" CausesValidation="False" OnClick="imgCancel_Click"
            meta:resourcekey="imgCancelResource1" />
        <asp:Label ID="lblCancel" runat="server" Text="Cancel" AssociatedControlID="imgCancel"
            Style="cursor: pointer;" meta:resourcekey="lblCancelResource1"></asp:Label>
    </div>
</asp:Panel>
