/* Initialization */

function Initialize() {
    InitializeDataTable();
    InitializeConnectingBox();
    InitializeLoadingBox();
}

function InitializeDataTable() {
    $('#tblServerList').dataTable({
        "sDom": 'rfrtpi',
        "bPaginate": true,
        "bLengthChange": false,
        "bFilter": true,
        "bSort": true,
        "bAutoWidth": false,
        "sPaginationType": 'full_numbers',
        "iDisplayLength": 9
    });
}

function InitializeConnectingBox() {
    $('#divConnectingToServer').dialog({
        modal: true,
        autoOpen: false,
        title: 'Connecting',
        resizable: false,
        dialogClass: 'jqueryUIDialogNoCloseButton',
        draggable: false
    });
}

function InitializeLoadingBox() {
    $('#divLoading').dialog({
        modal: true,
        autoOpen: false,
        title: 'Loading...',
        resizable: false,
        dialogClass: 'jqueryUIDialogNoCloseButton',
        draggable: false,
        closeOnEscape: false
    });
}

/* Button Functionality */

function GetAllServers() {
    $('#divLoading').dialog('open');
    Entity.GetServerList();
}

function GetAllServers_Callback(jsonServerList) {
    var servers = JSON.parse(jsonServerList);
    
    $('#tblServerList').dataTable().fnClearTable();

    for (var index = 0; index < servers.length; index++) {
        var currentServer = servers[index];

        $('#tblServerList').dataTable().fnAddData([
            currentServer.ServerName,
            currentServer.ServerIPAddress + ':' + currentServer.ServerPort,
            currentServer.ServerMaxLevel,
            currentServer.ServerCurrentPlayers + ' / ' + currentServer.ServerMaxPlayers,
            currentServer.GameType,
            currentServer.PVPType,
            '<input id="server_' + index + '" type="button" value="Connect" onclick="ConnectToServer(\'' +
                currentServer.ServerIPAddress + '\', ' + currentServer.ServerPort + ');" class="clsConnectButton" />'
        ]);
    }

    $('#divLoading').dialog('close');
}

function GoToMainMenu() {
    Entity.GoToMainMenu();
}

function ConnectToServer(ipAddress, port) {

    $('divConnectingToServer').dialog('open');

    Entity.ConnectToServer(ipAddress, port);
}