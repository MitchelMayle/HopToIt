$(document).ready(function () {

    // sends update request to database every second
    setInterval(updateTime, 1000);

    // pulls username from hidden field in header
    username = $("#child_username").text();

    // gets current time stored in child table
    var seconds = 0;
    getTime();
   
    function getTime() {
        $.ajax({
            url: "api/getTime",
            type: "GET",
            data: {
                username: username
            },
            dataType: "json"
        }).done(function (data) {
            seconds = parseInt(data);
        });
        
    }

    // posts new remaining time
    function updateTime() {

        if (seconds > 0) {

            $.ajax({
                url: "api/updateTime",
                type: "POST",
                data: { username: username, secondsRemaining: seconds },
                dataType: "json"
            });   
            $("#headerseconds").text(seconds + " Seconds");
            seconds--;
        }
        else if (seconds == 0) {

            $.ajax({
                url: "api/updateTime",
                type: "POST",
                data: { username: username, secondsRemaining: seconds },
                dataType: "json"
            });
            $("#headerseconds").text("NO TIME REMAINING");

            window.location.href = 'http://localhost:55601/OutOfTime';

        }
    }

});