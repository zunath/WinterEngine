// Called from Toolset.TreeViews.js: OnTreeViewNodeSelected()
function LoadObjectData() {
    var mode = ToolsetViewModel.CurrentObjectMode();
    Entity.LoadObjectData(mode, 1);//resourceID); // DEBUGGING
}

function LoadObjectData_Callback(jsonObject) {
    var mode = ToolsetViewModel.CurrentObjectMode();
    var gameObject = $.parseJSON(jsonObject);

    if (mode == 'Area') {
        ToolsetViewModel.AreaName(gameObject.Name);
        ToolsetViewModel.AreaTag(gameObject.Tag);
        ToolsetViewModel.AreaResref(gameObject.Resref);

        ToolsetViewModel.AreaDescription(gameObject.Description);
        ToolsetViewModel.AreaComments(gameObject.Comment);
        ToolsetViewModel.AreaLocalVariables(gameObject.LocalVariables);

        $('.clsAreaObjectField').removeAttr('disabled');
        $('.clsCreatureObjectField').attr('disabled', 'disabled');
        $('.clsItemObjectField').attr('disabled', 'disabled');
        $('.clsPlaceableObjectField').attr('disabled', 'disabled');
        $('.clsConversationObjectField').attr('disabled', 'disabled');
        $('.clsScriptObjectField').attr('disabled', 'disabled');

    }
    else if (mode == 'Creature') {
        ToolsetViewModel.CreatureName(gameObject.Name);
        ToolsetViewModel.CreatureTag(gameObject.Tag);
        ToolsetViewModel.CreatureResref(gameObject.Resref);

        ToolsetViewModel.CreatureDescription(gameObject.Description);
        ToolsetViewModel.CreatureComments(gameObject.Comment);
        ToolsetViewModel.CreatureLocalVariables(gameObject.LocalVariables);

        ToolsetViewModel.CreatureIsInvulnerable(gameObject.IsInvulnerable);
        ToolsetViewModel.CreatureHitPoints(gameObject.MaxHitPoints);
        ToolsetViewModel.CreatureMana(gameObject.MaxMana);
        ToolsetViewModel.CreatureStrength(gameObject.Strength);
        ToolsetViewModel.CreatureDexterity(gameObject.Dexterity);
        ToolsetViewModel.CreatureConstitution(gameObject.Constitution);
        ToolsetViewModel.CreatureIntelligence(gameObject.Intelligence);
        ToolsetViewModel.CreatureWisdom(gameObject.Wisdom);

        $('.clsAreaObjectField').attr('disabled', 'disabled');
        $('.clsCreatureObjectField').removeAttr('disabled');
        $('.clsItemObjectField').attr('disabled', 'disabled');
        $('.clsPlaceableObjectField').attr('disabled', 'disabled');
        $('.clsConversationObjectField').attr('disabled', 'disabled');
        $('.clsScriptObjectField').attr('disabled', 'disabled');
    }
    else if (mode == 'Item') {
        ToolsetViewModel.ItemName(gameObject.Name);
        ToolsetViewModel.ItemTag(gameObject.Tag);
        ToolsetViewModel.ItemResref(gameObject.Resref);

        ToolsetViewModel.ItemDescription(gameObject.Description);
        ToolsetViewModel.ItemComments(gameObject.Comment);
        ToolsetViewModel.ItemLocalVariables(gameObject.LocalVariables);

        ToolsetViewModel.ItemPrice(gameObject.Price);
        ToolsetViewModel.ItemWeight(gameObject.Weight);
        ToolsetViewModel.ItemIsUndroppable(gameObject.IsUndroppable);
        ToolsetViewModel.ItemIsPlot(gameObject.IsPlot);
        ToolsetViewModel.ItemIsStolen(gameObject.IsStolen);

        $('.clsAreaObjectField').attr('disabled', 'disabled');
        $('.clsCreatureObjectField').attr('disabled', 'disabled');
        $('.clsItemObjectField').removeAttr('disabled');
        $('.clsPlaceableObjectField').attr('disabled', 'disabled');
        $('.clsConversationObjectField').attr('disabled', 'disabled');
        $('.clsScriptObjectField').attr('disabled', 'disabled');
    }
    else if (mode == 'Placeable') {
        ToolsetViewModel.PlaceableName(gameObject.Name);
        ToolsetViewModel.PlaceableTag(gameObject.Tag);
        ToolsetViewModel.PlaceableResref(gameObject.Resref);

        ToolsetViewModel.PlaceableDescription(gameObject.Description);
        ToolsetViewModel.PlaceableComments(gameObject.Comment);
        ToolsetViewModel.PlaceableLocalVariables(gameObject.LocalVariables);


        $('.clsAreaObjectField').attr('disabled', 'disabled');
        $('.clsCreatureObjectField').attr('disabled', 'disabled');
        $('.clsItemObjectField').attr('disabled', 'disabled');
        $('.clsPlaceableObjectField').removeAttr('disabled');
        $('.clsConversationObjectField').attr('disabled', 'disabled');
        $('.clsScriptObjectField').attr('disabled', 'disabled');
    }
    else if (mode == 'Conversation') {
        ToolsetViewModel.ConversationName(gameObject.Name);
        ToolsetViewModel.ConversationTag(gameObject.Tag);
        ToolsetViewModel.ConversationResref(gameObject.Resref);

        ToolsetViewModel.ConversationComments(gameObject.Description);
        ToolsetViewModel.ConversationComments(gameObject.Comment);
        ToolsetViewModel.ConversationLocalVariables(gameObject.LocalVariables);


        $('.clsAreaObjectField').attr('disabled', 'disabled');
        $('.clsCreatureObjectField').attr('disabled', 'disabled');
        $('.clsItemObjectField').attr('disabled', 'disabled');
        $('.clsPlaceableObjectField').attr('disabled', 'disabled');
        $('.clsConversationObjectField').removeAttr('disabled');
        $('.clsScriptObjectField').attr('disabled', 'disabled');
    }
    else if (mode == 'Script') {
        ToolsetViewModel.ScriptName(gameObject.Name);
        ToolsetViewModel.ScriptTag(gameObject.Tag);
        ToolsetViewModel.ScriptResref(gameObject.Resref);

        ToolsetViewModel.ScriptDescription(gameObject.Description);
        ToolsetViewModel.ScriptComments(gameObject.Comment);

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

    if (mode == "Area") {
    }
    else if (mode == "Creature") {
    }
    else if (mode == "Item") {
    }
    else if (mode == "Placeable") {
    }
    else if (mode == "Conversation") {
    }
    else if (mode == "Script") {
    }
}

function ObjectTabApplyChanges_Callback() {
}

function ObjectTabDiscardChanges() {
    LoadObjectData();
}