using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TableManager : MonoBehaviour
{

    [SerializeField] Transform entryContainer;
    [SerializeField] Transform entryCard;

    [SerializeField] Database database;


    private void Awake()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath + "/students.json");
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);

            JsonUtility.FromJsonOverwrite(dataAsJson, database);

        }

    }//Closes Awake method
}//Closes TableManager class
