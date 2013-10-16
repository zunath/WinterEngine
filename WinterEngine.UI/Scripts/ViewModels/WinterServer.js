﻿// <autogenerated>
//   This file was generated by T4 code generator ViewModelGenerator.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


var WinterServer = function()
{
	var self = this;

	//	Type of the original .NET model. Must stay first ! (required for JSON.NET)
	self.$type = 'WinterEngine.DataTransferObjects.BusinessObjects.WinterServer, WinterEngine.DataTransferObjects';

	self.ServerName = ko.observable();
	self.ServerDescription = ko.observable();
	self.ServerAnnouncement = ko.observable();
	self.ServerMaxPlayers = ko.observable();
	self.ServerMaxLevel = ko.observable();
	self.Ping = ko.observable();
	self.Connection = ko.observable();
	self.LastPacketReceived = ko.observable();
	self.ServerPort = ko.observable();
	self.PVPTypeID = ko.observable();
	self.GameTypeID = ko.observable();
	self.IsAutoDownloadEnabled = ko.observable();
	self.IsCharacterDeletionEnabled = ko.observable();

	//	invoke the extendable's init() function. Must happen when all observables are created (in case a
    //	computable wants to make use of it)
    this.init();

    self.setModel = function(objFromServer) {
	  if (!objFromServer) return;

	  self.ServerName(objFromServer.ServerName);
	  self.ServerDescription(objFromServer.ServerDescription);
	  self.ServerAnnouncement(objFromServer.ServerAnnouncement);
	  self.ServerMaxPlayers(objFromServer.ServerMaxPlayers);
	  self.ServerMaxLevel(objFromServer.ServerMaxLevel);
	  self.Ping(objFromServer.Ping);
	  if (objFromServer.Connection) {
        var __Connection = new ConnectionAddress();
	    __Connection.setModel(objFromServer.Connection);
	    self.Connection(__Connection);
	  }
	  else
		self.Connection(null);
	  if (objFromServer.LastPacketReceived)
		self.LastPacketReceived(moment(objFromServer.LastPacketReceived).toDate());
	  else
	    self.LastPacketReceived(null);
	  self.ServerPort(objFromServer.ServerPort);
	  if (objFromServer.PVPTypeID) {
        var __PVPTypeID = new PVPTypeEnum();
	    __PVPTypeID.setModel(objFromServer.PVPTypeID);
	    self.PVPTypeID(__PVPTypeID);
	  }
	  else
		self.PVPTypeID(null);
	  if (objFromServer.GameTypeID) {
        var __GameTypeID = new GameTypeEnum();
	    __GameTypeID.setModel(objFromServer.GameTypeID);
	    self.GameTypeID(__GameTypeID);
	  }
	  else
		self.GameTypeID(null);
	  self.IsAutoDownloadEnabled(objFromServer.IsAutoDownloadEnabled);
	  self.IsCharacterDeletionEnabled(objFromServer.IsCharacterDeletionEnabled);
	  
	  //	check if change tracking is active for this viewmodel. If it is: reset isDirty flag
	  if (self.isDirty)
		  self.isDirty(false);
    }
}

//	inherit from extendable (will allow adding observables/computables on client-side)
WinterServer.prototype = new extendable();

