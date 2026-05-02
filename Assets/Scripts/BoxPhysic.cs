using UnityEngine;

public class CubeRespawn : MonoBehaviour
{
    [Header("Respawn")]
    public Transform spawnPoint;
    public float fallThreshold = -10f; // Altura Y mínima antes de respawnear

    private Rigidbody rb;
    private Vector3 spawnPosition;
    private Quaternion spawnRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Si hay spawnPoint usa su posición, si no usa la posición inicial del cubo
        if (spawnPoint != null)
        {
            spawnPosition = spawnPoint.position;
            spawnRotation = spawnPoint.rotation;
        }
        else
        {
            spawnPosition = transform.position;
            spawnRotation = transform.rotation;
        }
    }

    void Update()
    {
        if (transform.position.y < fallThreshold)
            Respawn();
    }

    void Respawn()
    {
        // Resetear física
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Reposicionar
        transform.position = spawnPosition;
        transform.rotation = spawnRotation;
    }
}