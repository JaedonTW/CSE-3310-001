using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// A generic class for weapons
/// </summary>
public abstract class Weapon : MonoBehaviour
{
    private int RechargeTicksRemaining { get; set; }
    /// <summary>
    /// ID for this weapon
    /// </summary>
    public sbyte ID = -1;
    /// <summary>
    /// Rigid body that this weapon is attached to.
    /// </summary>
    public Rigidbody2D body;
    /// <summary>
    /// The weapon class that this weapon is not supposed to deal damage to.
    /// </summary>
    public Hurtable.DamageGroup ignoring;
    /// <summary>
    /// The amount of time required for this weapon to recharge.
    /// </summary>
    public int rechargeTime;
    protected abstract void Use(float angle, Hurtable target);
    /// <summary>
    /// A method for firing/attacking with this method
    /// </summary>
    /// <param name="angle">The angle to be attacking at</param>
    /// <param name="target">The desired target, left null if it does not matter.</param>
    public void AttemptUse(float angle, Hurtable target = null)
    {
        if(RechargeTicksRemaining == 0)
        {
            RechargeTicksRemaining = rechargeTime;
            Use(angle,target);
        }
    }
    /// <summary>
    /// Iterates the weapon, to be called each tick.
    /// </summary>
    public void Tick()
    {
        if (RechargeTicksRemaining > 0)
            RechargeTicksRemaining--;
    }
    /// <summary>
    /// Checks if 'a' and 'b' have the same ID.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static bool operator ==(Weapon a, Weapon b)
    {
        if (ReferenceEquals(a, null))
            return ReferenceEquals(b, null);
        else if (ReferenceEquals(b, null))
            return false;
        return a.ID == b.ID;
    }
    /// <summary>
    /// Checks if 'a' and 'b' have different ID's.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static bool operator !=(Weapon a, Weapon b)
    {
        if (ReferenceEquals(a, null))
            return !ReferenceEquals(b, null);
        else if (ReferenceEquals(b, null))
            return true;
        return a.ID != b.ID;
    }
}
