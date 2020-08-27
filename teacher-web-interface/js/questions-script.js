"use strict";
$(document).ready(onDocumentReady);

let questions = {};

function onDocumentReady() {
    const url = window.location.href;
    var examID = url.split('?')[1].split('=')[1];

    getQstnIDs(examID);
}

function getQstnIDs(_examID) {
    $.get('https://hk-iot-team-02.azurewebsites.net/api/Exam_Question/' + _examID, function (data) {
        var body = {
            // Check if IDs is the key
            values: data.IDs
        }
        getQuestions(body);
        console.log(body);
    });
}

function getQuestions(_body) {
    $.ajax({
        url: 'https://hk-iot-team-02.azurewebsites.net/api/Questions/request-questions-for-exam',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(_body),
        success: function (data) {
            console.log(data);
        },
        error: function (data) {
            alert("Bad request") // DEBUG, delete for all instances 
        }
    });
}