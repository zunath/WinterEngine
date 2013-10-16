﻿// <autogenerated>
//   This file was generated by T4 code generator ViewModelGenerator.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


var Tile = function()
{
	var self = this;

	//	Type of the original .NET model. Must stay first ! (required for JSON.NET)
	self.$type = 'WinterEngine.DataTransferObjects.Tile, WinterEngine.DataTransferObjects';

	self.TileID = ko.observable();
	self.TextureCellX = ko.observable();
	self.TextureCellY = ko.observable();
	self.TileHeight = ko.observable();
	self.MapCellX = ko.observable();
	self.MapCellY = ko.observable();
	self.MapLayer = ko.observable();
	self.IsPassable = ko.observable();

	//	invoke the extendable's init() function. Must happen when all observables are created (in case a
    //	computable wants to make use of it)
    this.init();

    self.setModel = function(objFromServer) {
	  if (!objFromServer) return;

	  self.TileID(objFromServer.TileID);
	  self.TextureCellX(objFromServer.TextureCellX);
	  self.TextureCellY(objFromServer.TextureCellY);
	  self.TileHeight(objFromServer.TileHeight);
	  self.MapCellX(objFromServer.MapCellX);
	  self.MapCellY(objFromServer.MapCellY);
	  self.MapLayer(objFromServer.MapLayer);
	  self.IsPassable(objFromServer.IsPassable);
	  
	  //	check if change tracking is active for this viewmodel. If it is: reset isDirty flag
	  if (self.isDirty)
		  self.isDirty(false);
    }
}

//	inherit from extendable (will allow adding observables/computables on client-side)
Tile.prototype = new extendable();

