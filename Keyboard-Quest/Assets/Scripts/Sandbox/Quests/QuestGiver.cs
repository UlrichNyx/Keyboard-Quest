using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{

    public GameObject QuestsUI;
    //public Quest[] questsAvailable;  
    //private Quest highlighted; 

    //public void ShowQuestDescription(Quest quest)

   
    public void ShowQuestGiverWindow()
    {
        QuestsUI.SetActive(true);
    }

    public void HideQuestGiverWindow()
    {
        QuestsUI.SetActive(false);
    }

    void Update()
    {
        
    }
}
