function eventMouseClick(coordinates) {
    var point = new Object();
    point.coordinateLat = coordinates.lat();
    point.coordinateLng = coordinates.lng();

    $.ajax({
        type: "POST",
        url: "/Home/EventMouseClick",
        data: JSON.stringify(point),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            isEnterLocation = true;
            checkPathtoExcursionRoute();
            DrawPathToExcursionRoute(stringToArrayLatLng(data.pathToExcursionRoute));
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
 
    var point = new Object();
    point.coordinateLat = currentUserlocation.lat();
    point.coordinateLng = currentUserlocation.lng();
    point.listIdVisitedRoutes = [];

    for (var i = 0; i < arrayIdVisitedExcursionRoutes.length; i++)
    {
        point.listIdVisitedRoutes.push(arrayIdVisitedExcursionRoutes[i]);
    }

    $.ajax({
        type: "POST",
        url: "/Home/EventButClickNextRoute",
        data: JSON.stringify( point ),
        contentType: "application/json; charset=utf-8",
        success: function (data) { 
            checkPathtoExcursionRoute();
            DrawPathToExcursionRoute(stringToArrayLatLng(data.pathToExcursionRoute));
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