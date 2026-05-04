using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameManager gameManager;

    private bool isMoving = true;
    private int stunCountdown = 10;
    public Vector3 spawnPoint;

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
                this.transform.position.z + 0.02f             
            );  
        }
        else 
        {
            if (stunCountdown > 0)
            {
                stunCountdown -= 1;
            }
            else 
            {
                stunCountdown = 2;
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
