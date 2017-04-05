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
                equalTo: "Password"
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
});