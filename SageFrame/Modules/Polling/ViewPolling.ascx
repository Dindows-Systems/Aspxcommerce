<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ViewPolling.ascx.cs" Inherits="Modules_Polling_ViewPolling" %>

<script language="javascript" type="text/javascript">
    $(document).ready(function() {
        $(this).GetPollScipt({
            UserModuleID: '<%=UserModuleID %>',
            PortalID: '<%=PortalID %>',
            pollID: '<%=pollID %>',
            isPolled: '<%=isPolled %>',
            PollingServicePath: PollingServicePath
        });
    });
    
</script>

<div class="cssClassCommonSideBox" style="display: block;">
    <h2>
        <span class="cssClassPoll">Community Poll</span></h2>
    <div class="cssClassCommonSideBoxTable">
        <div runat="server" id="divMsg" class="mInfo" visible="false" />
        <div id="divPoll" class="poll-box" runat="server">
            <div class="poll-question">
                <asp:Literal ID="litQuestion" runat="server" />
            </div>
            <asp:Literal ID="ltrlPollResult" runat="server"></asp:Literal>
            <div id="divAnswers" class="sfdivAnswer" runat="server">
                <asp:Repeater runat="server" ID="rptChoices">
                    <ItemTemplate>
                        <p>
                            <label>
                                <input type="radio" value='<%# Eval("PollChoiceID") %>' name="rdoPoll" id='rdoPoll<%# Container.ItemIndex %>' />
                                <%# Eval("Choice") %>
                            </label>
                        </p>
                    </ItemTemplate>
                    <FooterTemplate>
                        <div id="divmessage" class="cssClassNotFound">
                        </div>
                        <input type="button" value="Vote" class="cssClassSubmitBtn" id="btnSubmit<%# UserModuleID %>" />
                        <input type="button" value="View Results" class="cssClassSubmitBtn" id="btnViewResult<%# UserModuleID %>" />
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</div>
<asp:HiddenField runat="server" ID="hidPollID" />
