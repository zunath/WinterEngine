
/* Button Functionality - Object Selection Menu */

function AreasButtonClick() {
    $('.clsActiveObjectType').removeClass('clsActiveObjectType');
    $('#liAreas').addClass('clsActiveObjectType');
    Entity.AreasButtonClick();

    $('.clsTreeViewDiv').addClass('clsHidden');
    $('#divAreaTreeView').removeClass('clsHidden');
}

function AreasButtonClick_Callback() {
}

function CreaturesButtonClick() {
    $('.clsActiveObjectType').removeClass('clsActiveObjectType');
    $('#liCreatures').addClass('clsActiveObjectType');
    Entity.CreaturesButtonClick();

    $('.clsTreeViewDiv').addClass('clsHidden');
    $('#divCreatureTreeView').removeClass('clsHidden');
}

function CreaturesButtonClick_Callback() {
}

function ItemsButtonClick() {
    $('.clsActiveObjectType').removeClass('clsActiveObjectType');
    $('#liItems').addClass('clsActiveObjectType');
    Entity.ItemsButtonClick();

    $('.clsTreeViewDiv').addClass('clsHidden');
    $('#divItemTreeView').removeClass('clsHidden');
}

function ItemsButtonClick_Callback() {
}

function PlaceablesButtonClick() {
    $('.clsActiveObjectType').removeClass('clsActiveObjectType');
    $('#liPlaceables').addClass('clsActiveObjectType');
    Entity.PlaceablesButtonClick();

    $('.clsTreeViewDiv').addClass('clsHidden');
    $('#divPlaceableTreeView').removeClass('clsHidden');
}

function PlaceablesButtonClick_Callback() {
}

function ConversationsButtonClick() {
    $('.clsActiveObjectType').removeClass('clsActiveObjectType');
    $('#liConversations').addClass('clsActiveObjectType');
    Entity.ConversationsButtonClick();

    $('.clsTreeViewDiv').addClass('clsHidden');
    $('#divConversationTreeView').removeClass('clsHidden');
}

function ConversationsButtonClick_Callback() {
}

function ScriptsButtonClick() {
    $('.clsActiveObjectType').removeClass('clsActiveObjectType');
    $('#liScripts').addClass('clsActiveObjectType');
    Entity.ScriptsButtonClick();

    $('.clsTreeViewDiv').addClass('clsHidden');
    $('#divScriptTreeView').removeClass('clsHidden');
}

function ScriptsButtonClick_Callback() {
}

function GraphicsButtonClick() {
    $('.clsActiveObjectType').removeClass('clsActiveObjectType');
    $('#liGraphics').addClass('clsActiveObjectType');
    Entity.GraphicsButtonClick();

    $('.clsTreeViewDiv').addClass('clsHidden');
    $('#divGraphicTreeView').removeClass('clsHidden');
}

function GraphicsButtonClick_Callback() {
}