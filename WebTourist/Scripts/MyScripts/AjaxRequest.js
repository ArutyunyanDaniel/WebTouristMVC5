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
    if (pathToExcursionRoute !== null) {
        pathToExcursionRoute.setMap(null);
        pathToExcursionRoute = null;
    }
    DrawPathToExcursionRoute(stringToArrayLatLng(data));
}

function errorFunc(errorData) {
    alert('Ошибка' + errorData.responseText);
}



function butClick() {

    $.ajax({
        type: "POST",
        url: "/Home/EventButClickNextRoute",
        data: { userLocation: String(currentUserlocation.lat() + ' ' + currentUserlocation.lng()) },
        dataType: "json",
        success: successFunc,
        error: erro
    });

}

function suc(data) {
    alert(data);
}

function erro(errorData) {
    alert('Ошибка' + errorData.responseText);
}
