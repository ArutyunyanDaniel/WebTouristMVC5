var map;
var pathToExcursionRoute = null;
var userMarker = null;
var excursionRoute = null;

function initMap() {
    map = new google.maps.Map(document.getElementById('map'), {
        zoom: 13,
        center: { lat: 47.234524, lng: 38.884903 },
        mapTypeId: 'terrain'
    });


    map.addListener('click', function (e) {
        placeMarkerAndPanTo(e.latLng, map);

        if (pathToExcursionRoute !== null) {
            pathToExcursionRoute.setMap(null);
            pathToExcursionRoute = null;
        }
        eventMouseClick(e.latLng);
    });

}

function placeMarkerAndPanTo(latLng, map) {
    if (userMarker !== null) {
        userMarker.setMap(null);
        userMarker = null;
    }

    userMarker = new google.maps.Marker({
        position: latLng,
        map: map
    });

    var infowindow = new google.maps.InfoWindow({
        content: "Your location."
    });

    userMarker.addListener('click', function () {
        infowindow.open(map, userMarker);
    });

    map.panTo(latLng);
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

function DrawPathToExcursionRoute(route) {
    pathToExcursionRoute = new google.maps.Polyline({
        path: route,
        geodesic: true,
        strokeColor: '#00B3FD',
        strokeOpacity: 1.0,
        strokeWeight: 6
    });
    pathToExcursionRoute.setMap(map);
}


function DrawExcursionRoutes(route) {
    excursionRoute = new google.maps.Polyline({
        path: route,
        geodesic: true,
        strokeColor: '#00B3FD',
        strokeOpacity: 1.0,
        strokeWeight: 6
    });
    excursionRoute.setMap(map);
}