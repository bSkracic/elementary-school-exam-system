package com.example.examsystemstudent

import android.content.Intent
import android.graphics.Color
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.os.CountDownTimer
import android.view.View
import android.widget.*
import androidx.appcompat.app.AlertDialog
import com.google.gson.Gson
import com.google.gson.JsonArray
import kotlinx.android.synthetic.main.activity_exam_main.*
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext
import org.json.JSONArray
import org.json.JSONObject
import org.w3c.dom.Text
import java.io.BufferedReader
import java.io.InputStreamReader
import java.io.OutputStreamWriter
import java.net.HttpURLConnection
import java.net.URL

data class Question(val ID: Int,
                    val Text: String,
                    val isFreeAnswer: Boolean,
                    val optionA: String?,
                    val optionB: String?,
                    val optionC: String?,
                    val optionD: String?,
                    val CorrectAnswer: String,
                    val MaxPoints: Int
){}

data class ExamData(var ID: Int,
                    var ExamID: Int,
                    var StudentID: Int,
                    var Attended: Boolean,
                    var PointsScored: Int
                    ){}

class ExamActivity : AppCompatActivity() {

    private var examID = 0
    private var studentID = 0
    private var examDataID = 0
    private var questions: MutableMap<Int, Question> = mutableMapOf()
    private var answers: MutableMap<Int, String> = mutableMapOf()
    private var currentIndex: Int = 0
    private var availableTime: Int = 0
    private lateinit var countDown: CountDownTimer

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_exam_main)

        // Retrieve extra data from the intent
        studentID = intent.getIntExtra("STUDENT_ID", 0)
        examID = intent.getIntExtra("EXAM_ID", 0)
        availableTime = intent.getIntExtra("AVAILABLE_TIME", 0)

        // Set UI elements for first load
        this.btn_prev.visibility = View.GONE
        this.btn_next.visibility = View.GONE
        this.btn_exam_submit.visibility = View.GONE
        this.question_options.visibility = View.GONE
        this.free_answer.visibility = View.GONE
        this.question_text.visibility = View.GONE
        this.question_order.visibility = View.GONE

        CoroutineScope(Dispatchers.IO).launch {
            retrieveQuestions()
        }

        this.btn_prev.setOnClickListener {
            if(currentIndex  < questions.size) {
                val question = questions[questions.keys.elementAt(currentIndex)]
                    constructAnswer(question?.isFreeAnswer!!, question?.ID!!)
            }
            currentIndex--
            prepareQuestion()
        }

        this.btn_next.setOnClickListener {
            val question = questions[questions.keys.elementAt(currentIndex)]
            if(currentIndex < questions.size) {
                constructAnswer(question?.isFreeAnswer!!, question?.ID!!)
            }
            currentIndex++
            prepareQuestion()
        }

        this.btn_exam_submit.setOnClickListener {
            openConfirmationDialog()
        }

    }

    override fun onBackPressed() {
        openConfirmationDialog()
    }

    private fun prepareQuestion() {
        this.btn_prev.visibility = View.VISIBLE
        this.btn_next.visibility = View.VISIBLE
        this.question_text.visibility = View.VISIBLE
        this.question_order.visibility = View.VISIBLE
        this.question_options.visibility = View.VISIBLE
        this.free_answer.visibility = View.VISIBLE
        this.btn_exam_submit.visibility = View.GONE

        if(currentIndex == 0) {
            this.btn_prev.visibility = View.GONE
        }
        else if(currentIndex == questions.size) {
            // Last slide
            this.question_options.visibility = View.GONE
            this.free_answer.visibility = View.GONE
            this.question_order.visibility = View.GONE
            this.btn_next.visibility = View.GONE
            this.question_text.text = "You are about to submit an exam. You can still check and change your answers before you submit it."
            this.btn_exam_submit.visibility = View.VISIBLE
        }

        if(currentIndex != questions.size) {
            val questionKey = questions.keys.elementAt(currentIndex)
            val question = questions[questionKey]
            this.question_order.text = "Question No: ${currentIndex + 1}"
            this.question_text.text = question?.Text
            if(question?.isFreeAnswer!!){
                this.question_options.visibility = View.GONE
                this.free_answer.setText("")
            } else {
                this.free_answer.visibility = View.GONE
                this.question_A.isChecked = false
                this.question_B.isChecked = false
                this.question_C.isChecked = false
                this.question_D.isChecked = false
                this.question_A.text = "A: ${question.optionA}"
                this.question_B.text = "B: ${question.optionB}"
                this.question_C.text = "C: ${question.optionC}"
                this.question_D.text = "D: ${question.optionD}"
            }
        }

    }

    private fun startTimer(){
        availableTime = 1 // REMOVE THIS
        countDown = object: CountDownTimer((availableTime * 60 * 1000).toLong(), 1000){
            override fun onFinish() {
                Toast.makeText(applicationContext, "Timer expired.", Toast.LENGTH_SHORT).show()
                openScheduleView()
            }

            override fun onTick(millisUntilFinished: Long) {
                val timeSec = millisUntilFinished / 1000
                val minutes = timeSec / 60
                val seconds = timeSec - (timeSec/60).toInt() * 60
                var secondsText = ""
                if(seconds.toString().length == 1){
                    secondsText = "0"
                }
                secondsText += seconds.toString()
                if(timeSec <= 10){
                    timer.setTextColor(Color.RED)
                }
                timer.text = "$minutes : $secondsText"
            }
        }
        countDown.start()
    }

    private fun constructAnswer(isFreeAnswer: Boolean, questionID: Int) {
        if(isFreeAnswer) {
            answers[questionID] = this.free_answer.text.toString()
        } else {
            var answer: String = "{"
            if(this.question_A.isChecked){
                answer += "A,"
            }
            if(this.question_B.isChecked){
                answer += "B,"
            }
            if(this.question_C.isChecked){
                answer += "C,"
            }
            if(this.question_D.isChecked){
                answer += "D,"
            }

            answer = answer.replaceRange(answer.length - 1, answer.length, "}")
            answers[questionID] = answer
        }
    }

    private suspend fun retrieveQuestions() {
        sendStartExamData()
        val url = URL("https://hk-iot-team-02.azurewebsites.net/api/Exam_Question/$examID")
        val jsonQuestionsArr = JSONArray(url.readText())

        if(jsonQuestionsArr == null){
            withContext(Dispatchers.Main){
                Toast.makeText(applicationContext, "Cannot load questions! Try again later.", Toast.LENGTH_SHORT)
                val intent = Intent(applicationContext, MainActivity::class.java)
                startActivity(intent)
            }
        }

        for(i in 0 until jsonQuestionsArr.length()){
            val jsonQuestion = jsonQuestionsArr.getJSONObject(i)

            var optionA: String? = null
            var optionB: String? = null
            var optionC: String? = null
            var optionD: String? = null
            if(jsonQuestion.get("A") != null){
                optionA = jsonQuestion.getString("A")
            }
            if(jsonQuestion.get("B") != null){
                optionB = jsonQuestion.getString("B")
            }
            if(jsonQuestion.get("C") != null){
                optionC = jsonQuestion.getString("C")
            }
            if(jsonQuestion.get("D") != null){
                optionD = jsonQuestion.getString("D")
            }

            val question = Question(
                jsonQuestion.getInt("ID"),
                jsonQuestion.getString("Text"),
                jsonQuestion.getBoolean("FreeAnswerType"),
                optionA,
                optionB,
                optionC,
                optionD,
                jsonQuestion.getString("CorrectAnswer"),
                jsonQuestion.getInt("MaxPoints")
            )

            questions[question.ID] = question
        }

        withContext(Dispatchers.Main){
            prepareUI()
            startTimer()
        }

    }

    private fun prepareUI(){
        this.question_text.visibility = View.VISIBLE
        this.question_order.visibility = View.VISIBLE
        currentIndex = 0
        prepareQuestion()
    }

    private fun checkAnswers() : Int {
        var points = 0
        for(answer in answers) {
            if(answer.value == questions[answer.key]?.CorrectAnswer) {
                points += questions[answer.key]?.MaxPoints!!
            }
        }

        return points
    }

    private suspend fun sendStartExamData(){
        val body = ExamData(0, examID, studentID, true, 0)
        val json = Gson().toJson(body)
        val url = URL("https://hk-iot-team-02.azurewebsites.net/api/Student_Exam")

        with(url.openConnection() as HttpURLConnection) {
            this.requestMethod = "POST"
            this.setRequestProperty("content-type", "application/json")

            val wr = OutputStreamWriter(outputStream);
            wr.write(json);
            wr.flush();

            BufferedReader(InputStreamReader(inputStream)).use {
                val response = StringBuffer()

                var inputLine = it.readLine()
                while (inputLine != null) {
                    response.append(inputLine)
                    inputLine = it.readLine()
                }
                val postResponse = JSONObject(response.toString())
                examDataID = postResponse.getInt("ID")
            }
        }
    }

    private suspend fun sendFinishedExamData(achievedPoints: Int) {
        val body = ExamData(examDataID, examID, studentID, true, achievedPoints)
        val json = Gson().toJson(body)
        val url = URL("https://hk-iot-team-02.azurewebsites.net/api/Student_Exam/$examDataID")

        with(url.openConnection() as HttpURLConnection) {
            this.requestMethod = "PUT"
            this.setRequestProperty("content-type", "application/json")

            val wr = OutputStreamWriter(outputStream);
            wr.write(json);
            wr.flush();

            BufferedReader(InputStreamReader(inputStream)).use {
                val response = StringBuffer()

                var inputLine = it.readLine()
                while (inputLine != null) {
                    response.append(inputLine)
                    inputLine = it.readLine()
                }
            }
        }
        openScheduleView()
    }

    private fun openScheduleView(){
        countDown.cancel()
        val teacherID = intent.getIntExtra("TEACHER_ID", 0)
        val intent = Intent(this, ScheduleView::class.java).apply{
            putExtra("TEACHER_ID", teacherID)
            putExtra("STUDENT_ID", studentID)
        }
        startActivity(intent)
    }

    private fun openConfirmationDialog(){
        val alertDialog = AlertDialog.Builder(this)
        alertDialog.setMessage("You are about a submit your exam. Are you sure?").setCancelable(true)
            .setPositiveButton("Yes") { _, _ ->
                CoroutineScope(Dispatchers.IO).launch {
                    try{
                        val points = checkAnswers()
                        sendFinishedExamData(points)
                    }catch(exception: Exception)
                    {
                        withContext(Dispatchers.Main){
                            Toast.makeText(applicationContext, "Network Error", Toast.LENGTH_SHORT).show()
                        }
                    }
                    openScheduleView()
                }
            }
            .setNegativeButton("Cancel") { dialog, _ ->
            }
        val alert = alertDialog.create()
        alert.setTitle("Confirmation")
        alert.show()
    }


}