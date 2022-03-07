using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDie : MonoBehaviour
{
    public float TimeLive = 1.0f;
    void Start()
    {
        Invoke("ObjectDestroy", TimeLive);
    }

    void ObjectDestroy()
    {
        Destroy(gameObject);
    }
}
