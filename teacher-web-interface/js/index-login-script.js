"use strict";

$(document).ready(onDocumentReady);

function onDocumentReady() {
    $('#submit').bind('click', sendLoginRequest);
}

function sendLoginRequest() {
    createSpinner();
    $.ajax({
        url: 'https://hk-iot-team-02.azurewebsites.net/api/Teachers/Login',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({
            Mail: $('#login-email').val(),
            Password: $('#login-password').val()
        }),
        success: function (data) {
            $('#wait').empty();
            $('#message-box').empty();
            console.log("Code: " + data.Code + "\nID:" + data.UserID);
            switch (data.Code) {
                case -2:
                    createAlertDiv("User does not exist.");
                    break;
                case -1:
                    createAlertDiv("Wrong password.");
                    break;
                case 0:
                    window.open("homepage.html?id=" + data.UserID, "_self");
                    break;
                default:
                    createAlertDiv("Internal error.");
                    break;
            }
        },
        error: function (data) {
            $('#wait').empty();
            console.log("Network error occured");
        },
    });
}

function createAlertDiv(message) {
    $('#message-box').empty();
    $('#message-box').append(
        '<label class="alert alert-danger" style="width: 100%; text-align: center;" role="alert">' +
        message +
        '</label >'
    );
}

function createSpinner() {
    $('#wait').append(
        '<span style="text-align: center;">Verifying...</span>'
    );
}

function logOut() {
    window.open("index.html", "_self");
}