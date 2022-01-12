public interface IAbilityFactory
{
    bool CanCreate(int id);
    IAbility Create(int id);
}
