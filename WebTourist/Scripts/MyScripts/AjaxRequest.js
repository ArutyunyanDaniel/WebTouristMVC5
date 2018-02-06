function eventMouseClick(ll) {
    $.ajax({
        type: "POST",
        url: "/Home/EventMouseClick",
        data: { userLocation: String(ll.lat() + ' ' + ll.lng()) },
        datatype: "json",
        success: successFunc,
        error: errorFunc
    });
}

function successFunc(data) {
    console.log(data);
    DrawPathToExcursionRoute(stringToArrayLatLng(data));
}

function errorFunc(errorData) {
    alert('Ошибка' + errorData.responseText);
}