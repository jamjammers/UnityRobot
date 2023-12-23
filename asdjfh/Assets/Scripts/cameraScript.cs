using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class cameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Quaternion idealDir;
    public GameObject target;

    public Quaternion cleaned;
    void Start()
    {
        
    }
    void FixedUpdate()
    {
        idealDir = calcPoint(target);
        cleaned = cleanRot(idealDir, 0.09f);
        transform.rotation = cleaned;
        // float diff = idealDir.y-transform.rotation.y;
        // transform.rotation = Quaternion.Euler(30,idealDir.y * 180f / Mathf.PI,0);

        // transform.LookAt(target.transform);
    }
    public Quaternion cleanRot(Quaternion ideal, float percentage)
    {
        // while (percentage>1) percentage-=1;
        // while (percentage<0) percentage+=1;
        //get average between current rot and ideal
        float xResult = ideal.x*percentage+transform.rotation.x*(1-percentage);
        float yResult = ideal.y*percentage+transform.rotation.y*(1-percentage);
        float zResult = ideal.z*percentage+transform.rotation.z*(1-percentage);


        xResult *= 360/Mathf.PI;
        yResult *= 360/Mathf.PI;
        zResult *= 360/Mathf.PI;

        Debug.Log(transform.rotation.y*360/Mathf.PI);

        return Quaternion.Euler(xResult,yResult,zResult);

    }
    public Quaternion calcPoint(GameObject t)
    {
        float xDiff = t.transform.position.x-transform.position.x;
        float zDiff = t.transform.position.z-transform.position.z;
        Angle result = new Angle(Mathf.Atan2(xDiff, zDiff), AngleUnit.Radian);
        // Debug.Log(Quaternion.Euler(30, result.ToDegrees(), 0));
        return Quaternion.Euler(0, result.ToDegrees(), 0);
    }
}