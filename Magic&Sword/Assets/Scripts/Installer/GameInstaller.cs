using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameInstaller : MonoBehaviour
{
    [SerializeField] private CastAbility _castAbility;
    [SerializeField] private SlotManager _slotManager;
    [SerializeField] private Energy _energy;
    
    private void Start()
    {
        Install();
        
        Destroy(gameObject);
    }

    private void Install()
    {
        _castAbility.Install(_slotManager, _energy);
    }
}
