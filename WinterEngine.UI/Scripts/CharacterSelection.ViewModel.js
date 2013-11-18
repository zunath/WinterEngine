
var CharacterSelectionViewModel;
function InitializeCharacterSelectionViewModel() {
    var model = JSON.parse(Entity.GetModelJSON());
    CharacterSelectionViewModel = ko.viewmodel.fromModel(model);
    ko.applyBindings(CharacterSelectionViewModel);


    CharacterSelectionViewModel.Refresh = function () {
        var parsedModel = JSON.parse(Entity.GetModelJSON());
        ko.viewmodel.updateFromModel(CharacterSelectionViewModel, parsedModel);
    };

}
