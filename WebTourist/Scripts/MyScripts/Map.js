var map;
var pathToExcursionRoute = null;
var userMarker = null;
var arrayMarkers = [];
var currentUserlocation = null;

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
        if (pathToExcursionRoute !== null) {
            pathToExcursionRoute.setMap(null);
            pathToExcursionRoute = null;
        }
        eventMouseClick(e.latLng);
    }); 
}

function initClustter() {
    var options = {
        imagePath: 'images/m'
    };
    var markerCluster = new MarkerClusterer(map, arrayMarkers, options);
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

    arrayMarkers.push(marker);
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
    var excursionRoute = new google.maps.Polyline({
        path: route,
        geodesic: true,
        strokeColor: '#00B3FD',
        strokeOpacity: 1.0,
        strokeWeight: 6
    });
    excursionRoute.setMap(map);
}