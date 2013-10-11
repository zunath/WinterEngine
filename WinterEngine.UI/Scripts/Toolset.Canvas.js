function LoadAreaCanvasImage(areaID) {
    var canvasImageBase64 = Entity.GetAreaCanvasImage(areaID);
    var image = new Image();
    var context = $('#cnvAreaTilesetImage').getContext("2d");

    image.src = canvasImageBase64;
    context.drawImage(image, 0, 0);
}