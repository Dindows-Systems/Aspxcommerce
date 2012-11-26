var Breadcrum = "";
$.extend($.expr[':'], {
    containsExact: function(a, i, m) {
        return $.trim(a.innerHTML.toLowerCase()) === m[3].toLowerCase();
    }
});
$(function() {
    Breadcrum = {
        vars: {
            itemCat: "",
            itmName: "",
            current: ""
        },
        config: {
            isPostBack: false,
            async: false,
            cache: false,
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: 'json',
            baseURL: AspxCommerce.utils.GetAspxServicePath(),
            method: "",
            url: "",
            ajaxCallMode: 0
        },
        ajaxCall: function(config) {
            $.ajax({
                type: Breadcrum.config.type,
                contentType: Breadcrum.config.contentType,
                cache: Breadcrum.config.cache,
                async: Breadcrum.config.async,
                url: Breadcrum.config.url,
                data: Breadcrum.config.data,
                dataType: Breadcrum.config.dataType,
                success: Breadcrum.ajaxSuccess,
                error: Breadcrum.ajaxFailure
            });
        },
        ajaxSuccess: function(msg) {
            switch (Breadcrum.config.ajaxCallMode) {
                case 1:
                    if (msg.d != null) {
                        var dx = jQuery.parseJSON(msg.d);
                        Breadcrum.vars.itemCat = dx.ItemCategory;
                        var tag = new Array();
                        var hrefarr = new Array();
                        tag = [];
                        hrefarr = [];
                        $('#breadcrumb ul').html('');
                        // current = decodeURI(itmName);
                        Breadcrum.vars.current = dx.ItemName; //decodeURIComponent(Breadcrum.vars.itmName);

                        var href = $(".cssClassCategoryNav a:containsExact(" + Breadcrum.vars.current + "):first").attr('href');
                        $('.cssClassCategoryNav a:containsExact(' + Breadcrum.vars.itemCat + '):first').parents('li').find('a:eq(0)').each(function() {
                            if ($(this).html() != Breadcrum.vars.current) {
                                tag.push($(this).html());
                                hrefarr.push($(this).attr('href'));
                            }
                        });
                        hrefarr.reverse();
                        // $('#breadcrumb ul').append('<li class="first"><a href="' + AspxCommerce.utils.GetAspxRedirectPath() + 'home.aspx" >home</a></li>');
                        if (AspxCommerce.utils.IsUserFriendlyUrl()) {
                            $('#breadcrumb ul').append('<li class="first"><a href=' + AspxCommerce.utils.GetAspxRedirectPath() + 'home.aspx' + ' >home</a></li>');
                        } else {
                            $('#breadcrumb ul').append('<li class="first"><a href=' + AspxCommerce.utils.GetAspxRedirectPath() + 'home' + ' >home</a></li>');
                        }
                        for (var x in tag.reverse()) {
                            $('#breadcrumb ul').append('<li ><a href="' + hrefarr[x] + '">' + tag[x] + '</a></li>');
                        }
                        // $('#breadcrumb ul li:last').addClass('last');
                        $('#breadcrumb ul').append('<li class="last">' + Breadcrum.vars.current + ' </li>');
                        tag = [];
                        hrefarr = [];

                        //  $('#breadcrumb ul li').not('.last').click(function() {
                        //    if ($(this).attr('class') == 'first') {
                        //    }
                        //   else {
                        //        var current = $(this).children().html();
                        //        $(this).nextAll().remove();
                        //        $('#breadcrumb li:last').remove();
                        //        $('#breadcrumb ul').append('<li class="last">' + current + '</li>');
                        //      }
                        //   });

                    } else {
                        $('#breadcrumb li:last').remove();
                    }
                    break;
                case 2:
                    if (msg.d != null) {
                        var dx = jQuery.parseJSON(msg.d);
                        Breadcrum.vars.itemCat = dx.ItemCategory;
                        var tag = new Array();
                        var hrefarr = new Array();

                        // current = decodeURI(itmName);
                        Breadcrum.vars.current = dx.ItemName; // decodeURIComponent(Breadcrum.vars.itmName);
                        var href = $(".cssClassCategoryNav a:containsExact(" + Breadcrum.vars.current + "):first").attr('href');
                        $('.cssClassCategoryNav a:containsExact(' + Breadcrum.vars.itemCat + '):first').parents('li').find('a:eq(0)').each(function() {
                            if ($(this).html() != Breadcrum.vars.current) {
                                tag.push($(this).html());
                                hrefarr.push($(this).attr('href'));
                            }
                        });
                        hrefarr.reverse();
                        $('#breadcrumb ul').html('');
                        if (AspxCommerce.utils.IsUserFriendlyUrl()) {
                            $('#breadcrumb ul').append('<li class="first"><a href=' + AspxCommerce.utils.GetAspxRedirectPath() + 'home.aspx' + ' >home</a></li>');
                        } else {
                            $('#breadcrumb ul').append('<li class="first"><a href=' + AspxCommerce.utils.GetAspxRedirectPath() + 'home' + ' >home</a></li>');
                        }
                        $('#breadcrumb ul li:gt(0)').remove();
                        for (var x in tag.reverse()) {
                            $('#breadcrumb ul').append('<li ><a href="' + hrefarr[x] + '">' + tag[x] + '</a></li>');
                        }

                        // $('#breadcrumb ul li:last').addClass('last');
                        $('#breadcrumb ul').append('<li class="last">' + Breadcrum.vars.current + ' </li>');
                        tag = [];
                        hrefarr = [];

                        $('#breadcrumb ul li').not('.last').click(function() {
                            if ($(this).attr('class') == 'first') {
                            } else {
                                var current = $(this).children().html();
                                $(this).nextAll().remove();
                                $('#breadcrumb li:last').remove();
                                $('#breadcrumb ul').append('<li class="last">' + current + '</li>');
                            }
                        });
                    } else {
                        $('#breadcrumb li:last').remove();
                    }
                    break;
            }

        },
        getBreadcrum: function() {
            var path = window.location.href;
            var cat = path.split('/');
            var pattern = "-";
            re = new RegExp(pattern, "g");
            $('#breadcrumb ul').html('');
            if (cat[4] == 'category') {
                var x = cat[5];
                x = x.split('.');
                Breadcrum.vars.itmName = x[0];
                var tag = new Array();
                var hrefarr = new Array();
                // current = decodeURI(itmName);
                //                    var pattern = "-";
                //                    re = new RegExp(pattern, "g");
                Breadcrum.vars.current = decodeURIComponent(Breadcrum.vars.itmName.replace('ampersand', '&').replace(re, ' ').replace('_', '-'));

                var href = $(".cssClassCategoryNav a:containsExact(" + Breadcrum.vars.current + "):first").attr('href');
                $('.cssClassCategoryNav a:containsExact(' + Breadcrum.vars.current + '):first').parents('li').find('a:eq(0)').each(function() {
                    if ($(this).html() != Breadcrum.vars.current) {
                        tag.push($(this).html());
                        hrefarr.push($(this).attr('href'));
                    }
                });
                hrefarr.reverse();
                if (AspxCommerce.utils.IsUserFriendlyUrl()) {
                    $('#breadcrumb ul').append('<li class="first"><a href=' + AspxCommerce.utils.GetAspxRedirectPath() + 'home.aspx' + ' >home</a></li>');
                } else {
                    $('#breadcrumb ul').append('<li class="first"><a href=' + AspxCommerce.utils.GetAspxRedirectPath() + 'home' + ' >home</a></li>');
                }
                $('#breadcrumb ul li:gt(0)').remove();
                for (var x in tag.reverse()) {
                    $('#breadcrumb ul').append('<li><a href="' + hrefarr[x] + '">' + tag[x] + '</a></li>');
                }
                tag = [];
                hrefarr = [];
                // $('#breadcrumb ul li:last').addClass('last');
                $('#breadcrumb ul').append('<li class="last">' + Breadcrum.vars.current + ' </li>');
                $('#breadcrumb ul li').not('.last').click(function() {
                    if ($(this).attr('class') == 'first') {
                    } else {
                        var current = $(this).children().html();
                        //alert(current);
                        $(this).nextAll().remove();
                        $('#breadcrumb li:last').remove();
                        $('#breadcrumb ul').append('<li class="last">' + Breadcrum.vars.current + '</li>');
                    }
                });
                $('#breadcrumb ul li.first').click(function() {
                    // $('#breadcrumb ul').html('');
                    //  $('#breadcrumb ul').append('<li class="first"><a href="sageframe/Default.aspx" >home</a></li>');
                });
            } else if (cat[4] == 'item') {
                var y = cat[5];
                y = y.split('.');
                Breadcrum.vars.itmName = y[0];
                Breadcrum.getCategoryForItem(Breadcrum.vars.itmName);
            } else if (cat[4] == 'tagsitems') {
                $('#breadcrumb ul').html('');
                if (AspxCommerce.utils.IsUserFriendlyUrl()) {
                    $('#breadcrumb ul').append('<li class="first"><a href=' + AspxCommerce.utils.GetAspxRedirectPath() + 'home.aspx' + ' >home</a></li>');
                } else {
                    $('#breadcrumb ul').append('<li class="first"><a href=' + AspxCommerce.utils.GetAspxRedirectPath() + 'home' + ' >home</a></li>');
                }
                $('#breadcrumb ul').append('<li class="last">Tags</li>');
            } else if (cat[4] == 'search') {
                $('#breadcrumb ul').html('');
                if (AspxCommerce.utils.IsUserFriendlyUrl()) {
                    $('#breadcrumb ul').append('<li class="first"><a href=' + AspxCommerce.utils.GetAspxRedirectPath() + 'home.aspx' + ' >home</a></li>');
                } else {
                    $('#breadcrumb ul').append('<li class="first"><a href=' + AspxCommerce.utils.GetAspxRedirectPath() + 'home' + ' >home</a></li>');
                }
                $('#breadcrumb ul').append('<li class="last">Search</li>');
            } else if (cat[4] == 'option') {
                $('#breadcrumb ul').html('');
                if (AspxCommerce.utils.IsUserFriendlyUrl()) {
                    $('#breadcrumb ul').append('<li class="first"><a href=' + AspxCommerce.utils.GetAspxRedirectPath() + 'home.aspx' + ' >home</a></li>');
                } else {
                    $('#breadcrumb ul').append('<li class="first"><a href=' + AspxCommerce.utils.GetAspxRedirectPath() + 'home' + ' >home</a></li>');
                }
                $('#breadcrumb ul').append('<li class="last">Shopping Options</li>');
            } else if (cat[4] == 'portal') {

                if (cat[6] == 'item') {
                    var m = cat[7];
                    m = m.split('.');
                    Breadcrum.vars.itmName = m[0];
                    Breadcrum.getCategoryForItemPortal(Breadcrum.vars.itmName);
                } else if (cat[6] == 'category') {
                    var x3 = cat[7];
                    x3 = x3.split('.');
                    Breadcrum.vars.itmName = x3[0];
                    var tag = new Array();
                    var hrefarr = new Array();

                    // current = decodeURI(itmName);
                    Breadcrum.vars.current = decodeURIComponent(Breadcrum.vars.itmName.replace('ampersand', '&').replace(re, ' ').replace('_', '-'));
                    var href = $(".cssClassCategoryNav a:containsExact(" + Breadcrum.vars.current + "):first").attr('href');
                    $('.cssClassCategoryNav a:containsExact(' + Breadcrum.vars.current + '):first').parents('li').find('a:eq(0)').each(function() {
                        if ($(this).html() != Breadcrum.vars.current) {
                            tag.push($(this).html());
                            hrefarr.push($(this).attr('href'));
                        }
                    });
                    hrefarr.reverse();
                    $('#breadcrumb ul').html('');
                    if (AspxCommerce.utils.IsUserFriendlyUrl()) {
                        $('#breadcrumb ul').append('<li class="first"><a href=' + AspxCommerce.utils.GetAspxRedirectPath() + 'home.aspx' + ' >home</a></li>');
                    } else {
                        $('#breadcrumb ul').append('<li class="first"><a href=' + AspxCommerce.utils.GetAspxRedirectPath() + 'home' + ' >home</a></li>');
                    }

                    for (var x in tag.reverse()) {
                        $('#breadcrumb ul').append('<li><a href="' + hrefarr[x] + '">' + tag[x] + '</a></li>');
                    }
                    // $('#breadcrumb ul li:last').addClass('last');
                    $('#breadcrumb ul').append('<li class="last">' + Breadcrum.vars.current + ' </li>');
                    tag = [];
                    hrefarr = [];

                    //                $('#breadcrumb ul li').not('.last').click(function() {
                    //                    if ($(this).attr('class') == 'first') {
                    //                    }
                    //                    else {
                    //                        var current3 = $(this).children().html();
                    //                        //alert(current);
                    //                        $(this).nextAll().remove();
                    //                        $('#breadcrumb li:last').remove();
                    //                        $('#breadcrumb ul').append('<li class="last">' + current3 + '</li>');
                    //                    }
                    //                });
                } else if (cat[6] == 'tagsitems') {
                    $('#breadcrumb ul').html('');
                    if (AspxCommerce.utils.IsUserFriendlyUrl()) {
                        $('#breadcrumb ul').append('<li class="first"><a href=' + AspxCommerce.utils.GetAspxRedirectPath() + 'home.aspx' + ' >home</a></li>');
                    } else {
                        $('#breadcrumb ul').append('<li class="first"><a href=' + AspxCommerce.utils.GetAspxRedirectPath() + 'home' + ' >home</a></li>');
                    }
                    $('#breadcrumb ul').append('<li class="last">Tags</li>');
                } else if (cat[6] == 'search') {
                    $('#breadcrumb ul').html('');
                    if (AspxCommerce.utils.IsUserFriendlyUrl()) {
                        $('#breadcrumb ul').append('<li class="first"><a href=' + AspxCommerce.utils.GetAspxRedirectPath() + 'home.aspx' + ' >home</a></li>');
                    } else {
                        $('#breadcrumb ul').append('<li class="first"><a href=' + AspxCommerce.utils.GetAspxRedirectPath() + 'home' + ' >home</a></li>');
                    }
                    $('#breadcrumb ul').append('<li class="last">Search</li>');
                } else if (cat[6] == 'option') {
                    $('#breadcrumb ul').html('');
                    if (AspxCommerce.utils.IsUserFriendlyUrl()) {
                        $('#breadcrumb ul').append('<li class="first"><a href=' + AspxCommerce.utils.GetAspxRedirectPath() + 'home.aspx' + ' >home</a></li>');
                    } else {
                        $('#breadcrumb ul').append('<li class="first"><a href=' + AspxCommerce.utils.GetAspxRedirectPath() + 'home' + ' >home</a></li>');
                    }
                    $('#breadcrumb ul').append('<li class="last">Shopping Options</li>');
                } else {

                    var x2 = cat[6];
                    if (x2 != undefined) {
                        x2 = x2.split('.')[0];
                    }
                    x2 = x2.replace(new RegExp("-", "g"), ' ');
                    if (x != '' && x2.toLowerCase() != 'default' && x2.toLowerCase() != 'home') {
                        $('#breadcrumb ul').html('');
                        if (AspxCommerce.utils.IsUserFriendlyUrl()) {
                            $('#breadcrumb ul').append('<li class="first"><a href=' + AspxCommerce.utils.GetAspxRedirectPath() + 'home.aspx' + ' >home</a></li>');
                        } else {
                            $('#breadcrumb ul').append('<li class="first"><a href=' + AspxCommerce.utils.GetAspxRedirectPath() + 'home' + ' >home</a></li>');
                        }
                        //$('#breadcrumb ul').append('<li class="first"><a href="' + AspxCommerce.utils.GetAspxRedirectPath() + 'home.aspx" >home</a></li>');
                        $('#breadcrumb ul').append('<li class="last">' + x2 + '</li>');
                    } else {
                        $('#breadcrumb ul').html('');
                        $('#breadcrumb').hide();
                    }
                }

            } else {
                $('#breadcrumb ul').html('');
                var x = cat[4];
                if (x != undefined) {
                    x = x.split('.')[0];
                }
                x = x.replace(new RegExp("-", "g"), ' ');
                if (x != '' && x.toLowerCase() != 'default' && x.toLowerCase() != 'home') {
                    $('#breadcrumb ul').html('');
                    if (AspxCommerce.utils.IsUserFriendlyUrl()) {
                        $('#breadcrumb ul').append('<li class="first"><a href=' + AspxCommerce.utils.GetAspxRedirectPath() + 'home.aspx' + ' >home</a></li>');
                    } else {
                        $('#breadcrumb ul').append('<li class="first"><a href=' + AspxCommerce.utils.GetAspxRedirectPath() + 'home' + ' >home</a></li>');
                    }

                    // $('#breadcrumb ul').append('<li class="first"><a href="' + aspxRootPath + 'home.aspx" >home</a></li>');
                    $('#breadcrumb ul').append('<li class="last">' + x + '</li>');
                } else {
                    $('#breadcrumb ul').html('');
                    $('#breadcrumb').hide();
                }
            }
        },
        getCategoryForItemPortal: function(itmName) {
            itmName = decodeURIComponent(itmName);
            Breadcrum.config.method = "AspxCommerceWebService.asmx/GetCategoryForItem";
            Breadcrum.config.url = Breadcrum.config.baseURL + Breadcrum.config.method;
            Breadcrum.config.data = JSON2.stringify({ storeID: AspxCommerce.utils.GetStoreID(), portalID: AspxCommerce.utils.GetPortalID(), itemSku: itmName });
            Breadcrum.config.ajaxCallMode = 1;
            Breadcrum.ajaxCall(Breadcrum.config);
        },
        getCategoryForItem: function(itmName) {
            itmName = decodeURIComponent(itmName);
            Breadcrum.config.method = "AspxCommerceWebService.asmx/GetCategoryForItem";
            Breadcrum.config.url = Breadcrum.config.baseURL + Breadcrum.config.method;
            Breadcrum.config.data = JSON2.stringify({ storeID: AspxCommerce.utils.GetStoreID(), portalID: AspxCommerce.utils.GetPortalID(), itemSku: itmName });
            Breadcrum.config.ajaxCallMode = 2;
            Breadcrum.ajaxCall(Breadcrum.config);
        },
        getBreadcrumforlive: function() {
            var path = window.location.href;
            var cat = path.split('/');
            if (cat[3] == 'category') {
                var x = cat[4];
                x = x.split('.');
                Breadcrum.vars.itmName = x[0];
                var tag = new Array();
                var hrefarr = new Array();

                // current = decodeURI(itmName);
                Breadcrum.vars.current = decodeURIComponent(Breadcrum.vars.itmName);

                var href = $(".cssClassCategoryNav a:containsExact(" + Breadcrum.vars.current + "):first").attr('href');
                $('.cssClassCategoryNav a:containsExact(' + Breadcrum.vars.current + '):first').parents('li').find('a:eq(0)').each(function() {
                    if ($(this).html() != Breadcrum.vars.current) {
                        tag.push($(this).html());
                        hrefarr.push($(this).attr('href'));
                    }
                });
                hrefarr.reverse();
                $('#breadcrumb ul').html('');
                if (AspxCommerce.utils.IsUserFriendlyUrl()) {
                    $('#breadcrumb ul').append('<li class="first"><a href=' + AspxCommerce.utils.GetAspxRedirectPath() + 'home.aspx' + ' >home</a></li>');
                } else {
                    $('#breadcrumb ul').append('<li class="first"><a href=' + AspxCommerce.utils.GetAspxRedirectPath() + 'home' + ' >home</a></li>');
                }
                $('#breadcrumb ul li:gt(0)').remove();
                for (var x in tag.reverse()) {
                    $('#breadcrumb ul').append('<li><a href="' + hrefarr[x] + '">' + tag[x] + '</a></li>');
                }
                tag = [];
                hrefarr = [];
                // $('#breadcrumb ul li:last').addClass('last');
                $('#breadcrumb ul').append('<li class="last">' + Breadcrum.vars.current + ' </li>');
                $('#breadcrumb ul li').not('.last').click(function() {
                    if ($(this).attr('class') == 'first') {
                    } else {
                        var current = $(this).children().html();
                        //alert(current);
                        $(this).nextAll().remove();
                        $('#breadcrumb li:last').remove();
                        $('#breadcrumb ul').append('<li class="last">' + current + '</li>');
                    }
                });
                $('#breadcrumb ul li.first').click(function() {
                    // $('#breadcrumb ul').html('');
                    //  $('#breadcrumb ul').append('<li class="first"><a href="sageframe/Default.aspx" >home</a></li>');
                });
            } else if (cat[3] == 'item') {
                var y = cat[4];
                y = y.split('.');
                Breadcrum.vars.itmName = y[0];
                Breadcrum.getCategoryForItem(Breadcrum.vars.itmName);
            } else if (cat[3] == 'tagsitems') {
                $('#breadcrumb ul').html('');
                if (AspxCommerce.utils.IsUserFriendlyUrl()) {
                    $('#breadcrumb ul').append('<li class="first"><a href=' + AspxCommerce.utils.GetAspxRedirectPath() + 'home.aspx' + ' >home</a></li>');
                } else {
                    $('#breadcrumb ul').append('<li class="first"><a href=' + AspxCommerce.utils.GetAspxRedirectPath() + 'home' + ' >home</a></li>');
                }
                $('#breadcrumb ul').append('<li class="last">Tags</li>');
            } else if (cat[3] == 'search') {
                $('#breadcrumb ul').html('');
                if (AspxCommerce.utils.IsUserFriendlyUrl()) {
                    $('#breadcrumb ul').append('<li class="first"><a href=' + AspxCommerce.utils.GetAspxRedirectPath() + 'home.aspx' + ' >home</a></li>');
                } else {
                    $('#breadcrumb ul').append('<li class="first"><a href=' + AspxCommerce.utils.GetAspxRedirectPath() + 'home' + ' >home</a></li>');
                }
                $('#breadcrumb ul').append('<li class="last">Search</li>');
            } else if (cat[3] == 'option') {
                $('#breadcrumb ul').html('');
                if (AspxCommerce.utils.IsUserFriendlyUrl()) {
                    $('#breadcrumb ul').append('<li class="first"><a href=' + AspxCommerce.utils.GetAspxRedirectPath() + 'home.aspx' + ' >home</a></li>');
                } else {
                    $('#breadcrumb ul').append('<li class="first"><a href=' + AspxCommerce.utils.GetAspxRedirectPath() + 'home' + ' >home</a></li>');
                }
                $('#breadcrumb ul').append('<li class="last">Shopping Options</li>');
            } else if (cat[3] == 'portal') {
                if (cat[5] == 'item') {
                    var m = cat[6];
                    m = m.split('.');
                    Breadcrum.vars.itmName = m[0];
                    Breadcrum.getCategoryForItemPortal(Breadcrum.vars.itmName);
                } else if (cat[5] == 'category') {
                    var x3 = cat[6];
                    x3 = x3.split('.');
                    Breadcrum.vars.itmName = x3[0];
                    var tag = new Array();
                    var hrefarr = new Array();

                    // current = decodeURI(itmName);
                    Breadcrum.vars.current = decodeURIComponent(Breadcrum.vars.itmName);
                    var href = $(".cssClassCategoryNav a:containsExact(" + Breadcrum.vars.current + "):first").attr('href');
                    $('.cssClassCategoryNav a:containsExact(' + Breadcrum.vars.current + '):first').parents('li').find('a:eq(0)').each(function() {
                        if ($(this).html() != Breadcrum.vars.current) {
                            tag.push($(this).html());
                            hrefarr.push($(this).attr('href'));
                        }
                    });
                    hrefarr.reverse();
                    $('#breadcrumb ul').html('');
                    if (AspxCommerce.utils.IsUserFriendlyUrl()) {
                        $('#breadcrumb ul').append('<li class="first"><a href=' + AspxCommerce.utils.GetAspxRedirectPath() + 'home.aspx' + ' >home</a></li>');
                    } else {
                        $('#breadcrumb ul').append('<li class="first"><a href=' + AspxCommerce.utils.GetAspxRedirectPath() + 'home' + ' >home</a></li>');
                    }

                    for (var x in tag.reverse()) {
                        $('#breadcrumb ul').append('<li><a href="' + hrefarr[x] + '">' + tag[x] + '</a></li>');
                    }
                    // $('#breadcrumb ul li:last').addClass('last');
                    $('#breadcrumb ul').append('<li class="last">' + Breadcrum.vars.current + ' </li>');
                    tag = [];
                    hrefarr = [];

                    //                $('#breadcrumb ul li').not('.last').click(function() {
                    //                    if ($(this).attr('class') == 'first') {
                    //                    }
                    //                    else {
                    //                        var current3 = $(this).children().html();
                    //                        //alert(current);
                    //                        $(this).nextAll().remove();
                    //                        $('#breadcrumb li:last').remove();
                    //                        $('#breadcrumb ul').append('<li class="last">' + current3 + '</li>');
                    //                    }
                    //                });
                } else if (cat[5] == 'tagsitems') {
                    $('#breadcrumb ul').html('');
                    if (AspxCommerce.utils.IsUserFriendlyUrl()) {
                        $('#breadcrumb ul').append('<li class="first"><a href=' + AspxCommerce.utils.GetAspxRedirectPath() + 'home.aspx' + ' >home</a></li>');
                    } else {
                        $('#breadcrumb ul').append('<li class="first"><a href=' + AspxCommerce.utils.GetAspxRedirectPath() + 'home' + ' >home</a></li>');
                    }
                    $('#breadcrumb ul').append('<li class="last">Tags</li>');
                } else if (cat[5] == 'search') {
                    $('#breadcrumb ul').html('');
                    if (AspxCommerce.utils.IsUserFriendlyUrl()) {
                        $('#breadcrumb ul').append('<li class="first"><a href=' + AspxCommerce.utils.GetAspxRedirectPath() + 'home.aspx' + ' >home</a></li>');
                    } else {
                        $('#breadcrumb ul').append('<li class="first"><a href=' + AspxCommerce.utils.GetAspxRedirectPath() + 'home' + ' >home</a></li>');
                    }
                    $('#breadcrumb ul').append('<li class="last">Search</li>');
                } else if (cat[5] == 'option') {
                    $('#breadcrumb ul').html('');
                    if (AspxCommerce.utils.IsUserFriendlyUrl()) {
                        $('#breadcrumb ul').append('<li class="first"><a href=' + AspxCommerce.utils.GetAspxRedirectPath() + 'home.aspx' + ' >home</a></li>');
                    } else {
                        $('#breadcrumb ul').append('<li class="first"><a href=' + AspxCommerce.utils.GetAspxRedirectPath() + 'home' + ' >home</a></li>');
                    }
                    $('#breadcrumb ul').append('<li class="last">Shopping Options</li>');
                } else {

                    var x2 = cat[5];
                    if (x2 != undefined) {
                        x2 = x2.split('.')[0];
                    }
                    x2 = x2.replace(new RegExp("-", "g"), ' ');
                    if (x2 != '' && x2.toLowerCase() != 'default' && x2.toLowerCase() != 'home') {
                        $('#breadcrumb ul').html('');
                        if (AspxCommerce.utils.IsUserFriendlyUrl()) {
                            $('#breadcrumb ul').append('<li class="first"><a href=' + AspxCommerce.utils.GetAspxRedirectPath() + 'home.aspx' + ' >home</a></li>');
                        } else {
                            $('#breadcrumb ul').append('<li class="first"><a href=' + AspxCommerce.utils.GetAspxRedirectPath() + 'home' + ' >home</a></li>');
                        }
                        //$('#breadcrumb ul').append('<li class="first"><a href="' + AspxCommerce.utils.GetAspxRedirectPath() + 'home.aspx" >home</a></li>');
                        $('#breadcrumb ul').append('<li class="last">' + x2 + '</li>');
                    } else {
                        $('#breadcrumb ul').html('');
                        $('#breadcrumb').hide();
                    }
                }

            } else {

                var x = cat[3];
                if (x != undefined) {
                    x = x.split('.')[0];
                }
                x = x.replace(new RegExp("-", "g"), ' ');
                if (x != '' && x.toLowerCase() != 'default' && x.toLowerCase() != 'home') {
                    $('#breadcrumb ul').html('');
                    if (AspxCommerce.utils.IsUserFriendlyUrl()) {
                        $('#breadcrumb ul').append('<li class="first"><a href=' + AspxCommerce.utils.GetAspxRedirectPath() + 'home.aspx' + ' >home</a></li>');
                    } else {
                        $('#breadcrumb ul').append('<li class="first"><a href=' + AspxCommerce.utils.GetAspxRedirectPath() + 'home' + ' >home</a></li>');
                    }

                    // $('#breadcrumb ul').append('<li class="first"><a href="' + aspxRootPath + 'home.aspx" >home</a></li>');
                    $('#breadcrumb ul').append('<li class="last">' + x + '</li>');
                } else {
                    $('#breadcrumb ul').html('');
                    $('#breadcrumb').hide();
                }
            }

        },
        Init: function() {
            $('#breadcrumb ul').html('');
            if ($('.cssClassCategoryNav').length != 0) {
                if (AspxCommerce.utils.GetAspxRootPath() == "/") {
                    Breadcrum.getBreadcrumforlive();
                } else {
                    Breadcrum.getBreadcrum();
                }
            }
        }
    };
    Breadcrum.Init();
});