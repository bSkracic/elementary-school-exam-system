"use strict";

$(document).ready(onDocumentReady);

let teacherID;
let classID;
let schedules;
let subjects;
let allExams;

// Not smart at all
let scheduleID;
let examID;

class DateTimeFactory {
    constructor(datetimeString) {
        const date = new Date(datetimeString);

        let month;
        let day;

        if ((date.getMonth() + 1).toString().length == 1) {
            month = "0" + (date.getMonth() + 1).toString();
        } else {
            month = (date.getMonth() + 1).toString();
        }
        if (date.getDate().toString().length == 1) {
            day = "0" + date.getDate().toString();;
        } else {
            day = date.getDate().toString();
        }   
        this.date = + date.getFullYear() + "-" + month + "-" + day; 

        let hours;
        let minutes;
        let seconds;

        if (date.getHours().toString().length == 1) {
            hours = "0" + date.getHours().toString();
        } else {
            hours = date.getHours().toString();
        }
        if (date.getMinutes().toString().length == 1) {
            minutes = "0" + date.getMinutes().toString();
        } else {
            minutes = date.getMinutes().toString();
        }
        if (date.getSeconds().toString().length == 1) {
            seconds = "0" + date.getSeconds().toString();
        } else {
            seconds = date.getSeconds().toString();
        }

        this.time = hours + ":" + minutes + ":" + seconds;
    }
}

/*
* Start
*/
function onDocumentReady() {
    // Reset global properties
    teacherID = -1;
    classID = -1;
    schedules = {};
 
    // Retrieve teacher's id from href
    const url = window.location.href;
    teacherID = url.split('?')[1].split('=')[1];

    // Populate containers
    populateDetails();
    retrieveSchedules();
    populateSubjectFilter();

    $('#new-schedule-modal').bind('show.bs.modal', function () {
        startNewScheduleModal();
    });

    $('#new-exam-modal').bind('show.bs.modal', function () {
        startNewExamModal();
    });
}

/*
* Container population
*/
function populateDetails() {
    $.get('https://hk-iot-team-02.azurewebsites.net/api/Teachers/' + teacherID, function (data) {
        $('#professor-name').html(data.Name + ' ' + data.Surname + ' teaches ' + data.Class);
    });
}

function retrieveSchedules() {
    $('#all-exams').hide();
    $('#scheduled-exams').show();

    $.get('https://hk-iot-team-02.azurewebsites.net/api/Teacher_Exam/' + teacherID, function (data) {
        schedules = {};
        data.forEach((schedule) => {
            schedules[schedule.ID] = schedule;
        });
        console.log(schedules); // DEBUG
        populateScheduledExams(schedules);
    });
}

function retrieveAllExams() {
    $('#all-exams').show();
    $('#scheduled-exams').hide();
    $.get('https://hk-iot-team-02.azurewebsites.net/api/Exams', function (data) {
        allExams = {};
        data.forEach((item) => {
            allExams[item.ID] = item;
        });
        populateAllExams(allExams);
    });
}

function populateScheduledExams(_schedules) {
    $('#scheduled-exams').empty();
    // Set spinner
    $('#wait-spinner').append('<span class="sr-only">Loading...</span>');
    const currentTime = new Date().getTime();
    for (const key in _schedules) {
        const schedule = _schedules[key];
        // Check active status
        const dateStart = new Date(schedule.DatetimeStart);
        const dateEnd = new Date(schedule.DatetimeEnd);
        let activeStatus = "READY";
        if (currentTime > dateStart.getTime() && currentTime < dateEnd.getTime()) {
            activeStatus = "ACTIVE";
        } else if (currentTime > dateEnd.getTime()) {
            activeStatus = "FINISHED";
        }

        const examItem = createListItem(schedule, activeStatus);
        $('#scheduled-exams').append(examItem);
        //Very sketchy, listener should be added in html element, not via ID selector
        $('#' + schedule.ID).bind('click', function () {
            startEditScheduleModal(schedule.ID);
        });
    }
    // Remove spinner
    $('#wait-spinner').empty();
}

