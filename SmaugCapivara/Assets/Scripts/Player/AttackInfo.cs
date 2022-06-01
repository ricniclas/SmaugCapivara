using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackInfo : MonoBehaviour
{
    private AttackClass attackClass;

    private void Awake()
    {
        attackClass = new AttackClass();
    }

    public void setAttackClass(int damage, AttackType attackType)
    {
        attackClass.setValues(damage, attackType);
    }

    public AttackClass getAttackClass()
    {
        return attackClass;
    }

}

public enum AttackType
{
    LIGHT,
    MEDIUM,
    HEAVY,
    EXTREME
}
