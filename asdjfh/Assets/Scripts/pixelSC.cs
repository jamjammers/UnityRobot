using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pixelSC : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision col){
        Collider child = col.GetContact(0).thisCollider;
        Debug.Log(child.name);
    }
}
