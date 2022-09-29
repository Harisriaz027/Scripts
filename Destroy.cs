using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
   

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -87.5f&&transform.position.y<-41)
        {
            Destroy(gameObject);
        }
    }
}
