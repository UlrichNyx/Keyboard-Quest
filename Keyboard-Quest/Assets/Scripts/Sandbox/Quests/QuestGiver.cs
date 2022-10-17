using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{

    public GameObject questsUI;
    public Quest[] questsAvailable;  
    private Quest highlighted; 

    //public void ShowQuestDescription(Quest quest)

    public void ShowQuestDescription(Quest quest)
    {
        Image questIconDisplay = questsUI.transform.Find("QuestIcon").GetComponent<Image>(); // Get the Image object
        Text questNameDisplay = questsUI.transform.Find("QuestNameText").GetComponent<Text>(); // Get the name Text object
        Text questDescriptionDisplay = questsUI.transform.Find("QuestDescriptionText").GetComponent<Text>();

        questIconDisplay.color = new Color32(255,255,255,255); // Set the image to white
        questIconDisplay.sprite = quest.sprite; // Set the sprite 
        questNameDisplay.text = quest.questName; // Set the name
        questDescriptionDisplay.text = quest.questDescription; // Set the description
    }
   
    public void ShowQuestGiverWindow()
    {
        questsUI.SetActive(true);
    }

    public void HideQuestGiverWindow()
    {
        questsUI.SetActive(false);
    }

    void Update()
    {
        
    }
}
