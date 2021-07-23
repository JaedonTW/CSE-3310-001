using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// A class to encapsulate all objects that can be damaged.
/// </summary>
public class Hurtable : MonoBehaviour
{
    /// <summary>
    /// The different classes of characters that can be damaged
    /// </summary>
    public enum DamegeGroups : byte
    {
        Player = 0,
        Enemy,
        Friendly,
    }
    // public variables
    public DamegeGroups DamageGroup { get; internal set; }
    /// <summary>
    /// The current health of this object, is input parameter.
    /// </summary>
    public int health;
    /// <summary>
    /// An optional weapon to be used by this hurtable object.
    /// </summary>
    public Weapon weapon;
    // private variables
    /// <summary>
    /// The Rigidbody2D of this object, automatically collected by Start().
    /// </summary>
    internal Rigidbody2D body;
    // public methods
    /// <summary>
    /// A method to be called when this 'Hurtable' is damaged.
    /// </summary>
    public virtual void OnDeath()
    {
        Destroy(gameObject);
    }
    /// <summary>
    /// A function for changing the health of this object
    /// </summary>
    /// <param name="change">The change in health</param>
    public virtual void ChangeHealth(int change)
    {
        health += change;
        if (health <= 0)
            OnDeath();
    }
    // protected methods
    // Start is called before the first frame update
    protected virtual void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }
}
