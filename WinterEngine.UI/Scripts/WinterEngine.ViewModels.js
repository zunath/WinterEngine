﻿
var ToolsetViewModel;
function InitializeToolsetViewModel() {
    var model = JSON.parse(Entity.GetModelJSON());
    ToolsetViewModel = ko.viewmodel.fromModel(model);
    ko.applyBindings(ToolsetViewModel);


    ToolsetViewModel.Refresh = function () {
        var parsedModel = JSON.parse(Entity.GetModelJSON());
        ko.viewmodel.updateFromModel(ToolsetViewModel, parsedModel);
    };

    ToolsetViewModel.GetActiveObject = function () {
        var mode = ToolsetViewModel.CurrentObjectMode();

        if (mode == 'Area') {
            return ToolsetViewModel.ActiveArea;
        }
        else if (mode == 'Creature') {
            return ToolsetViewModel.ActiveCreature;
        }
        else if (mode == 'Item') {
            return ToolsetViewModel.ActiveItem;
        }
        else if (mode == 'Placeable') {
            return ToolsetViewModel.ActivePlaceable;
        }
        else if (mode == 'Conversation') {
            return ToolsetViewModel.ActiveConversation;
        }
        else if (mode == 'Script') {
            return ToolsetViewModel.ActiveScript;
        }
        else if (mode == 'Tileset') {
            return ToolsetViewModel.ActiveTileset;
        }
    };
}
