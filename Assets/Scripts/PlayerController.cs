using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public CharacterController player;
    // public Camera mainCamera;
    private Rigidbody Physics;
    // Start is called before the first frame update
    public float Speed = 3.0f;
    public float Gravity = 9.8f;
    public float fallVelocity;
    public float JumpForce = 5.0f;
    public float RotationSpeed = 64.0f;
    public float horizontalMove;
    public float verticalMove;
    private float DistToGround;
    private float rotationY;
    private bool IsGrounded = true;
    private Vector3 movePlayer;


    void Start()
    {
        // Physics = GetComponent<Rigidbody>();
        player = GetComponent<CharacterController>();
    }

    // Update is called once per frame
  
    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag == "Floor"){
            IsGrounded = true;
        }
    }
    void Update()
    {
        //Movement
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");
        // transform.Translate(new Vector3(horizontal, 0.0f, vertical)* Time.deltaTime * Speed);
        // //Rotation
        rotationY = Input.GetAxis("Mouse X");
        // transform.Rotate(new Vector3(0.0f, rotationY * Time.deltaTime * RotationSpeed, 0.0f));
        //Jump
        // if(Input.GetKeyDown(KeyCode.Space) && IsGrounded){
        //     Physics.AddForce(new Vector3(0, JumpForce, 0), ForceMode.Impulse);
        //     IsGrounded = false;
        // }
    }
    private void FixedUpdate(){
        movePlayer = new Vector3(horizontalMove, 0.0f, verticalMove);
        movePlayer = movePlayer* Speed;
        SetGravity();
        PlayerSkills();
        player.Move(movePlayer* Time.deltaTime);
        player.transform.Rotate(new Vector3(0.0f, rotationY * Time.deltaTime * RotationSpeed, 0.0f));
        
        //   if(Input.GetKeyDown(KeyCode.Space) && IsGrounded){
        //     Physics.AddForce(new Vector3(0, JumpForce, 0), ForceMode.Impulse);
        //     IsGrounded = false;
        // }
    }

    //SKILLS

    public void PlayerSkills(){
        if(player.isGrounded && Input.GetKeyDown(KeyCode.Space)){
            fallVelocity = JumpForce;
            movePlayer.y = fallVelocity;
        }
    }
    void SetGravity(){
        if(player.isGrounded){
            fallVelocity = -Gravity * Time.deltaTime;
        }
        else{
            fallVelocity -= Gravity * Time.deltaTime;

        }
        movePlayer.y = fallVelocity;
    }
}
