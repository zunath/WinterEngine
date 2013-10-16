﻿// <autogenerated>
//   This file was generated by T4 code generator ViewModelGenerator.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


var ContentPackageXML = function()
{
	var self = this;

	//	Type of the original .NET model. Must stay first ! (required for JSON.NET)
	self.$type = 'WinterEngine.DataTransferObjects.XMLObjects.ContentPackageXML, WinterEngine.DataTransferObjects';

	self.FilePath = ko.observable();
	self.Name = ko.observable();
	self.Description = ko.observable();
	self.ResourceList = ko.observableArray();
	self.IsModified = ko.observable();

	//	invoke the extendable's init() function. Must happen when all observables are created (in case a
    //	computable wants to make use of it)
    this.init();

    self.setModel = function(objFromServer) {
	  if (!objFromServer) return;

	  self.FilePath(objFromServer.FilePath);
	  self.Name(objFromServer.Name);
	  self.Description(objFromServer.Description);
	  self.ResourceList.removeAll();	// clear array first
	  if (objFromServer.ResourceList && objFromServer.ResourceList.length > 0)
	  {
		  for (var i=0; i < objFromServer.ResourceList.length; i++) {
			  var _iter_item = objFromServer.ResourceList[i];
			  				  var _new_item = new ContentPackageResourceXML();
				  _new_item.setModel(_iter_item);
				  self.ResourceList.push(_new_item);
			  		  }
	  }
	  self.IsModified(objFromServer.IsModified);
	  
	  //	check if change tracking is active for this viewmodel. If it is: reset isDirty flag
	  if (self.isDirty)
		  self.isDirty(false);
    }
}

//	inherit from extendable (will allow adding observables/computables on client-side)
ContentPackageXML.prototype = new extendable();

