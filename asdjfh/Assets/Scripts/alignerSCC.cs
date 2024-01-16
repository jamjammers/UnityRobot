using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alignerSCC : MonoBehaviour
{


    public Vector3 tRotEuler;
    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("baudf");
    }

    // Update is called once per frame
    void Update()
    {
        tRotEuler = transform.rotation.eulerAngles;
        
    }
}
