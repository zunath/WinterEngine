function ToolsetViewModel() {
    var self = this;

    /* Area Object Fields */
    self.AreaName = ko.observable();
    self.AreaTag = ko.observable();
    self.AreaResref = ko.observable();
    self.AreaTileset = ko.observable();
    self.AreaBackgroundMusic = ko.observable();
    self.AreaBattleMusic = ko.observable();
    self.AreaDescription = ko.observable();
    self.AreaComments = ko.observable();
    self.AreaLocalVariables = ko.observableArray([]);
    self.AreaEventOnAreaEnter = ko.observable();
    self.AreaEventOnAreaExit = ko.observable();
    self.AreaEventOnAreaHeartbeat = ko.observable();
    self.AreaEventOnAreaUserDefinedEvent = ko.observable();

    /* Creature Object Fields */
    self.CreatureName = ko.observable();
    self.CreatureTag = ko.observable();
    self.CreatureResref = ko.observable();
    self.CreatureAppearance = ko.observable();
    self.CreatureRace = ko.observable();
    self.CreatureGender = ko.observable();
    self.CreatureFaction = ko.observable();
    self.CreatureConversation = ko.observable();
    self.CreatureIsInvulnerable = ko.observable();

    self.CreatureHitPoints = ko.observable();
    self.CreatureMana = ko.observable();
    self.CreatureStrength = ko.observable();
    self.CreatureDexterity = ko.observable();
    self.CreatureConstitution = ko.observable();
    self.CreatureIntelligence = ko.observable();
    self.CreatureWisdom = ko.observable();
    self.CreatureAbilities = ko.observableArray([]);

    self.CreatureEquipmentMainHand = ko.observable();
    self.CreatureEquipmentOffHand = ko.observable();
    self.CreatureEquipmentHead = ko.observable();
    self.CreatureEquipmentBody = ko.observable();
    self.CreatureEquipmentBack = ko.observable();
    self.CreatureEquipmentHands = ko.observable();
    self.CreatureEquipmentWaist = ko.observable();
    self.CreatureEquipmentLegs = ko.observable();
    self.CreatureEquipmentFeet = ko.observable();
    self.CreatureEquipmentEarL = ko.observable();
    self.CreatureEquipmentEarR = ko.observable();
    self.CreatureEquipmentRingL = ko.observable();
    self.CreatureEquipmentRingR = ko.observable();
    self.CreatureEquipmentNeck = ko.observable();

    self.CreatureInventory = ko.observableArray([]);
    self.CreatureDescription = ko.observable();
    self.CreatureComments = ko.observable();
    self.CreatureLocalVariables = ko.observableArray([]);

    self.CreatureEventOnSpawn = ko.observable();
    self.CreatureEventOnDamaged = ko.observable();
    self.CreatureEventOnDeath = ko.observable();
    self.CreatureEventOnConversation = ko.observable();
    self.CreatureEventOnHeartbeat = ko.observable();


    /* Item Object Fields */

    self.ItemName = ko.observable();
    self.ItemTag = ko.observable();
    self.ItemResref = ko.observable();

    self.ItemDescription = ko.observable();
    self.ItemComments = ko.observable();
    self.ItemLocalVariables = ko.observableArray([]);

    self.ItemEventOnSpawn = ko.observable();

    /* Placeable Object Fields */

    self.PlaceableName = ko.observable();
    self.PlaceableTag = ko.observable();
    self.PlaceableResref = ko.observable();

    self.PlaceableDescription = ko.observable();
    self.PlaceableComments = ko.observable();
    self.PlaceableLocalVariables = ko.observableArray([]);

    self.PlaceableEventOnSpawn = ko.observable();
    self.PlaceableEventOnDamaged = ko.observable();
    self.PlaceableEventOnDeath = ko.observable();
    self.PlaceableEventOnHeartbeat = ko.observable();
    self.PlaceableEventOnOpen = ko.observable();
    self.PlaceableEventOnClose = ko.observable();
    self.PlaceableEventOnUsed = ko.observable();


    /* Conversation Object Fields */

    self.ConversationName = ko.observable();
    self.ConversationTag = ko.observable();
    self.ConversationResref = ko.observable();

    /* Script Object Fields */

    self.ScriptName = ko.observable();
    self.ScriptTag = ko.observable();
    self.ScriptResref = ko.observable();
}