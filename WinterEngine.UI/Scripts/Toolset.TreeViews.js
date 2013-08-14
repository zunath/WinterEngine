function BuildObjectTreeViewContextMenu(node) {
    var items = {
        "CreateCategory": {
            "label": "Create Category",
            "action": function (obj) {
                this.create(obj);
            }
        },
        "CreateObject": {
            "label": "Create Object",
            "action": function (obj) {
                $('#divNewObject').data('NewObjectCaller', this);
                $('#divNewObject').data('NewObjectParent', obj);
                $('#divNewObject').dialog('open');
            }
        },
        "Rename": {
            "label": "Rename",
            "action": function (obj) {
                this.rename(obj);
            }
        },
        "DeleteCategory": {
            "label": "Delete Category",
            "action": function (obj) {
                $('#divConfirmDelete').data('DeleteObjectCaller', this);
                $('#divConfirmDelete').data('DeleteObjectParent', obj);
                $('#divConfirmDelete').dialog('open');
            }
        },
        "DeleteObject": {
            "label": "Delete",
            "action": function (obj) {
                
                $('#divConfirmDelete').data('DeleteObjectCaller', this);
                $('#divConfirmDelete').data('DeleteObjectParent', obj);
                $('#divConfirmDelete').dialog('open');
            }
        }
    };
    
    if ($(node).data("nodetype") == "root") {
        delete items.Rename;
        delete items.CreateObject;
        delete items.DeleteCategory;
        delete items.DeleteObject;
    }
    else if ($(node).data("nodetype") == "category") {
        if ($(node).data("issystemresource") == "True") {
            delete items.DeleteCategory;
        }
        delete items.DeleteObject;
        delete items.CreateCategory;
    }
    else if ($(node).data("nodetype") == "object") {
        delete items.DeleteCategory;
        delete items.CreateCategory;
        delete items.CreateObject;
    }
    
    return items;
}

function PopulateTreeView(selector, data) {
    $(selector).jstree({
        animation: 0,
        "plugins": ["json_data", "ui", "themeroller", "sort", "crrm", "contextmenu", "types"],
        "json_data": {
            "data": [
                data
            ]
        },
        "types": {
            type_attr: "data-nodetype",
            types: {
                "root": {
                    "max_children": -1,
                    "valid_children": ["category"],
                    "delete_node": false
                },
                "category": {
                    "max_children": -1,
                    "valid_children": ["object"],
                },
                "object": {
                    "max_children": 0,
                    "valid_children": "none",
                    "create_node": false

                }
            }
        },
        "contextmenu": {
            items: BuildObjectTreeViewContextMenu
        },
        "crrm": {
        }
    });
}

function HideAllTreeViews() {
    $('.clsTreeViewDiv').addClass('clsHidden');
}

function LoadTreeViews_Callback(jsonAreas,
                                jsonCreatures,
                                jsonItems,
                                jsonPlaceables,
                                jsonConversations,
                                jsonScripts) {
    PopulateTreeView("#divAreaTreeView", $.parseJSON(jsonAreas));
    PopulateTreeView("#divCreatureTreeView", $.parseJSON(jsonCreatures));
    PopulateTreeView("#divItemTreeView", $.parseJSON(jsonItems));
    PopulateTreeView("#divPlaceableTreeView", $.parseJSON(jsonPlaceables));
    PopulateTreeView("#divConversationTreeView", $.parseJSON(jsonConversations));
    PopulateTreeView("#divScriptTreeView", $.parseJSON(jsonScripts));

    $('#divAreaTreeView').removeClass('clsHidden');
}

function CreateNewObject() {
    if (!$('#formNewObject').valid()) return;

    var parent = $('#divNewObject').data('NewObjectParent');
    var name = $('#txtObjectName').val();
    var tag = $('#txtObjectTag').val();
    var resref = $('#txtObjectResref').val();
    var categoryID = $(parent).data('categoryid');
    var gameObjectType = $('#hdnCurrentObjectMode').val();

    Entity.AddNewObject(name, tag, resref, categoryID, gameObjectType);

}

function CreateNewObject_Callback(success, errorMessage, gameObjectType, name) {
    if (success) {
        var caller = $('#divNewObject').data('NewObjectCaller');
        var parent = $('#divNewObject').data('NewObjectParent');
        caller.create(parent, null, name, null, true);
        CloseNewObjectBox();
    }
    else {
        $('#lblNewObjectErrors').text(errorMessage);
    }
}

function CloseNewObjectBox() {
    $('#divNewObject').removeData('NewObjectCaller');
    $('#divNewObject').removeData('NewObjectParent');
    $('#txtObjectName').val('');
    $('#txtObjectTag').val('');
    $('#txtObjectResref').val('');
    $('#lblNewObjectErrors').text('');

    $('#divNewObject').dialog('close');
}

function DeleteObject() {
    var activeTreeSelector = $('#hdnActiveObjectTreeSelector').val();
    var selectedNode = $(activeTreeSelector).jstree('get_selected');
    var gameObjectType = $('#hdnCurrentObjectMode').val();
    var resref = $(selectedNode).data('resref');

    Entity.DeleteObject(resref, gameObjectType);
}

function DeleteObject_Callback(success, errorMessage) {
    if (success) {
        var caller = $('#divConfirmDelete').data('DeleteObjectCaller');
        var parent = $('#divConfirmDelete').data('DeleteObjectParent');

        caller.remove(parent);
        CloseDeleteObjectBox();
    }
    else {
        $('#lblConfirmDeleteErrors').text(errorMessage);
    }
}

function CloseDeleteObjectBox() {
    $('#divConfirmDelete').removeData('DeleteObjectCaller');
    $('#divConfirmDelete').removeData('DeleteObjectParent');
    $('#lblConfirmDeleteErrors').text('');
    $('#divConfirmDelete').dialog('close');
}
