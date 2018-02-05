
function getExcursionRouteFromDbAndDraw() {
    $.ajax({
        type: "POST",
        url: "/Home/AjaxTest1",
        data: param = "",
        dataType: "json",
        success: sF,
        error: eF
    });
}

function sF(data, status) {
    for (var i = 0; i < data.length; i++) {
        DrawEx(stringToArrayLatLng(data[i]));
    }
}

function eF(errorData) {
    alert('Ошибка' + errorData.responseText);
}


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

    DrawExcursionRoutes(stringToArrayLatLng(data));
}

function errorFunc(errorData) {
    alert('Ошибка' + errorData.responseText);
}