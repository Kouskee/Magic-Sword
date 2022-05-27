using Manager;
using UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private InventoryVisualisation _inventoryVis;
    private StrafeVisualisation _strafeVis;
    private SwitchTargetUnit _switchTargetUnit;

    public void Init(InventoryVisualisation inventoryVis, StrafeVisualisation strafeVis, SwitchTargetUnit switchTargetUnit)
    {
        _inventoryVis = inventoryVis;
        _strafeVis = strafeVis;
        _switchTargetUnit = switchTargetUnit;
    }

    private void Awake()
    {
        AddListenerNonBehavior();
    }

    private void AddListenerNonBehavior()
    {
        GlobalEventManager.OnUseAbility.AddListener(id => _inventoryVis.OnUseAbility(id)); 
        GlobalEventManager.OnStrafe.AddListener(dur => _strafeVis.OnStrafe(dur));
        GlobalEventManager.OnDestroyTargetEnemy.AddListener(unit => _switchTargetUnit.OnDestroyTargetEnemy(unit));
    }

    void Start()
    {
        _inventoryVis.Start();
    }
}