function eventMouseClick(coordinates) {
    var routeInformation = new Object();
    routeInformation.startCoordinatesLat = coordinates.lat();
    routeInformation.startCoordinatesLng = coordinates.lng();

    $.ajax({
        type: "POST",
        url: "/Home/EventMouseClick",
        data: JSON.stringify(routeInformation),
        contentType: "application/json; charset=utf-8",
        beforeSend: showTransparentBackgroundAndPreloader,

        success: function (data) {
            isEnterLocation = true;
            DrawPathToExcursionRoute(stringToArrayLatLng(data.Route));
            showFinishRouteMarker(data.finishCoordinatesLat, data.finishCoordinatesLng, map);
            showDistanceDuration(data.Distance, data.Duration);

            arrayIdVisitedExcursionRoutes = [];
            arrayIdVisitedExcursionRoutes.push(data.listIdVisitedRoutes[0]);
        },

        complete: hideTransparentBackgroundAndPreloader,

        error: errorFunc
    });

}

function eventButtomClick() {
    if (!isEnterLocation) {
        alert('Select your location');
        return;
    }

    var routeInformation = new Object();
    routeInformation.startCoordinatesLat = currentUserlocation.lat();
    routeInformation.startCoordinatesLng = currentUserlocation.lng();
    routeInformation.listIdVisitedRoutes = [];

    for (var i = 0; i < arrayIdVisitedExcursionRoutes.length; i++)
        routeInformation.listIdVisitedRoutes.push(arrayIdVisitedExcursionRoutes[i]);

    $.ajax({
        type: "POST",
        url: "/Home/EventButClickNextRoute",
        data: JSON.stringify(routeInformation),
        contentType: "application/json; charset=utf-8",

        success: function (data) {
            DrawPathToExcursionRoute(stringToArrayLatLng(data.Route));
            showFinishRouteMarker(data.finishCoordinatesLat, data.finishCoordinatesLng, map);
            showDistanceDuration(data.Distance, data.Duration);

            arrayIdVisitedExcursionRoutes = [];
            for (var i = 0; i < data.listIdVisitedRoutes.length; i++)
                arrayIdVisitedExcursionRoutes.push(data.listIdVisitedRoutes[i]);
        },
        error: errorFunc
    });

}

function showTransparentBackgroundAndPreloader() {
    $("#transparentBackground").show('slow');
    $("#preloader").show('slow');
}

function hideTransparentBackgroundAndPreloader() {
    $("#transparentBackground").hide('slow');
    $("#preloader").hide('slow');
}


function errorFunc(errorData) {
    alert('Ошибка' + errorData.responseText);
}

$('#but1').click(eventButtomClick);

$("#but1").mouseover(function () {
    $(this).css("color", "#00B3FD");
});

$("#but1").mouseout(function () {
    $(this).css("color", "#666666");
});

$(document).ready(loadPage);
