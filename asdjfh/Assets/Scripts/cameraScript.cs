using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class cameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Quaternion idealDir;
    public GameObject target;

    void Start()
    {
        
    }
    void FixedUpdate()
    {
        idealDir = calcPoint(target);
        float diff = idealDir.y-transform.rotation.y;
        transform.rotation = diff;
    }
    public Quaternion calcPoint(GameObject t)
    {
        float xDiff = t.transform.position.x-transform.position.x;
        float zDiff = t.transform.position.z-transform.position.z;
        Angle result = new Angle(Mathf.Atan2(xDiff, zDiff), AngleUnit.Radian);
        Debug.Log(Quaternion.Euler(30, result.ToDegrees(), 0));
        return Quaternion.Euler(30, result.ToDegrees(), 0);
    }
}
