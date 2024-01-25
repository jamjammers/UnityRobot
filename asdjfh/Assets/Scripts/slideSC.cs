using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slideSC : MonoBehaviour
{
    public string type = "outer";//outer vs center

    public float idealHeight;

    public float currentHeight;

    public IntakeManager manager;

    public float speedCoeff;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Intake (Manager)").GetComponent<IntakeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate(){
        transform.position.y
        /* if(manager.isplacing())
         * if height is really differnt from transform.y
         * transform.translate(new Vector3(0,0,coeff));
         * transform.translate(new Vector3(coeff/tan(60) which is root 3,0,0))
         * else position = new Vector3(height/root3,0,height)
        */ 
        if(manager.getMode()==TPos.PLACING){
            if(idealHeight>currentHeight+0.1||idealHeight<currentHeight+0.1){
                transform.translate(new Vector3(speedCoeff/Mathf.Sqrt(3), 0, speedCoeff));
            }else{
                transform.position = new Vector3(idealHeight/Mathf.Sqrt(3), transform.position.z, height);
            }
        }
    }
    public void setSlide(float h){
        idealHeight = (type == "center"?0.5f:1)*h;
    }
}
