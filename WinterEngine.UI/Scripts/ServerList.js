/* Initialization */

function Initialize() {
    InitializeDataTable();
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
    });
}

/* Button Functionality */

function GetAllServers() {
    $('#divLoading').modal('show');
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
            '<button id="server_' + index + '" type="button" class="btn btn-primary" value="Connect" onclick="ConnectToServer(\'' +
                currentServer.ServerIPAddress + '\', ' + currentServer.ServerPort + ');">Connect</button>'
        ]);
    }

    $('#divLoading').modal('hide');
}

function GoToMainMenu() {
    Entity.GoToMainMenu();
}

function ConnectToServer(ipAddress, port) {

    $('#divConnectingToServer').modal('show');
    Entity.ConnectToServer(ipAddress, port);
}

function UpdateDownloadProgressBar(percentComplete, fileName) {

    $('#lblConnectingToServer').text('Downloading File: ' + fileName);

    $('#divDownloadContainer').removeClass('clsHidden');
    $('#divDownloadProgress').attr('aria-valuenow', percentComplete).css('width', percentComplete);
    $('#divConnectingLoading').addClass('clsHidden');
}

function CancelConnectToServer() {
    $('#divConnectingToServer').modal('hide');
    $('#divDownloadContainer').addClass('clsHidden');
    $('#divConnectingLoading').removeClass('clsHidden');
    $('#divDownloadProgress').attr('aria-valuenow', 0).css('width', 0);
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

    $('#divLoading').modal('hide');
    $('#divConnectingToServer').modal('hide');
    $('#divUnableToConnectToServer').modal('show');
}

function UnableToConnectToServerClose() {
    $('#divUnableToConnectToServer').modal('hide');
}

function DisconnectFromServerWithReason_Callback(reason) {
    $('#lblConnectionFailureReason').text(reason);

    $('#divLoading').modal('hide');
    $('#divConnectingToServer').modal('hide');
    $('#divUnableToConnectToServer').modal('show');
}