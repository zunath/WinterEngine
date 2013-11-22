var CharacterCreationViewModel;
function InitializeCharacterCreationViewModel() {
    var model = JSON.parse(Entity.GetModelJSON());
    CharacterCreationViewModel = ko.viewmodel.fromModel(model);
    ko.applyBindings(CharacterCreationViewModel);

    CharacterCreationViewModel.Refresh = function () {
        var parsedModel = JSON.parse(Entity.GetModelJSON());
        ko.viewmodel.updateFromModel(CharacterCreationViewModel, parsedModel);
    };

}