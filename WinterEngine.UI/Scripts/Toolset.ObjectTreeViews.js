
// Builds the right-click context menu used on the tree views
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
                $('#lblConfirmDelete').text('Are you sure you want to delete this ' + ToolsetViewModel.CurrentObjectMode() + '?');
                $('#divConfirmDelete').dialog('open');
            }
        }
    };
    
    if ($(node).data("nodetype") == "root") {
        delete items.RenameCategory;
        delete items.CreateObject;
        delete items.DeleteCategory;
        delete items.DeleteObject;
    }
    else if ($(node).data("nodetype") == "category") {
        if ($(node).data("issystemresource") == "True") {
            delete items.DeleteCategory;
            delete items.RenameCategory;
        }
        delete items.DeleteObject;
        delete items.CreateCategory;
    }
    else if ($(node).data("nodetype") == "object") {
        if ($(node).data("issystemresource") == "True") {
            delete items.DeleteObject;
        }
        delete items.DeleteCategory;
        delete items.CreateCategory;
        delete items.CreateObject;
        delete items.RenameCategory;
    }
    
    return items;
}

// Initializes a specified div selector as a tree view.
function InitializeTreeView(selector, data) {
    $(selector).jstree({

        "core": {
            "animation": 0,
        },
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
    })


    .bind("	select_node.jstree", function (event) {
        OnTreeViewNodeSelected();
    });
}

// Fires when a tree node is selected.
// If a category or root node is selected, it is expanded.
// If an object node is selected, it will be loaded into the editor.
function OnTreeViewNodeSelected() {
    var selectedNode = $(ToolsetViewModel.CurrentObjectTreeSelector()).jstree('get_selected');
    var nodeType = $(selectedNode).data('nodetype');
    var resourceID = $(selectedNode).data('resourceid');

    if (nodeType == 'category' || nodeType == 'root') {
        $(ToolsetViewModel.CurrentObjectTreeSelector()).jstree('open_node', selectedNode);
    }
    else if (nodeType == 'object') {
        LoadObjectData(resourceID);
    }
}

// CALLBACK FUNCTION
// Initializes all tree views for the different object types.
function LoadTreeViews_Callback(jsonAreas,
                                jsonCreatures,
                                jsonItems,
                                jsonPlaceables,
                                jsonConversations,
                                jsonScripts,
                                jsonTilesets) {
    InitializeTreeView("#divAreaTreeView", $.parseJSON(jsonAreas));
    InitializeTreeView("#divCreatureTreeView", $.parseJSON(jsonCreatures));
    InitializeTreeView("#divItemTreeView", $.parseJSON(jsonItems));
    InitializeTreeView("#divPlaceableTreeView", $.parseJSON(jsonPlaceables));
    InitializeTreeView("#divConversationTreeView", $.parseJSON(jsonConversations));
    InitializeTreeView("#divScriptTreeView", $.parseJSON(jsonScripts));
    InitializeTreeView("#divTilesetTreeView", $.parseJSON(jsonTilesets));

    $(ToolsetViewModel.CurrentObjectTreeSelector()).removeClass('clsHidden');
}

function CreateNewCategory() {
    if (!$('#formNewCategory').valid()) return;

    var category = new Object();
    category.Name = $('#txtCategoryName').val();
    category.GameObjectType = ToolsetViewModel.CurrentObjectMode();

    Entity.SaveCategory("CreateNewCategory_Callback", JSON.stringify(category));
}

function CreateNewCategory_Callback(jsonCategory) {
    var parsedCategory = JSON.parse(jsonCategory);
    var caller = $('#divCreateCategory').data('Caller');
    var parent = $('#divCreateCategory').data('Parent');
    var newCategory = caller.create(parent, null, parsedCategory.Name, null, true);
    $(newCategory).data('nodetype', 'category');
    $(newCategory).data('categoryid', parsedCategory.ResourceID);
        
    CloseNewCategoryBox();
    
}

