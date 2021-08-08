using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    /// <summary>
    /// Damage dealt by this weapon per strike.
    /// </summary>
    public int damage;
    /// <summary>
    /// The square of the range for this weapon.
    /// </summary>
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
