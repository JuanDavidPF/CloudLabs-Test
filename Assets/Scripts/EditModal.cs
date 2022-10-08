using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EditModal : MonoBehaviour
{
    [SerializeField] TMP_InputField id;
    [SerializeField] TMP_InputField studentName;
    [SerializeField] TMP_InputField studentLastname;
    [SerializeField] TMP_InputField age;
    [SerializeField] TMP_InputField mail;
    [SerializeField] TMP_InputField grade;
    [SerializeField] Toggle approved;

    [SerializeField] TextMeshProUGUI btn_submit;
    [SerializeField] Button btn_delete;
    private Student studentReference;

    public void NewStudent()
    {
        if (btn_submit) btn_submit.text = "Add";

        studentReference = new Student();

        if (id) id.text = "";
        if (studentName) studentName.text = "";
        if (studentLastname) studentLastname.text = "";
        if (age) age.text = "";
        if (mail) mail.text = "";
        if (grade) grade.text = "";
        if (approved) approved.isOn = studentReference.approved;
        if (btn_delete) btn_delete.gameObject.SetActive(false);

        gameObject.SetActive(true);
    }//Closes EditStudent method


    public void EditStudent(Student student)
    {
        if (student == null) return;

        if (btn_submit) btn_submit.text = "Save";

        studentReference = student;
        if (id) id.text = studentReference._id;
        if (studentName) studentName.text = studentReference.name;
        if (studentLastname) studentLastname.text = studentReference.lastname;
        if (age) age.text = studentReference.age.ToString();
        if (mail) mail.text = studentReference.email;
        if (grade) grade.text = studentReference.score.ToString();
        if (approved) approved.isOn = studentReference.approved;
        if (btn_delete) btn_delete.gameObject.SetActive(true);

        gameObject.SetActive(true);
    }//Closes EditStudent method

    public void DeleteStudent()
    {
        if (studentReference == null) return;

        if (TableManager.database.students.Contains(studentReference)) TableManager.database.students.Remove(studentReference);
        TableManager.SaveJson();
        if (TableManager.reference) TableManager.reference.LoadCards();
        gameObject.SetActive(false);
    }//Closes DelteStudent method

    public void SaveChanges()
    {
        if (studentReference == null) return;
        if (id) studentReference._id = id.text;
        if (studentName) studentReference.name = studentName.text;
        if (studentLastname) studentReference.lastname = studentLastname.text;
        if (age && age.text != "") studentReference.age = int.Parse(age.text);
        if (mail) studentReference.email = mail.text;
        if (grade && grade.text != "") studentReference.score = float.Parse(grade.text);
        if (approved) studentReference.approved = approved.isOn;

        if (!TableManager.database.students.Contains(studentReference)) TableManager.database.students.Add(studentReference);

        TableManager.SaveJson();
        if (TableManager.reference) TableManager.reference.LoadCards();
        gameObject.SetActive(false);
    }//Closes SaveStudent method

}//Closes EditModal method
