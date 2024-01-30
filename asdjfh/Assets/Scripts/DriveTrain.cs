using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DriveTrain : MonoBehaviour
{
    //Edit → Project Settings → Physics → Gravity.
    public float fsAngle = 90;
    public Rigidbody rb;
    public GameObject center;
    public float Horiz;
    public float Vert;

    public Vector3 tRotEuler;

    public float accelX;
    public float accelZ;
    public float accelRot;

    
    public Vector3 outThing;

    public bool moveX;
    public bool moveZ;

    public float slow = 1;
    // [SerializeField]
    // private InputActionReference move;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Horiz = move.ReadValue<Vector2>().x;
        // Vert = move.ReadValue<Vector2>().y;
        // Debug.Log(move.ReadValue<Vector2>());
       
        
    }

    void upkeep(){
        tRotEuler = transform.rotation.eulerAngles;
        // tRotEuler = new Vector3(tRotEuler.x>180? tRotEuler.x-360:tRotEuler.x,tRotEuler.y>180? tRotEuler.y-360:tRotEuler.y, tRotEuler.z>180? 360:0 );
    }

    void FixedUpdate()
    {
        upkeep();

        smartFS(accelX, accelZ, accelRot);

        // roboCentric(accelX, accelZ, accelRot);

        //transform.rotation.z.toEuler;
        // transform.rotation = Quaternion.Euler(tRotEuler.x, tRotEuler.y, tRotEuler.z);
    }
    //util
    int getPolarity(int x){ return (int) ( (float) x / Mathf.Abs( (float) x)); }
    int getPolarity(double x){ return (int) ( (float) x / Mathf.Abs( (float) x)); }
    int getPolarity(float x){ return (int) ( (float) x / Mathf.Abs( (float) x)); }

    //stuff idk

    void dumbFieldCentric(float strafement, float movement, float rot){

        Vector3 IW = indWheel(strafement, movement, rot);

        moveX = Mathf.Abs(rb.velocity.x) < (10 * Mathf.Abs(IW.x));
        moveZ = Mathf.Abs(rb.velocity.z) < (10 * Mathf.Abs(IW.y));
        rb.velocity = rb.velocity + new Vector3(moveX? IW.x * 1.5f: 0, -0.1f, moveZ? IW.y * 1.5f: 0);
        //  if(moveX){
        //      rb.velocity = rb.velocity + new Vector3(IW.x * 1.5f, 5, 0);
        //  }
        //  if(moveZ){
        //      rb.velocity = rb.velocity + new Vector3(0, 5, IW.y * 1.5f);
        //  }

        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, 4) * IW.z);
        rb.MoveRotation(transform.rotation * rotation);
        // rb.MoveRotation(Quaternion.Euler(tRotEuler.x, 0,  tRotEuler.y+4));
    }

    void roboCentric(float strafement, float movement, float rot){

        Vector3 IW = indWheel(strafement, movement, rot);

        float angle = tRotEuler.y * Mathf.PI / 180;

        outThing = new Vector3(- Mathf.Cos(angle) * rb.velocity.z + Mathf.Sin(angle) * rb.velocity.x, 0, Mathf.Cos(angle) * rb.velocity.x + Mathf.Sin(angle) * rb.velocity.z);

        moveX = Mathf.Abs(Mathf.Cos(angle) * rb.velocity.z + Mathf.Sin(angle) * rb.velocity.x) < (10 * Mathf.Abs(IW.x));
        moveZ = Mathf.Abs( - Mathf.Cos(angle) * rb.velocity.x + Mathf.Sin(angle) * rb.velocity.z) < (10 * Mathf.Abs(IW.y));

        float zResult = (moveX? Mathf.Cos(angle) * IW.x : 0) + (moveZ? Mathf.Sin(angle) * IW.y : 0);
        float xResult = (moveZ? - Mathf.Cos(angle) * IW.y : 0) + (moveX? Mathf.Sin(angle) * IW.x : 0);

        rb.velocity = new Vector3(rb.velocity.x + xResult * 1.5f, -0.1f, rb.velocity.z + zResult * 1.5f);

        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, 200) * IW.z * Time.fixedDeltaTime);
        rb.MoveRotation(transform.rotation * rotation);
    }
    void smartFS(float strafement, float movement, float rot){
        Vector3 IW = indWheel(strafement, movement, rot);
        float angle = fsAngle * Mathf.PI / 180;
        outThing = new Vector3(- Mathf.Cos(angle) * rb.velocity.z + Mathf.Sin(angle) * rb.velocity.x, 0, Mathf.Cos(angle) * rb.velocity.x + Mathf.Sin(fsAngle) * rb.velocity.z);

        moveX = Mathf.Abs(Mathf.Cos(angle) * rb.velocity.z + Mathf.Sin(angle) * rb.velocity.x) < (10 * Mathf.Abs(IW.x));
        moveZ = Mathf.Abs( - Mathf.Cos(angle) * rb.velocity.x + Mathf.Sin(angle) * rb.velocity.z) < (10 * Mathf.Abs(IW.y));

        float zResult = (moveX? Mathf.Cos(angle) * IW.x : 0) + (moveZ? Mathf.Sin(angle) * IW.y : 0);
        float xResult = (moveZ? - Mathf.Cos(angle) * IW.y : 0) + (moveX? Mathf.Sin(angle) * IW.x : 0);

        rb.velocity = new Vector3(rb.velocity.x/1.1f + xResult * 2f, -0.1f, rb.velocity.z/1.1f + zResult * 2f);

        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, 200) * IW.z * Time.fixedDeltaTime);
        rb.MoveRotation(transform.rotation * rotation);

    }

    Vector3 indWheel(float x, float y, float r){


        float denominator = Mathf.Max(Mathf.Abs(x) + Mathf.Abs(y) + Mathf.Abs(r), 1);

        float leftFrontPower =  (y + x + r) / denominator * slow;
        float leftBackPower =   (y - x + r) / denominator * slow;
        float rightFrontPower = (y - x - r) / denominator * slow;
        float rightBackPower =  (y + x - r) / denominator * slow;


        float rotDiff = (leftFrontPower + leftBackPower - rightFrontPower - rightBackPower) / 3;
        float xDiff = (leftFrontPower - leftBackPower - rightFrontPower + rightBackPower) / 4;
        float zDiff = (leftFrontPower + leftBackPower + rightFrontPower + rightBackPower) / 4;

        return new Vector3(xDiff, zDiff, rotDiff);
    }
    //inputmangerstuff
    public void onMove(InputAction.CallbackContext ctx){
        accelX = ctx.ReadValue<Vector2>().x;
        accelZ = ctx.ReadValue<Vector2>().y;
    }

    public void reset(InputAction.CallbackContext ctx){
    //only does it on press, not when you release the button
        if(ctx.phase == (InputActionPhase) 3){
            fsAngle = tRotEuler.y;
        }
    }

    public void rotate(InputAction.CallbackContext ctx){
        accelRot = ctx.ReadValue<Vector2>().x;
    }
    public void slowmode(InputAction.CallbackContext ctx){
        slow = (ctx.phase == (InputActionPhase) 3)?0.4f:1f;
    }


    //classes

}
