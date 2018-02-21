
function initMap(cityLatLng) {
    map = new google.maps.Map(document.getElementById('map'), {
        zoom: 14,
        center: cityLatLng,
        mapTypeId: 'terrain'
    });

    map.addListener('click', function (e) {    
        checkStartMarker();
        checkFinishMarker();
        checkPathtoExcursionRoute();
        checkExcursionRoute();

        showStartRouteMarkerandPanTo(e.latLng, map);
        currentUserlocation = e.latLng;    
        eventMouseClick(e.latLng);
    });

    google.maps.event.addListener(map, 'zoom_changed', function () {
        if (map.getZoom() <= 10)
            firstStepToSecondStep();
    });
}


function initClustter() {
    var options = {
        imagePath: 'images/cluster'
    };
    var markerCluster = new MarkerClusterer(map, arrayAttractionMarkers, options);
}

function showStartRouteMarkerandPanTo(latLng, map) {        
    startRouteMarker = new google.maps.Marker({
        position: latLng,
        map: map
    });

    var infowindow = new google.maps.InfoWindow({
        content: "Your location."
    });

    startRouteMarker.addListener('click', function () {
        infowindow.open(map, startRouteMarker);
    });
    map.panTo(latLng);
}

function showFinishRouteMarker(coordinateLat, coordinateLng, map) {
    checkFinishMarker();
    var myLatLng = { lat: coordinateLat, lng: coordinateLng };

    finisRouteMarker = new google.maps.Marker({
        position: myLatLng,
        map: map
    });

    var infowindow = new google.maps.InfoWindow({
        content: "The beginning of the excursion route!"
    });

    finisRouteMarker.addListener('click', function () {
        infowindow.open(map, finisRouteMarker);
    });
}



function AddMarkerToArrayForClusterMarker(name, description, latLng) {
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
    checkPathtoExcursionRoute();
    pathToExcursionRoute = new google.maps.Polyline({
        path: route,
        geodesic: true,
        strokeColor: '#00B3FD',
        strokeOpacity: 1.0,
        strokeWeight: 6
    });
    pathToExcursionRoute.setMap(map);
}


function DrawExcursionRoutesWhenClickCheckBox(route) {
   var temp = new google.maps.Polyline({
        path: route,
        geodesic: true,
        strokeColor: '#00B3FD',
        strokeOpacity: 1.0,
        strokeWeight: 6
    });
   temp.setMap(map);
   arrayPolyne.push(temp);
}

function DrawExcursionRoutes(route) {
    checkExcursionRoute();
     excursionRoute = new google.maps.Polyline({
        path: route,
        geodesic: true,
        strokeColor: '#00B3FD',
        strokeOpacity: 1.0,
        strokeWeight: 6
    });
    excursionRoute.setMap(map);
}

