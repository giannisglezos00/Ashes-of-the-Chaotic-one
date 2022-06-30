using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proectile : MonoBehaviour
{
    public GameObject Player;
    public Vector3 movespeed;
    public float spedo;
    public float Time = 5f;
    private Character Direction;
    public float directionx;
    public float directiony;
    public Vector3 FinalDirection;
    public string DDirection;
    private void Awake()
    {
        Direction = Player.GetComponent<Character>();
        DDirection = Direction.Moving_Direction;
    }
    void Update()
    {
        Debug.Log(DDirection);
        #region ("TRASH")
        //Direction 
        /* switch ( Horizontal )
         {
             case -1:
                 Direction = new Vector3(-spedo, 0, 0);

                 break;
             case 1:
                 Direction = new Vector3(spedo, 0, 0);
                 break;
         }
         switch ( Vertical )
         {
             case -1:
                 Direction = new Vector3(0, -spedo, 0);
                 break;
             case 1:
                 Direction = new Vector3(0, spedo, 0);
                 break;
         }*/
        #endregion
    }
    void FixedUpdate()
    {
        
    }
   
    private void Start()
    {
        Destroy(gameObject, Time);
    }
}

