var AttributeSetManage = "";
$(function() {
    var storeId = AspxCommerce.utils.GetStoreID();
    var portalId = AspxCommerce.utils.GetPortalID();
    var userName = AspxCommerce.utils.GetUserName();
    var cultureName = AspxCommerce.utils.GetCultureName();

    var treeHTML = '';
    var attGroup = new Array();

    var flag = '';
    var ID = '';
    var SetName = '';
    var SetID = '';
    AttributeSetManage = {
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
        ajaxCall: function(config) {
            $.ajax({
                type: AttributeSetManage.config.type,
                contentType: AttributeSetManage.config.contentType,
                cache: AttributeSetManage.config.cache,
                async: AttributeSetManage.config.async,
                data: AttributeSetManage.config.data,
                dataType: AttributeSetManage.config.dataType,
                url: AttributeSetManage.config.url,
                success: AttributeSetManage.ajaxSuccess,
                error: AttributeSetManage.ajaxFailure
            });
        },
        init: function() {
            AttributeSetManage.LoadAttributeSetMgmtStaticImage();
            AttributeSetManage.BindAttributeSetGrid(null, null, null);
            $("divAttribSetGrid").show();
            $("#divAttribSetAddForm").hide();
            $("#divAttribSetEditForm").hide();

            $('#btnSaveAttributeSet').click(function() {
                var errors = '';
                var attributeSetName = $('#txtAttributeSetName').val();
                if (!attributeSetName) {
                    errors += 'Please enter attribute set name.';
                }
                if (errors == '') {
                    AttributeSetManage.CheckUniqueness(0, attributeSetName, storeId, portalId, false);
                } else {
                    csscody.alert('<h1>Information Alert</h1><p>' + errors + '</p>');
                    $('#txtAttributeSetName').focus();
                    return false;
                }
            });

            $('.btnUpdateAttributeSet').click(function() {
                var errors = '';
                var attributeSetName = $('#txtOldAttributeSetName').val();
                if (!attributeSetName) {
                    errors += 'Please enter attribute set name.';
                }
                if (errors == '') {
                    var attributeSet_Id = 0;
                    if ($(this).attr("id")) {
                        attributeSet_Id = $(this).attr("id").replace( /[^0-9]/gi , '');
                    }
                    AttributeSetManage.CheckUniqueness(attributeSet_Id, attributeSetName, storeId, portalId, true);
                } else {
                    csscody.alert('<h1>Information Alert</h1><p>' + errors + '</p>');
                    $('#txtOldAttributeSetName').focus();
                    return false;
                }
            });

            $('#btnAddNewGroup').click(function() {
                var attributeSetId = $(".btnResetEdit").attr("id").replace( /[^0-9]/gi , '');
                AttributeSetManage.AddGroupName(attributeSetId);
            });

            $('#btnBackAdd').click(function() {
                AttributeSetManage.clearForm();
                //show grid only            
                $("#divAttribSetAddForm").hide();
                $("#divAttribSetEditForm").hide();
                $("#divAttribSetGrid").show();
            });

            $('#btnBackEdit').click(function() {
                AttributeSetManage.clearForm();
                $("#divAttribSetAddForm").hide();
                $("#divAttribSetEditForm").hide();
                $("#divAttribSetGrid").show();
            });

            $('#btnAddNewSet').click(function() {
                AttributeSetManage.clearForm();
                AttributeSetManage.BindAttributesSet();
                $("#divAttribSetGrid").hide();
                $("#divAttribSetEditForm").hide();
                $("#divAttribSetAddForm").show();
            });

            $(".btnResetEdit").click(function() {
                $('#dvTree').html('');
                treeHTML = '';
                attGroup = new Array();
                //Get the Id of the attribute to delete
                var attributeSetId = $(this).attr("id").replace( /[^0-9]/gi , '');
                AttributeSetManage.CallEditFunction(attributeSetId);
            });

            $(".btnDeleteAttributeSet").click(function() {
                //Get the Id of the attribute to delete
                var attributeSetId = $(this).attr("id").replace( /[^0-9]/gi , '');
                AttributeSetManage.DeleteAttributeSet(attributeSetId, storeId, portalId, userName);
            });
            $('#txtSearchAttributeSetName,#ddlIsActive,#ddlUserInSystem').keyup(function(event) {
                if (event.keyCode == 13) {
                    AttributeSetManage.SearchAttributeSetName();
                }
            });
        },

        LoadAttributeSetMgmtStaticImage: function() {
            $('#ajaxAttributeSetMgmtImage').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
        },

        AddGroupName: function(_attributeSetId) {
            var x = '';
            var properties = { 'onComplete': function(x) { AttributeSetManage.AddNewGroup(_attributeSetId, x); } };
            csscody.prompt("<h1>Please Enter the Group Name</h1><p>write a name here</p>", x, properties);
        },

        AddNewGroup: function(_attributeSetId, node) {
            //alert(_attributeSetId+'::'+node);  
            SetID = '';
            SetID = _attributeSetId;
            if (node) {
                var _isActive = true;
                var _isModified = false;
                var _updateFlag = false;
                var _groupId = 0;
                //First call [sp_AspxAttributeGroupAddUpdate] then bind again the tree with id and then add JQuery dragdrop
                this.config.url = this.config.baseURL + "SaveUpdateAttributeGroupInfo";
                this.config.data = JSON2.stringify({ attributeSetId: _attributeSetId, groupName: node, groupID: _groupId, cultureName: cultureName, aliasName: node, storeId: storeId, portalId: portalId, userName: userName, isActive: _isActive, isModified: _isModified, flag: _updateFlag });
                this.config.ajaxCallMode = 1;
                this.ajaxCall(this.config);
            }
        },

        BindAttributeSetGrid: function(attributeSetNm, isAct, userInSystem) {
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvAttributeSet_pagesize").length > 0) ? $("#gdvAttributeSet_pagesize :selected").text() : 10;

            $("#gdvAttributeSet").sagegrid({
                url: this.config.baseURL,
                functionMethod: 'GetAttributeSetGrid',
                colModel: [
                    { display: 'Attribute Set ID', name: 'attr_id', cssclass: 'cssClassHeadCheckBox', hide: true, coltype: 'checkbox', align: 'center' },
                    { display: 'Attribute Set Name', name: 'attr_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Used In System', name: 'IsSystemUsed', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No' },
                    { display: 'Is Active', name: 'IsActive', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No' },
                    { display: 'Actions', name: 'action', cssclass: 'cssClassAction', controlclass: '', coltype: 'label', align: 'center' }
                ],
                buttons: [{ display: 'Edit', name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'AttributeSetManage.EditAttributesSet', arguments: '2' },
                    { display: 'Delete', name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'AttributeSetManage.DeleteAttributesSet', arguments: '2' },
                    { display: 'Active', name: 'active', enable: true, _event: 'click', trigger: '4', callMethod: 'AttributeSetManage.ActiveAttributesSet', arguments: '2' },
                    { display: 'Deactive', name: 'deactive', enable: true, _event: 'click', trigger: '5', callMethod: 'AttributeSetManage.DeactiveAttributesSet', arguments: '2' }
                ],
                rp: perpage,
                nomsg: "No Records Found!",
                param: { attributeSetName: attributeSetNm, isActive: isAct, usedInSystem: userInSystem, storeId: storeId, portalId: portalId, cultureName: cultureName, userName: userName },
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 4: { sorter: false } }
            });
        },

        show: function(id) {
            el = document.getElementById(id);
            if (el.style.display == 'none') {
                el.style.display = '';
            } else {
                el.style.display = 'none';
            }
        },

        DeleteAttributesSet: function(tblID, argus) {
            switch (tblID) {
            case "gdvAttributeSet":
                if (argus[3].toLowerCase() != "yes") {
                    AttributeSetManage.DeleteAttributeSet(argus[0], storeId, portalId, userName);
                } else {
                    csscody.alert('<h1>Information Alert</h1><p>You can\'t delete System Attribute Set</p>');
                    return false;
                }
                break;
            default:
                break;
            }
        },

        ActiveAttributesSet: function(tblID, argus) {
            switch (tblID) {
            case "gdvAttributeSet":
                if (argus[3].toLowerCase() != "yes") {
                    AttributeSetManage.ActivateAttributeSet(argus[0], storeId, portalId, userName, true);
                } else {
                    csscody.alert('<h1>Information Alert</h1><p>You can\'t activate System Attribute Set</p>');
                }
                break;
            default:
                break;
            }
        },

        DeactiveAttributesSet: function(tblID, argus) {
            switch (tblID) {
            case "gdvAttributeSet":
                if (argus[3].toLowerCase() != "yes") {
                    AttributeSetManage.ActivateAttributeSet(argus[0], storeId, portalId, userName, false);
                } else {
                    csscody.alert('<h1>Information Alert</h1><p>You can\'t deactivate System Attribute Set</p>');
                }
                break;
            default:
                break;
            }
        },

        DeleteAttributeSet: function(_attributeSetId, _storeId, _portalId, _userName) {
            // Ask user's confirmation before delete records
            var properties = {
                onComplete: function(e) {
                    AttributeSetManage.DeleteAttributeSetID(_attributeSetId, _storeId, _portalId, _userName, e);
                }
            }
            csscody.confirm("<h1>Delete Confirmation</h1><p>Are you sure you want to delete this attribute set?</p>", properties);
        },

        DeleteAttributeSetID: function(_attributeSetId, _storeId, _portalId, _userName, event) {
            if (event) {
                //Pass the selected attribute id and other parameters
                this.config.url = this.config.baseURL + "DeleteAttributeSetByAttributeSetID";
                this.config.data = JSON2.stringify({ attributeSetId: parseInt(_attributeSetId), storeId: _storeId, portalId: _portalId, userName: _userName });
                this.config.ajaxCallMode = 2;
                this.ajaxCall(this.config);
            }
            return false;
        },

        ActivateAttributeSet: function(_attributeSetId, _storeId, _portalId, _userName, _isActive) {
            //Pass the selected attribute id and other parameters
            this.config.url = this.config.baseURL + "UpdateAttributeSetIsActiveByAttributeSetID";
            this.config.data = JSON2.stringify({ attributeSetId: parseInt(_attributeSetId), storeId: _storeId, portalId: _portalId, userName: _userName, isActive: _isActive });
            this.config.ajaxCallMode = 3;
            this.ajaxCall(this.config);
            return false;
        },

        CheckUniqueness: function(AttributeSetID, AttributeSetName, StoreID, PortalID, UpdateFlag) {
            flag = UpdateFlag;
            ID = AttributeSetID;
            SetName = AttributeSetName;
            this.config.url = this.config.baseURL + "CheckAttributeSetUniqueness";
            this.config.data = JSON2.stringify({ attributeSetId: AttributeSetID, attributeSetName: AttributeSetName, storeId: StoreID, portalId: PortalID, updateFlag: UpdateFlag });
            this.config.ajaxCallMode = 4;
            this.ajaxCall(this.config);
        },

        SaveAttributeSetTree: function() {
            var saveString = '';
            var attributeIds = '';
            $("#tree>li").each(function(i) {
                if (!AttributeSetManage.isUnassignedNode($(this))) {
                    var groupIds = this.id.replace( /[^0-9]/gi , '');
                    $(this).find('li').each(function() {
                        attributeIds += this.id.replace( /[^0-9]/gi , '') + ',';
                    });
                    attributeIds = attributeIds.substr(0, attributeIds.length - 1);
                    if (attributeIds == '') {
                        attributeIds = 0;
                    }
                    saveString += groupIds + '-' + attributeIds + '#';
                    attributeIds = '';
                }
            });
            return saveString = saveString.substr(0, saveString.length - 1);
        },

        isUnassignedNode: function(li) {
            return (li.hasClass('unassigned-attributes'));
        },

        AddUpdateAttributeSet: function(attributeSet_id, _attributeSetBaseID, attributeSetName, storeId, portalId, isActive, isModified, userName, _updateFlag, _saveString) {
            AttributeSetManage.AttributeSetAddUpdate(attributeSet_id, _attributeSetBaseID, attributeSetName, storeId, portalId, isActive, isModified, userName, _updateFlag, _saveString);
        },

        clearForm: function() {
            $(".btnResetEdit").removeAttr("id");
            $(".btnDeleteAttributeSet").removeAttr("id");
            $(".btnUpdateAttributeSet").removeAttr("id");
            $("#txtAttributeSetName").val('');
            $('#' + lblAttributeSetInfo).html("Edit Attribute Set");
            $("#txtAttributeSetName").val('');
        },

        EditAttributesSet: function(tblID, argus) {
            switch (tblID) {
            case "gdvAttributeSet":
                if (argus[3].toLowerCase() != "yes") {
                    AttributeSetManage.CallEditFunction(argus[0]);
                } else {
                    csscody.alert('<h1>Information Alert</h1><p>Sorry! System attribute set can not be updated.</p>');
                    return false;
                }
                break;
            default:
                break;
            }
        },

        FillForm: function(response) {
            $.each(response.d, function(index, item) {
                if (index == 0) {
                    $('#' + lblAttributeSetInfo).html("Edit Attribute Set: '" + item.AttributeSetName + "'");
                    $("#txtOldAttributeSetName").val(item.AttributeSetName);

                    if (item.IsSystemUsed) {
                        $(".btnDeleteAttributeSet").hide();
                    } else {
                        $(".btnDeleteAttributeSet").show();
                    }
                }
            });
        },

        AttributeSetAddUpdate: function(_attributeSetId, _attributeSetBaseId, _attributeSetName, _storeId, _portalId, _isActive, _isModified, _userName, _flag, _saveString) {
            flag = _flag;
            this.config.url = this.config.baseURL + "SaveUpdateAttributeSetInfo";
            this.config.data = JSON2.stringify({ attributeSetId: _attributeSetId, attributeSetBaseId: _attributeSetBaseId, attributeSetName: _attributeSetName, storeId: _storeId, portalId: _portalId, isActive: _isActive, isModified: _isModified, userName: _userName, flag: _flag, saveString: _saveString });
            this.config.ajaxCallMode = 5;
            this.ajaxCall(this.config);
        },

        BindAttributesSet: function() {
            var _attributeSetId = 0;
            this.config.url = this.config.baseURL + "GetAttributeSetList";
            this.config.data = JSON2.stringify({ attributeSetId: _attributeSetId, storeId: storeId, portalId: portalId, userName: userName });
            this.config.ajaxCallMode = 6;
            this.ajaxCall(this.config);
        },

        CallEditFunction: function(_attributeSetId) {
            $(".btnResetEdit").attr("id", 'attributesetid_' + _attributeSetId);
            $(".btnDeleteAttributeSet").attr("id", 'attributesetid_' + _attributeSetId);
            $(".btnUpdateAttributeSet").attr("id", 'attributesetid_' + _attributeSetId);
            $("#divAttribSetGrid").hide();
            $("#divAttribSetAddForm").hide();
            $("#divAttribSetEditForm").show();
            var functionName = 'GetAttributeSetDetailsByAttributeSetID';
            var params = { attributeSetId: _attributeSetId, storeId: storeId, portalId: portalId, userName: userName, cultureName: cultureName };
            var mydata = JSON2.stringify(params);
            $.ajax({
                type: "POST",
                url: aspxservicePath + "AspxCommerceWebService.asmx/" + functionName,
                data: mydata,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(msg) {
                    treeHTML = '';
                    $("#tree").removeClass('ui-tree');
                    $("#tree").html('');
                    AttributeSetManage.FillForm(msg);
                    AttributeSetManage.BindTreeView(msg);
                    attGroup = new Array();
                    AttributeSetManage.AddDragDrop();
                    AttributeSetManage.AddContextMenu();
                },
                error: function() {
                    csscody.error('<h1>Error Message</h1><p>Failed to update attributes set.</p>');
                }
            });
            //        $(".btnResetEdit").attr("id", 'attributesetid_' + _attributeSetId);
            //        $(".btnDeleteAttributeSet").attr("id", 'attributesetid_' + _attributeSetId);
            //        $(".btnUpdateAttributeSet").attr("id", 'attributesetid_' + _attributeSetId);
            //        $("#divAttribSetGrid").hide();
            //        $("#divAttribSetAddForm").hide();
            //        $("#divAttribSetEditForm").show();
            //             this.config.url=this.config.baseURL+"GetAttributeSetDetailsByAttributeSetID";
            //             this.config.data=JSON2.stringify({ attributeSetId: _attributeSetId, storeId: storeId, portalId: portalId, userName: userName, cultureName: cultureName });
            //             this.config.ajaxCallMode=7;
            //             this.ajaxCall(this.config);
        },

        AddDragDrop: function() {
            $("#tree").tree({
                expand: '*',
                drop: function(event, ui) {
                    $('.ui-tree-droppable').removeClass('ui-tree-droppable ui-tree-droppable-top ui-tree-droppable-center ui-tree-droppable-bottom');
                    switch (ui.overState) {
                    case 'top':
                        if ((ui.sender.isNode(ui.draggable) == ui.target.isNode(ui.droppable)) && !ui.sender.isUnassignedNode(ui.draggable) && !ui.target.isUnassignedNode(ui.droppable)) {
                            ui.target.before(ui.sender.getJSON(ui.draggable), ui.droppable);
                            ui.sender.remove(ui.draggable);
                        }
                        break;
                    case 'bottom':
                        if ((ui.sender.isNode(ui.draggable) == ui.target.isNode(ui.droppable)) && !ui.sender.isUnassignedNode(ui.draggable) && !ui.target.isUnassignedNode(ui.droppable)) {
                            ui.target.after(ui.sender.getJSON(ui.draggable), ui.droppable);
                            ui.sender.remove(ui.draggable);
                        }
                        break;
                    case 'center':
                        if (!ui.sender.isNode(ui.draggable) && ui.target.isNode(ui.droppable)) {
                            ui.target.append(ui.sender.getJSON(ui.draggable), ui.droppable);
                            ui.sender.remove(ui.draggable);
                        }
                        break;
                    }
                },
                over: function(event, ui) {
                    $(ui.droppable).addClass('ui-tree-droppable');
                    //                if (ui.target.isUnassignedNode(ui.droppable)) {
                    //                    $('.ui-tree-droppable').removeClass('ui-tree-droppable ui-tree-droppable-top ui-tree-droppable-center ui-tree-droppable-bottom');
                    //                }
                },
                out: function(event, ui) {
                    $(ui.droppable).removeClass('ui-tree-droppable');
                },
                overtop: function(event, ui) {
                    //if (ui.sender.isNode(ui.draggable) == ui.target.isNode(ui.droppable)) {
                    $(ui.droppable).addClass('ui-tree-droppable-top');
                    // }
                },
                overcenter: function(event, ui) {
                    //if (!ui.sender.isNode(ui.draggable) && ui.target.isNode(ui.droppable)) {
                    $(ui.droppable).addClass('ui-tree-droppable-center');
                    //}
                },
                overbottom: function(event, ui) {
                    //if (ui.sender.isNode(ui.draggable) == ui.target.isNode(ui.droppable)) {
                    $(ui.droppable).addClass('ui-tree-droppable-bottom');
                    //}
                },
                outtop: function(event, ui) {
                    $(ui.droppable).removeClass('ui-tree-droppable-top');
                },
                outcenter: function(event, ui) {
                    $(ui.droppable).removeClass('ui-tree-droppable-center');
                },
                outbottom: function(event, ui) {
                    $(ui.droppable).removeClass('ui-tree-droppable-bottom');
                }
            });
        },

        AddContextMenu: function() {
            $('#tree>li').each(function(i) {
                if (!AttributeSetManage.isUnassignedNode($(this))) {
                    $(this).contextMenu('myMenu1', {
                    //                    onContextMenu: function(e) {
                    //                        //$("p").parent(".selected").css("background", "yellow");
                    //                        //alert($(e.target).parent("li").html());
                    //                        //if ($(e.target).parent("li").html() == null) return true;
                    //                        //else return false;

                    //                    },
                        onShowMenu: function(e, menu) {
                            if (AttributeSetManage.isNode(AttributeSetManage.getSelect().parent('li'))) {
                                if (AttributeSetManage.isSystemUsedGroup(AttributeSetManage.getSelect().parent('li'))) {
                                    $('#remove, #delete', menu).remove();
                                } else {
                                    $('#remove', menu).remove();
                                }
                            } else if (AttributeSetManage.isSystemUsedAttribute(AttributeSetManage.getSelect().parent('li'))) {
                                $('#rename, #delete, #remove', menu).remove();
                                $(menu).html('');
                            } else {
                                $('#rename, #delete', menu).remove();
                            }
                            return menu;
                        },
                        bindings: {
                            'rename': function(t) {
                                var value = AttributeSetManage.GetTitle(t);
                                //alert('Trigger was ' + t.id + '\nAction was Open');
                                var html = "<input id=\"txtEdit\" type=\"text\" value=\"" + value + "\" onblur=\"AttributeSetManage.SaveValue(this, '" + value + "')\" onfocus=\"PutCursorAtEnd(this)\" />";
                                AttributeSetManage.SetTitle(t, html);
                                $("#txtEdit").focus();
                            },
                            'delete': function(t) {
                                var attributeSetId = $(".btnResetEdit").attr("id").replace( /[^0-9]/gi , '');
                                var groupId = t.id.replace( /[^0-9]/gi , '');

                                // Ask user's confirmation before delete records
                                var properties = {
                                    onComplete: function(x) {
                                        AttributeSetManage.DeleteGroupFromAttributeSetID(attributeSetId, groupId, x);
                                    }
                                }
                                csscody.confirm("<h1>Delete Confirmation</h1><p>Are you sure to delete this group?</p>", properties);
                                //$('#tree').tree('nodeName', $('#tree').find('li'))
                                //$('#tree').tree('remove',$(this));
                                //alert(t);
                            },
                            'remove': function(t) {
                                //alert($(t).html());
                                var attributeSet_Id = $(".btnResetEdit").attr("id").replace( /[^0-9]/gi , '');
                                var groupId = t.id.replace( /[^0-9]/gi , '');
                                var is = AttributeSetManage.getSelect();
                                var liElement = is.parent("LI");
                                var attributeId = liElement[0].id.replace( /[^0-9]/gi , '');
                                // Ask user's confirmation before delete records
                                var properties = {
                                    onComplete: function(x) {
                                        AttributeSetManage.DeleteAttributeFromAttributeSetID(attributeSet_Id, groupId, attributeId, x);
                                    }
                                }
                                csscody.confirm("<h1>Remove Confirmation</h1><p>Are you sure to remove this attribute?</p>", properties);
                            }
                        },
                        menuStyle: {
                            border: '1px solid #000'
                        },
                        itemStyle: {
                            display: 'block',
                            cursor: 'pointer',
                            padding: '3px',
                            border: '1px solid #fff',
                            backgroundColor: 'transparent'
                        },
                        itemHoverStyle: {
                            border: '1px solid #0a246a',
                            backgroundColor: '#b6bdd2'
                        }
                    });
                }
            });
        },

        getSelect: function() {
            var select = $('.ui-tree-selected', this.element);
            if (select.length) return select;
            else return null;
        },

        isNode: function(li) {
            return (li.hasClass('ui-tree-node'));
        },

        isSystemUsedGroup: function(li) {
            return (li.hasClass('systemused-groups'));
        },

        isSystemUsedAttribute: function(li) {
            return (li.hasClass('systemused-attributes'));
        },

        PutCursorAtEnd: function(obj) {
            //        if (obj.value == obj.defaultValue) {
            //            $(obj).putCursorAtEnd(obj.length);
            //        }
            //bugs on js 1.4 fix if use 1.4.1
            // $('#txtEdit').focus().val($('#txtEdit').val());

        },

        GetTitle: function(node) {
            var title = $('>span.ui-tree-title', node);
            var html = '';
            if (title.length) {
                html = title.text().replace( /<span class="?ui-tree-title-img"?[^>]*>[\s\S]*?<\/span>/gi , '');
            } else {
                node = node.length ? node : $(node);
                html = node.text().replace( /<ul[^>]*>[\s\S]*<\/ul>/gi , '').replace( /\s*style="[^"]*"/gi , '');
            }
            return $.trim(html.replace( /\n/g , '').replace( /<a[^>]*>/gi , '').replace( /<\/a>/gi , ''));
        },

        SetTitle: function(node, title) {
            this.GetSPAN(node).html(title);
            //AttributeSetManage.GetSPAN(node).html(title);
        },

        GetSPAN: function(node) {
            node = node.length ? node : $(node);
            if (this.parentNodeName(node) == 'li') return $('>span.ui-tree-title:eq(0)', node);
            else if (this.parentNodeName(node) == 'ul') return $('span.ui-tree-title:eq(0)', node.parent());
            else return node;
        },

        parentNodeName: function(node) {
            if (node.attr != undefined) {
                return (node.length ? node.attr('nodeName') : $(node).attr('nodeName')).toLowerCase();
            }
        },

        SaveValue: function(obj, oldValue) {
            var functionName = 'RenameAttributeSetGroupAliasByGroupID';
            var value = $(obj).val();
            //$(obj).parent().removeClass("edit");
            if (value != "") {
                if (value != oldValue) {
                    //var property = $(obj).parent().attr("id");
                    var attributeSetId = $(".btnResetEdit").attr("id").replace( /[^0-9]/gi , '');
                    var id = $(obj).parent().parent().attr("id");
                    id = id.replace( /[^0-9]/gi , '');

                    $(obj).parent().html('<img id="imgSaving' + id + '" src="' + aspxTemplateFolderPath + '/images/saving.gif" alt="Saving..." />');

                    var params = { groupId: id, cultureName: cultureName, aliasName: value, attributeSetId: attributeSetId, storeId: storeId, portalId: portalId, isActive: true, isModified: true, userName: userName };
                    var mydata = JSON2.stringify(params);
                    $.ajax({
                        type: "POST",
                        url: aspxservicePath + "AspxCommerceWebService.asmx/" + functionName,
                        data: mydata,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: AttributeSetManage.SaveGroup_Success,
                        error: AttributeSetManage.Error
                    });
                } else {
                    $(obj).parent().html('<b>' + oldValue + '</b>');
                }
            } else {
                csscody.alert('<h1>Information Alert</h1><p>Please enter group name.</p>');
                $(obj).parent().html('<b>' + oldValue + '</b>');
            }
        },

        SaveGroup_Success: function(data, status) {
            $.each(data.d, function(index, item) {
                $("#imgSaving" + item.GroupID).parent().html('<b>' + item.AliasName + '</b>');
            });
        },

        Error: function(request, status, error) {
            csscody.error('<h1>Error Message</h1><p>' + request.statusText + '</p>');
        },

        DeleteAttributeFromAttributeSetID: function(attributeSetId, groupId, attributeId, event) {
            SetID = attributeSetId;
            if (event) {
                this.config.url = this.config.baseURL + "DeleteAttributeByAttributeSetID";
                this.config.data = JSON2.stringify({ attributeSetId: attributeSetId, groupId: groupId, attributeId: attributeId, storeId: storeId, portalId: portalId, userName: userName });
                this.config.ajaxCallMode = 8;
                this.ajaxCall(this.config);
            }
            return false;
        },

        DeleteGroupFromAttributeSetID: function(attributeSetId, groupId, event) {
            SetID = '';
            SetID = attributeSetId;
            if (event) {
                this.config.url = this.config.baseURL + "DeleteAttributeSetGroupByAttributeSetID";
                this.config.data = JSON2.stringify({ attributeSetId: attributeSetId, groupId: groupId, storeId: storeId, portalId: portalId, userName: userName, cultureName: cultureName });
                this.config.ajaxCallMode = 9;
                this.ajaxCall(this.config);
            }
            return false;
        },

        BindTreeView: function(response) {
            var unassignedAttributeExist = false;
            var unassignedAttributesIds = '';
            var unassignedAttributesName = '';
            var groupId = '';
            treeHTML += '<ul id="tree">';
            $.each(response.d, function(index, item) {
                AttributeSetManage.BindGroup(index, item.GroupID, item.GroupName, item.AttributeID, item.AttributeName, item.IsSystemUsed, item.IsSystemUsedGroup);
                groupId = item.GroupID;
                if (index == 0 && item.UnassignedAttributes != "") {
                    unassignedAttributeExist = true;
                    unassignedAttributesIds = item.UnassignedAttributes;
                    unassignedAttributesName = item.UnassignedAttributesName;
                }
            });

            if (unassignedAttributeExist) {
                AttributeSetManage.AddUnassignedAttribute(groupId, unassignedAttributesIds, unassignedAttributesName);
            }
            if (groupId > 0) {
                treeHTML += '</ul></li>';
            }
            treeHTML += '</ul>';
            $("#dvTree").html(treeHTML);
        },

        AddUnassignedAttribute: function(groupId, unassignedAttributesIds, unassignedAttributesName) {
            if (groupId > 0) {
                treeHTML += '</ul></li>';
            }
            treeHTML += '<li id="group" class="file-folder unassigned-attributes"><b>Unassigned Attributes</b><ul>';
            var unassignedName = unassignedAttributesName.split(',');
            var unassignedId = unassignedAttributesIds.split(',');
            for (var i = 0; i < unassignedId.length; i++) {
                //alert(unassignedId[i] + '::' + unassignedName[i]);
                treeHTML += '<li id="attribute' + unassignedId[i] + '" class="file php">' + unassignedName[i] + '</li>';
            }
            treeHTML += '</ul></li>';
        },

        BindGroup: function(index, groupID, groupName, attributeID, attributeName, isSystemUsedAttrib, isSystemUsedGroup) {
            if (groupID > 0) {
                var isGroupExist = false;
                for (var i = 0; i < attGroup.length; i++) {
                    if (attGroup[i].key == groupID) {
                        isGroupExist = true;
                        break;
                    }
                }
                if (!isGroupExist) {
                    if (attGroup.length != 0) {
                        treeHTML += '</ul></li>';
                    }
                    if (isSystemUsedGroup) {
                        treeHTML += '<li id="group' + groupID + '" class="file-folder systemused-groups"><b>' + groupName + '</b><ul>';
                    } else {
                        treeHTML += '<li id="group' + groupID + '" class="file-folder"><b>' + groupName + '</b><ul>';
                    }
                }
                if (attributeName != "") {
                    if (isSystemUsedAttrib) {
                        treeHTML += '<li id="attribute' + attributeID + '" class="file html systemused-attributes">' + attributeName + '</li>';
                    } else {
                        treeHTML += '<li id="attribute' + attributeID + '" class="file html">' + attributeName + '</li>';
                    }
                }
                if (!isGroupExist) {
                    attGroup.push({ key: groupID, value: groupName });
                }
            }
        },
        SearchAttributeSetName: function() {
            var attributeSetNm = $.trim($("#txtSearchAttributeSetName").val());
            var isAct = $.trim($('#ddlIsActive').val()) == "" ? null : $.trim($('#ddlIsActive').val()) == "True" ? true : false;
            var userInSystem = $.trim($("#ddlUserInSystem").val()) == "" ? null : $.trim($("#ddlUserInSystem").val()) == "True" ? true : false;
            if (attributeSetNm.length < 1) {
                attributeSetNm = null;
            }
            AttributeSetManage.BindAttributeSetGrid(attributeSetNm, isAct, userInSystem);
        },
        ajaxSuccess: function(msg) {
            switch (AttributeSetManage.config.ajaxCallMode) {
            case 0:
                break;
            case 1:
                AttributeSetManage.CallEditFunction(SetID);
                break;
            case 2:
                AttributeSetManage.BindAttributeSetGrid(null, null, null);
                csscody.info("<h2>Successful Message</h2><p>Attribute set has been deleted successfully.</p>");
                AttributeSetManage.clearForm();
                $("#divAttribSetAddForm").hide();
                $("#divAttribSetEditForm").hide();
                $("#divAttribSetGrid").show();
                break;
            case 3:
                AttributeSetManage.BindAttributeSetGrid(null, null, null);
                csscody.info("<h2>Successful Message</h2><p>Attribute set's status has been updated successfully.</p>");
                break;
            case 4:
                var errors = '';
                if (!msg.d) {
                    var isActive = true;
                    var saveString = '';
                    var _AttributeSetBaseID = 0;
                    _AttributeSetBaseID = $("#ddlAttributeSet").val();
                    if (!_AttributeSetBaseID) {
                        _AttributeSetBaseID = 0;
                    }
                    var isModified = false;
                    if (flag) {
                        isModified = true;
                        saveString = AttributeSetManage.SaveAttributeSetTree();
                    }
                    AttributeSetManage.AddUpdateAttributeSet(ID, _AttributeSetBaseID, SetName, storeId, portalId, isActive, isModified, userName, flag, saveString);
                    AttributeSetManage.BindAttributeSetGrid(null, null, null);
                } else {
                    errors += 'Please enter unique attribute set name! "' + SetName + '" already exists.<br/>';
                    //errors += "Attribute set with the '" + $('#txtAttributeSetName').val() + "' name already exists."; //' - Please enter unique attribute set name'; //Attribute set with the "sss" name already exists.
                    //$('#txtAttributeSetName').val('');
                    csscody.alert('<h1>Information Alert</h1><p>' + errors + '</p>');
                    $('#txtAttributeSetName').focus();
                    return false;
                }
                break;
            case 5:
                if (!flag) {
                    AttributeSetManage.CallEditFunction(msg.d);
                } else {
                    AttributeSetManage.clearForm();
                    $("#divAttribSetAddForm").hide();
                    $("#divAttribSetEditForm").hide();
                    $("#divAttribSetGrid").show();
                    csscody.info("<h2>Successful Message</h2><p>Attribute set has been saved successfully.</p>");
                }
                break;
            case 6:
                $("#ddlAttributeSet").get(0).options.length = 0;
                $.each(msg.d, function(index, item) {
                    $("#ddlAttributeSet").get(0).options[$("#ddlAttributeSet").get(0).options.length] = new Option(item.AttributeSetName, item.AttributeSetID);
                });
                break;
            case 7:
                break;
            case 8:
                AttributeSetManage.CallEditFunction(SetID);
                AttributeSetManage.AddDragDrop();
                AttributeSetManage.AddContextMenu();
                break;
            case 9:
                AttributeSetManage.CallEditFunction(SetID)
                break;
            }
        },
        ajaxFailure: function() {
            switch (AttributeSetManage.config.ajaxCallMode) {
            case 0:
                break;
            case 1:
                csscody.error('<h1>Error Message</h1><p>Failed to update attribute group</p>');
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                csscody.error('<h1>Error Message</h1><p>Failed to check attribute set uniqueness</p>');
                break;
            case 5:
                csscody.error('<h1>Error Message</h1><p>Failed to update attribute set</p>');
                break;
            case 6:
                csscody.error('<h1>Error Message</h1><p>Failed to load attribute set</p>');
                break;
            case 7:
                    //csscody.error('<h1>Error Message</h1><p>Failed to edit attributes</p>');
                break;
            case 8:
                csscody.error('<h1>Error Message</h1><p>Failed to remove attribute</p>');
                break;
            case 9:
                csscody.error('<h1>Error Message</h1><p>Failed to delete attribute set group</p>');
                break;
            }
        }
    };
    AttributeSetManage.init();
});