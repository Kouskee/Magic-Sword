using Manager;
using TMPro;
using UnityEngine;

public class TextCoins : MonoBehaviour
{
    private TMP_Text _txtCoins;
    private int _coins;

    public void Start()
    {
        TryGetComponent(out _txtCoins);
        
        GlobalEventManager.OnEnemyKilled.AddListener(OnEnemyKilled);
    }

    private void OnEnemyKilled()
    {
        _coins++;
        _txtCoins.text = _coins.ToString();
    }
}
