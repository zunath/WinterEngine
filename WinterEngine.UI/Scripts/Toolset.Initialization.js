 

function Initialize() {
    InitializeToolsetViewModel(); // Located in WinterEngine.ViewModels.js
    InitializeValidation();

    ResizeAccordion();

    $(window).resize(function () {
        ResizeAccordion();
    });

    // Unblock the UI - the UI blocking is done to prevent the user from making javascript calls before Awesomium has loaded.
    $.unblockUI();
}

function InitializeValidation() {
    $.validator.setDefaults({ ignore: [] });
    $('.clsValidationForm').validate({
        errorPlacement: $.noop
    });
}

// Accordion needs to be resized based on the heights of the menu bar, the object bar, and the apply/discard changes panel
function ResizeAccordion() {
    var accordionHeight = $(window).height() -
        $('#divMenuBar').height() -
        $('#divObjectBar').height() -
        $('#divApplyDiscardChangesPanel').height();

    $('#divObjectTabContainer').css('height', accordionHeight);
}