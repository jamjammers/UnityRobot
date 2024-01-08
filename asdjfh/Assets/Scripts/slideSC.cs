using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slideSC : MonoBehaviour
{
    public string type = "outer";//outer vs center

    public float height;

    public IntakeManager manager;
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
        /* if(manager.isplacing())
         * if height is really differnt from transform.y
         * transform.translate(new Vector3(0,0,coeff));
         * transform.translate(new Vector3(coeff/tan(60) which is root 3,0,0))
         * else position = new Vector3(height/root3,0,height)
        */ 
    }
    public void setArm(float h){
        height = (type == "center"?0.5f:1)*h;
    }
}
