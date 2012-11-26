<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SageMenuSettings.ascx.cs"
    Inherits="Modules_SageMenu_SageMenuSettings" %>

<script type="text/javascript">
    $(function(){
    var SageMenuSettings={
                  config:{
                                    isPostBack:false,
                                    async:true,                                                        
                                    cache:false,                                                                                               
                                    type:'POST',
                                    contentType:"application/json; charset=utf-8",                                  
                                    data:{data:''},
                                    dataType:'json',
                                    baseURL:SageMenuSettingPath+'MenuWebService.asmx/',
                                    method:"",
                                    url:"",                                   
                                    categoryList:"",
                                    ajaxCallMode:0, ///0 for get categories and bind, 1 for notification,2 for versions bind
                                    categoryContainer:$('#divCategoryList ul'),
                                    deleteButtonUrl:"images/btndelete.png",
                                    UserModuleID:'<%=UserModuleID%>',
                                    PortalID:'<%=PortalID%>',
                                    UserName:'<%=UserName%>',
                                    arr:[],
                                    arrPages:[]
                                    
                                                                       
                  },
                  init:function(){
                    this.BindEvents();
                    this.LoadUserModuleSettings();
                  },
                  BindEvents:function()
                  {
                    $('#btnSaveSetting').bind("click",function(){
                        SageMenuSettings.SaveSettings();
                    });
                  },
                  SaveSettings:function()
                  {
                        this.config.method="SaveMenuSetting";
                        this.config.url=this.config.baseURL+this.config.method;
                        this.config.data=SageMenuSettings.GetSettingList();
                        this.config.ajaxCallMode=0;
                        this.ajaxCall(this.config);
                  },
                  GetSettingList:function(){
                        var options=$('#rbMenuTypes li input[type="radio"]');                       
                        var _SettingValue="";
                        $.each(options,function(index,item){
                            if($(this).attr("checked"))
                            {
                                _SettingValue=$(this).attr("value");
                            }
                        });
                        var param=JSON2.stringify({lstMenuSetting:[{UserModuleID:SageMenuSettings.config.UserModuleID,SettingKey:'MenuType',SettingValue:_SettingValue,IsActive:true,PortalID:SageMenuSettings.config.PortalID,UpdatedBy:SageMenuSettings.config.UserName,AddedBy:SageMenuSettings.config.UserName}]});    
                        return param;
                  },
                  LoadUserModuleSettings:function()
                  {
                        this.config.method="GetMenuSettings";
                        this.config.url=this.config.baseURL+this.config.method;
                        this.config.data=JSON2.stringify({PortalID:SageMenuSettings.config.PortalID,UserModuleID:SageMenuSettings.config.UserModuleID});
                        this.config.ajaxCallMode=1;
                        this.ajaxCall(this.config);
                  },
                  BindSageMenuSettings:function(data)
                  {
                    var settings=data.d;                   
                    switch(parseInt(settings.MenuType))
                    {
                        case 0:
                            $('#rbTopNavAdmin').attr("checked","checked");
                            break;
                        case 1:
                            $('#rbTopNavClient').attr("checked","checked");
                            break;                       
                        case 2:
                            $('#rbSideMenu').attr("checked","checked");
                            break;
                        break;
                        case 3:
                            $('#rbFooterMenu').attr("checked","checked");
                            break;
                        break;
                    }
                  },
                  ajaxSuccess:function(data){
                    switch(SageMenuSettings.config.ajaxCallMode)
                    {
                        case 0:
                            alert("Setting Saved");
                            break;
                        case 1:
                            SageMenuSettings.BindSageMenuSettings(data);                           
                            break;
                    }
                  },
                  ajaxFailure:function(){
                    alert("dude you got error");
                  },
                  ajaxCall:function(config){                   
                                 $.ajax({  
                                    type:SageMenuSettings.config.type,
                                    contentType:SageMenuSettings.config.contentType,
                                    cache:SageMenuSettings.config.cache,
                                    url:SageMenuSettings.config.url,   
                                    data:SageMenuSettings.config.data,     
                                    dataType:SageMenuSettings.config.dataType,
                                    success:SageMenuSettings.ajaxSuccess,    
                                    error:SageMenuSettings.ajaxFailure
                                  });
                    
                 }
                
                
                 
    };
    SageMenuSettings.init();
    });
    
</script>

<div class="cssClassFormWrapper">
    <table id="tblSageMenuSetting" cellpadding="0" cellspacing="0">
        <tr>
            <td width="20%">
                <asp:Label ID="lblMenuType" CssClass="cssClassFormLabel" runat="server">Menu Type:</asp:Label>
            </td>
            <td>
               
                    <ul id="rbMenuTypes">
                        <%--<li><input type="radio" id="rbTopNavAdmin" value="0" name="MenuType" />Top Admin Menu</li>--%>
                        <li>
                            <input type="radio" id="rbTopNavClient" value="1" name="MenuType" />Top Client Menu</li>
                        <li>
                            <input type="radio" id="rbSideMenu" value="2" name="MenuType" />Side Menu</li>
                        <li>
                            <input type="radio" id="rbFooterMenu" value="3" name="MenuType" />Footer Menu</li>
                    </ul>
                
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <input type="button" value="Save" id="btnSaveSetting" class="cssClassBtn" />
            </td>
        </tr>
    </table>
</div>
