using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalBothPlayersToMainMenu : MonoBehaviour
{
    [Header("Scene")]
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    [Header("Detection")]
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private int requiredPlayers = 2;

    // Guardamos qué jugadores están dentro (por InstanceID) para no contar doble
    private readonly HashSet<int> playersInside = new HashSet<int>();
    private bool loading;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;

        // Si el collider es de un hijo, esto intenta subir al root (útil si el jugador tiene varios colliders)
        var playerRoot = other.attachedRigidbody ? other.attachedRigidbody.gameObject : other.gameObject;

        playersInside.Add(playerRoot.GetInstanceID());

        TryLoad();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;

        var playerRoot = other.attachedRigidbody ? other.attachedRigidbody.gameObject : other.gameObject;

        playersInside.Remove(playerRoot.GetInstanceID());
    }

    private void TryLoad()
    {
        if (loading) return;

        if (playersInside.Count >= requiredPlayers)
        {
            loading = true;
            SceneManager.LoadScene(mainMenuSceneName);
        }
    }
}