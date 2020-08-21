"use strict"
$(document).ready(onDocumentReady);

let teacherID;
let classID;
let exams;

// TODO: Write handler for date and time, parse date and time and display it separately in container -- DONE
// TODO: POST, DELETE, PUT request to create scheduled exam; Send confirmation dialog when deleting
// TODO: When POSTing, check if exam to be scheduled is already scheduled: warn user
// TODO: Create system for creating exams with questions (long task)
// TODO: Create login attempt on page refresh after 10 min of open session
// ERROR: Listener of list-item object does not pass id to modal -- FIXED
// ERROR: Font color highlight (exam status) does not work properly(check conditioning)

class DateTimeFactory {
    constructor(datetimeString) {
        const date = new Date(datetimeString);
        this.date = + date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + date.getDate(); 
        this.time = date.getHours() + ":" + date.getMinutes() + ":" + date.getSeconds();
    }
    // <summary>
    // Join date and time into string
    // </summary>
    static joinToString(date, time) {
        return date + 'T' + time;    
    }
}

// Depricated
/*
class Exam {
    constructor(jsonString) {
        this.id = jsonString.ID;
        this.title = jsonString.ExamTitle;
        this.subject = jsonString.Subject;
        this.time = jsonString.AvailableTime;
        this.dateTimeStart = new Date(jsonString.DatetimeStart);
        this.dateTimeEnd = new Date(jsonString.DatetimeEnd);
        this.active = true;
    }
}*/

// START 
function onDocumentReady() {
    // Reset global properties
    teacherID = -1;
    classID = -1;
    exams = {};
    // Retrieve teacher's id from href
    const url = window.location.href;
    teacherID = url.split('?')[1].split('=')[1];
    // Populate containers
    populateDetails();
    populateScheduledExams();
}

function populateDetails() {
    $.get('https://hk-iot-team-02.azurewebsites.net/api/Teachers/' + teacherID, function (data) {
        const dataList = [data.Name, data.Surname, data.Class, teacherID];
        let i = 0;
        $('#professor-details').children('label').each(function () {
            this.append(' ' + dataList[i])
            i++;
        });
    });
}

function populateScheduledExams() {
    $.get('https://hk-iot-team-02.azurewebsites.net/api/Teacher_Exam/' + teacherID, function (data) {
        data.forEach((exam) => {
            // Check if exam activity expired, this may work
            // UPDATE: it does not work properly
            const currentTime = new Date().getTime;
            let activeStatus = "READY";
            if (currentTime > Date(exam.DatetimeStart).getTime && currentTime < Date(exam.DatetimeEnd).getTime) {
                activeStatus = "ACTIVE";
            } else if (currentTime > Date(exam.DatetimeEnd).getTime) {
                activeStatus = "EXPIRED";
            }
            exams[exam.ID] = exam;
            const examItem = createListItem(exam, activeStatus);
            $('#scheduled-exams').append(examItem);
           
            //Very sketchy, listener should be added in html element, not via ID selector
            $('#' + exam.ID).bind('click', function () {
                startEditScheduleModal(exam.ID);
            });
      
        });

        console.log(exams); // DEBUG
    });
}

function createListItem(exam, status) {
    var color = 'orange';
    if (status == 'EXPIRED') {
        color = 'red';
    } else if (status == 'ACTIVE') {
        color = 'green'
    }
    //this is absolutetly terrible part of code
    const startDate = new DateTimeFactory(exam.DatetimeStart);
    const endDate = new DateTimeFactory(exam.DatetimeEnd);

    var examItem =
        '<button id="' + exam.ID +
        '" class="list-group-item d-flex justify-content-between align-items-center" data-toggle="modal" data-target="#edit-schedule-modal">' +
         '<div class="d-flex w-100 justify-content-between">'+
        '<h3 class="mb-2 h4">' + exam.ExamTitle + '</h4>' +
        '<span class="badge badge-light badge-pill">' + status.fontcolor(color) +' </span>' +
         '</div >' +
        '<p class="mb-2">' +
        'Start Date | Time: ' + '<br/>' + startDate.date.fontcolor(color) + ' | '.fontcolor(color) + startDate.time.fontcolor(color) + '<br/>' +
        'End Date | Time: ' + '<br/>' + endDate.date.fontcolor(color) + ' | '.fontcolor(color) + endDate.time.fontcolor(color) + '<br/>' +
        'Available Time: ' + '<br/>' + parseFloat(exam.AvailableTime).toFixed(0) + " min" + 
        '</p >' + '<small>'+ 'Subject: ' + exam.Subject + '</small>' +
        '</button >';
    return examItem;
}

function startEditScheduleModal(id) {
    var selectedExam = exams[id];
    var startDate = new DateTimeFactory(selectedExam.DatetimeStart);
    var endDate = new DateTimeFactory(selectedExam.DatetimeEnd);
    $('#edit-exam-title').html(selectedExam.ExamTitle);
    $('#edit-start-date').val(startDate.date);
    $('#edit-start-time').val(startDate.time);
    console.log(startDate.date);
    console.log(endDate.date);  
    $('#edit-end-date').val(endDate.date);
    $('#edit-end-time').val(endDate.time);
    $('#edit-avlbl-time').val(parseFloat(selectedExam.AvailableTime).toFixed(0));

    $('#save-changes').bind('click', function () {
        dateStart = $('#edit-start-date').val() + 'T' + $('#edit-start-time').val(); 
        dateEnd = $('#edit-end-date').val() + 'T' + $('#edit-end-time').val(); 
        var body = {
            ID: id,
            TeacherID: teacherID,
            ExamID: 
            DatetimeStart: dateStart,
            DatetimeEnd: dateEnd,

        };
    });
}

function PUT(exam) {
    $.ajax({
        url: 'https://hk-iot-team-02.azurewebsites.net/api/Teacher_Exam/' + exam.ID,
        type: 'PUT',
        contentType: 'application/json',
        data: body,
        success: function (data) {
            // TODO
        },
        error: function (data) {
            // TODO
        }
    });
}