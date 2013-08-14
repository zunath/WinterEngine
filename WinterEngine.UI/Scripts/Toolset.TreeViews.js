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
            "label": "Delete Object",
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
    var caller = $('#divNewObject').data('NewObjectCaller');
    var parent = $('#divNewObject').data('NewObjectParent');

    CreateObject_JSCallback(caller, parent, $('#txtObjectName').val());
    CloseNewObjectBox();
}
function CloseNewObjectBox() {
    $('#divNewObject').removeData('NewObjectCaller');
    $('#divNewObject').removeData('NewObjectParent');
    $('#divNewObject').dialog('close');
}

function DeleteObject() {
    var caller = $('#divConfirmDelete').data('DeleteObjectCaller');
    var parent = $('#divConfirmDelete').data('DeleteObjectParent');

    DeleteObject_JSCallback(caller, parent);
    CloseDeleteObjectBox();
}

function CloseDeleteObjectBox() {
    $('#divConfirmDelete').dialog('close');
}

/* TreeView Callbacks */
function CreateObject_JSCallback(caller, parent, name) {
    caller.create(parent, null, name, null, true);
}

function DeleteObject_JSCallback(caller, parent) {
    caller.remove(parent);
}