
var ToolsetViewModel;
function InitializeToolsetViewModel() {
    var model = JSON.parse(Entity.GetModelJSON());
    ToolsetViewModel = ko.viewmodel.fromModel(model);
    ko.applyBindings(ToolsetViewModel);


    ToolsetViewModel.Refresh = function () {
        var parsedModel = JSON.parse(Entity.GetModelJSON());
        ko.viewmodel.updateFromModel(ToolsetViewModel, parsedModel);
    };
}
