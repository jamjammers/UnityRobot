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

    public Material[] Colors = {};

    public Collider target;

    public Rigidbody rb;

    bool grabbed = false;
    void Start()
    {
        gameObject.GetComponent<MeshRenderer> ().material = Colors[(int)UnityEngine.Random.Range(0,Colors.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
        tRotEuler = transform.rotation.eulerAngles;
        if(c>1&&anyBase){
            Destroy(rb);
            
            transform.SetParent(target.gameObject.transform.parent.parent);
            adjustPixel();
            c = 0;
            grabbed = true;
        }
        wasBase = isBase;
        isBase = false;
        for(int i = baseChain.Length-1;i>0;i--){
            baseChain[i] = baseChain[i-1];
        }
        baseChain[0] = wasBase;
        anyBase = chain(baseChain);
        c=0;
        //designed to auto release if it opens (a catch kinda idea)
        if(grabbed){
            //nest is designed to prevent errors
            if(transform.parent.GetComponent<HornScript>().open){ release(); }
            
        }
    }
    //collision check
    void OnCollisionStay(Collision col){
        
        Collider child = col.GetContact(0).otherCollider;
        if(child.name == "Squisher"){
            target = child;
            c++;

        }else if(child.name == "SquisherB"){
            isBase = true;
        }
        
    }
    //drop pixel
    public void release(){
        grabbed = false;
        c=0;
        rb = gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
        rb.angularDrag = 12;
        rb.drag = 12;
        transform.parent = null;
    }
    private bool chain(bool[] ch){
        foreach(bool x in ch){
            if(x){return true; }
        }
        return false;
    }
    //when its up the pixels will go to the right place
    public void adjustPixel(){
        bool isRight = transform.parent.name == "RightGrips";
        transform.localEulerAngles = new Vector3(90, 0, 90);
        transform.localPosition = new Vector3(isRight? -0.033f : 0.025f, 0.115f, -0.0036f);
    }
}