function populateAllExams(_allExams) {
    // This does not work
    $('#all-exams-container').empty();
    // Set spinner
    $('#wait-spinner').append('<span class="sr-only">Loading...</span>');
    for (const key in _allExams) {
        const exam = _allExams[key];
        $('#all-exams-container').append(
            '<button class="card list-group-item d-flex justify-content-between align-items-center" data-toggle="modal" style="width: 20%; display: inline;"> ' +
                '<div class="d-flex w-100 justify-content-between">' +
                    '<h3 class="mb-2 h4">' + exam.Title + '</h3>' +
                '</div >' +
            '<small>' + 'Subject: ' + exam.Subject + '</small>' +
                '<br/>' +
                '<a class="nav-link" style="color: darkgreen;" data-toggle="modal" data-target="#edit-exam-modal" value="' + exam.ID +'" onclick="startEditExamModal(event)">EDIT</a>' +
            '</button >'
        );
    }
    // Remove spinner
    $('#wait-spinner').empty();
}

function populateSubjectFilter() {
    $.get('https://hk-iot-team-02.azurewebsites.net/api/Subjects', function (data) {
        subjects = {};
        data.forEach((item) => {
            $('#subject-filter').append(
                '<li class="nav-item">' +
                    '<a id="subject-'+ item.ID + '" class="nav-link" style="color: darkgreen;">' +
                        item.Name +
                    '</a>' +
                '</li>');
            subjects[item.ID] = item.Name;
            // Filter exams and schedules by selected subject 
            $('#subject-' + item.ID).bind('click', function () {
                $('#scheduled-exams-container').empty();
                $('#all-exams-container').empty();
                const subjectID = item.ID;
                console.log(item.Name);
                const currentTime = new Date().getTime();
                let filterSchedules = {};
                for (const key in schedules) {
                    const schedule = schedules[key];
                    if (schedule.SubjectID === subjectID) {
                        filterSchedules[key] = schedule;
                    }
                }
                let filterExams = {};
                for (const key in allExams) {
                    const exam = allExams[key];
                    if (exam.SubjectID === subjectID) {
                        filterExams[key] = exam;
                    }
                }
                populateScheduledExams(filterSchedules);
                populateAllExams(filterExams);
            });
        });
    })
}

function createListItem(exam, status) {
    var color = 'orange';
    if (status == 'FINISHED') {
        color = 'lightblue';
    } else if (status == 'ACTIVE') {
        color = 'lightgreen'
    }
    
    const startDate = new DateTimeFactory(exam.DatetimeStart);
    const endDate = new DateTimeFactory(exam.DatetimeEnd);
    //this is absolutetly terrible part of code
    var examItem =
        '<button id="' + exam.ID + '" class="card list-group-item d-flex justify-content-between align-items-center" data-toggle="modal" data-target="#edit-schedule-modal" style="width: 20%; display: inline;"> ' +
            '<div class="d-flex w-100 justify-content-between">'+
            '<h3 class="mb-2 h4">' + exam.ExamTitle +
                '<span class="badge badge-secondary badge-pill">' + status + ' </span>' +
            '</h3>' +
            '</div >' +
            '<p class="mb-2">' +
                'Start Date | Time: ' + '<br/>' + startDate.date.fontcolor(color) + ' | '.fontcolor(color) + startDate.time.fontcolor(color) + '<br/>' +
                'End Date | Time: ' + '<br/>' + endDate.date.fontcolor(color) + ' | '.fontcolor(color) + endDate.time.fontcolor(color) + '<br/>' +
                'Available Time: ' + '<br/>' + parseFloat(exam.AvailableTime).toFixed(0) + " min" + 
            '</p >' +
            '<small>' + 'Subject: ' + exam.Subject + '</small>' +
        '</button>';
    return examItem;
}

/*
* Modal handelers
*/
function startEditScheduleModal(id) {
    scheduleID = id;
    var selectedExam = schedules[id];
    var startDate = new DateTimeFactory(selectedExam.DatetimeStart);
    var endDate = new DateTimeFactory(selectedExam.DatetimeEnd);
    $('#edit-start-date').val(startDate.date);
    $('#edit-start-time').val(startDate.time);
    $('#edit-end-date').val(endDate.date);
    $('#edit-end-time').val(endDate.time);
    $('#edit-avlbl-time').val(parseFloat(selectedExam.AvailableTime).toFixed(0));
}

