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

    float velo = 0;

    bool end = false;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Intake (Manager)").GetComponent<IntakeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        typeCoeff = (type == "inner")?0.5f:1;
        speedCoeff = 0.05f;
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
        if(going){
            if(manager.getMode()==TPos.PLACING){
                transform.Translate(new Vector3(1 / Mathf.Sqrt(3), 0, 1) * typeCoeff * velo * speedCoeff);
                if(transform.localPosition.z > SLIDEMAX){
                    transform.localPosition = new Vector3(1/Mathf.Sqrt(3), 0, 1) * typeCoeff * SLIDEMAX;
                    going = false;
                    velo = 0;
                }else if(transform.localPosition.z < SLIDEMIN){
                    transform.localPosition = new Vector3(1/Mathf.Sqrt(3), 0, 1) * typeCoeff * SLIDEMIN;
                    going = false;
                    velo = 0;
                    if(end){
                        end = false;
                        manager.complete("slides");
                    }
                }
            
            }else{
                going = false;
            }
        }
        
    }
    public void setSlideVelocity(float h){
        going = true;
        velo = h;
        Debug.Log(velo);
    }
    public void noSlideVelocity(){
        going = true;
        velo = 0;
        Debug.Log(velo);
    }
    public void slideDown(){
        velo = -1.25f;
        going = true;
        end = true;
    }
}
