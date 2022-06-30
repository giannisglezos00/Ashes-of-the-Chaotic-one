using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Transporter : MonoBehaviour
{
    public string target;
    public Vector3 LastPosition;
    public GameObject Player;
    void Awake()
    {
        

    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        LastPosition = Player.GetComponent<Transform>().transform.position;
        Debug.Log(LastPosition);
        UnityEngine.SceneManagement.SceneManager.LoadScene(target);

    }
}
