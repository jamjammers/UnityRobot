using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static MotorClass;

public class DriveTrain : MonoBehaviour
{
    private double velocityX = 0;

    public Rigidbody rb;
    public GameObject center;
    public float Horiz;
    public float Vert;

    public float accelX;
    public float accelZ;
    public float accelRot;

    public bool moveX = true;
    public bool moveZ = true;

    //future stuff, individual wheel involvement
    Motor backLeft;
    Motor frontLeft;
    Motor backRight;
    Motor frontRight;



    // [SerializeField]
    // private InputActionReference move;
    // Start is called before the first frame update
    void Start()
    {
        backLeft = new Motor(rotDirection.CLOCKWISE, strafeDirection.LEFT);
        frontLeft = new Motor(rotDirection.CLOCKWISE, strafeDirection.RIGHT);

        backRight = new Motor(rotDirection.COUNTERCLOCKWISE, strafeDirection.RIGHT);
        frontRight = new Motor(rotDirection.COUNTERCLOCKWISE, strafeDirection.LEFT);
    }

    // Update is called once per frame
    void Update()
    {
        // Horiz = move.ReadValue<Vector2>().x;
        // Vert = move.ReadValue<Vector2>().y;
        // Debug.Log(move.ReadValue<Vector2>());
        
    }
    void FixedUpdate()
    {
        // str = (string) Horiz;


        moveX = Mathf.Abs(rb.velocity.x) < (10 * Mathf.Abs(accelX));
        moveZ = Mathf.Abs(rb.velocity.z) < (10 * Mathf.Abs(accelZ));
         if(moveX){
             rb.velocity = new Vector3(rb.velocity.x + accelX * 1.5f, 0, rb.velocity.z);
         }
         if(moveZ){
             rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z+accelZ * 1.5f);
         }

         Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, 100) * accelRot * Time.fixedDeltaTime);
         rb.MoveRotation(rb.rotation*rotation);
        // Debug.Log(Input.GetAxis("Horiz"));

    }
    //util
    int getPolarity(int x){ return (int)((float) x / Mathf.Abs((float) x)); }
    int getPolarity(double x){ return (int)((float) x / Mathf.Abs((float) x)); }
    int getPolarity(float x){ return (int)((float) x / Mathf.Abs((float) x)); }

    //stuff idk
    void roboCentric(float strafement, float movement){

    }

    //inputmangerstuff
    public void onMove(InputAction.CallbackContext ctx){
        accelX = ctx.ReadValue<Vector2>().x;
        accelZ = ctx.ReadValue<Vector2>().y;
    }
    public void reset(InputAction.CallbackContext ctx){
    //only does it on press, not when you release the button
        if(ctx.phase == (InputActionPhase)3){
            transform.position = new Vector3(0, 0.5f, 0);
            transform.rotation = Quaternion.Euler(-90, 0, -90);
        }
    }
    public void rotate(InputAction.CallbackContext ctx){
        accelRot = ctx.ReadValue<Vector2>().x;
    }

    //enum :skull:
    public enum rotDirection{
        CLOCKWISE, COUNTERCLOCKWISE
    }
    public enum strafeDirection{
        RIGHT, LEFT
    }
    //classes
    public class Motor
    {
        rotDirection rotDir;
        strafeDirection strafeDir;
        public Motor(rotDirection rd, strafeDirection sd){
            rotDir = rd;
            strafeDir = sd;
        }
    }
}
