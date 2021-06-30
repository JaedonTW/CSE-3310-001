using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MovableCharacter
{
    // params
    public float idleRadius;
    public enum NPCState : byte
    {
        IDLING = 0,
        FLEEING,
        ATTACKING,
    }
    Vector2 KnownLocation { get; set; }
    List<Vector2> Path { get; set; } = new List<Vector2>();
    NPCState CurrentState { get; set; }
    public void SetNPCState(NPCState state)
    {
        switch(state)
        {
            case NPCState.IDLING:
                KnownLocation = body.position;
                CurrentState = NPCState.IDLING;
                break;
            default:
                throw new System.NotImplementedException();
        }
        
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        SetNPCState(NPCState.IDLING);
    }
    // Update is called once per frame
    protected override void Update()
    {
        switch(CurrentState)
        {
            case NPCState.IDLING:
                if(Path.Count == 0)
                {
                    if (Sqr(body.position.x - KnownLocation.x) + Sqr(body.position.y - KnownLocation.y) > idleRadius * idleRadius)
                    {
                        Path.Add(new Vector2(KnownLocation.x - body.position.x, KnownLocation.y - body.position.y));
                        WalkInDirection(Path[0]);
                    }
                    else
                    {
                        var dir = Random.insideUnitCircle;
                        WalkInDirection(dir);
                        float travelDistance = Random.Range(0, idleRadius);
                        // calculating canidate destination
                        Vector2 destination = new Vector2(body.position.x + dir.x * travelDistance, body.position.y + dir.y * travelDistance);
                        // checking distance from idling location
                        if (idleRadius * idleRadius < Sqr(destination.x - KnownLocation.x) + Sqr(destination.y - KnownLocation.y))
                        {
                            // vector is too large.
                            // we have to solve the following for (x1,y1):
                            // m*(x1-x0) + y0 = y1
                            // r^2 = (x1-x0)^2 + (y1-y0)^2
                            // => r^2 = (m^2 + 1) * (x1 - x0)^2
                            // => x1 = r / sqrt(m^2 + 1) + x0
                            var m = Mathf.Atan2(dir.y, dir.x);
                            destination.x = idleRadius / Mathf.Sqrt(m * m + 1) + body.position.x;
                            destination.y = m * (destination.x - body.position.x) + body.position.y;
                            // now, the current destination is on the radius of our idle area.
                        }
                        Path.Add(destination);
                        WalkInDirection(dir);
                    }
                }
                break;
            default:
                break;
        }
        if(Path.Count > 0)
        {
            var cur = Path[0];
            if (Sqr(cur.x - body.position.x) + Sqr(cur.y - body.position.y) < walkingSpeed/* this choice is arbetrary*/)
                Path.RemoveAt(0);
            if(Path.Count > 0)
                WalkInDirection(new Vector2(Path[0].x - body.position.x,Path[0].y - body.position.y).normalized);
        }
        base.Update();   
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        SetIdle();
        if (Path.Count > 0)
            Path.RemoveAt(0);
    }
    // atomic actions
    // atomic actions are the smallest actions
    // private helper functions
    private float Sqr(float n) => n * n;

}
