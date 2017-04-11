/// <reference path="../jquery-3.1.1.js" />

$(document).ready(function () {

    $("#game").parents("body").on("keydown", function (e) {
        if (e.key == "ArrowRight") {
            goRight();
            checkNextStep();
        }
        else if (e.key == "ArrowUp") {
            goUp();
            checkNextStep();
            e.preventDefault();
            return false;
        }
        else if (e.key == "ArrowDown") {
            goDown();
            checkNextStep();
            e.preventDefault();
            return false;
        }
        else if (e.key == "ArrowLeft") {
            goLeft();
            checkNextStep();
        }

    });

    $("#btnRestart").on("click", restart);
});

function goRight() {
    var bunny = $(".bunny");
    if (!bunny.is(":last-child")) {
        bunny.next().addClass("bunny");
        bunny.removeClass("bunny");
    }
}

function goLeft() {
    var bunny = $(".bunny");
    if (!bunny.is(":first-child")) {
        bunny.prev().addClass("bunny");
        bunny.removeClass("bunny");
    }
}

function goUp() {
    var bunny = $(".bunny");
    var row = bunny.parent();
    if (!row.is(":first-child")) {
        var columnIndex = bunny.index();
        row.prev().children().eq(columnIndex).addClass("bunny");
        bunny.removeClass("bunny");
    }
}

function goDown() {
    var bunny = $(".bunny");
    var row = bunny.parent();
    if (!row.is(":last-child")) {
        var columnIndex = bunny.index();
        row.next().children().eq(columnIndex).addClass("bunny");
        bunny.removeClass("bunny");
    }
}

function checkNextStep() {
    var bunny = $(".bunny");
    if (bunny.hasClass("trap")) {
        $("h2").text("Game Over");
        $("h2").addClass("gameover");
        bunny.removeClass("bunny");
    }
    else if (bunny.hasClass("carrot")) {
        $("h2").text("You won 1 carrot!");
        $("h2").addClass("gamewin");
        bunny.removeClass("bunny");
        addCarrot();
    }
}

function restart() {
    $("#gameboard td:first").addClass("bunny");
    $("h2").text("Bunny Adventure").removeClass("gameover");
    $("h2").text("Bunny Adventure").removeClass("gamewin");
}

function addCarrot() {
    $.ajax({
        url: addCarrotURL,
        type: "POST",
        data: {
            username: username
        },
        dataType: "json"
    });
}