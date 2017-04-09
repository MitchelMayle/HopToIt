/// <reference path="Scripts/jquery-3.1.1.js" />
/// <reference path="Scripts/jquery.validate.js" />

$(document).ready(function () {

    $("#form").validate({

        rules: {
            First_Name: {
                required: true,
            },
            Last_Name: {
                required: true,
            },
            Email: {
                required: true,
            },
            UserName: {
                required: true,
            },
            User_Name: {
                required: true,
            },
            Password: {
                required: true,
            },
            ConfirmPassword: {
                equalTo: "#Password",
            },
        },

        messages: {
            First_Name: {
                required: "Please enter a first name"
            },
            Last_Name: {
                required: "Please enter a last name"
            },
            Email: {
                required: "Please enter an email address"
            },
            UserName: {
                required: "Please enter a username"
            },
            User_Name: {
                required: "Please enter a username"
            },
            Password: {
                required: "Please enter a password"
            },
            ConfirmPassword: {
                equalTo: "Passwords must match"
            },
        },

        errorClass: "error",
        validClass: "valid"
    });

    //Still need to input the user data into the message

    $("#Steps").on("blur",function (event) {
        $("#stepsMessage").html("You have entered " + this.value + ".  This will give your child " + Math.round((this.value/10)/60) + " minutes of play time on Hop To It")
    });

    $("#Minutes").on("blur", function (event) {
        $("#minutesMessage").html("You have entered " + this.value + ".  This will give your child " + this.value + " carrots to use as currency in Hop To It")
    });

    //Need to check that the date entered is not after current date
    //$("#inputDate").validate({
    //    maxDate: Date.now,
    //})
});