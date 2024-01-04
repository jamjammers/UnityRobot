using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntakeManager : MonoBehaviour
{
    bool rOpen = false;
    bool lOpen = false;

    TPos mode = INTAKING;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    class TPosAttr : Attribute{
        internal TPosAttr(float slidePos, float clawAngle){
            this.slidePos = slidePos;
            this.clawAngle = clawAngle;
        }
        public double slidePos{get; private set;}
        public double clawAngle{get; private set;}
    }
    enum TPos{
        [TPosAttr(0,0)]INTAKING,
        [TPosAttr(0,-90)]INTAKING_INTERMEDIATE,
        INTERMEDIATE,
        PLACING,
    }

    
}
