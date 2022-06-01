using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackClass : MonoBehaviour
{
    public int damage;
    public AttackType attackType;

    public void setValues(int damage, AttackType attackType)
    {
        this.damage = damage;
        this.attackType = attackType;
    }
}
