using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{
    [TextArea]
    public string notes = "[Placeholder]";
    
    public Vector3 tRotEuler;
    public Quaternion tRot;

    public void Update(){
        
        tRotEuler = transform.rotation.eulerAngles;
        tRot = transform.rotation;
    }
}
