using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AvatarManager : MonoBehaviour
{
    public static List<StudentAvatar> neutralStudents = new List<StudentAvatar>();
    public static List<StudentAvatar> nonApprovedStudents = new List<StudentAvatar>();
    public static List<StudentAvatar> approvedStudents = new List<StudentAvatar>();



    public static AvatarManager reference;
    [SerializeField] Database database;
    [SerializeField] StudentAvatar avatarPrefab;

    [SerializeField] ApprovementArea approvedArea;
    [SerializeField] ApprovementArea nonApprovedArea;

    [Space(20)]
    [Header("Alerts")]
    [SerializeField] GameObject errorAlert; [SerializeField] GameObject successAlert;
    private BoxCollider boxCollider;
    private void Awake()
    {
        reference = this;
        boxCollider = GetComponent<BoxCollider>();
        database = new Database();

        string filePath = Path.Combine(Application.streamingAssetsPath + "/students.json");
        if (!File.Exists(filePath)) return;
        JsonUtility.FromJsonOverwrite(File.ReadAllText(filePath), database);

        //Spawn Avatars
        foreach (var student in database.students)
        {
            Instantiate(avatarPrefab, GetRandomPointInsideCollider(), Quaternion.identity).data = student;
        }
    }

    public void Verify()
    {
        if (Check())
        {
            if (successAlert) successAlert.SetActive(true);
        }
        else if (errorAlert) errorAlert.SetActive(true);

    } //Closes Verify method
    public bool Check()
    {
        if (neutralStudents.Count > 0) return false;

        foreach (var student in approvedStudents) if (!student.data.approved) return false;
        foreach (var student in nonApprovedStudents) if (student.data.approved) return false;

        return true;
    }



    public Vector3 GetRandomPointInsideCollider()
    {
        Vector3 extents = boxCollider.size / 2f;
        Vector3 point = new Vector3(
            Random.Range(-extents.x, extents.x),
            Random.Range(-extents.y, extents.y),
            Random.Range(-extents.z, extents.z)
        );

        return boxCollider.transform.TransformPoint(point);
    }



}
