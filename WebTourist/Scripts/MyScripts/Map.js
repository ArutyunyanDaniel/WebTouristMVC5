var map;
var pathToExcursionRoute = null;
var userMarker = null;
var arrayAttractionMarkers = [];
var currentUserlocation = null;
var isEnterLocation= false;

function initMap() {
    map = new google.maps.Map(document.getElementById('map'), {
        zoom: 14,
        center: { lat: 47.21242644449467, lng: 38.92554759979248 },
        mapTypeId: 'terrain'
    });

    google.maps.event.addDomListener(window, 'load', initClustter);

    map.addListener('click', function (e) {
        placeMarkerAndPanTo(e.latLng, map);
        currentUserlocation = e.latLng;

        checkPathtoExcursionRoute();

        eventMouseClick(e.latLng);
    });
}

function initClustter() {
    var options = {
        imagePath: 'images/cluster'
    };
    var markerCluster = new MarkerClusterer(map, arrayAttractionMarkers, options);
}

function placeMarkerAndPanTo(latLng, map) {
    checkUserMarker();

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

function AddMarkerToArrayForClusterMarker(latLng, name, description, map) {
    var marker = new google.maps.Marker({
        position: latLng,
        title: name,
        icon: "/Images/museum.png"
    });

    var infowindow = new google.maps.InfoWindow({
        content: description
    });

    marker.addListener('click', function () {
        infowindow.open(map, marker);
    });

    arrayAttractionMarkers.push(marker);
}

function DrawPathToExcursionRoute(route) {
    pathToExcursionRoute = new google.maps.Polyline({
        path: route,
        geodesic: true,
        strokeColor: '#023a72',
        strokeOpacity: 1.0,
        strokeWeight: 6
    });
    pathToExcursionRoute.setMap(map);
}

function DrawExcursionRoutes(route) {
    var excursionRoute = new google.maps.Polyline({
        path: route,
        geodesic: true,
        strokeColor: '#00B3FD',
        strokeOpacity: 1.0,
        strokeWeight: 6
    });
    excursionRoute.setMap(map);
}

function checkUserMarker() {
    if (userMarker !== null) {
        userMarker.setMap(null);
        userMarker = null;
    }
}

function checkPathtoExcursionRoute() {
    if (pathToExcursionRoute !== null) {
        pathToExcursionRoute.setMap(null);
        pathToExcursionRoute = null;
    }
}