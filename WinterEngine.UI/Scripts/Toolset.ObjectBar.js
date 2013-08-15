
// Displays tree view, properties window, and other
// components based on object type.
function ChangeObjectMode(objectMode) {
    $('.clsObjectTabDiv').addClass('clsHidden');
    $('.clsTreeViewDiv').addClass('clsHidden');
    $('.clsActiveObjectType').removeClass('clsActiveObjectType');


    if (objectMode == "Area") {
        $('#liAreas').addClass('clsActiveObjectType');
        $('#divAreasTab').removeClass('clsHidden');
        $('#divAreaTreeView').removeClass('clsHidden');

        $('#hdnCurrentObjectMode').val('Area');
        $('#hdnActiveObjectTreeSelector').val('#divAreaTreeView');
        $('#hdnActiveObjectPropertiesTabSelector').val('#divAreasTab');
    }
    else if (objectMode == "Creature") {
        $('#liCreatures').addClass('clsActiveObjectType');
        $('#divCreaturesTab').removeClass('clsHidden');
        $('#divCreatureTreeView').removeClass('clsHidden');

        $('#hdnCurrentObjectMode').val('Creature');
        $('#hdnActiveObjectTreeSelector').val('#divCreatureTreeView');
        $('#hdnActiveObjectPropertiesTabSelector').val('#divCreaturesTab');
    }
    else if (objectMode == "Item") {
        $('#liItems').addClass('clsActiveObjectType');
        $('#divItemsTab').removeClass('clsHidden');
        $('#divItemTreeView').removeClass('clsHidden');
        $('#hdnCurrentObjectMode').val('Item');
        $('#hdnActiveObjectTreeSelector').val('#divItemTreeView');
        $('#hdnActiveObjectPropertiesTabSelector').val('#divItemsTab');
    }
    else if (objectMode == "Placeable") {
        $('#liPlaceables').addClass('clsActiveObjectType');
        $('#divPlaceablesTab').removeClass('clsHidden');
        $('#divPlaceableTreeView').removeClass('clsHidden');
        $('#hdnCurrentObjectMode').val('Placeable');
        $('#hdnActiveObjectTreeSelector').val('#divPlaceableTreeView');
        $('#hdnActiveObjectPropertiesTabSelector').val('#divPlaceablesTab');
    }
    else if (objectMode == "Conversation") {
        $('#liConversations').addClass('clsActiveObjectType');
        $('#divConversationsTab').removeClass('clsHidden');
        $('#divConversationTreeView').removeClass('clsHidden');
        $('#hdnCurrentObjectMode').val('Conversation');
        $('#hdnActiveObjectTreeSelector').val('#divConversationTreeView');
        $('#hdnActiveObjectPropertiesTabSelector').val('#divConversationsTab');
    }
    else if (objectMode == "Script") {
        $('#liScripts').addClass('clsActiveObjectType');
        $('#divScriptsTab').removeClass('clsHidden');
        $('#divScriptTreeView').removeClass('clsHidden');
        $('#hdnCurrentObjectMode').val('Script');
        $('#hdnActiveObjectTreeSelector').val('#divScriptTreeView');
        $('#hdnActiveObjectPropertiesTabSelector').val('#divScriptsTab');
    }
    else if (objectMode == "Graphic") {
        $('#liGraphics').addClass('clsActiveObjectType');
        $('#divGraphicTreeView').removeClass('clsHidden');
        $('#hdnCurrentObjectMode').val('Graphic');
        $('#hdnActiveObjectTreeSelector').val('#divGraphicTreeView');
        $('#hdnActiveObjectPropertiesTabSelector').val('#divGraphicsTab');
    }
    // Otherwise, hide all.
    else {
        $('#hdnActiveObjectTreeSelector').val('');
        $('#hdnActiveObjectPropertiesTabSelector').val('');
    }

}


/* Button Functionality - Object Selection Menu */

function AreasButtonClick() {
    Entity.AreasButtonClick();
}

function AreasButtonClick_Callback() {
    ChangeObjectMode("Area");
}

function CreaturesButtonClick() {
    Entity.CreaturesButtonClick();
}

function CreaturesButtonClick_Callback() {
    ChangeObjectMode("Creature");
}

function ItemsButtonClick() {
    Entity.ItemsButtonClick();
}

function ItemsButtonClick_Callback() {
    ChangeObjectMode("Item");
}

function PlaceablesButtonClick() {
    Entity.PlaceablesButtonClick();
}

function PlaceablesButtonClick_Callback() {
    ChangeObjectMode("Placeable");
}

function ConversationsButtonClick() {
    Entity.ConversationsButtonClick();
}

function ConversationsButtonClick_Callback() {
    ChangeObjectMode("Conversation");
}

function ScriptsButtonClick() {
    Entity.ScriptsButtonClick();
}

function ScriptsButtonClick_Callback() {
    ChangeObjectMode("Script");
}

function GraphicsButtonClick() {
    Entity.GraphicsButtonClick();
}

function GraphicsButtonClick_Callback() {
    ChangeObjectMode("Graphic");
}