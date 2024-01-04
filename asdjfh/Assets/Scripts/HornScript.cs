using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornScript : MonoBehaviour
{

    public string type;

    public string mode = "intaking";

    public float rotMin = 170;
    public float rotMax = 70;

    public bool open = true;
    public float rotZ;
    // Start is called before the first frame update
    void Start()
    {
        switch(type){
            case "L":
                rotMin = 190;
                rotMax = 290;
                break;
            case "R":
                rotMin = 170;
                rotMax = 70;
                break;

        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rotZ = transform.rotation.eulerAngles.z;
        switch(type){
            case "L":
                if(open && rotZ < rotMax){
                    transform.Rotate(0.0f, 0.0f, 10.0f, Space.Self);
                }else if(!open && rotZ > rotMin){
                    transform.Rotate(0.0f, 0.0f, -10.0f, Space.Self);
                }
                break;
            case "R":
                if(open && rotZ > rotMax){
                    transform.Rotate(0.0f, 0.0f, -10.0f, Space.Self);
                }else if(!open && rotZ < rotMin){
                    transform.Rotate(0.0f, 0.0f, 10.0f, Space.Self);
                }
                break;
            case "claw":
                if(open && rotZ > rotMax){
                    transform.Rotate(0.0f, 0.0f, -10.0f, Space.Self);
                }else if(!open && rotZ < rotMin){
                    transform.Rotate(0.0f, 0.0f, 10.0f, Space.Self);
                }
                break;
        }
    }
    public void grabAll(){
        if(type == "R" || type == "L"){
            open = !open;
        }else if(type == "claw" && mode == "intaking"){
            open = !open;
        }
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
