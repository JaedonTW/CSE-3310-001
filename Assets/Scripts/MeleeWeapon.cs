using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    public float range;
    public float arcsize;
    public int damage;
    // speed in ticks
    public int speed;
    public float halfLength;
    public GameObject sprite;
    //
    float virticalOffset;
    float horizontalOffset;
    float angularOffset;
    //
    // remaining time in ticks
    int RemainingTime { get; set; }
    SpriteRenderer spriteRenderer;

    public override void Use(float angle)
    {
        // 90 degree change for angle of sprite.
        var initAngle = angle*Mathf.Rad2Deg - arcsize * 0.5f - 90;
        SetAngle(initAngle);
        spriteRenderer.enabled = true;
        RemainingTime = speed;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="angle">Desired angle in degrees</param>
    void SetAngle(float angle)
    {
        angle %= 360;
        var angles = spriteRenderer.transform.eulerAngles;
        angles = new Vector3(angles.x, angles.y,angle + angularOffset);
        spriteRenderer.transform.eulerAngles = angles;
        //
        Vector3 pos = new Vector3(angle >= 90 && angle < 270 ? -horizontalOffset : horizontalOffset, virticalOffset);
        var theta = (angle + angularOffset) * Mathf.Deg2Rad;
        pos.x -= Mathf.Cos(theta) * halfLength;
        pos.y -= Mathf.Sin(theta) * halfLength;
        spriteRenderer.transform.localPosition = pos;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = sprite.GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        //length = Mathf.Abs(spriteRenderer.bounds.max.y - spriteRenderer.bounds.min.y);
    }

    // Update is called once per frame
    void Update()
    {
        if(RemainingTime > 0)
        {
            if (--RemainingTime == 0)
                spriteRenderer.enabled = false;
            else
            {
                var angles = spriteRenderer.transform.eulerAngles;
                SetAngle(angles.z + arcsize / speed);
            }
        }
    }

}
