var map;
var excursionRoutes = null;
var userMarker = null;
var route = null;

function initMap() {
    map = new google.maps.Map(document.getElementById('map'), {
        zoom: 13,
        center: { lat: 47.234524, lng: 38.884903 },
        mapTypeId: 'terrain'
    });

    $.ajax({
        type: "POST",
        url: "/Home/AjaxTest1",
        data: param = "",
        dataType: "json",
        success: sF,
        error: eF
    });

    map.addListener('click', function (e) {
        placeMarkerAndPanTo(e.latLng, map);

        if (excursionRoutes != null) {
            excursionRoutes.setMap(null);
            excursionRoutes = null;
        }

        $.ajax({
            type: "POST",
            url: "/Home/EventMouseClick",
            data: { userLocation: String(e.latLng.lat() + ' ' + e.latLng.lng()) },
            datatype: "json",
            success: successFunc,
            error: errorFunc
        });
    });

}

function successFunc(data) {

    DrawExcursionRoutes(stringToArrayLatLng(data));
}

function errorFunc(errorData) {
    alert('Ошибка' + errorData.responseText);
}

function placeMarkerAndPanTo(latLng, map) {
    if (userMarker != null) {
        userMarker.setMap(null);
        userMarker = null;
    }

    userMarker = new google.maps.Marker({
        position: latLng,
        map: map,
        title: String(latLng.lat()) + String(latLng.lng())
    });

    map.panTo(latLng);

    var infowindow = new google.maps.InfoWindow({
        content: "Your location."
    });

    userMarker.addListener('click', function () {
        infowindow.open(map, userMarker);
    });
}

function ShowMarker(latLng, name, description, map) {
    var marker = new google.maps.Marker({
        position: latLng,
        map: map,
        title: name,
        icon: "/Images/museum.png"
    });
    var infowindow = new google.maps.InfoWindow({
        content: description
    });

    marker.addListener('click', function () {
        infowindow.open(map, marker);
    });

}


function sF(data, status) {
    for (var i = 0; i < data.length; i++) {
        DrawEx(stringToArrayLatLng(data[i]));
    }
    
}

function eF(errorData) {
    alert('Ошибка' + errorData.responseText);
}

function StringToLatLng(ll) {
    ll = ll.slice(7, -1);
    var latlng = ll.split(' ');
    return new google.maps.LatLng(parseFloat(latlng[0]), parseFloat(latlng[1]));
}

function stringToArrayLatLng(coordinatesString) {
    coordinatesString = coordinatesString.slice(1, -1);
    var temp = coordinatesString.split(",");
    var arrayCoordinates = [];

    while (temp.length) {
        var item = temp[0].split(' ');
        temp.splice(0, 1);

        var latLng = new google.maps.LatLng(parseFloat(item[0]), parseFloat(item[1]));
        arrayCoordinates.push({ lat: latLng.lat(), lng: latLng.lng() });
    }
    return arrayCoordinates;
}

function DrawExcursionRoutes(route) {
    excursionRoutes = new google.maps.Polyline({
        path: route,
        geodesic: true,
        strokeColor: '#FF0000',
        strokeOpacity: 1.0,
        strokeWeight: 6
    });

    excursionRoutes.setMap(map);
}


function DrawEx(route) {
    route = new google.maps.Polyline({
        path: route,
        geodesic: true,
        strokeColor: '#FF0000',
        strokeOpacity: 1.0,
        strokeWeight: 6
    });

    route.setMap(map);
}