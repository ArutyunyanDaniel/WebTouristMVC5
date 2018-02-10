function eventMouseClick(coordinates) {
    var routeInformation = new Object();
    routeInformation.startCoordinatesLat = coordinates.lat();
    routeInformation.startCoordinatesLng = coordinates.lng();

    $.ajax({
        type: "POST",
        url: "/Home/EventMouseClick",
        data: JSON.stringify(routeInformation),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            isEnterLocation = true;
            DrawPathToExcursionRoute(stringToArrayLatLng(data.Route));
            showFinishRouteMarker(data.finishCoordinatesLat, data.finishCoordinatesLng, map);
            alert(data.Distance + ' ' + data.Duration);
            arrayIdVisitedExcursionRoutes = [];
            arrayIdVisitedExcursionRoutes.push(data.listIdVisitedRoutes[0]);
        },
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
        data: JSON.stringify(routeInformation ),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            DrawPathToExcursionRoute(stringToArrayLatLng(data.Route));
            showFinishRouteMarker(data.finishCoordinatesLat, data.finishCoordinatesLng, map);
            alert(data.Distance + ' ' + data.Duration);
            arrayIdVisitedExcursionRoutes = [];
            for (var i = 0; i < data.listIdVisitedRoutes.length; i++)
            {
                arrayIdVisitedExcursionRoutes.push(data.listIdVisitedRoutes[i]);
            }
        },
        error: errorFunc
    });
}

function errorFunc(errorData) {
    alert('Ошибка' + errorData.responseText);
}