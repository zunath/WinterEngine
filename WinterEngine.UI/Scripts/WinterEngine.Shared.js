
function InitializeDialogBox(selector, dialogTitle) {
    $(selector).dialog({
        modal: true,
        autoOpen: false,
        title: dialogTitle,
        resizable: false,
        dialogClass: 'jqueryUIDialogNoCloseButton',
        draggable: false
    });
}