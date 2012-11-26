(function($){
    $.createSageMenu=function(p)
    {
        p=$.extend
        ({
            PortalID:1,
            UserModuleID:1,
            UserName:'user',
            PageName:'Home',
            ContainerClientID:'divNav1' ,
            CultureCode:'en-US',
            baseURL:'Services/Services.aspx/'
        },p);
        
         var SageMenu={
                  config:{
                                    isPostBack:false,
                                    async:true,                                                        
                                    cache:false,                                                                                               
                                    type:'POST',
                                    contentType:"application/json; charset=utf-8",                                  
                                    data:{data:''},
                                    dataType:'json',
                                    baseURL:p.baseURL,
                                    method:"",
                                    url:"",
                                    ajaxCallMode:0, 
                                    arr:[],
                                    arrPages:[],
                                    UserModuleID:p.UserModuleID,
                                    PortalID:p.PortalID,
                                    UserName:p.UserName,
                                    PageName:p.PageName,
                                    ContainerClientID:p.ContainerClientID,
                                    CultureCode:p.CultureCode                                    
                                                                       
                  },
                  init:function(){                 
                   SageMenu.LoadTopAdminMenu();             
                   this.BindEvents();                  
                  },
                  HighlightSelected:function(){
                      var menu= $(SageMenu.config.ContainerClientID+" ul li");                 
                      $.each(menu,function(index,item){
                            var hreflink = $(this).find("a").attr("href"); 
                            if (location.href.toLowerCase().indexOf(hreflink.toLowerCase())>-1)
                            {
                                $(this).addClass('cssClassActive');
                            }
                        });
                  },
                  ajaxSuccess:function(data){
                    switch(SageMenu.config.ajaxCallMode)
                    {
                        case 0:                           
                        case 1:
                            SageMenu.BuildMenu(data);                             
                            break;
                        case 2:
                            SageMenu.BindPages(data);                            
                            break;
                                          
                    }
                  },
                  BindEvents:function(){
                    $(SageMenu.config.ContainerClientID+" ul li").live("click",function(){
                        $(SageMenu.config.ContainerClientID+" ul li").removeClass("cssClassActive");
                        $(this).addClass("cssClassActive");
                    });
                  },
                  ajaxFailure:function(){                    
                  },
                  ajaxCall:function(config){                   
                                 $.ajax({  
                                    type:SageMenu.config.type,
                                    contentType:SageMenu.config.contentType,
                                    cache:SageMenu.config.cache,
                                    url:SageMenu.config.url,   
                                    data:SageMenu.config.data,     
                                    dataType:SageMenu.config.dataType,
                                    success:SageMenu.ajaxSuccess,    
                                    error:SageMenu.ajaxFailure
                                  });
                    
                 },              
                LoadTopAdminMenu:function(){
                        this.config.method="GetAdminMenu";
                        this.config.url=this.config.baseURL+this.config.method;
                        this.config.data='{}';
                        this.config.ajaxCallMode=2;
                        this.ajaxCall(this.config);
                },              
                 BindPages:function(data){
                    var pages=data.d;
                    var PageID="";
                    var parentID="";
                    var PageLevel=0;
                    var itemPath="";          
                    var html="";
                    html+='<ul class="sf-menu">';
                    $.each(pages, function(index, item) {
                        PageID = item.PageID;
                        parentID = item.ParentID;
                        categoryLevel = item.Level;
                       
                        if (item.Level == 3) {
                            var PageLink=PagePath+item.TabPath+".aspx";
						     if (item.ChildCount > 0) {
                            html += '<li class="cssClassParent"><a href='+PageLink+'>';
						    }
						    else
						    {html += '<li><a href='+PageLink+'>';
						    }
                            html += '<span>'+item.PageName+'</span>';
                            html += "</a>";

                            if (item.ChildCount > 0) {
                                html += "<ul>";
                                itemPath += '<span>'+item.PageName+'</span>';
                                html += SageMenu.BindChildCategory(pages, PageID);
                                html += "</ul>";
                            }
                            html += "</li>";
                        }
                        itemPath = '';
                    });   
                    html+='</ul>';                    
                    $(SageMenu.config.ContainerClientID).addClass("cssClassNavigation");   
                    $(SageMenu.config.ContainerClientID).append(html);                               
                    SageMenu.InitializeSuperfish();
					SageMenu.HighlightSelected();
                 },               
                 BindChildCategory:function(response, PageID) {
                    var strListmaker = '';
                    var childNodes = '';
                    var path = '';
                    var itemPath="";
                    itemPath += "";
                    $.each(response, function(index, item) {
                        if (item.Level > 3) {
                            if (item.ParentID == PageID) {
                                itemPath += item.PageName;
                                var PageLink=PagePath+item.TabPath+".aspx";
                                strListmaker += '<li><a  href='+PageLink+'>' + item.PageName + '</a>';
                                childNodes = SageMenu.BindChildCategory(response, item.PageID);                   
                                itemPath = itemPath.replace(itemPath.lastIndexOf(item.AttributeValue), '');
                                if (childNodes != '') {
                                    strListmaker += "<ul>" + childNodes + "</ul>";
                                }
                                strListmaker += "</li>";
                            }
                        }
                    });
                    return strListmaker;
                   },
                   InitializeSuperfish:function(){                                      
                    jQuery('ul.sf-menu').superfish();
                   } 
    };
    SageMenu.init();
    };    
    
    $.fn.SageMenuBuilder=function(p){
        $.createSageMenu(p);
    };
})(jQuery);

   
   