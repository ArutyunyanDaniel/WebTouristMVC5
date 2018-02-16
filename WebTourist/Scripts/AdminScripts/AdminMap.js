
var map;
function initMapAdmin() {
    map = new google.maps.Map(document.getElementById('map'), {
        zoom: 14,
        center: { lat: 47.21242644449467, lng: 38.92554759979248 },
        mapTypeId: 'terrain'
    });
    map.addListener('click', function (e) {
        showStartRouteMarkerandPanTo(e.latLng, map);
    });
}

function showStartRouteMarkerandPanTo(latLng, map) {

    var startRouteMarker = new google.maps.Marker({
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