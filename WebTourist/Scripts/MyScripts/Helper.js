function StringToLatLng(ll) {
    var latlng = ll.split(' ');
    return new google.maps.LatLng(parseFloat(latlng[0]), parseFloat(latlng[1]));
}

function stringToArrayLatLng(coordinatesString) {
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