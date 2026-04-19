using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    
    public CharacterController player;

    private float horizontalMove = 0f;
    private float verticalMove = 0f;
    
    private Vector3 playerMovement;
    private float gravity = 9.8f;

    private Vector3 playerInput;

    public float speed = 5f;
    public float fallVelocity;


    public float JumpForce = 5f;
    public bool isGrounded;


    public Camera mainCamera;
    private Vector3 camForward;
    private Vector3 camRight;
    

    private Rigidbody rigidBody;



    void Start()
    {
        player = GetComponent<CharacterController>();
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {    
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);
        
        camDirection();

        playerMovement = playerInput.x * camRight + playerInput.z * camForward;
        playerMovement *= speed;

        player.transform.LookAt(player.transform.position + playerMovement);

        SetGravity();
        
        Jump();

        player.Move(playerMovement*Time.deltaTime);


        


    }


    private void camDirection()
    {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;


        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;

    }


    public void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.tag == "Ground") 
        {
            isGrounded = true;
        }

    }

    public void OnCollisionExit(Collision collision) 
    {
        if (collision.gameObject.tag == "Ground") 
        {
            isGrounded = false;
        }
    }

    public void SetGravity()
    {

        if (player.isGrounded)
        {
            fallVelocity = -gravity * Time.deltaTime;
        }
        else
        {
            fallVelocity -= gravity * Time.deltaTime;
        }

        playerMovement.y = fallVelocity;

    }

    public void Jump() 
    {
        if(player.isGrounded && Input.GetButtonDown("Jump"))
        {
            fallVelocity = JumpForce;
            playerMovement.y = fallVelocity;
        }
    }


}
