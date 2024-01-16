using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class pixelSC : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Vector3 tRotEuler;
    public int count = 0;
    public int c = 0;
    public List<Collider> d;

    public Rigidbody rb;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        count = d.Count;
        if(count>2){
            Destroy(rb);
            transform.SetParent(d[0].gameObject.transform.parent);
        }
        if(count != 0){
            c=count;
        }
        d.Clear();
        // count = 0;   
    }
    // void OnCollisionEnter(Collision col){
    //     Collider child = col.GetContact(0).otherCollider;
    //     // Debug.Log(child.name);
    //     if(child.name == "Squisher"||child.name =="aCol"){
    //         Debug.Log(child.name);

    //         count ++;
    //     }
    // }
    void OnCollisionStay(Collision col){
        
        Collider child = col.GetContact(0).otherCollider;
        if(child.name == "Squisher"){
            d.Add(child);

        }
        // Collider test = col.GetContact(1).otherCollider;
        
        // Debug.Log(child.name);
        // if(child.name == "Squisher"||child.name =="aCol"){
        //     Debug.Log(child.name);

        //     count ++;
        // }
        // count = d.Count;

    }
    // void OnCollisionExit(Collision col){
    //     if(count>0&&col.gameObject.name == "Main Assembly"){
    //         count--;
    //     }
        
    // }
    public void release(){
        Debug.Log("Baudf");
        rb = gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
        rb.angularDrag = 4;
        rb.drag = 4;
        transform.parent = null;
    }
}
