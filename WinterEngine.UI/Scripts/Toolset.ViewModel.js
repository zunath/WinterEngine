﻿
var ToolsetViewModel;
function InitializeToolsetViewModel() {
    var model = JSON.parse(Entity.GetModelJSON());
    ToolsetViewModel = ko.viewmodel.fromModel(model);
    ko.applyBindings(ToolsetViewModel);


    ToolsetViewModel.Refresh = function () {
        var parsedModel = JSON.parse(Entity.GetModelJSON());
        ko.viewmodel.updateFromModel(ToolsetViewModel, parsedModel);
        ResizeAccordion();
    };

    ToolsetViewModel.RefreshModuleProperties = function () {
        var parsedModel = JSON.parse(Entity.GetModelJSON()).ActiveModule;
        var currentViewModel = ko.viewmodel.toModel(ToolsetViewModel);
        currentViewModel.ActiveModule = parsedModel;

        ko.viewmodel.updateFromModel(ToolsetViewModel, currentViewModel);
        ResizeAccordion();
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
        ResizeAccordion();
    };

    ToolsetViewModel.RefreshObject = function (objectTypeName) {
        var parsedObject = JSON.parse(Entity.GetModelJSON())["Active" + objectTypeName];
        ko.viewmodel.updateFromModel(ToolsetViewModel["Active" + objectTypeName], parsedObject);
        ResizeAccordion();
    };

    ToolsetViewModel.RefreshStatusVariables = function () {
        var parsedModel = JSON.parse(Entity.GetModelJSON());
        
        ToolsetViewModel.CurrentObjectMode(parsedModel.CurrentObjectMode);
        ToolsetViewModel.CurrentObjectTreeSelector(parsedModel.CurrentObjectTreeSelector);
        ToolsetViewModel.IsObjectLoadedForCurrentObjectMode(parsedModel.IsObjectLoadedForCurrentObjectMode);
        ToolsetViewModel.IsModuleOpened(parsedModel.IsModuleOpened);
        ResizeAccordion();
    };

    ToolsetViewModel.SaveModuleProperties = function () {
        var model = ko.viewmodel.toModel(ToolsetViewModel);
        var module = model.ActiveModule;
        var levels = model.LevelRequirementList;
        Entity.SaveModuleProperties(JSON.stringify(module), JSON.stringify(levels));
    };

    ToolsetViewModel.SaveContentPackages = function () {
        var model = ko.viewmodel.toModel(ToolsetViewModel);
        var attachedPackages = model.AttachedContentPackages;
        Entity.UpdateContentPackages(JSON.stringify(attachedPackages));
    };

}
