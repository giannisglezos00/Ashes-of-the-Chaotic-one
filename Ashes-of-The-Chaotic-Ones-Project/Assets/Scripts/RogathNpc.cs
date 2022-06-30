using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class RogathNpc : MonoBehaviour
{
    public GameObject Player;
    public GameObject indicator;
    public NPCConversation Conversation;

    private Character C;
    private void Awake()
    {
        C = Player.GetComponent<Character>();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        indicator.SetActive(true);
        
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        indicator.SetActive(false);
    }
    private void OnTriggerStay2D(Collider2D collider)
    {
        if ( Input.GetKeyDown(KeyCode.E) )
        {
            ConversationManager.Instance.StartConversation(Conversation);
            C.InitiateInteraction();

        }
        else
        {
            Debug.Log("YOU'RE NOT PRESSING E");
        }
    }
    
}
