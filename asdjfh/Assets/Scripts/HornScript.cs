using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornScript : MonoBehaviour
{

    public string type;

    public float rotMin = 170;
    public float rotMax = 70;

    public bool open = true;
    public float rotZ;
    // Start is called before the first frame update
    void Start()
    {
        rotMin = (type == "L")? 190 : 170;
        rotMax = (type == "L")? 290 : 70;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rotZ = transform.rotation.eulerAngles.z;
        if(type == "L"){
            if(open && rotZ < rotMax){
                transform.Rotate(0.0f, 0.0f, 10.0f, Space.Self);
            }else if(!open && rotZ > rotMin){
                transform.Rotate(0.0f, 0.0f, -10.0f, Space.Self);
            }
        }else if(type == "R"){
            if(open && rotZ > rotMax){
                transform.Rotate(0.0f, 0.0f, -10.0f, Space.Self);
            }else if(!open && rotZ < rotMin){
                transform.Rotate(0.0f, 0.0f, 10.0f, Space.Self);
            }
        }
    }
    public void grabAll(){
        open = !open;
        Debug.Log("all");
    }
    public void grabR(){
        if(type == "R"){
            open = !open;
        }
    }
    public void grabL(){
        if(type == "L"){
            open = !open;
        }
    }
}
