using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Reflection;

public class IntakeManager : MonoBehaviour
{
    //instance vars

    public bool going = false;
    public bool gripWait = false;

    public bool rOpen = false;
    prog rProg = prog.CLOSE;
    public bool lOpen = false;
    prog lProg = prog.CLOSE;

    public bool toIntermediate = false;


    public TPos mode = TPos.INTAKING;

    bool slideReady = false;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(TPos.INTAKING.getAxonAngle());
    }

    // Update is called once per frame
    void Update()
    {

    }

    //getter and setter
    public TPos getMode()
    {
        return mode;
    }

    public void setMode(TPos t)
    {
        if (!going) return;

        mode = t;
    }


    //private
    private void oHornMess()
    {
        if (!going) return;

        BroadcastMessage("openHorn", "claw");
    }

    //outside access
    public void complete(string s)
    {
        if (!going) return;

        switch (s)
        {
            case "L":
                if (lProg == prog.TOCLOSE)
                {
                    lProg = prog.CLOSE;
                    if (toIntermediate)
                    {
                        Invoke("oHornMess", .1f);
                        toIntermediate = false;
                        if (mode != TPos.PLACING) { mode = TPos.INTAKING_INTERMEDIATE; }
                    }
                }
                else if (lProg == prog.TOOPEN)
                {
                    lProg = prog.OPEN;
                }
                break;
            case "R":

                if (rProg == prog.TOCLOSE)
                {
                    rProg = prog.CLOSE;
                    if (toIntermediate)
                    {
                        Invoke("oHornMess", .1f);
                        toIntermediate = false;
                        if (mode != TPos.PLACING) { mode = TPos.INTAKING_INTERMEDIATE; }
                    }
                }
                else if (rProg == prog.TOOPEN)
                {
                    rProg = prog.OPEN;
                }
                break;
            case "claw":
                BroadcastMessage("openHorn", "all");
                if (mode != TPos.PLACING) { mode = TPos.INTAKING; }
                rProg = lProg = prog.OPEN;
                rOpen = lOpen = true;
                break;
            case "arm":
                if (mode == TPos.PLACING)
                {
                    BroadcastMessage("openHorn", "claw");
                }
                else
                {
                    BroadcastMessage("closeHorn", "claw");
                }
                break;
            case "slides":
                if (slideReady)
                {
                    slideReady = false;
                    BroadcastMessage("closeHorn", "arm");
                }
                break;
        }
    }


    //IA
    public void IAgrabAll(InputAction.CallbackContext ctx)
    {
        if (!going) return;
        if (ctx.phase == (InputActionPhase)3)
        {
            if (mode == TPos.INTAKING || mode == TPos.PLACING)
            {
                if (rOpen || lOpen)
                {
                    BroadcastMessage("closeHorn", "all");
                    rOpen = lOpen = false;
                    rProg = lProg = prog.TOCLOSE;
                    toIntermediate = true;
                }
                else
                {
                    BroadcastMessage("openHorn", "all");
                    rOpen = lOpen = true;
                    rProg = lProg = prog.TOOPEN;
                    toIntermediate = false;
                }
            }
            else if (mode == TPos.INTAKING_INTERMEDIATE)
            {
                BroadcastMessage("closeHorn", "claw");
                gripWait = true;
            }
        }
    }

    public void IAgrabL(InputAction.CallbackContext ctx)
    {
        if (!going) return;

        if (ctx.phase == (InputActionPhase)3)
        {
            if (mode == TPos.INTAKING || mode == TPos.PLACING)
            {
                toIntermediate = false;
                BroadcastMessage(lOpen ? "closeHorn" : "openHorn", "L");
                lOpen = !lOpen;
                if (lOpen) { lProg = prog.TOOPEN; }
                else { lProg = prog.TOCLOSE; }
            }
        }
    }

    public void IAgrabR(InputAction.CallbackContext ctx)
    {
        if (!going) return;

        if (ctx.phase == (InputActionPhase)3)
        {
            toIntermediate = false;
            if (mode == TPos.INTAKING || mode == TPos.PLACING)
            {
                BroadcastMessage(rOpen ? "closeHorn" : "openHorn", "R");
                rOpen = !rOpen;
                if (rOpen) { rProg = prog.TOOPEN; }
                else { rProg = prog.TOCLOSE; }
            }
        }
    }


    public void IAmoveArm(InputAction.CallbackContext ctx)
    {
        if (!going) return;

        if (ctx.phase == (InputActionPhase)3)
        {

            if (mode == TPos.INTAKING_INTERMEDIATE || mode == TPos.DRIVING_INTERMEDIATE)
            {
                mode = TPos.PLACING;
                toIntermediate = false;
                BroadcastMessage("openHorn", "arm");

            }
            else if (mode == TPos.PLACING)
            {
                BroadcastMessage("closeHorn", "all");
                slideReady = true;
                BroadcastMessage("slideDown");

            }
        }
    }
    public void IAmoveSlides(InputAction.CallbackContext ctx)
    {
        if (!going) return;

        if ((int)ctx.phase == 4)
        {
            gameObject.BroadcastMessage("noSlideVelocity");
        }
        else if ((int)ctx.phase == 3)
        {
            gameObject.BroadcastMessage("setSlideVelocity", ctx.ReadValue<Vector2>().y);
        }
        //move up * ctx, angle of 60 from ground, broad cast it to slides if we are in intake mode, else not
    }
    public void start()
    {
        going = true;
    }

}

//prog enum
public enum prog
{
    CLOSE, TOCLOSE, TOOPEN, OPEN
}

//attributes of TPos (claw and ) 
public class TPosAttr : Attribute
{
    internal TPosAttr(float axonAngle, float clawAngle)
    {
        this.axonAngle = axonAngle;
        this.clawAngle = clawAngle;
    }
    public double axonAngle { get; private set; }
    public double clawAngle { get; private set; }
}

//medium to do the things
public static class TPosClass
{

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
        return (TPosAttr)Attribute.GetCustomAttribute(ForValue(pos), typeof(TPosAttr));
    }
    private static MemberInfo ForValue(TPos p)
    {
        return typeof(TPos).GetField(Enum.GetName(typeof(TPos), p));
    }

}

//actual enum
public enum TPos
{
    [TPosAttr(0, 0)] INTAKING,
    [TPosAttr(0, -100)] INTAKING_INTERMEDIATE,
    [TPosAttr(0, -150)] DRIVING_INTERMEDIATE,
    [TPosAttr(140, -160)] PLACING
}