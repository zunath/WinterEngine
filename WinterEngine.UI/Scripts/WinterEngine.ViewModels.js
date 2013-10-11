
var ToolsetViewModel;
function InitializeToolsetViewModel() {

    var data = Entity.GetModelJSON();
    var mapping = {
        'ActiveArea': {
            create: function (options) {
                return ko.observable(options.data);
            }
        },
        'ActiveCreature': {
            create: function (options) {
                return ko.observable(options.data);
            }
        },
        'ActiveItem': {
            create: function (options) {
                return ko.observable(options.data);
            }
        },
        'ActivePlaceable': {
            create: function (options) {
                return ko.observable(options.data);
            }
        },
        'ActiveConversation': {
            create: function (options) {
                return ko.observable(options.data);
            }
        },
        'ActiveScript': {
            create: function (options) {
                return ko.observable(options.data);
            }
        },
        'ActiveTileset': {
            create: function(options) {
                return ko.observable(options.data);
            }
        },
        'TileMap': {
            create: function (options) {
                return ko.observable(options.data);
            }
        },
        'Script': {
            create: function (options) {
                return ko.observable(options.data);
            }
        }
    }

    ToolsetViewModel = ko.mapping.fromJSON(data, mapping);
    ko.applyBindings(ToolsetViewModel);
}