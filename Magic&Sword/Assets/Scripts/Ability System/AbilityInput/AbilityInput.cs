using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityInput : MonoBehaviour
{
    private InputPlayer _inputPlayer;

    private void OnEnable()
    {
        _inputPlayer = new InputPlayer();
        _inputPlayer.CharacterControls.Enable();
        _inputPlayer.CharacterControls.InputNumbers.performed += AbilityPerformed;
    }

    private void AbilityPerformed(InputAction.CallbackContext context)
    {
        int id = (int)context.ReadValue<float>();
        if(id >= 1 && id <= 4 ) UseAbility(id);
    }
    
    private void UseAbility(int id)
    {
        EventManager.SendPressedButtonAbilityK(id);
    }

    private void OnDisable()
    {
        _inputPlayer.CharacterControls.InputNumbers.performed -= AbilityPerformed;
        _inputPlayer.CharacterControls.Disable();
    }
}
