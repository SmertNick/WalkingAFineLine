using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private KeyBindings keyBindings;
    [SerializeField] private MouseSettings mouseSettings;
    [SerializeField] private Pool objecPool;

    public KeyBindings KeyBindings => keyBindings;
    public MouseSettings MouseSettings => mouseSettings;
    public IBulletProvider BulletProvider => objecPool;
    public IParticlesProvider PartilcesProvider => objecPool;
    
    public static GameManager instance;

    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
    }
}
