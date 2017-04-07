$(document).ready(function () {

    // sends update request to database every 3 seconds
    setInterval(updateTime, 1000);

    var seconds = 999;
    username = $("#child_username").text();

    function updateTime() {
        $.ajax({
            url: "api/updateTime",
            type: "POST",
            data: { username: username, secondsRemaining: seconds-- },
            dataType: "json"
        });

        $("#headerseconds").text(seconds + " Seconds");
        console.log("UPDATED");
        
    }

});