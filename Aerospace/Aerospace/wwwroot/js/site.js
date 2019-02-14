// Write your JavaScript code.
$(document).ready(function () {
    $('#table_id').DataTable();

    $("#opensky").click(function () {
        $.ajax({
            url: "https://opensky-network.org/api/states/all",
            success: function (result) {
                alert("success api call");
            }
        });
    });
});