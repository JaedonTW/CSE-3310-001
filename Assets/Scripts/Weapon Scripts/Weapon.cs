using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// A generic class for weapons
/// </summary>
public abstract class Weapon : MonoBehaviour
{
    public sbyte ID = -1;
    public Rigidbody2D body;
    public Hurtable.DamegeGroups ignoring;
    public int rechargeTime;
    public abstract void Use(float angle);
    public abstract void Tick();
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
