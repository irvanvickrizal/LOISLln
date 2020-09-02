var countryZone = "Bangladesh Standard Time";

$(document).ready(function () {
    var hubProxy = $.connection.clockHub;

    hubProxy.client.setTime = function (time) {
        $('#clock').html(time);
    };

    $.connection.hub.start().done(function () {
        setInterval(function () {
            hubProxy.server.getTime(countryZone);
        }, 1000);
    });
});
function select_country() {
    countryZone = $("#lstCountry").val();
}