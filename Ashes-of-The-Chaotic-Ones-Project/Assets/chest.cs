using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest : MonoBehaviour
{
    public GameObject Player;
    public GameObject chestui;
    public GameObject Interactindicator;
    public bool isOpen = false;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if ( collider.gameObject.tag == "Player" )
        {
            //when the e button is pressed, it will open
            if ( Input.GetKeyDown(KeyCode.E) )
            {
                isOpen ^= true;
                if (isOpen == true)
                {
                    Interactindicator.SetActive(true);
                    chestui.SetActive(true);
                    
                }
                else
                {
                    chestui.SetActive(false);
                }
            }
        }
    }
    //when it leaves the colider
    private void OnTriggerExit2D(Collider2D collider)
    {
        Interactindicator.SetActive(false);
        chestui.SetActive(false);
    }
}
