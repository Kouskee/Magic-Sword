using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityInput : MonoBehaviour
{
    private InputPlayer _inputPlayer;

    private void Awake()
    {
        _inputPlayer = new InputPlayer();
    }

    private void OnEnable()
    {
        _inputPlayer.CharacterControls.Enable();
        _inputPlayer.CharacterControls.CastAbility.performed += OnCastAbility;
    }
    
    private void OnCastAbility(InputAction.CallbackContext obj)
    {
        AbilityPerformed(obj.ReadValue<float>());
    }

    private void AbilityPerformed(float button)
    {
        int id = (int)button;
        UseAbility(id);
    }
    
    private void UseAbility(int id)
    {
        EventManager.SendPressedButtonAbilityK(id);
    }

    private void OnDisable()
    {
        _inputPlayer.CharacterControls.CastAbility.performed -= OnCastAbility;
        _inputPlayer.CharacterControls.Disable();
    }
}
