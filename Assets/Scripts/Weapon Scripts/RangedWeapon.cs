﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{

    public Bullet bullet_type;
    private int RechargeTicksRemaining { get; set; }
    /// <summary>
    /// The action for actually firing the weapon, overrides method in 'Weapon'
    /// </summary>
    /// <param name="angle">angle to be fired at in radians</param>
    public override void Use(float angle)
    {
        if (RechargeTicksRemaining > 0)
            return;
        RechargeTicksRemaining = rechargeTime;
        //
        float bullet_place_distance = 0.5f;
        //
        Vector3 position = new Vector3(body.position.x, body.position.y);
        Quaternion rotation = transform.rotation;
        float init_velocity = 10;
        position = new Vector3(bullet_place_distance * Mathf.Cos(angle) + position.x, bullet_place_distance * Mathf.Sin(angle) + position.y);
        Bullet b = Instantiate(bullet_type, position, rotation, body.transform);
        //angle = b.tra nsform.rotation.eulerAngles.z;
        b.ignoring = ignoring;
        b.body.velocity = new Vector2(init_velocity * Mathf.Cos(angle), init_velocity * Mathf.Sin(angle));
    }
    /// <summary>
    /// An updater method to be called every tick and maintains the weapon recharge counter.
    /// </summary>
    public override void Tick()
    {
        if (RechargeTicksRemaining > 0)
            RechargeTicksRemaining--;
    }
}