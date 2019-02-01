// Write your JavaScript code.
$(document).ready(function () {
    $("#opensky").click(function () {
        $.ajax({
            url: "https://opensky-network.org/api/states/all",
            success: function (result) {
                alert("success api call");
            }
        });
    });
});

var map;
function initMap() {
    map = new google.maps.Map(document.getElementById('map'), {
        center: { lat: -34.397, lng: 150.644 },
        zoom: 8
    });
}