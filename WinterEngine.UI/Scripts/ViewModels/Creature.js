﻿// <autogenerated>
//   This file was generated by T4 code generator ViewModelGenerator.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


var Creature = function()
{
	var self = this;

	//	Type of the original .NET model. Must stay first ! (required for JSON.NET)
	self.$type = 'WinterEngine.DataTransferObjects.Creature, WinterEngine.DataTransferObjects';

	self.FirstName = ko.observable();
	self.LastName = ko.observable();
	self.Race = ko.observable();
	self.Strength = ko.observable();
	self.Dexterity = ko.observable();
	self.Wisdom = ko.observable();
	self.Constitution = ko.observable();
	self.Intelligence = ko.observable();
	self.HitPoints = ko.observable();
	self.Mana = ko.observable();
	self.MaxHitPoints = ko.observable();
	self.MaxMana = ko.observable();
	self.Level = ko.observable();
	self.Location = ko.observable();
	self.IsInvulnerable = ko.observable();
	self.MainHandItem = ko.observable();
	self.OffHandItem = ko.observable();
	self.HeadItem = ko.observable();
	self.BodyItem = ko.observable();
	self.BackItem = ko.observable();
	self.HandsItem = ko.observable();
	self.WaistItem = ko.observable();
	self.LegsItem = ko.observable();
	self.FeetItem = ko.observable();
	self.EarLeftItem = ko.observable();
	self.EarRightItem = ko.observable();
	self.RingLeftItem = ko.observable();
	self.RingRightItem = ko.observable();
	self.NeckItem = ko.observable();
	self.OnSpawnEventScriptID = ko.observable();
	self.OnDamagedEventScriptID = ko.observable();
	self.OnDeathEventScriptID = ko.observable();
	self.OnConversationEventScriptID = ko.observable();
	self.OnHeartbeatEventScriptID = ko.observable();
	self.OnSpawnEventScript = ko.observable();
	self.OnDamagedEventScript = ko.observable();
	self.OnDeathEventScript = ko.observable();
	self.OnConversationEventScript = ko.observable();
	self.OnHeartbeatEventScript = ko.observable();
	self.Description = ko.observable();
	self.Tag = ko.observable();
	self.Resref = ko.observable();
	self.GameObjectTypeID = ko.observable();
	self.GameObjectType = ko.observable();
	self.ResourceCategoryID = ko.observable();
	self.ResourceCategory = ko.observable();
	self.TemporaryDisplayName = ko.observable();
	self.GraphicResource = ko.observable();
	self.GraphicResourceID = ko.observable();
	self.LocalVariables = ko.observableArray();
	self.ResourceID = ko.observable();
	self.Name = ko.observable();
	self.ResourceTypeID = ko.observable();
	self.ResourceType = ko.observable();
	self.Comment = ko.observable();
	self.IsSystemResource = ko.observable();
	self.CreateDate = ko.observable();

	//	invoke the extendable's init() function. Must happen when all observables are created (in case a
    //	computable wants to make use of it)
    this.init();

    self.setModel = function(objFromServer) {
	  if (!objFromServer) return;

	  self.FirstName(objFromServer.FirstName);
	  self.LastName(objFromServer.LastName);
	  if (objFromServer.Race) {
        var __Race = new Race();
	    __Race.setModel(objFromServer.Race);
	    self.Race(__Race);
	  }
	  else
		self.Race(null);
	  self.Strength(objFromServer.Strength);
	  self.Dexterity(objFromServer.Dexterity);
	  self.Wisdom(objFromServer.Wisdom);
	  self.Constitution(objFromServer.Constitution);
	  self.Intelligence(objFromServer.Intelligence);
	  self.HitPoints(objFromServer.HitPoints);
	  self.Mana(objFromServer.Mana);
	  self.MaxHitPoints(objFromServer.MaxHitPoints);
	  self.MaxMana(objFromServer.MaxMana);
	  self.Level(objFromServer.Level);
	  if (objFromServer.Location) {
        var __Location = new Location();
	    __Location.setModel(objFromServer.Location);
	    self.Location(__Location);
	  }
	  else
		self.Location(null);
	  self.IsInvulnerable(objFromServer.IsInvulnerable);
	  if (objFromServer.MainHandItem) {
        var __MainHandItem = new Item();
	    __MainHandItem.setModel(objFromServer.MainHandItem);
	    self.MainHandItem(__MainHandItem);
	  }
	  else
		self.MainHandItem(null);
	  if (objFromServer.OffHandItem) {
        var __OffHandItem = new Item();
	    __OffHandItem.setModel(objFromServer.OffHandItem);
	    self.OffHandItem(__OffHandItem);
	  }
	  else
		self.OffHandItem(null);
	  if (objFromServer.HeadItem) {
        var __HeadItem = new Item();
	    __HeadItem.setModel(objFromServer.HeadItem);
	    self.HeadItem(__HeadItem);
	  }
	  else
		self.HeadItem(null);
	  if (objFromServer.BodyItem) {
        var __BodyItem = new Item();
	    __BodyItem.setModel(objFromServer.BodyItem);
	    self.BodyItem(__BodyItem);
	  }
	  else
		self.BodyItem(null);
	  if (objFromServer.BackItem) {
        var __BackItem = new Item();
	    __BackItem.setModel(objFromServer.BackItem);
	    self.BackItem(__BackItem);
	  }
	  else
		self.BackItem(null);
	  if (objFromServer.HandsItem) {
        var __HandsItem = new Item();
	    __HandsItem.setModel(objFromServer.HandsItem);
	    self.HandsItem(__HandsItem);
	  }
	  else
		self.HandsItem(null);
	  if (objFromServer.WaistItem) {
        var __WaistItem = new Item();
	    __WaistItem.setModel(objFromServer.WaistItem);
	    self.WaistItem(__WaistItem);
	  }
	  else
		self.WaistItem(null);
	  if (objFromServer.LegsItem) {
        var __LegsItem = new Item();
	    __LegsItem.setModel(objFromServer.LegsItem);
	    self.LegsItem(__LegsItem);
	  }
	  else
		self.LegsItem(null);
	  if (objFromServer.FeetItem) {
        var __FeetItem = new Item();
	    __FeetItem.setModel(objFromServer.FeetItem);
	    self.FeetItem(__FeetItem);
	  }
	  else
		self.FeetItem(null);
	  if (objFromServer.EarLeftItem) {
        var __EarLeftItem = new Item();
	    __EarLeftItem.setModel(objFromServer.EarLeftItem);
	    self.EarLeftItem(__EarLeftItem);
	  }
	  else
		self.EarLeftItem(null);
	  if (objFromServer.EarRightItem) {
        var __EarRightItem = new Item();
	    __EarRightItem.setModel(objFromServer.EarRightItem);
	    self.EarRightItem(__EarRightItem);
	  }
	  else
		self.EarRightItem(null);
	  if (objFromServer.RingLeftItem) {
        var __RingLeftItem = new Item();
	    __RingLeftItem.setModel(objFromServer.RingLeftItem);
	    self.RingLeftItem(__RingLeftItem);
	  }
	  else
		self.RingLeftItem(null);
	  if (objFromServer.RingRightItem) {
        var __RingRightItem = new Item();
	    __RingRightItem.setModel(objFromServer.RingRightItem);
	    self.RingRightItem(__RingRightItem);
	  }
	  else
		self.RingRightItem(null);
	  if (objFromServer.NeckItem) {
        var __NeckItem = new Item();
	    __NeckItem.setModel(objFromServer.NeckItem);
	    self.NeckItem(__NeckItem);
	  }
	  else
		self.NeckItem(null);
	  self.OnSpawnEventScriptID(objFromServer.OnSpawnEventScriptID);
	  self.OnDamagedEventScriptID(objFromServer.OnDamagedEventScriptID);
	  self.OnDeathEventScriptID(objFromServer.OnDeathEventScriptID);
	  self.OnConversationEventScriptID(objFromServer.OnConversationEventScriptID);
	  self.OnHeartbeatEventScriptID(objFromServer.OnHeartbeatEventScriptID);
	  if (objFromServer.OnSpawnEventScript) {
        var __OnSpawnEventScript = new Script();
	    __OnSpawnEventScript.setModel(objFromServer.OnSpawnEventScript);
	    self.OnSpawnEventScript(__OnSpawnEventScript);
	  }
	  else
		self.OnSpawnEventScript(null);
	  if (objFromServer.OnDamagedEventScript) {
        var __OnDamagedEventScript = new Script();
	    __OnDamagedEventScript.setModel(objFromServer.OnDamagedEventScript);
	    self.OnDamagedEventScript(__OnDamagedEventScript);
	  }
	  else
		self.OnDamagedEventScript(null);
	  if (objFromServer.OnDeathEventScript) {
        var __OnDeathEventScript = new Script();
	    __OnDeathEventScript.setModel(objFromServer.OnDeathEventScript);
	    self.OnDeathEventScript(__OnDeathEventScript);
	  }
	  else
		self.OnDeathEventScript(null);
	  if (objFromServer.OnConversationEventScript) {
        var __OnConversationEventScript = new Script();
	    __OnConversationEventScript.setModel(objFromServer.OnConversationEventScript);
	    self.OnConversationEventScript(__OnConversationEventScript);
	  }
	  else
		self.OnConversationEventScript(null);
	  if (objFromServer.OnHeartbeatEventScript) {
        var __OnHeartbeatEventScript = new Script();
	    __OnHeartbeatEventScript.setModel(objFromServer.OnHeartbeatEventScript);
	    self.OnHeartbeatEventScript(__OnHeartbeatEventScript);
	  }
	  else
		self.OnHeartbeatEventScript(null);
	  self.Description(objFromServer.Description);
	  self.Tag(objFromServer.Tag);
	  self.Resref(objFromServer.Resref);
	  self.GameObjectTypeID(objFromServer.GameObjectTypeID);
	  if (objFromServer.GameObjectType) {
        var __GameObjectType = new GameObjectTypeEnum();
	    __GameObjectType.setModel(objFromServer.GameObjectType);
	    self.GameObjectType(__GameObjectType);
	  }
	  else
		self.GameObjectType(null);
	  self.ResourceCategoryID(objFromServer.ResourceCategoryID);
	  if (objFromServer.ResourceCategory) {
        var __ResourceCategory = new Category();
	    __ResourceCategory.setModel(objFromServer.ResourceCategory);
	    self.ResourceCategory(__ResourceCategory);
	  }
	  else
		self.ResourceCategory(null);
	  self.TemporaryDisplayName(objFromServer.TemporaryDisplayName);
	  if (objFromServer.GraphicResource) {
        var __GraphicResource = new ContentPackageResource();
	    __GraphicResource.setModel(objFromServer.GraphicResource);
	    self.GraphicResource(__GraphicResource);
	  }
	  else
		self.GraphicResource(null);
	  self.GraphicResourceID(objFromServer.GraphicResourceID);
	  self.LocalVariables.removeAll();	// clear array first
	  if (objFromServer.LocalVariables && objFromServer.LocalVariables.length > 0)
	  {
		  for (var i=0; i < objFromServer.LocalVariables.length; i++) {
			  var _iter_item = objFromServer.LocalVariables[i];
			  				  var _new_item = new LocalVariable();
				  _new_item.setModel(_iter_item);
				  self.LocalVariables.push(_new_item);
			  		  }
	  }
	  self.ResourceID(objFromServer.ResourceID);
	  self.Name(objFromServer.Name);
	  self.ResourceTypeID(objFromServer.ResourceTypeID);
	  if (objFromServer.ResourceType) {
        var __ResourceType = new ResourceTypeEnum();
	    __ResourceType.setModel(objFromServer.ResourceType);
	    self.ResourceType(__ResourceType);
	  }
	  else
		self.ResourceType(null);
	  self.Comment(objFromServer.Comment);
	  self.IsSystemResource(objFromServer.IsSystemResource);
	  if (objFromServer.CreateDate)
		self.CreateDate(moment(objFromServer.CreateDate).toDate());
	  else
	    self.CreateDate(null);
	  
	  //	check if change tracking is active for this viewmodel. If it is: reset isDirty flag
	  if (self.isDirty)
		  self.isDirty(false);
    }
}

//	inherit from extendable (will allow adding observables/computables on client-side)
Creature.prototype = new extendable();

