using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    public Bullet bullet_type;
    public int bullet_count;
    public override void Use(float angle)
    {
        //
        float bullet_place_distance = 0.5f;
        //
        Vector3 position = new Vector3(body.position.x, body.position.y);
        Quaternion rotation = transform.rotation;
        float init_velocity = 10;
        position = new Vector3(bullet_place_distance * Mathf.Cos(angle) + position.x, bullet_place_distance * Mathf.Sin(angle) + position.y);
        Bullet b = Instantiate(bullet_type, position, rotation, body.transform);
        //angle = b.transform.rotation.eulerAngles.z;
        b.body.velocity = new Vector2(init_velocity * Mathf.Cos(angle), init_velocity * Mathf.Sin(angle));
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
