using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtable : MonoBehaviour
{
    protected int health;
    public Weapon weapon;
    public Rigidbody2D body;
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
