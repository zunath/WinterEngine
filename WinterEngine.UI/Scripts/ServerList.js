/* Initialization */

function Initialize() {
    InitializeDataTable();
    InitializeConnectingBox();
    InitializeLoadingBox();
    InitializeUnableToConnectToServerBox();
    InitializeDownloadProgressBar();

    $('input:button').button();

    $('.clsProgressBar').progressbar({
        value: false
    });

    GetAllServers();
}

function InitializeDataTable() {
    $('#tblServerList').dataTable({
        "sDom": 'rtf',
        "bPaginate": false,
        "bFilter": false,
        "bSort": true,
        "bAutoWidth": false,
        //"sPaginationType": 'full_numbers',
        "bJQueryUI": true,
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
                currentServer.ServerIPAddress + '\', ' + currentServer.ServerPort + ');" />'
        ]);
    }

    $('input:button').button();

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

function ConnectToServer_Callback(responseID) {
    var reason;

    // Values come from AuthorizationTypeEnum.cs
    if (responseID == 0) {
        reason = "Unknown";
    }
    else if (responseID == 1) {
        reason = "Master server could not locate user.";
    }
    else if (responseID == 2) {
        reason = "Master server could not locate client-server.";
    }
    else if (responseID == 3) {
        reason = "Authorization token mismatch.";
    }
    else if (responseID == 5) {
        reason = "An unknown error occurred.";
    }

    $('#lblConnectionFailureReason').text(reason);

    $('#divLoading').dialog('close');
    $('#divConnectingToServer').dialog('close');
    $('#divUnableToConnectToServer').dialog('open');
}

function UnableToConnectToServerClose() {
    $('#divUnableToConnectToServer').dialog('close');
}

function DisconnectFromServerWithReason_Callback(reason) {
    $('#lblConnectionFailureReason').text(reason);

    $('#divLoading').dialog('close');
    $('#divConnectingToServer').dialog('close');
    $('#divUnableToConnectToServer').dialog('open');
}