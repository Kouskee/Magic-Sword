using UnityEngine;

public class CastAbility : MonoBehaviour
{
    private Timer _timer;
    private bool _canCast = true;
    private int _idButton;
    private AbstractSpell _ability;
    private SlotManager _slotManager;
    private Energy _energy;

    private float _canCastAfterTime = float.MinValue;

    private void Awake()
    {
        _timer = new Timer(0);

        EventManager.OnPressedAbilityKeyboard += GetPressedAbility;
    }

    public void Install(SlotManager slotManager, Energy energy)
    {
        _slotManager = slotManager;
        _energy = energy;
    }

    private void GetPressedAbility(int id)
    {
        _idButton = id;
        _ability = _slotManager.GetAbilityFromSlot(id);

        if (_ability != null)
            CallUseAbility();
    }

    private void CallUseAbility()
    {
        float cost = _ability.AbilityConfig.Cost;
        float coolDown = _ability.AbilityConfig.Cooldown;
        bool canCastEnergy = _energy.CanCast(cost);
        if (canCastEnergy)
        {
            // switch (_idButton)
            // {
            //     case 1:
            //     {
            //         // if (_timerTwo <= 0)
            //         // {
            //         //     _energy.StealEnergy(cost);
            //         //     _timerTwo = coolDown;
            //         // }
            //         
            //     }
            //         break;
            //     case 2:
            //     {
            //         
            //     }
            //         break;
            //     case 3:
            //     {
            //         _energy.StealEnergy(cost);
            //         _timer = new Timer(coolDown);
            //         _timer.TimeIsUp += OnTimeIsUp;
            //         _canCast = false;
            //     }
            //         break;
            //     case 4:
            //     {
            //         _energy.StealEnergy(cost);
            //         _timer = new Timer(coolDown);
            //         _timer.TimeIsUp += OnTimeIsUp;
            //         _canCast = false;
            //     }
            //         break;
            // }
        }

        // if (_canCastAfterTime <= Time.time)
        // {
        //     //Debug.Log(_canCastAfterTime + " " + Time.time);
        //     _energy.StealEnergy(cost);
        //     _canCastAfterTime = coolDown + Time.time;
        // }

        _ability.CanUse();
        
    }

    private void OnTimeIsUp()
    {
        _timer.TimeIsUp -= OnTimeIsUp;
        _canCast = true;
    }

    private void Update()
    {
        _timer.Update(Time.deltaTime);
    }
}