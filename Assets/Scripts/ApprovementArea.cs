using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApprovementArea : MonoBehaviour
{

    public enum AreaType
    {
        Approved, NonApproved, Unassigned

    }
    [SerializeField] AreaType type;
    [SerializeField] Color stateColor;



    public void EnteredArea(StudentAvatar avatar)
    {
        if (!avatar) return;
        switch (type)
        {
            case AreaType.Approved:
                AvatarManager.neutralStudents.Remove(avatar);
                if (!AvatarManager.approvedStudents.Contains(avatar)) AvatarManager.approvedStudents.Add(avatar);


                break;
            case AreaType.Unassigned:
                if (!AvatarManager.neutralStudents.Contains(avatar)) AvatarManager.neutralStudents.Add(avatar);

                break;
            case AreaType.NonApproved:
                AvatarManager.neutralStudents.Remove(avatar);
                if (!AvatarManager.nonApprovedStudents.Contains(avatar)) AvatarManager.nonApprovedStudents.Add(avatar);

                break;
        }
        avatar.SetState(stateColor, type.ToString());

    }//Closes Entered method

    public void ExitedArea(StudentAvatar avatar)
    {
        if (!avatar) return;
        switch (type)
        {
            case AreaType.Approved:
                AvatarManager.approvedStudents.Remove(avatar);
                break;
            case AreaType.Unassigned:
                AvatarManager.neutralStudents.Remove(avatar);
                break;
            case AreaType.NonApproved:
                AvatarManager.nonApprovedStudents.Remove(avatar);
                break;
        }
    }//Closes Entered method





    private void OnCollisionEnter(Collision other)
    {
        if (type != AreaType.Unassigned) return;

        StudentAvatar avatar = other.transform.GetComponentInParent<StudentAvatar>();
        if (!avatar) return;
        EnteredArea(avatar);
    }
    private void OnCollisionExit(Collision other)
    {
        if (type != AreaType.Unassigned) return;

        StudentAvatar avatar = other.transform.GetComponentInParent<StudentAvatar>();
        if (!avatar) return;
        ExitedArea(avatar);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (type == AreaType.Unassigned) return;

        StudentAvatar avatar = other.GetComponentInParent<StudentAvatar>();
        if (!avatar) return;
        EnteredArea(avatar);

    }//CLoses OnTriggerEnter method

    private void OnTriggerExit(Collider other)
    {
        if (type == AreaType.Unassigned) return;

        StudentAvatar avatar = other.GetComponentInParent<StudentAvatar>();
        if (!avatar) return;
        ExitedArea(avatar);
    }//CLoses OnTriggerEnter method



}//CLoses ApprovementArea method
