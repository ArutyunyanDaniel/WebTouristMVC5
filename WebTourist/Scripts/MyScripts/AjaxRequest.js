﻿function eventMouseClick(coordinates) {
    var routeInformation = new Object();
    routeInformation.startCoordinatesLat = coordinates.lat();
    routeInformation.startCoordinatesLng = coordinates.lng();
    routeInformation.IdCurrentCity = idCity;
    $.ajax({
        type: "POST",
        url: "/Home/EventMouseClick",
        data: JSON.stringify(routeInformation),
        contentType: "application/json; charset=utf-8",

        beforeSend: showTransparentBackgroundAndPreloader,

        success: function (data) {
            isEnterLocation = true;
            DrawPathToExcursionRoute(stringToArrayLatLng(data.WayToExcursionRoute));
            DrawExcursionRoutes(stringToArrayLatLng(data.ExcursionRoute));

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
    routeInformation.IdCurrentCity = idCity;
    routeInformation.listIdVisitedRoutes = [];

    for (var i = 0; i < arrayIdVisitedExcursionRoutes.length; i++)
        routeInformation.listIdVisitedRoutes.push(arrayIdVisitedExcursionRoutes[i]);

    $.ajax({
        type: "POST",
        url: "/Home/EventButClickNextRoute",
        data: JSON.stringify(routeInformation),
        contentType: "application/json; charset=utf-8",

        success: function (data) {
            DrawPathToExcursionRoute(stringToArrayLatLng(data.WayToExcursionRoute));
            DrawExcursionRoutes(stringToArrayLatLng(data.ExcursionRoute));

            showFinishRouteMarker(data.finishCoordinatesLat, data.finishCoordinatesLng, map);
            showDistanceDuration(data.Distance, data.Duration);

            arrayIdVisitedExcursionRoutes = [];
            for (var i = 0; i < data.listIdVisitedRoutes.length; i++)
                arrayIdVisitedExcursionRoutes.push(data.listIdVisitedRoutes[i]);
        },
        error: errorFunc
    });

}

function eventCheckBoxClick() {
    if ($('#checkbox').prop('checked')) {
        var cityName = { 'idCurrentCity': idCity };

        $.ajax({
            type: "POST",
            url: "/Home/EventCheckBoxClick",
            data: JSON.stringify(cityName),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                for (var i = 0; i < data.length; i++) {
                    DrawExcursionRoutesWhenClickCheckBox(stringToArrayLatLng(data[i]));
                }
            },
            error: errorFunc
        });
    }
    else {
        for (var i = 0; i < arrayPolyne.length; i++) {
            if (arrayPolyne[i] !== null) {
                arrayPolyne[i].setMap(null);
                arrayPolyne[i] = null;
            }
        }
    }
}

function CityAccept() {
    var currentCity = { 'city': $('#input-text').val() };

    $.ajax({
        type: "POST",
        url: "/Home/EventCitySelect",
        data: JSON.stringify(currentCity),
        contentType: "application/json; charset=utf-8",

        success: function (data) {
            if (data.success) {
                idCity = data.idSelectedCity;
                $('.first-step').fadeToggle("slow");
                $('.second-step').hide();
                $('.butPossition').fadeToggle("slow");
            }

            else
                alert("Not City");
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