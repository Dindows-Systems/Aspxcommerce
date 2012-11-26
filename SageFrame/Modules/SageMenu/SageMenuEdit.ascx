<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SageMenuEdit.ascx.cs" Inherits="Modules_SageMenu_SageMenuEdit" %>
<script type="text/javascript">
    $(function(){
    var SageMenuEdit={
                  config:{
                                    isPostBack:false,
                                    async:true,                                                        
                                    cache:false,                                                                                               
                                    type:'POST',
                                    contentType:"application/json; charset=utf-8",                                  
                                    data:{data:''},
                                    dataType:'json',
                                    baseURL:SageMenuSettingPath+'Services/SageMenuWCFService.svc/',
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
//                  $('ul.sagemenu-side li.parent').live("click",function(){
//                    $(this).find("ul").slideToggle("slow");
//                  });
//                  $('ul.sagemenu-side li.child').live("click",function(){
//                    return false;
//                  });
                  },
                  SaveSettings:function()
                  {
                        this.config.method="SaveMenuSetting";
                        this.config.url=this.config.baseURL+this.config.method;
                        this.config.data=SageMenuEdit.GetSettingList();
                        this.config.ajaxCallMode=0;
                        this.ajaxCall(this.config);
                  },                 
                  LoadUserModuleSettings:function()
                  {
                        this.config.method="GetMenuSettings";
                        this.config.url=this.config.baseURL+this.config.method;
                        this.config.data=JSON2.stringify({PortalID:SageMenuEdit.config.PortalID,UserModuleID:SageMenuEdit.config.UserModuleID});
                        this.config.ajaxCallMode=1;
                        this.ajaxCall(this.config);
                  },                 
                  ajaxSuccess:function(data){
                    switch(SageMenuEdit.config.ajaxCallMode)
                    {
                        case 0:
                            alert("Setting Saved");
                            break;
                        case 1:
                            SageMenuEdit.LoadMenuData(data);                           
                            break;
                        case 2:
                            SageMenuEdit.BindMenuData(data);
                            break;
                    }
                  },
                  ajaxFailure:function(){
                    alert("dude you got error");
                  },
                  ajaxCall:function(config){                   
                                 $.ajax({  
                                    type:SageMenuEdit.config.type,
                                    contentType:SageMenuEdit.config.contentType,
                                    cache:SageMenuEdit.config.cache,
                                    url:SageMenuEdit.config.url,   
                                    data:SageMenuEdit.config.data,     
                                    dataType:SageMenuEdit.config.dataType,
                                    success:SageMenuEdit.ajaxSuccess,    
                                    error:SageMenuEdit.ajaxFailure
                                  });
                    
                 },
                 LoadMenuData:function(data)
                 {
                
                    switch(parseInt(data.d.MenuType))
                    {
                    case 0:
                        SageMenuEdit.GetMenuData("GetPages",2);
                        break;
                    case 1:
                        SageMenuEdit.GetMenuData("GetPages",2);
                        break;
                    break;
                    case 2:
                    break;
                    case 3:
                    break;
                    }
                 },
                 GetMenuData:function(functionName,ajaxCallMode){
                        this.config.method=functionName;
                        this.config.url=this.config.baseURL+this.config.method;
                        this.config.data='{}';
                        this.config.ajaxCallMode=ajaxCallMode;
                        this.ajaxCall(this.config);
                 },
                 BindMenuData:function(data)
                 {
                    var pages=data.d;
                    var pages=data.d;
                    var categoryID="";
                    var parentID="";
                    var categoryLevel=0;
                      var itemPath="";          
                 var html="";
                 html+='<ul class="sagemenu-side">';
                $.each(pages, function(index, eachCat) {
                    categoryID = eachCat.PageID;
                    parentID = eachCat.ParentID;
                    categoryLevel = eachCat.Level;
                   
                    if (eachCat.Level == 0) {
                       
                        if(eachCat.ChildCount>0)
                        {
                         html += '<li class="parent"><input checked='+eachCat.ShowInMenu+' type="checkbox" /><a href="#">';
                        html += eachCat.PageName;
                        html+='>>';
                        }
                        else
                        {
                         html += '<li><input type="checkbox" checked='+eachCat.ShowInMenu+'><a href="#">';
                        html += eachCat.PageName;
                        
                        }
                        html += "</a>";

                        if (eachCat.ChildCount > 0) {
                            html += "<ul>";
                            itemPath += eachCat.PageName;
                            html += SageMenuEdit.BindChildCategory(pages, categoryID);
                            html += "</ul>";
                        }
                        html += '</li>';
                    }
                    itemPath = '';
                });   
                html+='</ul>';             
                    $('#divPageList').append(html);
                    SageMenuEdit.InitPaging();
                 },
                 BindChildCategory:function(response, categoryID) {
                    var strListmaker = '';
                    var childNodes = '';
                    var path = '';
                    var itemPath="";
                    itemPath += "";
                    $.each(response, function(index, eachCat) {
                        if (eachCat.Level > 0) {
                            if (eachCat.ParentID == categoryID) {
                                itemPath += eachCat.PageName;
                                if(eachCat.ShowInMenu)
                                {
                                strListmaker += '<li class="child"><input checked="checked" type="checkbox" /><a   href="#">' + eachCat.PageName + '</a>';
                                }
                                else
                                {
                                strListmaker += '<li class="child"><input type="checkbox" /><a   href="#">' + eachCat.PageName + '</a>';
                                }
                                childNodes = SageMenuEdit.BindChildCategory(response, eachCat.PageID);                   
                                itemPath = itemPath.replace(itemPath.lastIndexOf(eachCat.AttributeValue), '');
                                if (childNodes != '') {
                                    strListmaker += "<ul>" + childNodes + "</ul>";
                                }
                                strListmaker += "</li>";
                            }
                        }
                    });
                    return strListmaker;
                   },
                   InitPaging:function(){                                    
                           $('#paging_container3').pajinate({
					        items_per_page : 3,
					        item_container_id : '.sagemenu-side',
					        nav_panel_id : '.alt_page_navigation'        					
				            });
                   }
                
                
                 
    };
    SageMenuEdit.init();
    });
    
    </script>
    <div class="cssClassFormWrapper">
    <h3>Show In Menu</h3>
    <div id="paging_container3" class="container">
    <div class="alt_page_navigation"></div>
    <div id="divPageList">    
    </div>    
    </div>
    <div style="clear:both"></div>
    <input type="button" class="cssClassBtn" value="Save" />
    </div>
    
    