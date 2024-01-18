using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Reflection;

public class IntakeManager : MonoBehaviour
{
    bool rOpen = false;
    prog rProg = prog.CLOSE;
    bool lOpen = false;
    prog lProg = prog.CLOSE;

    bool toIntermediate = false;

    //only measures the vertical change
    float ARMMAX = 0.25f;//0.3?
    float ARMMIN = 0;
    float armHeight = 0;

    TPos mode = TPos.INTAKING;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(TPos.INTAKING.getAxonAngle());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void complete(string s){
        switch(s){
            case "L":
                if(lProg == prog.TOCLOSE){
                    lProg = prog.CLOSE;
                }else if(lProg == prog.TOOPEN){
                    lProg = prog.OPEN;
                }
                break;
            case "R":
                if(rProg == prog.TOCLOSE){
                    rProg = prog.CLOSE;
                }else if(rProg == prog.TOOPEN){
                    rProg = prog.OPEN;
                }
                break;
        }
        }
    public bool isPlacing(){
        return mode == TPos.PLACING;
        }

    public void IAgrabAll(){
        if(mode == TPos.INTAKING || mode == TPos.PLACING){
            if(rOpen==lOpen){
                BroadcastMessage("grab"+(rOpen?"L":"R"));
                if(!rOpen){ rProg = prog.TOCLOSE; }
                if(!lOpen){ lProg = prog.TOCLOSE; }
                rOpen = false;
                lOpen = false;
                // toIntermediate = true;
            }else{
                BroadcastMessage("grabL");
                BroadcastMessage("grabR");
                rOpen = !rOpen;
                lOpen = !lOpen;
                if(rOpen){
                    rProg = prog.TOOPEN;
                    lProg = prog.TOOPEN;
                }else{
                    rProg = prog.TOCLOSE;
                    lProg = prog.TOCLOSE;
                }
            }
        }
        }
    
    public void IAgrabL(){
        if(mode == TPos.INTAKING || mode == TPos.PLACING){
            BroadcastMessage("grabL");
            lOpen = !lOpen;
            if(lOpen){ lProg = prog.TOOPEN; }
            else{ lProg = prog.TOCLOSE; }
        }
        }
    public void IAgrabR(){
        if(mode == TPos.INTAKING || mode == TPos.PLACING){
            BroadcastMessage("grabR");
            rOpen = !rOpen;
            if(rOpen){ rProg = prog.TOOPEN; }
            else{ rProg = prog.TOCLOSE; }
        }    
        }
    public void IAmoveArm(InputAction.CallbackContext ctx){
        armHeight += ctx.ReadValue<Vector2>().y;
        if(armHeight<ARMMIN){
            armHeight = ARMMIN;
        }
        if(armHeight>ARMMAX){
            armHeight = ARMMAX;
        }
        gameObject.BroadcastMessage("setArm", armHeight);

        
        //move up * ctx, angle of 60 from ground, broad cast it to slides if we are in intake mode, else not
        }
        public void grabAll(){
            BroadcastMessage("release", null, SendMessageOptions.DontRequireReceiver);

        }
        public void grabL(){
            BroadcastMessage("release", null, SendMessageOptions.DontRequireReceiver);

        }
        public void grabR(){
            BroadcastMessage("release", null, SendMessageOptions.DontRequireReceiver);

        }
//

}


public enum prog{
    CLOSE, TOCLOSE, TOOPEN, OPEN
    }
    

public class TPosAttr : Attribute{
        internal TPosAttr(float axonAngle, float clawAngle){
            this.axonAngle = axonAngle;
            this.clawAngle = clawAngle;
        }
        public double axonAngle{get; private set;}
        public double clawAngle{get; private set;}
    }

public static class TPosClass{

    public static double getAxonAngle(this TPos pos)
    {  
        TPosAttr attr = GetAttr(pos);
        return attr.axonAngle;
    }

    public static double getClawAngle(this TPos pos)
    {
        TPosAttr attr = GetAttr(pos);
        return attr.clawAngle;
    }


    private static TPosAttr GetAttr(TPos pos)
    {
        return(TPosAttr)Attribute.GetCustomAttribute(ForValue(pos), typeof(TPosAttr));
    }
    private static MemberInfo ForValue(TPos p)
    {
        return typeof(TPos).GetField(Enum.GetName(typeof(TPos), p));
    }

    }

public enum TPos{
    [TPosAttr(0, 0)]INTAKING,
    [TPosAttr(0, -100)]INTAKING_INTERMEDIATE,
    [TPosAttr(0, -150)]DRIVING_INTERMEDIATE,
    [TPosAttr(140,-160)]PLACING
    }