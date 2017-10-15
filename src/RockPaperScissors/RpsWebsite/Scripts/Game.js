// Game Logic in Javascript
// Methods that are called by the Play/Index.cshtml or via the StatusPoll state machine.
// author: Skyler

var inGame = true
var lastStatus = ""
var myMove = ""

var playerWins
var playerLosses
var playerDraws

var statusPollId = -1
var pollInterval = 1000

// Initialize the GameState
function Init(endpoint, playerId, wins, losses, draws) {
    playerWins = wins;
    playerLosses = losses;
    playerDraws = draws;

    
    NewGame(endpoint, playerId);
    StartStatusPoll(endpoint, playerId);
    Record(wins, losses, draws);

    InitButtons(endpoint, playerId);
    DisableButtons();

    Debug("Started v0.4. Endpoint: " + endpoint + " PlayerId: " + playerId);
}

// Start the asynchronous poll to the server
function StartStatusPoll(endpoint, playerId) {
    var statusUrl = endpoint + "/api/rps/status/" + playerId;

    // If a poll already exists, stop it first
    StopStatusPoll();

    statusPollId = setInterval(
        function () {
            $.get(statusUrl, StatusCallback);
        },
        pollInterval
    );
}

// Stop the asynchronous poll to the server
function StopStatusPoll()
{
    if (statusPollId !== -1) {
        clearInterval(statusPollId);
        statusPollId = -1;
    }
}

// The main heartbeat of the game state.
// Polls the server for changes and takes actions.
function StatusCallback(data, status) {

    if (lastStatus !== data) {
        lastStatus = data;

        switch (data) {
            case "Waiting-Alone":
                Status("Waiting for Opponent...");
                break;

            case "Waiting-BothMoves":
                EnableButtons();
                Status("Make your move!");
                break;

            case "Waiting-OpponentMove":
                EnableButtons();
                Status("You chose " + myMove + ". Waiting for opponent.");
                break;

            case "Waiting-PlayerMove":
                EnableButtons();
                Status("Opponent is waiting for you!");
                break;

            case "GameDone-Winner":
                $.post('/play/result/329847');
                NewGameButton();
                StopStatusPoll();
                Record(playerWins + 1, playerLosses, playerDraws);
                Status2("You chose " + myMove + ". Opponent chose " + MoveThatLoses(myMove) + ".", "You win!");
                break;

            case "GameDone-Loser":
                $.post('/play/result/87321');
                NewGameButton();
                StopStatusPoll();
                Record(playerWins, playerLosses + 1, playerDraws);
                Status2("You chose " + myMove + ". Opponent chose " + MoveThatBeats(myMove) + ".", "You lose!");
                break;

            case "GameDone-Draw":
                $.post('/play/result/6719422');
                NewGameButton();
                StopStatusPoll();
                Record(playerWins, playerLosses, playerDraws + 1);
                Status2("You chose " + myMove + ". Opponent chose " + myMove + ".", "Draw!");
                break;
        }
    }
}

// Join a new game
function NewGame(endpoint, playerId) {
    var newGameUrl = endpoint + "/api/rps/" + playerId;
    $.get(newGameUrl, function (data, status) { });
}

// Post your move to the server
var validMoves = { "Rock":true, "Paper":true, "Scissors":true };
function PostMove(endpoint, playerId, move) {
    if (!inGame) {
        return;
    }

    Debug("Posting move " + move);
    if (!validMoves[move]) {
        Debug("Invalid move attempted! '" + move + "'");
        return;
    }

    myMove = move;
    DisableButtons();

    var moveUrl = endpoint + "/api/rps/" + playerId + "/" + move;
    $.post(moveUrl);
}

// Gross! What hacks! This info should be sent back from the server.
function MoveThatBeats(move) {
    switch (move) {
        case "Rock":
            return "Paper";
        case "Paper":
            return "Scissors";
        case "Scissors":
            return "Rock";
    }
}

// Gross! What hacks! This info should be sent back from the server.
function MoveThatLoses(move) {
    switch (move) {
        case "Rock":
            return "Scissors";
        case "Paper":
            return "Rock";
        case "Scissors":
            return "Paper";
    }
}

// Initialize the buttons
function InitButtons(endpoint, playerId) {
    document.getElementById("rockButton").onclick = function () { PostMove(endpoint, playerId, "Rock") };
    document.getElementById("paperButton").onclick = function () { PostMove(endpoint, playerId, "Paper") };
    document.getElementById("scissorsButton").onclick = function () { PostMove(endpoint, playerId, "Scissors") };
}

// Enable the buttons
function EnableButtons() {
    inGame = true;

    var rockButton = document.getElementById("rockButton");
    rockButton.disabled = false;
    rockButton.className = "btn btn-primary btn-lg";

    var paperButton = document.getElementById("paperButton")
    paperButton.disabled = false;
    paperButton.className = "btn btn-primary btn-lg";

    var scissorsButton = document.getElementById("scissorsButton");
    scissorsButton.disabled = false;
    scissorsButton.className = "btn btn-primary btn-lg";
}

// Disable the buttons
function DisableButtons() {
    inGame = false;

    var rockButton = document.getElementById("rockButton");
    rockButton.disabled = true;
    rockButton.className = "btn btn-primary btn-lg disabled";

    var paperButton = document.getElementById("paperButton")
    paperButton.disabled = true;
    paperButton.className = "btn btn-primary btn-lg disabled";

    var scissorsButton = document.getElementById("scissorsButton");
    scissorsButton.disabled = true;
    scissorsButton.className = "btn btn-primary btn-lg disabled";
}

// Create the 'New Game' button (overwrites the Rock Paper Scissors buttons- this could be a new page eventually)
function NewGameButton() {
    document.getElementById("controls").innerHTML = "<input class=\"btn btn-primary btn-lg btn-block\" type=\"button\" value=\"New Game\" onclick=\"location.reload()\" />";
}

// Update the Record message
function Record(wins, losses, draws) {
    document.getElementById("recordcontent").innerHTML = "[Your Record] Win: " + wins + " Draw: " + draws + " Loss: " + losses;
}

// Update the status message
function Status(message) {
    document.getElementById("status").innerHTML = "<h2>" + message + "</h2>";
}

// Update the status with 2 messages
function Status2(message1, message2) {
    document.getElementById("status").innerHTML = "<h2>" + message1 + "</h2>" + "<h2>" + message2 + "</h2>";
}

// Update the debug message
function Debug(message) {
    document.getElementById("debug").innerHTML = message;
}