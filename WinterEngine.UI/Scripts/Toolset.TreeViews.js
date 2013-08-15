function BuildObjectTreeViewContextMenu(node) {
    var items = {
        "CreateCategory": {
            "label": "Create Category",
            "action": function (obj) {
                $('#divCreateCategory').data('Caller', this);
                $('#divCreateCategory').data('Parent', obj);
                $('#divCreateCategory').dialog('open');
            }
        },
        "CreateObject": {
            "label": "Create Object",
            "action": function (obj) {
                $('#divNewObject').data('Caller', this);
                $('#divNewObject').data('Parent', obj);
                $('#divNewObject').dialog('open');
            }
        },
        "RenameCategory": {
            "label": "Rename",
            "action": function (obj) {
                $('#divRenameTreeNode').data('RenameMode', 'RenameCategory');
                $('#divRenameTreeNode').data('Caller', this);
                $('#divRenameTreeNode').data('Parent', obj);

                $('#divRenameTreeNode').dialog('open');
            }
        },
        "RenameObject": {
            "label": "Rename",
            "action": function (obj) {
                $('#divRenameTreeNode').data('RenameMode', 'RenameObject');
                $('#divRenameTreeNode').data('Caller', this);
                $('#divRenameTreeNode').data('Parent', obj);

                $('#divRenameTreeNode').dialog('open');
            }
        },
        "DeleteCategory": {
            "label": "Delete Category",
            "action": function (obj) {
                $('#divConfirmDelete').data('Caller', this);
                $('#divConfirmDelete').data('Parent', obj);
                $('#divConfirmDelete').data('DeleteMode', 'DeleteCategory');
                $('#lblConfirmDelete').text('Are you sure you want to delete this category? ' +
                    'All objects contained in this category will also be deleted.');
                $('#divConfirmDelete').dialog('open');
            }
        },
        "DeleteObject": {
            "label": "Delete",
            "action": function (obj) {
                
                $('#divConfirmDelete').data('Caller', this);
                $('#divConfirmDelete').data('Parent', obj);
                $('#divConfirmDelete').data('DeleteMode', 'DeleteObject');
                $('#lblConfirmDelete').text('Are you sure you want to delete this ' + $('#hdnCurrentObjectMode').val() + '?');
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
        delete items.RenameObject;
        delete items.DeleteObject;
        delete items.CreateCategory;
    }
    else if ($(node).data("nodetype") == "object") {
        delete items.DeleteCategory;
        delete items.CreateCategory;
        delete items.CreateObject;
        delete items.RenameCategory;
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

    var currentTreeSelector = $('#hdnActiveObjectTreeSelector').val();
    $(currentTreeSelector).removeClass('clsHidden');
}

function CreateNewCategory() {
    if (!$('#formNewCategory').valid()) return;

    var name = $('#txtCategoryName').val();
    var gameObjectType = $('#hdnCurrentObjectMode').val();

    Entity.AddNewCategory(name, gameObjectType);
}

function CreateNewCategory_Callback(success, errorMessage, name, categoryID) {
    if (success) {
        var caller = $('#divCreateCategory').data('Caller');
        var parent = $('#divCreateCategory').data('Parent');
        var newCategory = caller.create(parent, null, name, null, true);
        $(newCategory).data('nodetype', 'category');
        $(newCategory).data('categoryid', categoryID);
        
        CloseNewCategoryBox();
    }
    else {
        $('#lblNewCategoryErrors').text(errorMessage);
    }
}

function CreateNewObject() {
    if (!$('#formNewObject').valid()) return;

    var parent = $('#divNewObject').data('Parent');
    var name = $('#txtObjectName').val();
    var tag = $('#txtObjectTag').val();
    var resref = $('#txtObjectResref').val();
    var categoryID = $(parent).data('categoryid');
    var gameObjectType = $('#hdnCurrentObjectMode').val();

    Entity.AddNewObject(name, tag, resref, categoryID, gameObjectType);

}

function CreateNewObject_Callback(success, errorMessage, gameObjectType, name, resref) {
    if (success) {
        var caller = $('#divNewObject').data('Caller');
        var parent = $('#divNewObject').data('Parent');
        var newObject = caller.create(parent, null, name, null, true);
        $(newObject).data('resref', resref);
        $(newObject).data('nodetype', 'object');
        CloseNewObjectBox();
    }
    else {
        $('#lblNewObjectErrors').text(errorMessage);
    }
}

function CloseNewCategoryBox() {
    $('#divCreateCategory').removeData('Caller');
    $('#divCreateCategory').removeData('Parent');
    $('#txtCategoryName').val('');
    $('#lblNewCategoryErrors').text('');

    $('#divCreateCategory').dialog('close');
}

function CloseNewObjectBox() {
    $('#divNewObject').removeData('Caller');
    $('#divNewObject').removeData('Parent');
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
    var mode = $('#divConfirmDelete').data('DeleteMode');

    if (mode == 'DeleteCategory') {
        var categoryID = $(selectedNode).data('categoryid');
        Entity.DeleteCategory(categoryID, gameObjectType);
    }
    else if (mode == 'DeleteObject') {
        var resref = $(selectedNode).data('resref');
        Entity.DeleteObject(resref, gameObjectType, true);
    }
}

function DeleteObject_Callback(success, errorMessage) {
    if (success) {
        var caller = $('#divConfirmDelete').data('Caller');
        var parent = $('#divConfirmDelete').data('Parent');

        caller.remove(parent);
        CloseDeleteObjectBox();
    }
    else {
        $('#lblConfirmDeleteErrors').text(errorMessage);
    }
}

function CloseDeleteObjectBox() {
    $('#divConfirmDelete').removeData('Caller');
    $('#divConfirmDelete').removeData('Parent');
    $('#lblConfirmDeleteErrors').text('');
    $('#divConfirmDelete').dialog('close');
}

function RenameObject() {
    var activeTreeSelector = $('#hdnActiveObjectTreeSelector').val();
    var selectedNode = $(activeTreeSelector).jstree('get_selected');
    var gameObjectType = $('#hdnCurrentObjectMode').val();
    var mode = $('#divRenameTreeNode').data('RenameMode');
    var newObjectName = $('#txtRenameTreeNode').val();

    if (mode == "RenameCategory") {
        var categoryID = $(selectedNode).data('categoryid');
        Entity.RenameCategory(newObjectName, categoryID);
    }
    else if (mode == "RenameObject") {
        var resref = $(selectedNode).data('resref');
        Entity.RenameObject(newObjectName, resref, gameObjectType);
    }
}

function RenameObject_Callback(success, errorMessage, newName) {
    if (success) {
        var activeTreeSelector = $('#hdnActiveObjectTreeSelector').val();
        var caller = $('#divRenameTreeNode').data('Caller');
        var parent = $('#divRenameTreeNode').data('Parent');

        $(activeTreeSelector).jstree('rename_node', [caller, newName]);
        CloseRenameObjectBox();
    }
    else {
        $('#lblRenameTreeNodeErrors').text(errorMessage);
    }
}

function CloseRenameObjectBox() {
    $('#divRenameTreeNode').removeData('Caller');
    $('#divRenameTreeNode').removeData('Parent');
    $('#lblRenameTreeNodeErrors').text('');
    $('#txtRenameTreeNode').val('');
    $('#divRenameTreeNode').dialog('close');
}

