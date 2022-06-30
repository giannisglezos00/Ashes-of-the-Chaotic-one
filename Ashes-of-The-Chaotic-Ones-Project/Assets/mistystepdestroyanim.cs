using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mistystepdestroyanim : MonoBehaviour
{

    void Awake()
    {
        Destroy(gameObject, 1.5f);
    }

}
