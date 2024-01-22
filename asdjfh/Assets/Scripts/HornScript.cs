using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HornScript : MonoBehaviour
{

    public string type;

    // public string mode = "intaking";

    public float rotMin = 0;
    public float rotMax = 0;

    public bool open = false;
    public bool going = true;
    public float rotZ;

    public Transform parentT;
    public float parentRotZ;
    public float result;

    public GameObject managerObj;
    public IntakeManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = managerObj.GetComponent<IntakeManager>();
        switch(type){
            case "L":
                rotMin = 0;
                rotMax = 110;
                break;
            case "R":
                rotMin = 360;
                rotMax = 250;
                break;
            case "claw":
                rotMin = -10;
                rotMax = -90;
                break;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //setup the result
        if(type == "R" || type == "L"){
            parentRotZ = parentT.rotation.eulerAngles.z;
            while(parentRotZ>180){ parentRotZ -= 360;}
            rotZ = 180+transform.localRotation.eulerAngles.z;
            while(rotZ>360){rotZ -=360;}
            while (rotZ<0){rotZ +=360;}
            result = rotZ+(type=="L"?parentRotZ-180:-parentRotZ);
            while(result<0){ result += 360;}
        }else{
            rotZ = transform.rotation.eulerAngles.z;
            while(rotZ>180){rotZ-=360;}
        }
        

        if(going){
            switch(type){
                case "L":
                    if(open && rotZ < rotMax){
                        transform.Rotate(0.0f, 0.0f, 10.0f, Space.Self);
                    }else if(!open && rotZ > rotMin){
                        transform.Rotate(0.0f, 0.0f, -10.0f, Space.Self);
                    }else{
                        // manager.complete(type);
                        going = false;
                    }
                    break;
                case "R":
                    if(open && (rotZ > rotMax)){
                        transform.Rotate(0.0f, 0.0f, -10.0f, Space.Self);
                    }else if(!open && rotZ < rotMin){
                        transform.Rotate(0.0f, 0.0f, 10.0f, Space.Self);
                    }else{
                        // manager.complete(type);
                        going = false;
                    }
                    break;
                case "claw":
                    if(open && rotZ > rotMax){
                        transform.Rotate(0.0f, 0.0f, -10.0f, Space.Self);
                    }else if(!open && (rotZ < rotMin)){
                        transform.Rotate(0.0f, 0.0f, 10.0f, Space.Self);
                    }
                    break;
            }
        }
    }
    public void grabR(){
        if(type == "R"){
            going = true;
            open = !open;
            BroadcastMessage("release", null, SendMessageOptions.DontRequireReceiver);
        }
    }
    public void grabL(){
        if(type == "L"){
            going = true;
            open = !open;
            BroadcastMessage("release", null, SendMessageOptions.DontRequireReceiver);

        }
    }
    public void moveClaw(){
        if(type == "claw"){
            going = true;
            open = !open;
        }
    }
}
