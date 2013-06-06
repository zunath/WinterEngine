/* Page Initialization */

function Initialize() {

    InitializeLevelProgressBar();
}

function InitializeLevelProgressBar() {
    $('#divLevelProgressBar').progressbar({
        max: 100,
        value: 50
    });
}