// WARNING
// This function is using an incorrect assumption that /api/rps returns a gameId, that's not correct!!!
// This call returns a userId that isn't currently used. Don't use this function!
function JoinGame(endpoint) {
    var joinGameUrl = endpoint + "/api/rps";
    $.get(joinGameUrl, function (data, status) {
        document.getElementById("response").innerHTML = "<p>" + data + "</p>";
        window.location.href = "/Play/InGame?gameId=" + data;
    });
}