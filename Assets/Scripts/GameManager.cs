using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private KeyBindings keyBindings;
    [SerializeField] private MouseSettings mouseSettings;
    [SerializeField] private Pool objecPool;
    [SerializeField] private EndGameText endGameText;

    public KeyBindings KeyBindings => keyBindings;
    public MouseSettings MouseSettings => mouseSettings;
    public IBulletProvider BulletProvider => objecPool;
    public IParticlesProvider ParticlesProvider => objecPool;
    
    public static GameManager instance;

    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
    }

    public void RestartGame(GameEndType gameEndType)
    {
        endGameText.ShowText((int)gameEndType);
        StartCoroutine(RestartGameTimer(3f));
    }
    
    
    private IEnumerator RestartGameTimer(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(0);
    }
}
