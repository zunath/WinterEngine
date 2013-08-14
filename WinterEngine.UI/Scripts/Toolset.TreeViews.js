
function BuildObjectTreeViewContextMenu(node) {
    var items = {
        "CreateCategory": {
            "label": "Create New Category",
            "action": function (obj) {
                this.create(obj);
            }
        },
        "CreateObject": {
            "label": "Create Object",
            "action": function (obj) {
                this.create(obj);
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
                this.remove(obj);
            }
        },
        "DeleteObject": {
            "label": "Delete Object",
            "action": function (obj) {
                this.remove(obj);
            }
        }
    };
    /*
    if ($(node).data("nodetype") == "root") {
        delete items.Rename;
        delete items.CreateObject;
        delete items.DeleteCategory;
        delete items.DeleteObject;
    }
    else if ($(node).data("nodetype") == "category") {
        delete items.DeleteObject;
        delete items.CreateCategory;
    }
    else if ($(node).data("nodetype") == "object") {
        delete items.DeleteCategory;
        delete items.CreateCategory;
        delete items.CreateObject;
    }
    */
    return items;
}

function PopulateTreeView(selector, data) {
    $(selector).jstree({
        "json_data": {
            "data": [
                data
            ]
        },
        "plugins": ["json_data", "ui", "themeroller", "sort", "crrm", "contextmenu", "types"],
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