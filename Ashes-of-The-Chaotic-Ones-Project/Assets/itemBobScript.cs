using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemBobScript : MonoBehaviour
{
    public Vector3 uppos;
    public Vector3 originalpos;



    public float amp;
    public float freq;
    void Awake()
    {
        originalpos = transform.position;
    }

    void Update()
    {

        transform.position = new Vector3(originalpos.x, Mathf.Sin(Time.time * freq)* amp + originalpos.y, 1);
    }
}
