using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameManager gameManager;

    private bool isMoving = true;
    private float stunCountdown = 10;
    public Vector3 spawnPoint;

    public float speed = 1;

    void Start()
    {        
    }

    void Update()
    {
        if (isMoving)
        {
            this.transform.position = new Vector3(
                this.transform.position.x,
                this.transform.position.y,
                this.transform.position.z + speed * Time.deltaTime            
            );  
        }
        else 
        {
            if (stunCountdown > 0)
            {
                stunCountdown -= 1 * Time.deltaTime;
            }
            else 
            {
                stunCountdown = 10;
                isMoving = true;
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            isMoving = false;
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            
            gameManager.RespawnPlayers();
        }
    }
}
