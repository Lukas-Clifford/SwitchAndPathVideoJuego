using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameManager gameManager;
    
    public CharacterController player;
    public bool isPlayerOne = true;

    public Animator animator;
    public Transform shootingPoint;
    public Rigidbody projectilePrefab;

    public float shootForce = 20f;

    public Vector3 spawnPoint;

    public float speed = 5f;
    public float fallVelocity = 0f;
    public float JumpForce = 5f;

    private float horizontalMove = 0f;
    private float verticalMove = 0f;
    
    private Vector3 playerMovement;
    private Vector3 playerInput;

    private float gravity = 9.8f;


    void Start()
    {}

    void Update()
    {    
        // asignar controles de movimiento
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
        
        // chapar el movimiento dentro del rango 0 y 1
        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);


        // movimiento según la camara
        playerMovement = playerInput.x * gameManager.getCamRight() + playerInput.z * gameManager.getCamFoward();
        playerMovement *= speed;

        player.transform.LookAt(player.transform.position + playerMovement);

        SetGravity();
        PlayerSkills();
        
        // mover al jugador
        player.Move(playerMovement*Time.deltaTime);
        
        // Booleanos de animaciones de correr
        if (playerInput.magnitude > 0.1f) 
            animator.SetBool("running", true);
        else 
            animator.SetBool("running", false);


        // comprobar si está bajo el nivel 10 para reiniciar el nivel
        if (player.transform.position.y < -10)
        {
            gameManager.RespawnPlayers();
        }
        
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
