using System;

public class EventManager
{
    public static Action<int> OnPressedAbilityKeyboard;

    public static void SendPressedButtonAbilityK(int id)
    {
        OnPressedAbilityKeyboard?.Invoke(id);
    }
}
