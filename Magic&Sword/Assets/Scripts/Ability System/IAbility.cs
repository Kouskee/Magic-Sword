public interface IAbility
{
    float Cost { get; }
    void Use();
    bool CanUse();
}
