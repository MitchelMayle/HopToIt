$(document).onload(function () {

    setInterval(updateTime, 1000);

    username = $("#child_username").text();

    // gets current time stored in child table
    var seconds;
    getTime();
   
    function getTime() {
        $.ajax({
            url: getTimeURL,
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

        if (seconds >= 0) {

            $.ajax({
                url: updateTimeURL,
                type: "POST",
                data: { userName: username, secondsRemaining: seconds },
                dataType: "json"
            });

            if (seconds > 0) {
                $("#headerseconds").text(seconds + " Seconds");
                seconds--;
            }
            else {
                $("#headerseconds").text("NO TIME REMAINING");
                window.location.href = redirectURL;
            }
        }
    }
});