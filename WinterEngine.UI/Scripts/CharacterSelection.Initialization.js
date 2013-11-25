
function Initialize() {
    InitializeCharacterSelectionViewModel();
    
    $(window).resize(function () {
        ResizeCharacterList();
    });
    //$('#divLoading').modal('show');

    Entity.RequestServerInformation();
}

function RequestServerInformation_Callback() {
    CharacterSelectionViewModel.Refresh();

    //$('#divLoading').modal('hide');


}

function ResizeCharacterList() {
    var characterListHeight = $(window).height() -
        $('#tarServerAnnouncements').height() -
        $('#divMainPanelHeading').height() -
        $('#divMainPanelFooter').height();

    $('#divObjectTabContainer').css('height', characterListHeight);
}