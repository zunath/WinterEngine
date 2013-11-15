
// Displays tree view, properties window, and other
// components based on object type.
function ChangeObjectMode(objectMode) {
    
    // Object is selected
    if (objectMode != '') {
        Entity.ChangeObjectMode(objectMode, '#div' + objectMode + 'TreeView');
    }
    // Otherwise, hide all.
    else {
        Entity.ChangeObjectMode('', '');
    }
}

function ChangeObjectMode_Callback() {
    ToolsetViewModel.RefreshStatusVariables();
}