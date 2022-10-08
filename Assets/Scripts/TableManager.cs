using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    public static TableManager reference;

    [SerializeField] Transform entryContainer;
    [SerializeField] StudentCardHandler entryCardPrefab;


    [Space(20)]
    [Header("Screens")]
    public EditModal editModal;
    [SerializeField] GameObject failedExercise;

    [SerializeField] GameObject succesfulExercise;

    public static Database database;

    private List<StudentCardHandler> cards = new List<StudentCardHandler>();

    private void Awake()
    {
        reference = this;
        database = new Database();
        LoadCards();
    }//Closes Awake method

    public static void SaveJson()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath + "/students.json");

        if (!File.Exists(filePath)) return;
        File.WriteAllText(filePath, JsonUtility.ToJson(database));

    }//Closes SaveInJson method
    public void LoadCards()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath + "/students.json");
        if (!entryContainer || !entryCardPrefab || !File.Exists(filePath)) return;

        JsonUtility.FromJsonOverwrite(File.ReadAllText(filePath), database);

        CreateCards();
        DeactivateCards();

        for (int i = 0; i < database.students.Count; i++)
        {
            StudentCardHandler card = cards[i];
            Student student = database.students[i];
            card.data = student;
            card.gameObject.SetActive(true);

        }


    }//Closes LoadCards method

    private void DeactivateCards()
    {
        foreach (var card in cards) card.gameObject.SetActive(false);
    }//Closes DeactivateCards method

    private void CreateCards()
    {
        //This is for reusing cards instead of creating all cards every refresh
        int cardsNeeded = Mathf.Max(0, database.students.Count - cards.Count);

        for (int i = 0; i < cardsNeeded; i++)
        {

            cards.Add(Instantiate(entryCardPrefab, entryContainer));
        }

    }//Closes DeactivateCards method

    public void Check()
    {
        if (VerifyAssignment())
        {
            if (succesfulExercise) succesfulExercise.SetActive(true);
        }
        else if (failedExercise) failedExercise.SetActive(true);

    }//Closes Check method


    public bool VerifyAssignment()
    {
        foreach (var student in database.students)
        {
            if (student.approved && student.score < 3f ||
            !student.approved && student.score >= 3f) return false;
        }
        return true;
    }//Closes VerifyAssinment method


}//Closes TableManager class
