

function initMap() {
    map = new google.maps.Map(document.getElementById('map'), {
        zoom: 13,
        center: { lat: 47.234524, lng: 38.884903 },
        mapTypeId: 'terrain'
    });

    map.addListener('click', function (e) {
   

            placeMarkerAndPanTo(e.latLng, map);
    });

}

function placeMarkerAndPanTo(latLng, map) {
    var marker = new google.maps.Marker({
        position: latLng,
        map: map

    });
    map.panTo(latLng);

    var infowindow = new google.maps.InfoWindow({
        content: "Your location."
    });

    marker.addListener('click', function () {
        infowindow.open(map, marker);
    });
}