function CreateNewObject() {
    if (!$('#formNewObject').valid()) return;

    var parent = $('#divNewObject').data('Parent');
    var gameObject = new Object();
    gameObject.Name = $('#txtObjectName').val();
    gameObject.Tag = $('#txtObjectTag').val();
    gameObject.Resref = $('#txtObjectResref').val();
    gameObject.ResourceCategoryID = $(parent).data('categoryid');

    // Example: Entity.SaveArea(jsonData);
    Entity["Save" + ToolsetViewModel.CurrentObjectMode()]("CreateNewObject_Callback", JSON.stringify(gameObject), false);
}

function CreateNewObject_Callback(jsonNewObject) {
    var parsedObject = JSON.parse(jsonNewObject);
    var caller = $('#divNewObject').data('Caller');
    var parent = $('#divNewObject').data('Parent');
    var newObject = caller.create(parent, null, parsedObject.Name, null, true);
    $(newObject).data('resourceid', parsedObject.ResourceID);
    $(newObject).data('nodetype', 'object');
    CloseNewObjectBox();

    ToolsetViewModel.Refresh();
    
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
    var selectedNode = $(ToolsetViewModel.CurrentObjectTreeSelector()).jstree('get_selected');
    var mode = $('#divConfirmDelete').data('DeleteMode');

    if (mode == 'DeleteCategory') {
        var categoryID = $(selectedNode).data('categoryid');
        Entity["DeleteCategory" + ToolsetViewModel.CurrentObjectMode()]("DeleteObject_Callback", categoryID);
    }
    else if (mode == 'DeleteObject') {
        var resourceID = $(selectedNode).data('resourceid');
        Entity["Delete" + ToolsetViewModel.CurrentObjectMode()]("DeleteObject_Callback", resourceID);
    }
}

function DeleteObject_Callback() {
    var caller = $('#divConfirmDelete').data('Caller');
    var parent = $('#divConfirmDelete').data('Parent');

    caller.remove(parent);
    CloseDeleteObjectBox();
    ToolsetViewModel.Refresh();
    $(ToolsetViewModel.CurrentObjectTreeSelector()).jstree('deselect_all');
    
}

// Closes the pop-up div for deleting objects and clears all temporary data.
function CloseDeleteObjectBox() {
    $('#divConfirmDelete').removeData('Caller');
    $('#divConfirmDelete').removeData('Parent');
    $('#lblConfirmDeleteErrors').text('');
    $('#divConfirmDelete').dialog('close');
}

// Handles requesting that a category or object be renamed.
function RenameObject() {
    var selectedNode = $(ToolsetViewModel.CurrentObjectTreeSelector()).jstree('get_selected');
    var mode = $('#divRenameTreeNode').data('RenameMode');
    
    if (mode == "RenameCategory") {
        var category = new Object();
        category.Name = $('#txtRenameTreeNode').val();
        category.ResourceID = $(selectedNode).data('categoryid');

        Entity.SaveCategory(JSON.stringify(category), "RenameObject_Callback");
    }
}

// CALLBACK FUNCTION
// If successful, object is renamed in the UI.
// If failed, error message will display.
function RenameObject_Callback(success, errorMessage, newName) {
    if (success) {
        var selectedNode = $(ToolsetViewModel.CurrentObjectTreeSelector()).jstree('get_selected');

        $(ToolsetViewModel.CurrentObjectTreeSelector()).jstree('set_text', selectedNode, newName);
        CloseRenameObjectBox();

        ToolsetViewModel.Refresh();
    }
    else {
        $('#lblRenameTreeNodeErrors').text(errorMessage);
    }
}

// Closes the pop-up div for object/category renaming and clears all temporary data.
function CloseRenameObjectBox() {
    $('#lblRenameTreeNodeErrors').text('');
    $('#txtRenameTreeNode').val('');
    $('#divRenameTreeNode').dialog('close');
}

