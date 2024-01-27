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
    public bool going = false;
    public float rotZ;


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
                rotMin = 0;
                rotMax = -110;
                break;
            case "claw":
                rotMin = 0;
                rotMax = -90;
                break;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //setup the result
        if(type == "R" || type == "L"){
            rotZ = 180+transform.localRotation.eulerAngles.z;
            while(rotZ>180){rotZ -=360;}
            while (rotZ<-180){rotZ +=360;}
        }else{
            rotZ = transform.rotation.eulerAngles.z;
            while(rotZ>180){rotZ-=360;}
        }
        

        if(going){
            switch(type){
                case "L":
                    if(open){
                        if(rotZ < rotMax){
                            transform.Rotate(0.0f, 0.0f, 10.0f, Space.Self);
                        }else{
                            manager.complete(type);
                            transform.Rotate(0.0f, 0.0f, rotMax-rotZ, Space.Self);
                            going = false;
                        }
                    }else{
                        if(rotZ > rotMin){
                            transform.Rotate(0.0f, 0.0f, -10.0f, Space.Self);
                        }else{
                            manager.complete(type);
                            transform.Rotate(0.0f, 0.0f, rotMin-rotZ, Space.Self);
                            going = false;
                        }
                    }
                    break;
                case "R":
                    if(open){
                        if(rotZ > rotMax){
                            transform.Rotate(0.0f, 0.0f, -10.0f, Space.Self);
                        }else{
                            manager.complete(type);
                            transform.Rotate(0.0f, 0.0f, rotMax-rotZ, Space.Self);
                            going = false;
                        }
                    }else{
                        if(rotZ < rotMin){
                            transform.Rotate(0.0f, 0.0f, 10.0f, Space.Self);
                        }else{
                            manager.complete(type);
                            transform.Rotate(0.0f, 0.0f, rotMin-rotZ, Space.Self);
                            going = false;
                        }
                    }
                    break;
                case "claw":
                    if(open){
                        if(rotZ > rotMax){
                            transform.Rotate(0.0f, 0.0f, -10.0f, Space.Self);
                        }else{
                            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, rotMax);
                            going = false;
                        }
                    }else{
                        if(rotZ < rotMin){
                            transform.Rotate(0.0f, 0.0f, 10.0f, Space.Self);
                        }else{
                            manager.complete("claw");
                            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, rotMin);
                            going = false;
                        }
                    }
                    break;
            }
        }
    }
    public void openHorn(string t){
        if(type == t || (t == "all" && type != "claw")){
            going = true;
            open = true;
            if(type != "claw"){
                BroadcastMessage("release", null, SendMessageOptions.DontRequireReceiver);
            }
        }
    }
    public void closeHorn(string t){
        if(type == t || (t == "all" && type != "claw")){
            going = true;
            open = false;
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
            Debug.Log("deprecated");
        }
    }
}
