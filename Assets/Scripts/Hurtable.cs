using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtable : MonoBehaviour
{
    // public variables
    public int health;
    public Weapon weapon;
    // private variables
    protected Rigidbody2D body;
    // public methods
    public virtual void Kill()
    {
        Destroy(this.gameObject);
    }
    public void ChangeHealth(int change)
    {
        health += change;
        if (health <= 0)
            Kill();
    }
    // protected methods
    // Start is called before the first frame update
    protected virtual void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }
}
