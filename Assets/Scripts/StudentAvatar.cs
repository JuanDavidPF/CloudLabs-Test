using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class StudentAvatar : MonoBehaviour
{

    [SerializeField] GameObject studentPanel;
    [SerializeField] TextMeshProUGUI studentName;
    [SerializeField] TextMeshProUGUI studentGrade;
    [SerializeField] TextMeshProUGUI studentState;




    Transform _t;
    Rigidbody _rb;
    private Vector3 screenPoint;
    private Vector3 offset;
    private bool isDragging;


    private Student _data;
    public Student data
    {
        get { return _data; }
        set
        {
            _data = value;
            if (_data == null) return;
            if (studentName) studentName.text = _data.name + " " + _data.lastname;
            if (studentGrade) studentGrade.text = _data.score.ToString();
            if (studentState) studentState.gameObject.SetActive(false);
        }
    }

    private void Awake()
    {
        _t = transform;
        _rb = GetComponent<Rigidbody>();
    }//Closes Awake method


    public void SetState(Color colorState, string state)
    {
        if (!studentState) return;
        studentState.gameObject.SetActive(true);
        studentState.color = colorState;
        studentState.text = state;
    }

    private void Update()
    {
        if (AvatarManager.reference && _t.position.y < -20)
        {
            _t.position = AvatarManager.reference.GetRandomPointInsideCollider();
        }
        _rb.isKinematic = isDragging;
    }

    private void OnMouseDown()
    {

        isDragging = true;
        studentPanel.SetActive(true);
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }
    private void OnMouseDrag()
    {

        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        curPosition.y = Mathf.Max(0, curPosition.y);
        transform.position = curPosition;

    }

    private void OnMouseOver()
    {
        if (!isDragging && studentPanel) studentPanel.SetActive(true);
    }

    private void OnMouseExit()
    {
        if (!isDragging && studentPanel) studentPanel.SetActive(false);
    }

    private void OnMouseUp()
    {
        isDragging = false;
        studentPanel.SetActive(false);
    }
}

