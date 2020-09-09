package com.example.examsystemstudent

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Toast
import com.google.gson.Gson
import kotlinx.android.synthetic.main.activity_main.*
import java.io.BufferedReader
import java.io.InputStreamReader
import java.io.OutputStreamWriter
import java.net.HttpURLConnection
import java.net.URL
import kotlinx.coroutines.*
import org.json.JSONObject

// TODO add network availble check otherwise app crashes

data class LoginRequest(val Mail: String, val Password: String)

class MainActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        this.login_submit.setOnClickListener {
            val email = this.login_email.text.toString()
            val password = this.login_password.text.toString()
            val request = LoginRequest(email, password)

            CoroutineScope(Dispatchers.IO).launch{
                sendLoginRequest(request)
            }

        }

    }

    override fun onBackPressed() {

    }

    private suspend fun sendLoginRequest(loginRequest: LoginRequest){
        val loginURL = URL("https://hk-iot-team-02.azurewebsites.net/api/Students/Login")
        val jsonBody = Gson().toJson(loginRequest)
        println("Request : $jsonBody")
        try{
            with(loginURL.openConnection() as HttpURLConnection) {
                this.requestMethod = "POST"
                this.setRequestProperty("content-type", "application/json")

                val wr = OutputStreamWriter(outputStream);
                wr.write(jsonBody);
                wr.flush();

                println("Response Code : $responseCode")

                BufferedReader(InputStreamReader(inputStream)).use {
                    val response = StringBuffer()

                    var inputLine = it.readLine()
                    while (inputLine != null) {
                        response.append(inputLine)
                        inputLine = it.readLine()
                    }
                    println("Response : $response")

                    val primaryResponse = JSONObject(response.toString()).getJSONObject("Response")
                    val code = primaryResponse.getInt("Code")
                    var userID: Any? = primaryResponse.get("UserID")
                    var teacherID : Any? = JSONObject(response.toString()).get("TeacherID")
                    responseHandler(code, userID, teacherID)
                }
            }
        }catch(ex: Exception){
            withContext(Dispatchers.Main){
                Toast.makeText(applicationContext, "Network error. Check your Internet connection!", Toast.LENGTH_SHORT).show()
            }

        }

    }

    private suspend fun responseHandler(code: Int, userID: Any?, teacherID: Any?){
        withContext(Dispatchers.Main){
            when(code){
                -2 -> {
                    Toast.makeText(applicationContext, "Student does not exist.", Toast.LENGTH_SHORT).show()
                }
                -1 -> {
                    Toast.makeText(applicationContext, "Wrong password.", Toast.LENGTH_SHORT).show()
                }
                0 -> {
                    openScheduleView(userID as Int, teacherID as Int)
                }
            }
        }
    }

    private fun openScheduleView(userID: Int, teacherID: Int){
        val intent = Intent(this, ScheduleView::class.java).apply{
            putExtra("STUDENT_ID", userID)
            putExtra("TEACHER_ID", teacherID)
        }
        startActivity(intent)
    }
}