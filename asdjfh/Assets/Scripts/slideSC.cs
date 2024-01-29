using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slideSC : MonoBehaviour
{
    public string type = "outer";//outer vs center

    public float idealHeight;

    public float currentHeight;

    public Vector3 locPos;

    public IntakeManager manager;

    public float speedCoeff;

    public float typeCoeff;

    public bool going = false;

    float SLIDEMAX = 0.25f;//0.3?
    float SLIDEMIN = 0;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Intake (Manager)").GetComponent<IntakeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        typeCoeff = (type == "inner")?0.5f:1;
        speedCoeff = 0.1f;
    }
    void FixedUpdate(){
        locPos = transform.localPosition;
        currentHeight = transform.localPosition.z;
        /* if(manager.isplacing())
         * if height is really differnt from transform.y
         * transform.translate(new Vector3(0,0,coeff));
         * transform.translate(new Vector3(coeff/tan(60) which is root 3,0,0))
         * else position = new Vector3(height/root3,0,height)
        */ 
        if(transform.localPosition.z > SLIDEMAX){
            if(manager.getMode()==TPos.PLACING){
                if(idealHeight < currentHeight - 0.01 || idealHeight > currentHeight + 0.02){
                    float dir = (idealHeight < currentHeight)?-1:1;
                    transform.Translate(new Vector3(dir * typeCoeff * speedCoeff / Mathf.Sqrt(3), 0,dir * typeCoeff * speedCoeff));
                }else{
                    going = false;
                    transform.localPosition = new Vector3(typeCoeff * idealHeight/Mathf.Sqrt(3), 0, typeCoeff * idealHeight);
                }
            }else{
                going = false;
            }
        }
    }
    public void setSlideVelocity(float h){
        going = true;
        idealHeight = h;
    }
}
