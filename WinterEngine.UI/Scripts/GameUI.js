
/* MAIN UI JQUERY FUNCTIONS */

function InitializeAllComponents() {
    InitializeActionBar();
    InitializeChatBox();
    InitializeGameArea();
    InitializeInventory();
    InitializeJournal();
    InitializeMiniMap();
    InitializeParty();
    InitializeProgressBar();
    InitializeStatusBar();
    InitializeStatusEffects();
    InitializeMasonryContainer();
}

/* COMPONENT INITIALIZATION FUNCTIONS */

function InitializeProgressBar() {

    $('#divExperienceProgressBar').progressbar({
        value: 50
    });
    /*
    $('#divExperienceProgressBar').draggable({
        disabled: false,
        opacity: 0.5,
        scroll: false
    });
    */
    var componentWidth = $('#divExperienceProgressBar').width();
    var componentHeight = $('#divExperienceProgressBar').height();

    var rightPosition = $(window).width() - componentWidth;
    var topPosition = $(window).height() - componentHeight - 50;

    
    $('#divExperienceProgressBar').css('right', rightPosition);
    $('#divExperienceProgressBar').css('top', topPosition);

}

function InitializeInventory() {
    $('#divInventory').dialog({
        title: 'Inventory',
        resizable: false,
        autoOpen: false
    });

    BuildInventoryItemTable();
}

function InitializeStatusBar() {
}

function InitializeGameArea() {
}

function InitializeMiniMap() {
}

function InitializeStatusEffects() {
}

function InitializeChatBox() {
}

function InitializeJournal() {
}

function InitializeParty() {
}

function InitializeActionBar() {
}

function InitializeMasonryContainer() {
    var windowHeight = $(window).height();
    var windowWidth = $(window).width();

    $('#divMasonryContainer').css('max-height', windowHeight);
    $('#divMasonryContainer').css('max-width', windowWidth);
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