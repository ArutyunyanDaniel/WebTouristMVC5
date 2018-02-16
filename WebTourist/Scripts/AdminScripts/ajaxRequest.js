function eventB() {
  
   
    var name = $("#inpT").val() ;

    $.ajax({
        type: "POST",
        url: "/Admin/ExcrusionSearch",
        data: JSON.stringify({ nameS: name }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",

        success: function (data) {
            console.log(data[0].CoordinatesRoute);
            $('#result').html(data[0].CoordinatesRoute);
        },
        error: function (data) { }

    });

}

$('#butT').click(eventB);