using System;
using UnityEngine;

public class SlotSpell : MonoBehaviour
{
    public event Action<CastAbility> Changed;

    public CastAbility Abibilty { get; set; }
}