function startEditExamModal(event) {
    const id = $(event.target).attr('value');
    console.log(id);
    examID = id;
    const exam = allExams[id];
    $('#edit-exam-modal').ready(function () {
        // Populate modal
        $('#edit-exam-title').val(exam.Title);

        $('#select-subject').empty();
        for (const key in subjects) {
            $('#select-subject').append(
                '<option value="' + key + '">' + subjects[key] + '</option>');
        }

        $('#select-subject').val(exam.SubjectID);

        $('#edit-questions').bind('click', function () {
            window.open("questions.html?id=" + exam.ID, "_self");
        });

    });
    console.log(exam.Title);
}

function startNewExamModal() {

    $('#n-select-subject').empty();

    for (const key in subjects) {
        $('#n-select-subject').append(
            '<option value="' + key + '">' + subjects[key] + '</option>');
    }

    $('#submit-create-exam').bind('click', function () {
        
    });
}

//-- AJAX METHODS
/*
* Schedule AJAX methods 
*/
function schedulePUT() {
    const dateStart = $('#edit-start-date').val() + 'T' + $('#edit-start-time').val();
    const dateEnd = $('#edit-end-date').val() + 'T' + $('#edit-end-time').val();
    //check if end date is set before start date
    const tempDateStart = new Date(dateStart);
    const tempDateEnd = new Date(dateEnd);
    const currentTime = new Date().getTime();
    if (tempDateStart.getTime() < currentTime || tempDateEnd.getTime() < currentTime) {
        alert("Cannot schedule exams in the past!");
        return;
    }
    if (tempDateStart.getTime() > tempDateEnd.getTime()) {
        alert("End date cannot be before start date!");
        return;
    }

    var body = {
        ID: Number(scheduleID),
        TeacherID: Number(teacherID),
        ExamID: 0,
        DatetimeStart: dateStart,
        DatetimeEnd: dateEnd,
        AvailableTime: Number($('#edit-avlbl-time').val())
    };
    console.log(body); // DEBUG

    $.ajax({
        url: 'https://hk-iot-team-02.azurewebsites.net/api/Teacher_Exam/' + scheduleID,
        type: 'PUT',
        contentType: 'application/json',
        data: JSON.stringify(body),
        success: function () {
            retrieveSchedules();
        },
        error: function () {
            alert("Bad request") // DEBUG, delete for all instances 
        }
    });
}

function scheduleDELETE() {

    // bring back the delete

    $.ajax({
        url: 'https://hk-iot-team-02.azurewebsites.net/api/Teacher_Exam/' + scheduleID,
        type: 'DELETE',
        success: function () {
            retrieveSchedules();
        },
        error: function () {
            alert("Bad request!");
        }
    });
}

function schedulePOST(schedule) {

    const examID = $('#selected-exam-name').val();
    const dateStart = $('#new-start-date').val() + 'T' + $('#new-start-time').val();
    const dateEnd = $('#new-end-date').val() + 'T' + $('#new-end-time').val();
    const time = $('#new-avlbl-time').val();
    // Check if end date is before start date and vice versa
    const tempStartDate = new Date(dateStart);
    const tempEndDate = new Date(dateEnd);
    const currentTime = new Date().getTime();
    if (tempStartDate.getTime() < currentTime || tempEndDate.getTime() < currentTime) {
        alert("Cannot schedule exams in the past!");
        return;
    }
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

    console.log(body);

    $.ajax({
        url: 'https://hk-iot-team-02.azurewebsites.net/api/Teacher_Exam/',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(body),
        success: function () {
            retrieveSchedules();
        },
        error: function () {
            alert("Bad request!");
        }
    });
}
/*
* Exam AJAX methods
*/
function examPUT() {

    var body = {
        ID: Number(examID),
        Title: $('#edit-exam-title').val(),
        SubjectID: Number($('#select-subject').val()),
        Subject: "none"
    };

    console.log(body);

    $.ajax({
        url: 'https://hk-iot-team-02.azurewebsites.net/api/Exams/' + examID,
        type: 'PUT',
        contentType: 'application/json',
        data: JSON.stringify(body),
        success: function (data) {
            retrieveAllExams();
        },
        error: function (data) {
            alert("Bad request") 
        }
    });
}

function examPOST() {

    var body = {
        ID: 0,
        Title: $('#new-exam-title').val(),
        SubjectID: Number($('#n-select-subject').val()),
        Subject: "none"
    };

    console.log(body);

    $.ajax({
        url: 'https://hk-iot-team-02.azurewebsites.net/api/Exams/',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(body),
        success: function (data) {
            retrieveAllExams();
        },
        error: function (data) {
            alert("Bad request") 
        }
    });
}
