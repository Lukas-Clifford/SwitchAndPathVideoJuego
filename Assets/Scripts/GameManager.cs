using UnityEngine;

public class GameManager : MonoBehaviour
{

    public PlayerScript player1;
    public PlayerScript player2;


    void Start(){}

    void Update()
    {
        
    }

    public void RespawnPlayers()
    {
        player1.player.enabled = false;
        player1.player.transform.position = player1.spawnPoint;
        player1.player.enabled = true;
        
        player2.player.enabled = false;
        player2.player.transform.position = player2.spawnPoint;
        player2.player.enabled = true;

    }

}
