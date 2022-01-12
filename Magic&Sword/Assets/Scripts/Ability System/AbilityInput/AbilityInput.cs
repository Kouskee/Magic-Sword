using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityInput : MonoBehaviour
{
    private InputPlayer _inputPlayer;

    private void OnEnable()
    {
        _inputPlayer = new InputPlayer();
        _inputPlayer.CharacterControls.Enable();
        _inputPlayer.CharacterControls.InputNumbers.performed += OnAbilityPerformed;
    }

    private void OnAbilityPerformed(InputAction.CallbackContext context)
    {
        int id = (int)context.ReadValue<float>();
        UseAbility(id);
    }
    
    private void UseAbility(int id)
    {
        EventManager.SendPressedButtonAbilityK(id);
    }

    private void OnDisable()
    {
        _inputPlayer.CharacterControls.InputNumbers.performed -= OnAbilityPerformed;
        _inputPlayer.CharacterControls.Disable();
    }
}
