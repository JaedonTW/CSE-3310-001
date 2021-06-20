using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtable : MonoBehaviour
{
    protected int health;
    public Bullet bullet;
    public Rigidbody2D body;
    public void Fire()
    {
        //
        float bullet_place_distance = 0.5f;
        //
        Vector3 position = new Vector3(body.position.x, body.position.y);
        Quaternion rotation = transform.rotation;
        float angle = rotation.eulerAngles.z;// subtracting so that the bullet fires forward
        float init_force = 100;
        position = new Vector3(bullet_place_distance * Mathf.Cos(angle) + position.x, bullet_place_distance * Mathf.Sin(angle) + position.y);
        Bullet b = Instantiate(bullet,position,rotation,transform);
        //angle = b.transform.rotation.eulerAngles.z;
        Vector2 force = new Vector2(init_force * Mathf.Cos(angle), init_force * Mathf.Sin(angle));
        b.body.AddForce(force);
    }
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
