using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StudentCardHandler : MonoBehaviour
{

    private Student m_data;

    public Student data
    {
        get { return m_data; }
        set
        {
            m_data = value;
            UpdateCard();
        }
    }

    [SerializeField] private TextMeshProUGUI idTMPro;
    [SerializeField] private TextMeshProUGUI nameTMPro;
    [SerializeField] private TextMeshProUGUI ageTMPro;
    [SerializeField] private TextMeshProUGUI emailTMPro;
    [SerializeField] private TextMeshProUGUI scoreTMPro;
    [SerializeField] private Toggle approvedToggle;
    private void UpdateCard()
    {
        if (data == null) return;

        if (idTMPro) idTMPro.text = data._id;
        if (nameTMPro) nameTMPro.text = data.name + ' ' + data.lastname;
        if (ageTMPro) ageTMPro.text = data.age.ToString();
        if (emailTMPro) emailTMPro.text = data.email;
        if (scoreTMPro) scoreTMPro.text = data.score.ToString();
        if (approvedToggle) approvedToggle.isOn = data.approved;
    }//Closes Updatecard method


    public void ApproveStudent(bool approved)
    {
        if (data == null) return;
        data.approved = approved;
        UpdateCard();
        TableManager.SaveJson();
    }//Closes ChechCard method

    public void EditStudent()
    {
        if (!TableManager.reference || !TableManager.reference.editModal) return;
        TableManager.reference.editModal.EditStudent(data);
    }//Closes EditStudent method

}//Closes StudentCardHandler class
