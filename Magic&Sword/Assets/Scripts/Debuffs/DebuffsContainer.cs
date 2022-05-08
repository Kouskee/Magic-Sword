using System;
using System.Collections.Generic;
using Debuffs;

namespace Debufs
{
    public class DebuffsContainer
    {
        private List<IDebuff> _debuffs;
        private TypeDamage _typeDamage;
        
        public DebuffsContainer(TypeDamage typeDamage)
        {
            _debuffs = new List<IDebuff>();
            _typeDamage = typeDamage;
            
            switch (typeDamage)
            {
                case TypeDamage.Fire:
                    AddFireDebuffs();
                    break;
                case TypeDamage.Water:
                    AddWaterDebuffs();
                    break;
                case TypeDamage.Earth:
                    AddEarthDebuffs();
                    break;
                case TypeDamage.Air:
                    AddAirDebuffs();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(typeDamage), typeDamage, null);
            }
        }
        
        private void AddFireDebuffs()
        {
            IDebuff burning = new Burning(_typeDamage);
            
            _debuffs.Add(burning);
        }
        
        private void AddWaterDebuffs()
        {
            IDebuff cold = new Cold(_typeDamage);
            
            _debuffs.Add(cold);
        }

        private void AddEarthDebuffs()
        {
            
        }
        
        private void AddAirDebuffs()
        {
            
        }

        public List<IDebuff> GetDebuffs() => _debuffs;
    }
}