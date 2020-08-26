"use strict";
$(document).ready(onDocumentReady);

let teacherID;
let classID;
let schedules;

//SCHEDULE: 25/08 -> Make exam select feature for scheduling and editing the schedules -- POSTPONED -- DONE
//SCHEDULE: 26/08 -> Make exam maker feature for creating exams with questions

// TODO: FILTER SCHEDULED EXAM VIEW BY SUBJECT ANY WAY YOU CAN
// TODO: Write handler for date and time, parse date and time and display it separately in container -- DONE
// TODO: POST -- DONE, DELETE -- DONE , PUT -- DONE request to create scheduled exam; Send confirmation dialog when deleting
// TODO: When POSTing, check if exam to be scheduled is already scheduled: warn user -- SKIPPED
// TODO: Create system for creating exams with questions (long task)
// TODO: Create login attempt on page refresh after 10 min of an open session
// TODO: Create logout functionallity
// TODO: Create seperate nav for all exams and schedules

// ERROR: Listener of list-item object does not pass id to modal -- FIXED
// ERROR: Font color highlight (exam status) does not work properly(check conditioning) -- FIXED, try something with badges
// ERROR: Start date in modal does not display correctly (does not even display) -- FIXED
// ERROR: schedule exam button onclick does not call startNewScheduleModal() -- FIXED

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
    schedules = {};
    // Retrieve teacher's id from href
    const url = window.location.href;
    teacherID = url.split('?')[1].split('=')[1];
    // Populate containers
    populateDetails();
    populateScheduledExams();

    $('#new-schedule-modal').bind('show.bs.modal', function () {
        startNewScheduleModal();
    });
}

function populateDetails() {
    $.get('https://hk-iot-team-02.azurewebsites.net/api/Teachers/' + teacherID, function (data) {
        const dataList = [data.Name, data.Surname, data.Class, teacherID];
        let i = 0;
        $('#professor-details').children('label').each(function () {
            this.append(' ' + dataList[i]);
            i++;
        });
    });
}

function populateScheduledExams() {
    $('#scheduled-exams').empty();

    $.get('https://hk-iot-team-02.azurewebsites.net/api/Teacher_Exam/' + teacherID, function (data) {
        schedules = {};
        data.forEach((exam) => {    
            const currentTime = new Date().getTime();
            const dateStart = new Date(exam.DatetimeStart);
            const dateEnd = new Date(exam.DatetimeEnd);
            let activeStatus = "READY";
            if (currentTime > dateStart.getTime() && currentTime < dateEnd.getTime()) {
                activeStatus = "ACTIVE";
            } else if (currentTime > dateEnd.getTime()) {
                activeStatus = "FINISHED";
            }

            schedules[exam.ID] = exam;
            const examItem = createListItem(exam, activeStatus);
            $('#scheduled-exams').append(examItem);
           
            //Very sketchy, listener should be added in html element, not via ID selector
            $('#' + exam.ID).bind('click', function () {
                startEditScheduleModal(exam.ID);
            });
      
        });

        console.log(schedules); // DEBUG
    });
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
        '<button id="' + exam.ID + '" class="list-group-item d-flex justify-content-between align-items-center" data-toggle="modal" data-target="#edit-schedule-modal">' +
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
        '</button >';
    return examItem;
}

function startEditScheduleModal(id) {
    var selectedExam = schedules[id];
    var startDate = new DateTimeFactory(selectedExam.DatetimeStart);
    var endDate = new DateTimeFactory(selectedExam.DatetimeEnd);
    $('#edit-start-date').val(startDate.date);
    $('#edit-start-time').val(startDate.time);
    $('#edit-end-date').val(endDate.date);
    $('#edit-end-time').val(endDate.time);
    $('#edit-avlbl-time').val(parseFloat(selectedExam.AvailableTime).toFixed(0));

    $('#save-changes').bind('click', function () {
        const dateStart = $('#edit-start-date').val() + 'T' + $('#edit-start-time').val(); 
        const dateEnd = $('#edit-end-date').val() + 'T' + $('#edit-end-time').val(); 
        //check if end date is set before start date
        const tempDateStart = new Date(dateStart);
        const tempDateEnd = new Date(dateEnd);
        if (tempDateStart.getTime() > tempDateEnd.getTime()) {
            alert("End date cannot be before start date!");
            startEditScheduleModal(id); // why does this does not work
            return;
        }

        var body = {
            ID: Number(id),
            TeacherID: Number(teacherID),
            ExamID: 0,
            DatetimeStart: dateStart,
            DatetimeEnd: dateEnd,
            AvailableTime: Number($('#edit-avlbl-time').val())
        };
        schedulePUT(body);
        console.log(body); // DEBUG
    });

    $('#delete-schedule').bind('click', function () {
        const isConfirmed = confirm("You are about to delete this schedule. Proceed?");
        if (isConfirmed) {
            scheduleDELETE(selectedExam.ID);    
        }
    });
}

function schedulePUT(schedule) {
    $.ajax({
        url: 'https://hk-iot-team-02.azurewebsites.net/api/Teacher_Exam/' + schedule.ID,
        type: 'PUT',
        contentType: 'application/json',
        data: JSON.stringify(schedule),
        success: function (data) {
            alert("Successful!");
            populateScheduledExams();
        },
        error: function (data) {
            alert("Bad request") // DEBUG, delete for all instances 
        }
    });
}

function scheduleDELETE(id) {
    $.ajax({
        url: 'https://hk-iot-team-02.azurewebsites.net/api/Teacher_Exam/' + id,
        type: 'DELETE',
        success: function () {
            alert("Changes saved!");
            populateScheduledExams();
        },
        error: function () {
            alert("Bad request!");
        }
    });
}

function schedulePOST(schedule) {
    $.ajax({
        url: 'https://hk-iot-team-02.azurewebsites.net/api/Teacher_Exam/',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(schedule),
        success: function () {
            alert("Changes saved!");
            populateScheduledExams();
        },
        error: function () {
            alert("Bad request!");
        }
    });
}