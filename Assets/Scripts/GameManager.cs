using UnityEngine;

public class GameManager : MonoBehaviour
{

    public PlayerScript player1;
    public PlayerScript player2;
    public Enemy enemy;

    public Camera mainCamera;
    private Vector3 camForward;
    private Vector3 camPos;

    public int cameraMaxDistance = 23;
    public int cameraMinDistance = 17;


    void Start(){}

    void Update()
    {
        moveCameraFromPlayer(player1.player);
        moveCameraFromPlayer(player2.player);
    }

    private void moveCameraFromPlayer(CharacterController player) 
    {
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
    }

    public void RespawnPlayers()
    {
        player1.player.enabled = false;
        player1.player.transform.position = player1.spawnPoint;
        player1.player.enabled = true;
        
        player2.player.enabled = false;
        player2.player.transform.position = player2.spawnPoint;
        player2.player.enabled = true;

        mainCamera.transform.position = new Vector3(
                mainCamera.transform.position.x,
                mainCamera.transform.position.y,
                player1.transform.position.z - 18f
            );    
        
        enemy.transform.position = enemy.spawnPoint;

        

    }

    public Vector3 getCamRight() 
    {
        Vector3 camRight = mainCamera.transform.right;
        camRight.y = 0;
        return camRight.normalized;
    }
    

    public Vector3 getCamFoward() 
    {
        Vector3 camForward = mainCamera.transform.forward;
        camForward.y = 0;
        return camForward.normalized;
    }


}
