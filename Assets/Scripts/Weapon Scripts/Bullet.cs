using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    //public Collider2D collider;
    public Rigidbody2D body;
    internal Hurtable.DamegeGroups ignoring;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() =>
        // sets the direction the bullet is facing to be the same as the direction the bullet is moving
        body.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(body.velocity.y, body.velocity.x)*Mathf.Rad2Deg);
    //
    /// <summary>
    /// Event for when the bullet hits something.
    /// </summary>
    /// <param name="collision">The collision event object</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Hurtable from = collision.GetComponentInParent<Hurtable>();
        // Checking if the object that was hit is hurtable
        if (from != null)
            // Runs the friendly fire check
            if (ignoring == from.DamageGroup)
                return;
            // deals damage to contacting object
            else from.ChangeHealth(-damage);
        // destroys the bullet
        Destroy(gameObject);
    }
}
