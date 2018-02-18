$('#but1').click(eventButtomClick);

$('#checkbox').click(eventCheckBoxClick);

$('#butAcceptCity').click(CityAccept);

$(document).ready(loadPage);


$("#but1").mouseover(function () {
    $(this).css("color", "#00B3FD");
});

$("#but1").mouseout(function () {
    $(this).css("color", "#666666");
});
