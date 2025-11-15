using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Interface that makes classes provide an attack direction, used by the AttackComponent
public interface IAttackDirectionProvider
{
    public Vector3 GetNormalizedAttackDirectionVector();
}
