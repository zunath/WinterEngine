
// Displays tree view, properties window, and other
// components based on object type.
function ChangeObjectMode(objectMode) {
    $('.clsObjectTabDiv').addClass('clsHidden');
    $('#divObjectTabContainerButtons').removeClass('clsHidden');
    $('.clsTreeViewDiv').addClass('clsHidden');
    $('.clsActiveObjectType').removeClass('clsActiveObjectType');
    
    // Object is selected
    if (objectMode != '') {
        $('#li' + objectMode).addClass('clsActiveObjectType');        // Ex: #liArea
        $('#div' + objectMode + 'Tab').removeClass('clsHidden');      // Ex: #divAreaTab
        $('#div' + objectMode + 'TreeView').removeClass('clsHidden'); // Ex: #divAreaTreeView
        Entity.ChangeObjectMode(objectMode,
            '#div' + objectMode + 'TreeView',                         // Ex: #divAreaTreeView
            '#div' + objectMode + 'Tab');                             // Ex: #divAreaTab
    }
    // Otherwise, hide all.
    else {
        Entity.ChangeObjectMode('', '', '');
        $('#divObjectTabContainerButtons').addClass('clsHidden');
    }
}

function ChangeObjectMode_Callback() {
    ToolsetViewModel.Refresh();
}