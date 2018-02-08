function eventMouseClick(coordinates) {
    $.ajax({
        type: "POST",
        url: "/Home/EventMouseClick",
        data: { userLocation: String(coordinates.lat() + ' ' + coordinates.lng()) },
        datatype: "json",
        success: successFunc,
        error: errorFunc
    });
}

function eventButtomClick() {
    if (!isEnterLocation) {
        alert('Select your location');
        return;
    }
    $.ajax({
        type: "POST",
        url: "/Home/EventButClickNextRoute",
        data: { userLocation: String(currentUserlocation.lat() + ' ' + currentUserlocation.lng()) },
        dataType: "json",
        success: successFunc,
        error: errorFunc
    });
}

function successFunc(data) {
    isEnterLocation = true;
    checkPathtoExcursionRoute();
    DrawPathToExcursionRoute(stringToArrayLatLng(data));
}

function errorFunc(errorData) {
    alert('Ошибка' + errorData.responseText);
}