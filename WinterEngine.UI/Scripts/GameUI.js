
function OpenInventory() {
    $('#divInventory').dialog({
        title: 'Inventory',
        resizable: false
    });
}

function BuildInventoryItemTable() {
    var numberOfInventoryRows = 5;
    var numberOfInventoryColumns = 5;
    var table = '<table id="tblInventory">';
    table += '<tbody>';

    var currentItemID = 1;
    for (var x = 1; x <= numberOfInventoryRows; x++) {
        var row = '<tr>';

        for (var y = 1; y <= numberOfInventoryColumns; y++) {
            var item = '<td id="tdItem' + currentItemID + '" class="clsInventoryTableCell clsActionBarButton">';

            item += 'test';

            item += '</td>';

            row += item;
            currentItemID++;
        }

        row += '</tr>';

        table += row;
    }

    table += '</tbody>';
    table += '</table>';

    $('#divInventory').append(table);
}