using System.Collections;
using Enemy;

public interface IDebuff
{
    void Activate(IUnitDebuff unitDebuff, Unit unit, int stack);
    IEnumerator Tick(float resist, float health);
    void Destroy();

    TypeDamage TypeDamage { get; }
}
