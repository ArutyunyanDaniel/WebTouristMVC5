var map;

function initMap() {
    map = new google.maps.Map(document.getElementById('map'), {
        zoom: 13,
        center: { lat: 47.234524, lng: 38.884903 },
        mapTypeId: 'terrain'
    });
    

    map.addListener('click', function (e) {
        placeMarkerAndPanTo(e.latLng, map);
        
        $.ajax({
            type: "POST",
            url: "/Home/AjaxTest2",
            data: { coordinateLat: e.latLng.lat(), coordinateLng: e.latLng.lng() },     
            datatype: "json",
            success: successFunc,
            error: errorFunc
        });
    });

}

function successFunc(data) {
    alert(data);
}

function errorFunc(errorData) {
            alert('Ошибка' + errorData.responseText);
}

function placeMarkerAndPanTo(latLng, map) {
    var marker = new google.maps.Marker({
        position: latLng,
        map: map,
        title: String(latLng.lat()) + String(latLng.lng())
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

function StringToLatLng(ll)
{
    ll = ll.slice(7, -1);
    var latlng = ll.split(' ');
    return new google.maps.LatLng(parseFloat(latlng[0]), parseFloat(latlng[1]));
}
