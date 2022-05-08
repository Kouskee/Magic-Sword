using System.Collections;
using Enemy;
using UnityEngine;

public abstract class State : ScriptableObject
{
    public Unit Unit { get; set; }
    public Transform Player { get; set; }
    public UnitMoveConfig Config { get; set; }

    public bool IsFinished { get; protected set; }

    public abstract void Init(float duration = default);

    public virtual IEnumerator UpdateDelay()
    {
        yield return new WaitForSeconds(0.1f);
    }
    public virtual void Exit(){}

    public virtual void Update() { }
}