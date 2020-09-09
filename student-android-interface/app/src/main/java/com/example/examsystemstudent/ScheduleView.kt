package com.example.examsystemstudent

import android.content.Context
import android.content.Intent
import android.os.Build
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.Menu
import android.view.MenuItem
import android.view.View
import android.widget.Toast
import androidx.annotation.RequiresApi
import androidx.core.view.get
import androidx.recyclerview.widget.LinearLayoutManager
import kotlinx.android.synthetic.main.activity_schedule_view.*
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext
import org.json.JSONArray
import org.json.JSONObject
import java.net.URL
import java.time.LocalDateTime

data class Schedule(val ExamID: Int,
                    val ExamTitle: String,
                    val Subject: String,
                    val DatetimeStart: String ,
                    val DatetimeEnd: String,
                    val AvailableTime: Double)

data class TakenExam (
    val ExamTitle: String,
    val PointsScored: Double,
    val MaxPoints: Double
)

class ScheduleView : AppCompatActivity(), OnItemClickListener {

    private var studentID: Int = 0
    private var teacherID : Int = 0
    private var activeSchedules : MutableList<Schedule> = mutableListOf()
    private var upcomingSchedules: MutableList<Schedule> = mutableListOf()
    private var takenExams: MutableList<TakenExam> = mutableListOf()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_schedule_view)

        // Retrieve student's and teacher's ID
        studentID = intent.getIntExtra("STUDENT_ID", 0)
        teacherID = intent.getIntExtra("TEACHER_ID", 0)

        // Retrieve schedules and taken exams
       CoroutineScope(Dispatchers.IO).launch {
           retrieveSchedules()
       }

        this.filter_activeExams.setOnClickListener {
            this.schedule_container.adapter = SchedulesAdapter(activeSchedules, this, true)
            if(activeSchedules.size > 0){
                this.label_info.visibility = View.GONE
            }else{
                this.label_info.visibility = View.VISIBLE
            }
        }

        this.filter_upcomingExams.setOnClickListener {
            this.schedule_container.adapter = SchedulesAdapter(upcomingSchedules, this, false)
            if(upcomingSchedules.size > 0){
                this.label_info.visibility = View.GONE
            }else{
                this.label_info.visibility = View.VISIBLE
            }
        }

        this.filter_results.setOnClickListener {
            this.schedule_container.adapter = TakenExamsAdapter(takenExams, this)
            if(takenExams.size > 0){
                this.label_info.visibility = View.GONE
            }else{
                this.label_info.visibility = View.VISIBLE
            }
        }
    }

    override fun onCreateOptionsMenu(menu: Menu?): Boolean {
        super.onCreateOptionsMenu(menu)
        menuInflater.inflate(R.menu.main, menu)
        return true
    }

    override fun onOptionsItemSelected(item: MenuItem): Boolean {
        val intent = Intent(this, MainActivity::class.java)
        startActivity(intent)
        return super.onOptionsItemSelected(item)
    }

    @RequiresApi(Build.VERSION_CODES.O)
    private suspend fun retrieveSchedules(){

        val url = URL("https://hk-iot-team-02.azurewebsites.net/api/Student_Exam/$studentID")
        val response = JSONObject(url.readText())
        val scheduleArray = response.getJSONArray("ActiveSchedules")
        val takenExamArray = response.getJSONArray("PastExams")

        // Retrieve past and active schedules
        activeSchedules = mutableListOf<Schedule>()
        upcomingSchedules = mutableListOf()
        for(i in 0 until scheduleArray.length()){
            val schedule = scheduleArray.getJSONObject(i)

            val scheduleItem = Schedule(
                schedule.getInt("ExamID"),
                schedule.getString("ExamTitle"),
                schedule.getString("Subject"),
                schedule.getString("DatetimeStart"),
                schedule.getString("DatetimeEnd"),
                schedule.getDouble("AvailableTime")
            )

            val dateStart = LocalDateTime.parse(schedule.getString("DatetimeStart"))
            val dateEnd = LocalDateTime.parse(schedule.getString("DatetimeEnd"))
            val currentTime = LocalDateTime.now()
            if(currentTime > dateStart && currentTime < dateEnd){
                activeSchedules.add(scheduleItem)
            }else if(currentTime < dateEnd){
                upcomingSchedules.add(scheduleItem)
            }

        }

        takenExams = mutableListOf()
        // Retrieve taken exam data
        for(i in 0 until takenExamArray.length()){
            val jsonTakenExam = takenExamArray.getJSONObject(i)

            val takenExam = TakenExam(
                jsonTakenExam.getString("ExamTitle"),
                jsonTakenExam.getDouble("PointsScored"),
                jsonTakenExam.getDouble("MaxPoints")
            )
            takenExams.add(takenExam)
        }

        withContext(Dispatchers.Main){
            populateScheduleContainer(activeSchedules)
        }

    }

    private fun populateScheduleContainer(filteredSchedules: MutableList<Schedule>){
        schedule_container.adapter = SchedulesAdapter(filteredSchedules, this, true)
        schedule_container.layoutManager = LinearLayoutManager(this)
        if(activeSchedules.size > 0){
            this.label_info.visibility = View.GONE
        }
    }

    override fun onItemClickedListener(schedule: Schedule) {
        val intent = Intent(this, ExamActivity::class.java).apply {
            putExtra("STUDENT_ID", studentID)
            putExtra("TEACHER_ID", teacherID)
            putExtra("EXAM_ID", schedule.ExamID)
            putExtra("AVAILABLE_TIME", schedule.AvailableTime.toInt())
        }
        startActivity(intent)
    }
}

