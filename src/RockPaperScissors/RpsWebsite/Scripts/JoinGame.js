function joinGame(endpoint) {
    var joinGameUrl = endpoint + "/api/rps";
    $.get(joinGameUrl, function (data, status) {
        document.getElementById("response").innerHTML = "<p>" + data + "</p>";
        window.location.href = "/Play/InGame?gameId=" + data;
    });
}