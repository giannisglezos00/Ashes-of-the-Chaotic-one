using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using UnityEditor.SceneManagement;

public class Managment : MonoBehaviour
{
    public NPCConversation myConcersation;

    void Start()
    {
        //if (Input.GetMouseButton(0)){
            ConversationManager.Instance.StartConversation(myConcersation);
        //}
    }


}
