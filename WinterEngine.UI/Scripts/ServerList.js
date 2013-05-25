function Initialize() {
    InitializeDataTable();
}

function GetAllServers() {
    var jsonServerList = Entity.GetServerList();

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
            '<input id="server_' + index + '" type="button" value="Connect" onclick="ConnectToServer(\'' + currentServer.ServerIPAddress + '\', ' + currentServer.ServerPort + ');" />'
        ]);

    }
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


function ConnectToServer(ipAddress, port) {
    Entity.ConnectToServer(ipAddress, port);
}