using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class cameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 idealDir;
    public GameObject target;

    public Vector3 tRotEuler;

    public Quaternion cleaned;
    void Start()
    {
        
    }
    void FixedUpdate()
    {
        tRotEuler = transform.rotation.eulerAngles;
        tRotEuler = normalizeVector(tRotEuler);

        idealDir = calcPoint(target);
        cleaned = cleanRot(idealDir, 0.1f);
        transform.rotation = cleaned;
        // float diff = idealDir.y-transform.rotation.y;
        // transform.rotation = Quaternion.Euler(30,idealDir.y * 180f / Mathf.PI,0);

        // transform.LookAt(target.transform);
    }
    public Vector3 normalizeVector(Vector3 vect){
        Vector3 v = vect;
        while (v.y>180){ v -= new Vector3(0,360,0); }
        return v;
    }

    public Quaternion cleanRot(Vector3 idealEuler, float percentage)
    {
        // while (percentage>1) percentage-=1;
        // while (percentage<0) percentage+=1;
        //get average between current rot and ideal
        float yResult = idealEuler.y*percentage+tRotEuler.y*(1-percentage);

        // yResult *= 360/Mathf.PI;
        Vector3 result = new Vector3(30,yResult,0);
        return Quaternion.Euler(result);

    }
    public Vector3 calcPoint(GameObject t)
    {
        float xDiff = t.transform.position.x-transform.position.x;
        float zDiff = t.transform.position.z-transform.position.z;
        Angle result = new Angle(Mathf.Atan2(xDiff, zDiff), AngleUnit.Radian);
        // Debug.Log(Quaternion.Euler(30, result.ToDegrees(), 0));
        float resultY = result.ToDegrees();
        return new Vector3(0, resultY, 0);
    }
}