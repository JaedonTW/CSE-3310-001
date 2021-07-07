using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    //public Collider2D collider;
    public Rigidbody2D body;
    internal Hurtable ignoring;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        body.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(body.velocity.y, body.velocity.x)*180/Mathf.PI);
    }
    //
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Hurtable from = collision.GetComponentInParent<Hurtable>();
        if (from != null)
            if (ignoring == from)
                return;
            else from.ChangeHealth(-damage);
        print("Destroying bullet");
        Destroy(this.gameObject);
    }
}
