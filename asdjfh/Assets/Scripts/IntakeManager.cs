using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public class IntakeManager : MonoBehaviour
{
    bool rOpen = false;
    bool lOpen = false;

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

    public void grabAll(){

    }
    public void grabL(){
        
    }
    public void grabR(){
        
    }
    public void moveArm(InputAction.CallbackContext ctx){
        //move up * ctx, angle of 60 from ground, broad cast it to slides if we are in intake mode, else not
    }
    
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