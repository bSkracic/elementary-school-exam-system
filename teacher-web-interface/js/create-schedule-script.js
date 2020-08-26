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

    $('#create-schedule').bind('click', function () {
        const examID = $('#selected-exam-name').val();
        const dateStart = $('#new-start-date').val() + 'T' + $('#new-start-time').val();
        const dateEnd = $('#new-end-date').val() + 'T' + $('#new-end-time').val();
        const time = $('#new-avlbl-time').val();
        // Check if end date is before start date and vice versa
        const tempStartDate = new Date(dateStart);
        const tempEndDate = new Date(dateEnd);
        if (tempStartDate.getTime() > tempEndDate.getTime()) {
            alert("End date cannot be before start date!");
            return;
        }
        // Construct POST method body
        var body = {
            TeacherID: teacherID,
            ExamID: examID,
            DatetimeStart: dateStart,
            DatetimeEnd: dateEnd,
            AvailableTime: time
        };

        schedulePOST(body);
    });
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

function filterExams() {
    const searchTerm = $(this).val();
    let filteredExamMap = {};
    for (const key in examMap) {
        var exam = examMap[key];
        if (exam.Title.contains(searchTerm)) {
            filteredExamMap[key] = examMap[key];
        }
    }
    populateExamSelector(filterExams);
}