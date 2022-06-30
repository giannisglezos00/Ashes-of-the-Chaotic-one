using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyself : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem particle;
    void Awake()
    {
        particle = GetComponent<ParticleSystem>();

        particle.enableEmission = true;
        StartCoroutine(Destroyobect());



    }
    // After 4 seconds, destroy game object
    IEnumerator Destroyobect()
    {
        yield return new WaitForSeconds(2);
        particle.enableEmission = false;
        Destroy(gameObject, 5f);
    }
}
