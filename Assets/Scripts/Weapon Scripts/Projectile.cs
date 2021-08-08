using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    internal const int Speed = 10;
    /// <summary>
    /// Damage Dealt to the target.
    /// </summary>
    public int damage;
    //public Collider2D collider;
    /// <summary>
    /// The rigid body of this bullet.
    /// </summary>
    public Rigidbody2D body;
    internal Hurtable.DamegeGroups ignoring;
    /// <summary>
    /// The max number of bounces on walls before 
    ///   this projectile is destroyed.
    /// </summary>
    public int maxBounces = 0;
    /// <summary>
    /// sets the direction the bullet is facing to be the same as the direction the bullet is moving
    /// </summary>
    private void FixDirection() => 
        body.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(body.velocity.y, body.velocity.x) * Mathf.Rad2Deg);
    // Start is called before the first frame update
    void Start() => FixDirection();
        
    //
    /// <summary>
    /// Event for when the bullet hits something.
    /// </summary>
    /// <param name="collision">The collision event object</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        Hurtable from = collision.collider.GetComponentInParent<Hurtable>();
        // Checking if the object that was hit is hurtable
        if (from != null)
        {
            Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
            // Runs the friendly fire check
            if (ignoring == from.DamageGroup)
                return;
            // deals damage to contacting object
            else from.ChangeHealth(-damage);
        }
        else if (--maxBounces > 0)
        {
            body.velocity = Vector3.Reflect(body.velocity.normalized, collision.contacts[0].normal)*Speed;
            //body.velocity = body.velocity.normalized * Speed;
            return;
        }
        // destroys the bullet
        Destroy(gameObject);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        body.velocity = body.velocity.normalized * Speed;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //FixDirection();
        body.velocity = body.velocity.normalized * Speed;
    }
}
