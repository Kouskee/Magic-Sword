using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Singleton { get; private set; }

    private void Awake()
    {
        if (!Singleton)
        {
            Singleton = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(gameObject);
    }

    public void WinGame()
    {
        
    }

    public void Death()
    {
        
    }
    
    void Update()
    {
        
    }
}
