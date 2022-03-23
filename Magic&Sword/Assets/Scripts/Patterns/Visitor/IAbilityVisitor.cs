using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbilityVisitor
{
    public void Visit(FlyingStone flyingStone);
    public void Visit(DimpleAbility dimpleAbility);
}
