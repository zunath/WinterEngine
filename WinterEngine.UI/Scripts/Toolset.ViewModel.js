var ToolsetViewModel = {

    /* Active Settings */

    CurrentObjectMode: ko.observable(),
    CurrentObjectTreeSelector: ko.observable(),
    CurrentObjectTabSelector: ko.observable(),

    /* Area Object Fields */
    AreaResourceID: ko.observable(),
    AreaName: ko.observable(),
    AreaTag: ko.observable(),
    AreaResref: ko.observable(),
    AreaTileset: ko.observable(),
    AreaBackgroundMusic: ko.observable(),
    AreaBattleMusic: ko.observable(),
    AreaDescription: ko.observable(),
    AreaComments: ko.observable(),
    AreaLocalVariables: ko.observableArray([]),
    AreaEventOnAreaEnter: ko.observable(),
    AreaEventOnAreaExit: ko.observable(),
    AreaEventOnAreaHeartbeat: ko.observable(),
    AreaEventOnAreaUserDefinedEvent: ko.observable(),

    /* Creature Object Fields */
    CreatureResourceID: ko.observable(),
    CreatureName: ko.observable(),
    CreatureTag: ko.observable(),
    CreatureResref: ko.observable(),
    CreatureAppearance: ko.observable(),
    CreatureRace: ko.observable(),
    CreatureGender: ko.observable(),
    CreatureFaction: ko.observable(),
    CreatureConversation: ko.observable(),
    CreatureIsInvulnerable: ko.observable(),

    CreatureHitPoints: ko.observable(),
    CreatureMana: ko.observable(),
    CreatureStrength: ko.observable(),
    CreatureDexterity: ko.observable(),
    CreatureConstitution: ko.observable(),
    CreatureIntelligence: ko.observable(),
    CreatureWisdom: ko.observable(),
    CreatureAbilities: ko.observableArray([]),

    CreatureEquipmentMainHand: ko.observable(),
    CreatureEquipmentOffHand: ko.observable(),
    CreatureEquipmentHead: ko.observable(),
    CreatureEquipmentBody: ko.observable(),
    CreatureEquipmentBack: ko.observable(),
    CreatureEquipmentHands: ko.observable(),
    CreatureEquipmentWaist: ko.observable(),
    CreatureEquipmentLegs: ko.observable(),
    CreatureEquipmentFeet: ko.observable(),
    CreatureEquipmentEarL: ko.observable(),
    CreatureEquipmentEarR: ko.observable(),
    CreatureEquipmentRingL: ko.observable(),
    CreatureEquipmentRingR: ko.observable(),
    CreatureEquipmentNeck: ko.observable(),

    CreatureInventory: ko.observableArray([]),
    CreatureDescription: ko.observable(),
    CreatureComments: ko.observable(),
    CreatureLocalVariables: ko.observableArray([]),

    CreatureEventOnSpawn: ko.observable(),
    CreatureEventOnDamaged: ko.observable(),
    CreatureEventOnDeath: ko.observable(),
    CreatureEventOnConversation: ko.observable(),
    CreatureEventOnHeartbeat: ko.observable(),


    /* Item Object Fields */
    
    ItemResourceID: ko.observable(),
    ItemName: ko.observable(),
    ItemTag: ko.observable(),
    ItemResref: ko.observable(),

    ItemDescription: ko.observable(),
    ItemComments: ko.observable(),
    ItemLocalVariables: ko.observableArray([]),

    ItemType: ko.observable(),
    ItemPrice: ko.observable(),
    ItemWeight: ko.observable(),
    ItemIsUndroppable: ko.observable(),
    ItemIsPlot: ko.observable(),
    ItemIsStolen: ko.observable(),

    ItemProperties: ko.observableArray([]),

    ItemEventOnSpawn: ko.observable(),

    /* Placeable Object Fields */

    PlaceableResourceID: ko.observable(),
    PlaceableName: ko.observable(),
    PlaceableTag: ko.observable(),
    PlaceableResref: ko.observable(),

    PlaceableDescription: ko.observable(),
    PlaceableComments: ko.observable(),
    PlaceableLocalVariables: ko.observableArray([]),

    PlaceableEventOnSpawn: ko.observable(),
    PlaceableEventOnDamaged: ko.observable(),
    PlaceableEventOnDeath: ko.observable(),
    PlaceableEventOnHeartbeat: ko.observable(),
    PlaceableEventOnOpen: ko.observable(),
    PlaceableEventOnClose: ko.observable(),
    PlaceableEventOnUsed: ko.observable(),


    /* Conversation Object Fields */

    ConversationResourceID: ko.observable(),
    ConversationName: ko.observable(),
    ConversationTag: ko.observable(),
    ConversationResref: ko.observable(),

    /* Script Object Fields */

    ScriptResourceID: ko.observable(),
    ScriptName: ko.observable(),
    ScriptTag: ko.observable(),
    ScriptResref: ko.observable(),


    /* Functions */

    SaveChanges: function () {
        var mode = ToolsetViewModel.CurrentObjectMode();

        if (mode == 'Area') {
            Entity.SaveObjectData(mode);
        }
        else if (mode == 'Creature') {
            Entity.SaveObjectData(mode);
        }
        else if (mode == 'Item') {
            Entity.SaveObjectData(mode);
        }
        else if (mode == 'Placeable') {
            Entity.SaveObjectData(mode);
        }
        else if (mode == 'Conversation') {
            Entity.SaveObjectData(mode);
        }
        else if (mode == 'Script') {
            Entity.SaveObjectData(mode);
        }
    },
    DiscardChanges: function () {
    
    },

};