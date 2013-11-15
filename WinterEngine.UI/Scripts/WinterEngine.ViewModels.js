﻿
var ToolsetViewModel;
function InitializeToolsetViewModel() {
    var model = JSON.parse(Entity.GetModelJSON());
    ToolsetViewModel = ko.viewmodel.fromModel(model);
    ko.applyBindings(ToolsetViewModel);


    ToolsetViewModel.Refresh = function () {
        var parsedModel = JSON.parse(Entity.GetModelJSON());
        ko.viewmodel.updateFromModel(ToolsetViewModel, parsedModel);

        if (ToolsetViewModel.IsObjectLoadedForCurrentObjectMode()) {
            $('#btnObjectTabApplyChanges').button('enable');
            $('#btnObjectTabDiscardChanges').button('enable');
        }
        else {
            $('#btnObjectTabApplyChanges').button('disable');
            $('#btnObjectTabDiscardChanges').button('disable');
        }
    };

    ToolsetViewModel.RefreshModuleProperties = function () {
        var parsedModel = JSON.parse(Entity.GetModelJSON()).ActiveModule;
        var currentViewModel = ko.viewmodel.toModel(ToolsetViewModel);
        currentViewModel.ActiveModule = parsedModel;

        ko.viewmodel.updateFromModel(ToolsetViewModel, currentViewModel);
    };

    ToolsetViewModel.GetActiveObject = function () {
        var mode = ToolsetViewModel.CurrentObjectMode();
        return ToolsetViewModel["Active" + mode];
    };

    ToolsetViewModel.SaveActiveObject = function () {
        var mode = ToolsetViewModel.CurrentObjectMode();
        var obj = ko.viewmodel.toModel(ToolsetViewModel.GetActiveObject());
        var jsonObject = JSON.stringify(obj);

        // Dynamic method call to C# based on current mode
        window["Entity"]["Save" + mode](jsonObject);
    };

    ToolsetViewModel.RefreshActiveObject = function () {
        var mode = ToolsetViewModel.CurrentObjectMode();
        ToolsetViewModel.RefreshObject(mode);
    };

    ToolsetViewModel.RefreshObject = function (objectTypeName) {
        var parsedObject = JSON.parse(Entity.GetModelJSON())["Active" + objectTypeName];
        var currentViewModel = ko.viewmodel.toModel(ToolsetViewModel);
        currentViewModel["Active" + objectTypeName] = parsedObject;

        ko.viewmodel.updateFromModel(ToolsetViewModel, currentViewModel);
    };

    ToolsetViewModel.RefreshStatusVariables = function () {
        var parsedModel = JSON.parse(Entity.GetModelJSON());
        
        ToolsetViewModel.CurrentObjectMode(parsedModel.CurrentObjectMode);
        ToolsetViewModel.CurrentObjectTreeSelector(parsedModel.CurrentObjectTreeSelector);
        ToolsetViewModel.IsObjectLoadedForCurrentObjectMode(parsedModel.IsObjectLoadedForCurrentObjectMode);
        ToolsetViewModel.IsModuleOpened(parsedModel.IsModuleOpened);
    };

}
