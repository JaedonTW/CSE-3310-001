using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBaddieScript : MonoBehaviour
{
    // use this to get parent obj.
    public Rigidbody2D body;
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
    }
}
