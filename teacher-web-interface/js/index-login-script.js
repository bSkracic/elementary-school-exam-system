"use strict";
$(document).ready(onDocumentReady);

// TODO: Set spinner when waiting for server response -- does not work, try everything

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
            console.log("Code: " + data.Code + "\nID:" + data.TeacherID);
            switch (data.Code) {
                case -2:
                    createAlertDiv("User does not exists");
                    break;
                case -1:
                    createAlertDiv("Wrong password");
                    break;
                default:
                    window.open("homepage.html?id=" + data.TeacherID, "_self");
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
        '<div class="alert alert-danger" role="alert">' +
        message +
        '</div >'
    );
}

function createSpinner() {
    $('#wait').append(
        '<span class="s">Loading...</span>'
    );
}