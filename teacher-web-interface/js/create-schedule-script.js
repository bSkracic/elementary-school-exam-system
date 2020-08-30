"use strict";

/*<summary>
 * DATE: 26/08/2020
-> Check api handler for Exams
-> Retrieve all {exam,id,subject} objects
-> Populate a nested modal with items made out of that objects
-> Add onclick event listeners for every item; when clicked pass id value to parent modal (i have no idea how to do this)
 </summary>*/

let examMap = {};
// Dependency: teacherID from homepage-script.js 

// Fired when modal is loaded
function startNewScheduleModal() {
    $('#first').show();
    $('#second').hide();
    $('#second-submit').hide();
    retrieveExams();
}

function backModal() {
    $('#first').show();
    $('#second').hide();
    $('#second-submit').hide();
}

function retrieveExams() {
    $('#exam-container').empty();
    examMap = {};
    $.get('https://hk-iot-team-02.azurewebsites.net/api/Exams', function (data) {
        data.forEach((item) => {
            examMap['exam-' + item.ID] = item;
        });
        populateExamSelector(examMap);
    })
    console.log(examMap);
} 

function populateExamSelector(_examMap) {
    $('#exam-container').empty();
    for (const key in _examMap) {
        $('#exam-container').append(
            '<button id="exam-' + _examMap[key].ID + '" class="list-group-item">' +
            _examMap[key].Title +
            '</button>'
        );
        $('#' + key).bind('click', function () {
            $('#first').hide();
            $('#second').show();
            $('#second-submit').show();
            const itemID = $(this).attr("id");
            $('#selected-exam-name').html(_examMap[itemID].Title);
            $('#selected-exam-name').val(_examMap[key].ID);
        });
    }
}

function filterExams(event) {
    const searchTerm = $(event.target).val();
    if (searchTerm === "") {
        populateExamSelector(examMap);
        return;
    }
    let filteredExamMap = {};
    for (const key in examMap) {
        var exam = examMap[key];
        if (exam.Title.includes(searchTerm)) {
            filteredExamMap[key] = examMap[key];
        }
    }
    populateExamSelector(filteredExamMap);
}