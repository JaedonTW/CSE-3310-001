using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public Rigidbody2D body;
    public Hurtable.DamegeGroups ignoring;
    public int rechargeTime;
    public abstract void Use(float angle);
    public abstract void Tick();
}
