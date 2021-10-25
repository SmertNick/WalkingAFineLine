using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Detector : MonoBehaviour
{
    [SerializeField] private GameEndType gameEndType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RestartGame();
        }
    }

    private void RestartGame()
    {
        GameManager.instance.RestartGame(gameEndType);
    }

}

public enum GameEndType { Lose, Win }
