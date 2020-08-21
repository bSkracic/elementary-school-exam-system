"use strict"
$(document).ready(onDocumentReady);

function onDocumentReady() {
    $('#submit').bind('click', sendLoginRequest);
}

function sendLoginRequest() {
    $.ajax({
        url: 'https://hk-iot-team-02.azurewebsites.net/api/Teachers/Login',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({
            Mail: $('#login-email').val(),
            Password: $('#login-password').val()
        }),
        success: function (data) {
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