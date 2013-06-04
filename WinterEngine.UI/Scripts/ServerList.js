/* Initialization */

function Initialize() {
    InitializeDataTable();
    InitializeConnectingBox();
    InitializeLoadingBox();
    InitializeUnableToConnectToServerBox();
    InitializeDownloadProgressBar();
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

function InitializeUnableToConnectToServerBox() {
    $('#divUnableToConnectToServer').dialog({
        modal: true,
        autoOpen: false,
        title: 'Connection Failed',
        resizable: false,
        dialogClass: 'jqueryUIDialogNoCloseButton',
        draggable: false
    });
}

function InitializeDownloadProgressBar() {
    $('#divDownloadProgress').progressbar({
        max: 100,
        value: 0
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

    $('#divConnectingToServer').dialog('open');
    Entity.ConnectToServer(ipAddress, port);
}

function UpdateDownloadProgressBar(percentComplete, fileName) {

    $('#lblConnectingToServer').text('Downloading File: ' + fileName);

    $('#divDownloadProgress').removeClass('clsHidden');
    $("#divDownloadProgress").progressbar("option", "value", percentComplete);

}

function CancelConnectToServer() {
    $('#divConnectingToServer').dialog('close');
    $('#divDownloadProgress').addClass('clsHidden');
    $('#divDownloadProgress').progressbar("option", "value", 0);
    $('#lblConnectingToServer').text('Please wait while a connection to the server is established.');
    Entity.CancelConnectToServer();
}

/* Server connection failures */

function UnableToConnectToServer() {
    $('#divLoading').dialog('close');
    $('#divConnectingToServer').dialog('close');
    $('#divUnableToConnectToServer').dialog('open');
}

function UnableToConnectToServerClose() {
    $('#divUnableToConnectToServer').dialog('close');
}