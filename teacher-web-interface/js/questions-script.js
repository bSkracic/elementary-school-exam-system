"use strict";

$(document).ready(onDocumentReady);

/*
* Global properties 
*/
let questions = {};
let examID = 0;

/*
* Start
*/
function onDocumentReady() {
    const url = window.location.href;
    examID = url.split('?')[1].split('=')[1];

    getQuestions();

    $('#new-question-modal').bind('show.bs.modal', function () {
        startNewQstnModal();
    });

}

function getQuestions() {
    $.get('https://hk-iot-team-02.azurewebsites.net/api/Exam_Question/' + examID, function (data) {
        questions = {};
        data.forEach((question) => {
            questions[question.ID] = question;
        });
        populateQuestionContainer();
    });

}

/*
* Container population
*/
function populateQuestionContainer() {
    let i = 1;
    $('#question-container').empty();
    for (const key in questions) {
        const question = questions[key];
        var isFreeAnswer = 'Yes';
        if (!question.FreeAnswerType) {
            isFreeAnswer = 'No';
        }
        const color = 'green';

        let correctAnswer = "";
        let optionA = "";
        let optionB = "";
        let optionC = "";
        let optionD = "";

        if (!question.FreeAnswerType) {
            correctAnswer = question.CorrectAnswer.split('{')[1].split('}')[0];
            optionA = question.A;
            optionB = question.B;
            optionC = question.C;
            optionD = question.D;
        } else {
            correctAnswer = question.CorrectAnswer;
        }

        $('#question-container').append(
            '<button id="' + question.ID + '"' + 'class="panel panel-default" style="width: 100%;" data-toggle="modal" data-target="#edit-question-modal">' +
            '<div class="panel-heading">Question: ' + i + '</div>' +
            '<div class="panel-body" style="text-align: left;">' +
                    '<div class="panel-body">' + question.Text + '</div>' +
                    'Free Answer:'.fontcolor(color) + isFreeAnswer + '</br>' +
                    'A: '.fontcolor(color) + optionA + '<br />' +
                    'B: '.fontcolor(color) + optionB + '<br />' +
                    'C: '.fontcolor(color) + optionC + '<br />' +
                    'D: '.fontcolor(color) + optionD + '<br />' +
                    'Correct Answer: '.fontcolor(color) + correctAnswer.toString() + '<br />' +
                    'Max Points: '.fontcolor(color) + question.MaxPoints + '<br />' +
               '</div>' +
            '</button>'
        );
        $('#' + question.ID).bind('click', function () {
            startEditQstnModal(question.ID, i);
        });
        i++;
    }

    // Add plus button
    $('#question-container').append(
        '<button class="panel panel-default" style="width: 100%; height: 5%; background-color: green; color: white" data-toggle="modal" data-target="#new-question-modal">' +
           '<span class="glyphicon glyphicon-plus"></span>' +
        '</button>'
    );

}

/*
* Modal handelers
*/
function startEditQstnModal(_id, _index) {
    const question = questions[_id];
    // Filter correct answer if it is not free type and make it true/false
    $('#edit-question-modal').bind('show.bs.modal', function () {
        $('#question-id').html(_id);
        $('#question-index').html('Question: ' + _index);
        $('#question-text').val(question.Text);
        $('#free-answer').prop('checked', question.FreeAnswerType);
        $('#max-points').val(question.MaxPoints);
       
        if (!question.FreeAnswerType) {
            const crrctAnswArray = question.CorrectAnswer.split('{')[1].split('}')[0].split(',');
            $('#option-A').prop('disabled', false);
            $('#option-B').prop('disabled', false);
            $('#option-C').prop('disabled', false);
            $('#option-D').prop('disabled', false);
            $('#option-text-A').prop('disabled', false);
            $('#option-text-B').prop('disabled', false);
            $('#option-text-C').prop('disabled', false);
            $('#option-text-D').prop('disabled', false);

            $('#option-A').prop('checked', false);
            $('#option-B').prop('checked', false);
            $('#option-C').prop('checked', false);
            $('#option-D').prop('checked', false);

            crrctAnswArray.forEach((item) => {
                switch (item) {
                    case "A":
                        $('#option-A').prop('checked', true);
                        break;
                    case "B":
                        $('#option-B').prop('checked', true);
                        break;
                    case "C":
                        $('#option-C').prop('checked', true);
                        break;
                    case "D":
                        $('#option-D').prop('checked', true);
                        break;
                }
            });

            $('#option-text-A').val(question.A);
            $('#option-text-B').val(question.B);
            $('#option-text-C').val(question.C);
            $('#option-text-D').val(question.D);
            $('#correct-answer').val("");
            $('#correct-answer').prop('disabled', true);
        }
        else {
            $('#option-text-A').val();
            $('#option-text-B').val();
            $('#option-text-C').val();
            $('#option-text-D').val();
            $('#option-A').prop('disabled', true);
            $('#option-B').prop('disabled', true);
            $('#option-C').prop('disabled', true);
            $('#option-D').prop('disabled', true);
            $('#option-text-A').prop('disabled', true);
            $('#option-text-B').prop('disabled', true);
            $('#option-text-C').prop('disabled', true);
            $('#option-text-D').prop('disabled', true);
            //populate
            $('#correct-answer').prop('disabled', false);
            $('#correct-answer').val(question.CorrectAnswer);
        }

    });

}

