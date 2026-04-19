using UnityEngine;

public class PlayerScript : MonoBehaviour
{


    private float horizontalMove = 0f;
    private float verticalMove = 0f;

    public float velocity = 5f;
    public float JumpForce = 5f;
    public bool isGrounded;

    private Rigidbody rigidBody;


    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {    
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(horizontalMove,0,verticalMove) * Time.deltaTime * velocity);
        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

    }

    public void Jump() 
    {
        if (isGrounded)
        {
            rigidBody.AddForce(new Vector3(0,JumpForce, 0), ForceMode.Impulse);
        }
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


}
