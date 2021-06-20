using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //public Collider2D collider;
    public Rigidbody2D body;
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Hurtable from = collision.collider.GetComponentInParent<Hurtable>();
        if(from != null)
            from.ChangeHealth(-1);
        Destroy(this.gameObject);
    }
}
