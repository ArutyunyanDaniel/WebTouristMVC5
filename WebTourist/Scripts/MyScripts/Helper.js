var map;
var pathToExcursionRoute = null;
var startRouteMarker = null;
var finisRouteMarker = null;
var arrayAttractionMarkers = [];
var currentUserlocation = null;
var isEnterLocation = false;
var excursionRoute = null;
var arrayIdVisitedExcursionRoutes = [];
var arrayPolyne = [];
var idCity = null;


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

function firstStepToSecondStep() {
    $('.second-step').show("slow");
    $('.first-step').hide("slow");
    $('.butPossition').hide("slow");
    $("#disNumber").text('');
    $("#durNumber").text('');
    $("#checkbox").prop("checked", false);
}

function secondStepToFristStep() {
    $('.second-step').hide("slow");
    $('.first-step').fadeToggle("slow");
    $('.butPossition').fadeToggle("slow");
}

function showDistanceDuration(distance, duration) {
    $("#disNumber").text(distance);
    $("#durNumber").text(duration);
}

function showTransparentBackgroundAndPreloader() {
    $("#transparentBackground").show('slow');
    $("#preloader").show('slow');
}

function hideTransparentBackgroundAndPreloader() {
    $("#transparentBackground").hide('slow');
    $("#preloader").hide('slow');
}

function checkStartMarker() {
    if (startRouteMarker !== null) {
        startRouteMarker.setMap(null);
        startRouteMarker = null;
    }
}

function checkFinishMarker() {
    if (finisRouteMarker !== null) {
        finisRouteMarker.setMap(null);
        finisRouteMarker = null;
    }
}

function checkPathtoExcursionRoute() {
    if (pathToExcursionRoute !== null) {
        pathToExcursionRoute.setMap(null);
        pathToExcursionRoute = null;
    }
}

function checkExcursionRoute() {
    if (excursionRoute !== null) {
        excursionRoute.setMap(null);
        excursionRoute = null;
    }
}