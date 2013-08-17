function ObjectTabApplyChanges() {
    if (ToolsetViewModel.CurrentObjectMode() == "Area") {
    }
    else if (ToolsetViewModel.CurrentObjectMode() == "Creature") {
    }
    else if (ToolsetViewModel.CurrentObjectMode() == "Item") {
    }
    else if (ToolsetViewModel.CurrentObjectMode() == "Placeable") {
    }
    else if (ToolsetViewModel.CurrentObjectMode() == "Conversation") {
    }
    else if (ToolsetViewModel.CurrentObjectMode() == "Script") {
    }
}

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
        ToolsetViewModel.AreaComments(gameObject.Comments);
        ToolsetViewModel.AreaLocalVariables(gameObject.LocalVariables);


    }
    else if (mode == 'Creature') {
        ToolsetViewModel.CreatureName(gameObject.Name);
        ToolsetViewModel.CreatureTag(gameObject.Tag);
        ToolsetViewModel.CreatureResref(gameObject.Resref);

        ToolsetViewModel.CreatureDescription(gameObject.Description);
        ToolsetViewModel.CreatureComments(gameObject.Comments);
        ToolsetViewModel.CreatureLocalVariables(gameObject.LocalVariables);

        ToolsetViewModel.CreatureIsInvulnerable(gameObject.IsInvulnerable);
        ToolsetViewModel.CreatureHitPoints(gameObject.MaxHitPoints);
        ToolsetViewModel.CreatureMana(gameObject.MaxMana);
        ToolsetViewModel.CreatureStrength(gameObject.Strength);
        ToolsetViewModel.CreatureDexterity(gameObject.Dexterity);
        ToolsetViewModel.CreatureConstitution(gameObject.Constitution);
        ToolsetViewModel.CreatureIntelligence(gameObject.Intelligence);
        ToolsetViewModel.CreatureWisdom(gameObject.Wisdom);

    }
    else if (mode == 'Item') {
        ToolsetViewModel.ItemName(gameObject.Name);
        ToolsetViewModel.ItemTag(gameObject.Tag);
        ToolsetViewModel.ItemResref(gameObject.Resref);

        ToolsetViewModel.ItemDescription(gameObject.Description);
        ToolsetViewModel.ItemComments(gameObject.Comments);
        ToolsetViewModel.ItemLocalVariables(gameObject.LocalVariables);

        ToolsetViewModel.ItemPrice(gameObject.Price);
        ToolsetViewModel.ItemWeight(gameObject.Weight);
        ToolsetViewModel.ItemIsUndroppable(gameObject.IsUndroppable);
        ToolsetViewModel.ItemIsPlot(gameObject.IsPlot);
        ToolsetViewModel.ItemIsStolen(gameObject.IsStolen);
    }
    else if (mode == 'Placeable') {
        ToolsetViewModel.PlaceableName(gameObject.Name);
        ToolsetViewModel.PlaceableTag(gameObject.Tag);
        ToolsetViewModel.PlaceableResref(gameObject.Resref);

        ToolsetViewModel.PlaceableDescription(gameObject.Description);
        ToolsetViewModel.PlaceableComments(gameObject.Comments);
        ToolsetViewModel.PlaceableLocalVariables(gameObject.LocalVariables);


    }
    else if (mode == 'Conversation') {
        ToolsetViewModel.ConversationName(gameObject.Name);
        ToolsetViewModel.ConversationTag(gameObject.Tag);
        ToolsetViewModel.ConversationResref(gameObject.Resref);

        ToolsetViewModel.ConversationDescription(gameObject.Description);
        ToolsetViewModel.ConversationComments(gameObject.Comments);
        ToolsetViewModel.ConversationLocalVariables(gameObject.LocalVariables);
    }
    else if (mode == 'Script') {
        ToolsetViewModel.ScriptName(gameObject.Name);
        ToolsetViewModel.ScriptTag(gameObject.Tag);
        ToolsetViewModel.ScriptResref(gameObject.Resref);

        ToolsetViewModel.ScriptDescription(gameObject.Description);
        ToolsetViewModel.ScriptComments(gameObject.Comments);
        ToolsetViewModel.ScriptLocalVariables(gameObject.LocalVariables);
    }
}

function ObjectTabApplyChanges_Callback() {
}

function ObjectTabDiscardChanges() {
    if (ToolsetViewModel.CurrentObjectMode() == "Area") {
    }
    else if (ToolsetViewModel.CurrentObjectMode() == "Creature") {
    }
    else if (ToolsetViewModel.CurrentObjectMode() == "Item") {
    }
    else if (ToolsetViewModel.CurrentObjectMode() == "Placeable") {
    }
    else if (ToolsetViewModel.CurrentObjectMode() == "Conversation") {
    }
    else if (ToolsetViewModel.CurrentObjectMode() == "Script") {
    }
}

function ObjectTabDiscardChanges_Callback() {
}