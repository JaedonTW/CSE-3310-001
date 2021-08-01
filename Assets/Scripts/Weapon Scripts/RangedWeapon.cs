using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    [SerializeField]
    private Projectile bullet;
    /// <summary>
    /// If set to greater than 1, will disperse shots uniformly across a 30 degree arc centered on the direction fired.
    /// Is assumed to be odd.
    /// </summary>
    public int numberOfProjectiles = 1;
    /// <summary>
    /// The action for actually firing the weapon, overrides method in 'Weapon'
    /// </summary>
    /// <param name="angle">angle to be fired at in radians</param>
    protected override void Use(float angle, Hurtable target)
    {
        //
        float bullet_place_distance = 0.5f;
        //
        var half = numberOfProjectiles >> 1;
        float dTheta = 30 * Mathf.Deg2Rad / numberOfProjectiles;
        var theta = -half * dTheta + angle;
        for (int i = -half; i <= half; i++)
        {
            Vector3 position = new Vector3(body.position.x, body.position.y);
            position = new Vector3(bullet_place_distance * Mathf.Cos(theta) + position.x, bullet_place_distance * Mathf.Sin(theta) + position.y);
            Projectile b = Instantiate(bullet, position, Quaternion.identity, body.transform);
            //angle = b.tra nsform.rotation.eulerAngles.z;
            b.ignoring = ignoring;
            b.body.velocity = new Vector2(Projectile.Speed * Mathf.Cos(theta), Projectile.Speed * Mathf.Sin(theta));
            theta += dTheta;
        }
    }
}
