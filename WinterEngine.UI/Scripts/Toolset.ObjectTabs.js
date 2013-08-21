// Called from Toolset.TreeViews.js: OnTreeViewNodeSelected()
function LoadObjectData(resourceID) {
    var mode = ToolsetViewModel.CurrentObjectMode();
    Entity.LoadObjectData(mode, resourceID);
}

function LoadObjectData_Callback(jsonObject) {
    var mode = ToolsetViewModel.CurrentObjectMode();
    var gameObject = JSON.parse(jsonObject);

    if (mode == 'Area') {
        
        ToolsetViewModel.ActiveArea.ResourceID(gameObject.ResourceID);
        ToolsetViewModel.ActiveArea.Name(gameObject.Name);
        ToolsetViewModel.ActiveArea.Tag(gameObject.Tag);
        ToolsetViewModel.ActiveArea.Resref(gameObject.Resref);

        ToolsetViewModel.ActiveArea.Description(gameObject.Description);
        ToolsetViewModel.ActiveArea.Comment(gameObject.Comment);
        ToolsetViewModel.ActiveArea.LocalVariables(gameObject.LocalVariables);
        

        //ToolsetViewModel.ActiveArea = gameObject;

        $('.clsAreaObjectField').removeAttr('disabled');
        $('.clsCreatureObjectField').attr('disabled', 'disabled');
        $('.clsItemObjectField').attr('disabled', 'disabled');
        $('.clsPlaceableObjectField').attr('disabled', 'disabled');
        $('.clsConversationObjectField').attr('disabled', 'disabled');
        $('.clsScriptObjectField').attr('disabled', 'disabled');

    }
    else if (mode == 'Creature') {
        ToolsetViewModel.ActiveCreature.Name(gameObject.Name);
        ToolsetViewModel.ActiveCreature.Tag(gameObject.Tag);
        ToolsetViewModel.ActiveCreature.Resref(gameObject.Resref);

        ToolsetViewModel.ActiveCreature.Description(gameObject.Description);
        ToolsetViewModel.ActiveCreature.Comment(gameObject.Comment);
        ToolsetViewModel.ActiveCreature.LocalVariables(gameObject.LocalVariables);

        ToolsetViewModel.ActiveCreature.IsInvulnerable(gameObject.IsInvulnerable);
        ToolsetViewModel.ActiveCreature.HitPoints(gameObject.MaxHitPoints);
        ToolsetViewModel.ActiveCreature.Mana(gameObject.MaxMana);
        ToolsetViewModel.ActiveCreature.Strength(gameObject.Strength);
        ToolsetViewModel.ActiveCreature.Dexterity(gameObject.Dexterity);
        ToolsetViewModel.ActiveCreature.Constitution(gameObject.Constitution);
        ToolsetViewModel.ActiveCreature.Intelligence(gameObject.Intelligence);
        ToolsetViewModel.ActiveCreature.Wisdom(gameObject.Wisdom);

        $('.clsAreaObjectField').attr('disabled', 'disabled');
        $('.clsCreatureObjectField').removeAttr('disabled');
        $('.clsItemObjectField').attr('disabled', 'disabled');
        $('.clsPlaceableObjectField').attr('disabled', 'disabled');
        $('.clsConversationObjectField').attr('disabled', 'disabled');
        $('.clsScriptObjectField').attr('disabled', 'disabled');
    }
    else if (mode == 'Item') {
        ToolsetViewModel.ActiveItem.Name(gameObject.Name);
        ToolsetViewModel.ActiveItem.Tag(gameObject.Tag);
        ToolsetViewModel.ActiveItem.Resref(gameObject.Resref);

        ToolsetViewModel.ActiveItem.Description(gameObject.Description);
        ToolsetViewModel.ActiveItem.Comment(gameObject.Comment);
        ToolsetViewModel.ActiveItem.LocalVariables(gameObject.LocalVariables);

        ToolsetViewModel.ActiveItem.Price(gameObject.Price);
        ToolsetViewModel.ActiveItem.Weight(gameObject.Weight);
        ToolsetViewModel.ActiveItem.IsUndroppable(gameObject.IsUndroppable);
        ToolsetViewModel.ActiveItem.IsPlot(gameObject.IsPlot);
        ToolsetViewModel.ActiveItem.IsStolen(gameObject.IsStolen);

        $('.clsAreaObjectField').attr('disabled', 'disabled');
        $('.clsCreatureObjectField').attr('disabled', 'disabled');
        $('.clsItemObjectField').removeAttr('disabled');
        $('.clsPlaceableObjectField').attr('disabled', 'disabled');
        $('.clsConversationObjectField').attr('disabled', 'disabled');
        $('.clsScriptObjectField').attr('disabled', 'disabled');
    }
    else if (mode == 'Placeable') {
        ToolsetViewModel.ActivePlaceable.Name(gameObject.Name);
        ToolsetViewModel.ActivePlaceable.Tag(gameObject.Tag);
        ToolsetViewModel.ActivePlaceable.Resref(gameObject.Resref);

        ToolsetViewModel.ActivePlaceable.Description(gameObject.Description);
        ToolsetViewModel.ActivePlaceable.Comment(gameObject.Comment);
        ToolsetViewModel.ActivePlaceable.LocalVariables(gameObject.LocalVariables);


        $('.clsAreaObjectField').attr('disabled', 'disabled');
        $('.clsCreatureObjectField').attr('disabled', 'disabled');
        $('.clsItemObjectField').attr('disabled', 'disabled');
        $('.clsPlaceableObjectField').removeAttr('disabled');
        $('.clsConversationObjectField').attr('disabled', 'disabled');
        $('.clsScriptObjectField').attr('disabled', 'disabled');
    }
    else if (mode == 'Conversation') {
        ToolsetViewModel.ActiveConversation.Name(gameObject.Name);
        ToolsetViewModel.ActiveConversation.Tag(gameObject.Tag);
        ToolsetViewModel.ActiveConversation.Resref(gameObject.Resref);

        ToolsetViewModel.ActiveConversation.Comment(gameObject.Description);
        ToolsetViewModel.ActiveConversation.Comment(gameObject.Comment);
        ToolsetViewModel.ActiveConversation.LocalVariables(gameObject.LocalVariables);


        $('.clsAreaObjectField').attr('disabled', 'disabled');
        $('.clsCreatureObjectField').attr('disabled', 'disabled');
        $('.clsItemObjectField').attr('disabled', 'disabled');
        $('.clsPlaceableObjectField').attr('disabled', 'disabled');
        $('.clsConversationObjectField').removeAttr('disabled');
        $('.clsScriptObjectField').attr('disabled', 'disabled');
    }
    else if (mode == 'Script') {
        ToolsetViewModel.ActiveScript.Name(gameObject.Name);
        ToolsetViewModel.ActiveScript.Tag(gameObject.Tag);
        ToolsetViewModel.ActiveScript.Resref(gameObject.Resref);

        ToolsetViewModel.ActiveScript.Description(gameObject.Description);
        ToolsetViewModel.ActiveScript.Comment(gameObject.Comment);

        $('.clsAreaObjectField').attr('disabled', 'disabled');
        $('.clsCreatureObjectField').attr('disabled', 'disabled');
        $('.clsItemObjectField').attr('disabled', 'disabled');
        $('.clsPlaceableObjectField').attr('disabled', 'disabled');
        $('.clsConversationObjectField').attr('disabled', 'disabled');
        $('.clsScriptObjectField').removeAttr('disabled');
    }
}

function ObjectTabApplyChanges() {
    var mode = ToolsetViewModel.CurrentObjectMode();
    var jsonModel = ko.toJSON(ToolsetViewModel);
    Entity.SaveObjectData(mode, jsonModel);
}

function ObjectTabApplyChanges_Callback() {
}

function ObjectTabDiscardChanges() {
    LoadObjectData();
}