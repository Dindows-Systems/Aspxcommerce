 var OnLinecustomers;

    $(function() {
        var storeId = AspxCommerce.utils.GetStoreID();
        var portalId = AspxCommerce.utils.GetPortalID();
        var userName = AspxCommerce.utils.GetUserName();
        var cultureName = AspxCommerce.utils.GetCultureName();
        var customerId = AspxCommerce.utils.GetCustomerID();
        var ip = AspxCommerce.utils.GetClientIP();
        var countryName = AspxCommerce.utils.GetAspxClientCoutry();
        var sessionCode = AspxCommerce.utils.GetSessionCode();
        var userFriendlyURL = AspxCommerce.utils.IsUserFriendlyUrl();
        OnLinecustomers = {
            config: {
                isPostBack: false,
                async: false,
                cache: false,
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: '{}',
                dataType: "json",
                baseURL: aspxservicePath + "AspxCommerceWebService.asmx/",
                url: "",
                method: ""
            },
            init: function() {
                OnLinecustomers.SelectFirstTab();
                OnLinecustomers.LoadCustomerOnlineStaticImage();
                OnLinecustomers.bindRegisteredUserGrid(null, null, null);
                OnLinecustomers.bindAnonymousUserGrid(null, null);
                $("#btnSearchRegisteredUser").click(function() {
                    OnLinecustomers.SearchOnlineRegisteredUser();
                });
                $('#txtSearchUserName1,#txtSearchHostAddress1,#txtBrowserName1').keyup(function(event) {
                    if (event.keyCode == 13) {
                        OnLinecustomers.SearchOnlineRegisteredUser();
                    }
                });
                $("#btnSearchAnonymousUser").click(function() {
                    OnLinecustomers.SearchOnlineAnonymousUser();
                });
                $('#txtSearchHostAddress0,#txtBrowserName0').keyup(function(event) {
                    if (event.keyCode == 13) {
                        OnLinecustomers.SearchOnlineAnonymousUser();
                    }
                });
            },
            LoadCustomerOnlineStaticImage: function() {
                $('#ajaxCustomerOnline').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
                $('#ajaxCustomerOnline2').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
            },
            SelectFirstTab: function() {
                var $tabs = $('#container-7').tabs({ fx: [null, { height: 'show', opacity: 'show'}] });
                $tabs.tabs('select', 0);
            },

            bindRegisteredUserGrid: function(searchUsername, hostaddress, browser) {
                this.config.method = "GetRegisteredUserOnlineCount";
                var offset_ = 1;
                var current_ = 1;
                var perpage = ($("#gdvOnlineRegisteredUser_pagesize").length > 0) ? $("#gdvOnlineRegisteredUser_pagesize :selected").text() : 10;

                $("#gdvOnlineRegisteredUser").sagegrid({
                    url: this.config.baseURL,
                    functionMethod: this.config.method,
                    colModel: [
                { display: 'User Name', name: 'user_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
				{ display: 'Session User Host Address', name: 'hostaddress_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
				{ display: 'Session User Agent', name: 'agent_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
				{ display: 'Session Browser', name: 'browser_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
				{ display: 'Session URL', name: 'url_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                { display: 'Start Time', name: 'start_time', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left' }
                ],
                    rp: perpage,
                    nomsg: "No Records Found!",
                    param: { searchUserName: searchUsername, searchHostAddress: hostaddress, searchBrowser: browser, portalID: portalId, storeID: storeId, portalID: portalId, userName: userName },
                    current: current_,
                    pnew: offset_,
                    sortcol: { 0: { sorter: false }, 10: { sorter: false} }
                });
            },

            bindAnonymousUserGrid: function(hostaddress, browser) {
                this.config.method = "GetAnonymousUserOnlineCount";
                var offset_ = 1;
                var current_ = 1;
                var perpage = ($("#gdvOnlineAnonymousUser_pagesize").length > 0) ? $("#gdvOnlineAnonymousUser_pagesize :selected").text() : 10;

                $("#gdvOnlineAnonymousUser").sagegrid({
                    url: this.config.baseURL,
                    functionMethod: this.config.method,
                    colModel: [
                    // { display: 'S.No.', name: 'item_id', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'center', hide: true },
                    // { display: 'RowID', name: 'attr_id', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', checkFor: '5', elemClass: 'attrChkbox', elemDefault: false, controlclass: 'attribHeaderChkbox' },
				{display: 'User Name', name: 'user_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
				{ display: 'Session User Host Address', name: 'hostaddress_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
				{ display: 'Session User Agent', name: 'agent_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
				{ display: 'Session Browser', name: 'browser_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
				{ display: 'Session URL', name: 'url_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    //{ display: 'SessionUserAgent', name: 'attr_alias', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
				{display: 'Start Time', name: 'start_time', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left' }

				],
                    rp: perpage,
                    nomsg: "No Records Found!",
                    param: { searchHostAddress: hostaddress, searchBrowser: browser, portalID: portalId, storeID: storeId, portalID: portalId, userName: userName },
                    current: current_,
                    pnew: offset_,
                    sortcol: { 0: { sorter: false }, 10: { sorter: false} }
                });
            },

            SearchOnlineAnonymousUser: function() {
                var HostAddress = $.trim($("#txtSearchHostAddress0").val());
                var Browser = $.trim($("#txtBrowserName0").val());

                if (HostAddress.length < 1) {
                    HostAddress = null;
                }
                if (Browser.length < 1) {
                    Browser = null;
                }
                OnLinecustomers.bindAnonymousUserGrid(HostAddress, Browser);
            },

            SearchOnlineRegisteredUser: function() {
                var SearchUserName = $.trim($("#txtSearchUserName1").val());
                var HostAddress = $.trim($("#txtSearchHostAddress1").val());
                var Browser = $.trim($("#txtBrowserName1").val());

                if (SearchUserName.length < 1) {
                    SearchUserName = null;
                }
                if (HostAddress.length < 1) {
                    HostAddress = null;
                }
                if (Browser.length < 1) {
                    Browser = null;
                }
                OnLinecustomers.bindRegisteredUserGrid(SearchUserName, HostAddress, Browser);
            }
        }

        OnLinecustomers.init();
    });