function toggleFreeAnswerInputs() {
    $('#option-A').prop('disabled', function (index, value) { return !value; });
    $('#option-B').prop('disabled', function (index, value) { return !value; });
    $('#option-C').prop('disabled', function (index, value) { return !value; });
    $('#option-D').prop('disabled', function (index, value) { return !value; });
    $('#option-text-A').prop('disabled', function (index, value) { return !value; });
    $('#option-text-B').prop('disabled', function (index, value) { return !value; });
    $('#option-text-C').prop('disabled', function (index, value) { return !value; });
    $('#option-text-D').prop('disabled', function (index, value) { return !value; });
    $('#correct-answer').prop('disabled', function (index, value) { return !value; });
    $('#new-option-A').prop('disabled', function (index, value) { return !value; });
    $('#new-option-B').prop('disabled', function (index, value) { return !value; });
    $('#new-option-C').prop('disabled', function (index, value) { return !value; });
    $('#new-option-D').prop('disabled', function (index, value) { return !value; });
    $('#new-option-A-text').prop('disabled', function (index, value) { return !value; });
    $('#new-option-B-text').prop('disabled', function (index, value) { return !value; });
    $('#new-option-C-text').prop('disabled', function (index, value) { return !value; });
    $('#new-option-D-text').prop('disabled', function (index, value) { return !value; });
    $('#new-correct-answer').prop('disabled', function (index, value) { return !value; });
}

function startNewQstnModal() {
    $('#new-max-points').val(1);
    $('#new-correct-answer').prop('disabled', true);
    $('#new-option-A').prop('checked', false);
    $('#new-option-B').prop('checked', false);
    $('#new-option-C').prop('checked', false);
    $('#new-option-D').prop('checked', false);
}

function closeModal() {
    $('#edit-question-modal').modal('toggle');
}

/*
* Question AJAX methods
*/
function questionPUT() {
    const questionID = Number($('#question-id').html());

    let optionA = null;
    let optionB = null;
    let optionC = null;
    let optionD = null;
    let correctAnswer = "";

    if ($('#max-points').val() || $('#max-points').val() < 1) {
        alert("Max points must be at least 1!");
    }

    if (!$('#free-answer').prop('checked')) {
        // Check if any answer is selected

        const check = !$('#option-A').prop('checked') && !$('#option-B').prop('checked')
            && !$('#option-C').prop('checked') && !$('#option-D').prop('checked');

        if (check) {
            alert('You need to select at least one correct answer!');
            return;
        }
        // Construct correct answer body
        const options = ['A', 'B', 'C', 'D'];
        let result = [];
        for (let i in options) {
            if ($('#option-' + options[i]).prop('checked')) {
                result.push(options[i]);;
            }
        }
        correctAnswer = "{" + result.toString() + "}";
        optionA = $('#option-text-A').val();
        optionB = $('#option-text-B').val();
        optionC = $('#option-text-C').val();
        optionD = $('#option-text-D').val();
    } else {
        correctAnswer = $('#correct-answer').val();
    }

    var body = {
        ID: questionID,
        Text: $('#question-text').val(),
        FreeAnswerType: $('#free-answer').prop('checked'),
        A: optionA,
        B: optionB,
        C: optionC,
        D: optionD,
        CorrectAnswer: correctAnswer,
        MaxPoints: Number($('#max-points').val())
    };

    // Send PUT request
    $.ajax({
        url: 'https://hk-iot-team-02.azurewebsites.net/api/Questions/' + questionID,
        type: 'PUT',
        contentType: 'application/json',
        data: JSON.stringify(body),
        success: function () {
            getQuestions();
        },
        error: function () {
            alert("Bad request!");
        }
    });
}

function questionPOST() {

    let optionA = null;
    let optionB = null;
    let optionC = null;
    let optionD = null;
    let correctAnswer = "";

    if ($('#new-max-points').val() || $('#new-max-points').val() < 1) {
        alert("Max points must be at least 1!");
    }

    if (!$('#new-free-answer').prop('checked')) {
        // Check if any answer is selected

        const check = !$('#new-option-A').prop('checked') && !$('#new-option-B').prop('checked')
            && !$('#new-option-C').prop('checked') && !$('#new-option-D').prop('checked');

        if (check) {
            alert('You need to select at least one correct answer!');
            return;
        }
        // Construct correct answer body
        const options = ['A', 'B', 'C', 'D'];
        let result = [];
        for (let i in options) {
            if ($('#new-option-' + options[i]).prop('checked')) {
                result.push(options[i]);;
            }
        }
        correctAnswer = "{" + result.toString() + "}";
        optionA = $('#new-option-text-A').val();
        optionB = $('#new-option-text-B').val();
        optionC = $('#new-option-text-C').val();
        optionD = $('#new-option-text-D').val();
    } else {
        correctAnswer = $('#new-correct-answer').val();
    }

    var body = {
        ID: 0,
        Text: $('#new-question-text').val(),
        FreeAnswerType: $('#new-free-answer').prop('checked'),
        A: optionA,
        B: optionB,
        C: optionC,
        D: optionD,
        CorrectAnswer: correctAnswer,
        MaxPoints: Number($('#new-max-points').val())
    };

    console.log(body);

    // Send POST request
    $.ajax({
        url: 'https://hk-iot-team-02.azurewebsites.net/api/Questions',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(body),
        success: function (data) {
            relationPOST(data.ID);
        },
        error: function () {
            alert("Bad request!");
        }
    }); 
}

function relationPOST(_questionID) {

    var body = {
        ID: 0,
        ExamID: Number(examID),
        QuestionID: Number(_questionID),
        QuestionNumber: 0 
    };

    $.post("https://hk-iot-team-02.azurewebsites.net/api/Exam_Question", body)
        .done(function () {
            getQuestions();
        });
}

function questionDELETE() {
    console.log('DELETE');
    const questionID = Number($('#question-id').html());
    $.ajax({
        url: 'https://hk-iot-team-02.azurewebsites.net/api/Questions/' + questionID,
        type: 'DELETE',
        success: function () {
            getQuestions();
        },
        error: function () {
            alert("Bad request!");
        }
    });
}