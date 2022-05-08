namespace Enemy
{
    public interface IUnitDebuff
    {
        public void AddDebuff(IDebuff debuff);
        public void RemoveDebuff(IDebuff debuff);
        public void SetHealth(float health);
        public void Death();
    }
}