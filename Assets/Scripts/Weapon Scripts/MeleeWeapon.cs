using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    public int damage;
    public float rangeSqrd;
    //
    // remaining time in ticks
    int RemainingTime { get; set; }
    SpriteRenderer spriteRenderer;

    protected override void Use(float angle, Hurtable target = null)
    {
        if ((target.body.position - body.position).sqrMagnitude <= rangeSqrd)
            target.ChangeHealth(-damage);
    }

}
