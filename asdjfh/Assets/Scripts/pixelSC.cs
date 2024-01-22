using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class pixelSC : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Vector3 tRotEuler;
    public int c = 0;

    public bool isBase = false;
    public bool wasBase = false;

    public bool[] baseChain = {false, false, false, false, false};
    public bool anyBase;


    public Collider target;

    public Rigidbody rb;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(c>1&&anyBase){
            Destroy(rb);
            transform.SetParent(target.gameObject.transform.parent.parent);
            c = 0;
        }
        wasBase = isBase;
        isBase = false;
        for(int i = baseChain.Length-1;i>0;i--){
            baseChain[i] = baseChain[i-1];
        }
        baseChain[0] = wasBase;
        //for(i){ chain[i] = chain[i-1] } chain[0] = wasBase;
        anyBase = chain(baseChain);
        c=0;
    }
    void OnCollisionStay(Collision col){
        
        Collider child = col.GetContact(0).otherCollider;
        if(child.name == "Squisher"){
            target = child;
            c++;

        }else if(child.name == "SquisherB"){
            isBase = true;
        }
        
    }
    public void release(){
        Debug.Log("Baudf");
        c=0;
        rb = gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
        rb.angularDrag = 4;
        rb.drag = 4;
        transform.parent = null;
    }
    private bool chain(bool[] ch){
        foreach(bool x in ch){
            if(x){return true; }
        }
        return false;
    }
}
