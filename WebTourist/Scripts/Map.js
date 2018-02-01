
var map;

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

function ShowMarker(latLng, name, description, map) {


    var marker = new google.maps.Marker({
        position: latLng,
        map: map,
        title: name
    });
    var infowindow = new google.maps.InfoWindow({
        content: description
    });

    marker.addListener('click', function () {
        infowindow.open(map, marker);
    });

}

function StringToLatLng(ll) {
    ll = ll.slice(7, -1);
    var latlng = ll.split(' ');

    return new google.maps.LatLng(parseFloat(latlng[0]), parseFloat(latlng[1]));
}