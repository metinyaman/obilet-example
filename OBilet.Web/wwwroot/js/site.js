// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $("#originName").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/OBilet/AutoComplete",
                type: "POST",
                dataType: "json",
                data: { Prefix: request.term },
                success: function (data) {
                    response($.map(data, function (item) { return { label: item.name, value: item.id }; }));
                }
            });
        },
        select: function (event, ui) {
            $("#originName").val(ui.item.label);
            $("#originId").val(ui.item.value);
            return false;
        }
    });
});

$(document).ready(function () {
    $("#destinationName").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/OBilet/AutoComplete",
                dataType: "json",
                data: { Prefix: request.term },
                success: function (data) {
                    response($.map(data, function (item) { return { label: item.name, value: item.id }; }));
                }
            });
        },
        select: function (event, ui) {
            $("#destinationName").val(ui.item.label);
            $("#destinationId").val(ui.item.value);
            return false;
        }
    });
});

$("#today").click(function () {
    document.getElementById("departureDate").value = `${new Date().toLocaleDateString('en-GB').split('/').reverse().join('-')}`;
    document.getElementById("today").classList.add("active");
    document.getElementById("tomorrow").classList.remove("active");
});

$("#tomorrow").click(function () {
    var tomorrow = new Date();
    tomorrow.setDate(tomorrow.getDate() + 1);
    document.getElementById("departureDate").value = `${tomorrow.toLocaleDateString('en-GB').split('/').reverse().join('-')}`;
    document.getElementById("tomorrow").classList.add("active");
    document.getElementById("today").classList.remove("active");
});

$("#back").click(function () {
    console.log("selam");
});