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
        focus: function (event, ui) {
            $("#originName").val(ui.item.label);
            return false;
        },
        change: function (event, ui) {
            if (ui.item == null) {
                $("#originName").val(null);
                $("#originId").val(0);
            }
        },
        select: function (event, ui) {
            $("#originName").val(ui.item.label);
            $("#originId").val(ui.item.value);
            return false;
        }
    });

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
        focus: function (event, ui) {
            $("#destinationName").val(ui.item.label);
            return false;
        },
        change: function (event, ui) {
            if (ui.item == null) {
                $("#destinationName").val(null);
                $("#destinationId").val(0);
            }
        },
        select: function (event, ui) {
            $("#destinationName").val(ui.item.label);
            $("#destinationId").val(ui.item.value);
            return false;
        }
    });

    $("#change").click(function (e) {
        e.preventDefault();
        $.ajax({
            type: "POST",
            url: "/OBilet/Change/",
            data: {
                OriginId: originId = document.getElementById("originId").value,
                OriginName: originId = document.getElementById("originName").value,
                DestinationId: originId = document.getElementById("destinationId").value,
                DestinationName: originId = document.getElementById("destinationName").value,
            },
            success: function (result) {
                document.getElementById("originId").value = result.originId;
                document.getElementById("originName").value = result.originName;
                document.getElementById("destinationId").value = result.destinationId;
                document.getElementById("destinationName").value = result.destinationName;
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

    $("#departureDate").change(function () {
        document.getElementById("tomorrow").classList.remove("active");
        document.getElementById("today").classList.remove("active");

        var today = new Date();
        var tomorrow = new Date();
        tomorrow.setDate(tomorrow.getDate() + 1);

        var value = document.getElementById("departureDate").value;

        if (value == `${today.toLocaleDateString('en-GB').split('/').reverse().join('-')}`)
            document.getElementById("today").classList.add("active");

        if (value == `${tomorrow.toLocaleDateString('en-GB').split('/').reverse().join('-')}`)
            document.getElementById("tomorrow").classList.add("active");
    });
});



