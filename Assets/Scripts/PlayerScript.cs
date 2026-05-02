using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Animator animator;
    
    public CharacterController player;
    public Vector3 spawnPoint;

    private float horizontalMove = 0f;
    private float verticalMove = 0f;
    
    private Vector3 playerMovement;
    private Vector3 playerInput;

    private float gravity = 9.8f;

    public float speed = 5f;
    public float fallVelocity = 0f;
    public float JumpForce = 5f;


    public Camera mainCamera;
    private Vector3 camForward;
    private Vector3 camRight;
    private Vector3 camPos;

    public int cameraMaxDistance = 23;
    public int cameraMinDistance = 17;


    public bool isPlayerOne = true;
    public GameManager gameManager;
    
    
    public Transform shootingPoint;
    public Rigidbody projectilePrefab;
    public float shootForce = 20f;


    void Start()
    {
        player = GetComponent<CharacterController>();
    }

    void Update()
    {    

        if (isPlayerOne)
        {
            horizontalMove = Input.GetAxis("Horizontal_P1");
            verticalMove = Input.GetAxis("Vertical_P1");
        } 
        else 
        {
            horizontalMove = Input.GetAxis("Horizontal_P2");
            verticalMove = Input.GetAxis("Vertical_P2");
        }
        
        

        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);
        if (playerInput.magnitude > 0.1f) 
            animator.SetBool("running", true);
        else 
            animator.SetBool("running", false);


        playerMovement = playerInput.x * camRight + playerInput.z * camForward;
        playerMovement *= speed;

        player.transform.LookAt(player.transform.position + playerMovement);

        
        camDirection();
        SetGravity();
        PlayerSkills();

        player.Move(playerMovement*Time.deltaTime);

        
        camPos = mainCamera.transform.position;
        float distance = Vector3.Distance(camPos, player.transform.position);

        if(distance >= cameraMaxDistance) 
        {
            mainCamera.transform.position += (new Vector3(0,0,3) * Time.deltaTime);
        }
        else if (distance <= cameraMinDistance)
        {
            mainCamera.transform.position -= (new Vector3(0,0,3) * Time.deltaTime);
        }

        if (player.transform.position.y < -10)
        {
            gameManager.RespawnPlayers();
            mainCamera.transform.position = new Vector3(
                mainCamera.transform.position.x,
                mainCamera.transform.position.y,
                player.transform.position.z - 18f
            );        
        }


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


    public void SetGravity()
    {

        if (player.isGrounded)
        {
            fallVelocity = -gravity * Time.deltaTime;
            animator.SetBool("jumping", false);
        }
        else
        {
            fallVelocity -= gravity * Time.deltaTime;
        }

        playerMovement.y = fallVelocity;

    }

    private void PlayerSkills()
    {
        Jump();
        Shoot();
    }

    public void Jump() 
    {
        if(player.isGrounded && Input.GetButtonDown("Jump_P1") && isPlayerOne)
        {
            animator.SetBool("jumping", true);
            fallVelocity = JumpForce;
            playerMovement.y = fallVelocity;
        }
        else if(player.isGrounded && Input.GetButtonDown("Jump_P2") && !isPlayerOne)
        {
            animator.SetBool("jumping", true);
            fallVelocity = JumpForce;
            playerMovement.y = fallVelocity;
        }
    }

    public void Shoot()
    {
        if(Input.GetButtonDown("Shoot_P1") && isPlayerOne)
        {
            Rigidbody projectile = Instantiate(projectilePrefab, shootingPoint.position, shootingPoint.rotation);
            projectile.linearVelocity = shootingPoint.forward * shootForce;
            Destroy(projectile.gameObject, 1f); 

        } 
        else if(Input.GetButtonDown("Shoot_P2") && !isPlayerOne)
        {

            Rigidbody projectile = Instantiate(projectilePrefab, shootingPoint.position, shootingPoint.rotation);
            projectile.linearVelocity = shootingPoint.forward * shootForce;
            Destroy(projectile.gameObject, 1f); 

        }
        
    }
    

}
