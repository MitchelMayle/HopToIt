$(document).ready(function () {

    // sends update request to database every second
    setInterval(updateTime, 1000);

    // pulls username from hidden field in header
    username = $("#child_username").text();

    // gets current time stored in child table
    getTime();
    var seconds = 0;

    function getTime() {
        $.ajax({
            url: "api/getTime",
            type: "GET",
            data: {
                username: username
            },
            dataType: "json"
        }).done(function (data) {
            console.log(data);
            seconds = parseInt(data);
        });
        
    }

    // posts new remaining time
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