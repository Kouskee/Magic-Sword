public interface IAbilityFactory
{
    bool CanCreate(string id);
    IAbility Create(string id);
}
