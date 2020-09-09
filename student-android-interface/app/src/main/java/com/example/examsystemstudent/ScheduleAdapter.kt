package com.example.examsystemstudent

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.TextView
import androidx.core.view.isVisible
import androidx.recyclerview.widget.RecyclerView

class ScheduleRecyclerViewHolder(itemView: View, isActiveSchedules: Boolean) : RecyclerView.ViewHolder(itemView) {
    private var sTitle: TextView? = null
    private var sDatetimeStart: TextView? = null
    private var sDatetimeEnd: TextView? = null
    private var sAvailableTime: TextView? = null
    private var sSubject: TextView? = null
    private var beginButton: Button? = null

    init {
        sTitle = itemView.findViewById(R.id.schedule_title)
        sSubject = itemView.findViewById(R.id.schedule_subject)
        sDatetimeStart = itemView.findViewById(R.id.schedule_datetime_start)
        sDatetimeEnd = itemView.findViewById(R.id.schedule_datetime_end)
        sAvailableTime = itemView.findViewById(R.id.schedule_available_time)
        beginButton = itemView.findViewById(R.id.button_begin)
    }

    fun bind(schedule: Schedule, clickListener: OnItemClickListener, isActiveSchedules: Boolean) {
        sTitle?.text = schedule.ExamTitle
        sSubject?.text = schedule.Subject
        val dateStart = schedule.DatetimeStart.split("T")
        sDatetimeStart?.text = dateStart[0] + " | " + dateStart[1]
        val dateEnd = schedule.DatetimeEnd.split("T")
        sDatetimeEnd?.text = dateEnd[0] + " | " + dateEnd[1]
        sAvailableTime?.text = schedule.AvailableTime.toInt().toString() + " min"

        if(isActiveSchedules){
            beginButton?.setOnClickListener {
                clickListener.onItemClickedListener(schedule)
            }
        }else{
            beginButton?.visibility = View.GONE
        }
    }
}

class SchedulesAdapter(private var scheduleList: MutableList<Schedule>, private val itemClickListener: OnItemClickListener, private val isActiveSchedules: Boolean):
    RecyclerView.Adapter<ScheduleRecyclerViewHolder>() {
    override fun onCreateViewHolder(parent: ViewGroup, position: Int): ScheduleRecyclerViewHolder {
        val view = LayoutInflater.from(parent.context).inflate(R.layout.schedule_item_layout, parent, false)
        return ScheduleRecyclerViewHolder(view, isActiveSchedules)
    }

    override fun getItemCount(): Int {
        return scheduleList.size
    }
    override fun onBindViewHolder(holder: ScheduleRecyclerViewHolder, position: Int){
        val schedule = scheduleList[position]
        holder.bind(schedule, itemClickListener, isActiveSchedules)
    }
}

interface OnItemClickListener {
    fun onItemClickedListener(schedule: Schedule)
}

/*
* Taken exams
*/

class TakenExamRecyclerViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
    private var tTitle: TextView? = null
    private var tPoints: TextView? = null

    init {
        tTitle = itemView.findViewById(R.id.t_exam_title)
        tPoints = itemView.findViewById(R.id.text_points)
    }

    fun bind(takenExam: TakenExam, clickListener: OnItemClickListener) {
        tTitle?.text = takenExam.ExamTitle
        tPoints?.text = "${takenExam.PointsScored.toInt()}/${takenExam.MaxPoints.toInt()}"
    }
}

class TakenExamsAdapter(var takenExamList:MutableList<TakenExam>, private val itemClickListener: OnItemClickListener):
    RecyclerView.Adapter<TakenExamRecyclerViewHolder>() {
    override fun onCreateViewHolder(parent: ViewGroup, position: Int): TakenExamRecyclerViewHolder {
        val view = LayoutInflater.from(parent.context).inflate(R.layout.taken_exam_item_layout, parent, false)
        return TakenExamRecyclerViewHolder(view)
    }

    override fun getItemCount(): Int {
        return takenExamList.size
    }
    override fun onBindViewHolder(holder: TakenExamRecyclerViewHolder, position: Int){
        val takenExam = takenExamList[position]
        holder.bind(takenExam, itemClickListener)
    }
}
