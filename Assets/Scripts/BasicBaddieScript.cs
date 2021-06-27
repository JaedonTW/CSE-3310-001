using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBaddieScript : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dF = Random.insideUnitCircle;
        dF.Scale(new Vector2(4f,4f));
        body.AddForce(dF);
        if(0.05>Random.Range(0f, 1f))
        {
            weapon.Use();
        }
    }
}
