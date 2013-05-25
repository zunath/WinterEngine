function Initialize() {
    InitializeDataTable();
}

function GetAllServers() {
    var jsonServerList = Entity.GetServerList();
    //var serverList = $(jsonServerList).parseJSON();

    $('#test').text(jsonServerList);

}
       
function InitializeDataTable() {
    $('#tblServerList').dataTable({
        "sDom": 'rfrti',
        "bPaginate": true,
        "bLengthChange": false,
        "bFilter": true,
        "bSort": true,
        "bAutoWidth": false,
        "sPaginationType": 'full_numbers'

    });


}