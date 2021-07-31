using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    static int count = 0;
    public RangedWeapon()
        :base()
    {
        count++;
    }
    [SerializeField]
    private Projectile bullet;
    /// <summary>
    /// If set to greater than 1, will disperse shots uniformly across a 30 degree arc centered on the direction fired.
    /// </summary>
    public int numberOfProjectiles = 1;
    /// <summary>
    /// The action for actually firing the weapon, overrides method in 'Weapon'
    /// </summary>
    /// <param name="angle">angle to be fired at in radians</param>
    protected override void Use(float angle, Hurtable target)
    {
        print("Count: " + count);
        //
        float bullet_place_distance = 0.5f;
        //
        Vector3 position = new Vector3(body.position.x, body.position.y);
        position = new Vector3(bullet_place_distance * Mathf.Cos(angle) + position.x, bullet_place_distance * Mathf.Sin(angle) + position.y);
        print("'bullet' is null: " + (bullet == null));
        Projectile b = Instantiate(bullet, position, Quaternion.identity, body.transform);
        //angle = b.tra nsform.rotation.eulerAngles.z;
        b.ignoring = ignoring;
        b.body.velocity = new Vector2(Projectile.Speed * Mathf.Cos(angle), Projectile.Speed * Mathf.Sin(angle));
    }
}
