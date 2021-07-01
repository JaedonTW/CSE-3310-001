using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.AI;
/// <summary>
/// A class to encapsulate all non-player characters
/// </summary>
public class NPC : MovableCharacter
{
    // params
    /// <summary>
    /// When idling, the radius of the area the NPC is to remain in.
    /// NOTE: input parameter
    /// </summary>
    public float idleRadius;
    /// <summary>
    /// The different protocols an NPC could be following.
    /// </summary>
    public enum NPCProtocol : byte
    {
        IDLING = 0,
        FLEEING,
        ATTACKING,
    }
    /// <summary>
    /// Represents the current location the NPC is currently idling around or going to.  
    /// Could probably be deprecated and replaced entirely with atomic actions since 
    ///   atomic actions can create new atomic actions.
    /// </summary>
    Vector2 KnownLocation { get; set; }
    /// <summary>
    /// The current stack of atomic actions being performed by this NPC.
    /// Executed from top to bottom.
    /// </summary>
    Stack<IAtomicNPCAction> PlannedActions { get; } = new Stack<IAtomicNPCAction>();
    /// <summary>
    /// The current Protocol being followed by this NPC.
    /// </summary>
    NPCProtocol CurrentState { get; set; }
    public void SetNPCState(NPCProtocol state)
    {
        switch(state)
        {
            case NPCProtocol.IDLING:
                KnownLocation = body.position;
                CurrentState = NPCProtocol.IDLING;
                break;
            default:
                throw new System.NotImplementedException();
        }
        
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        // automatically sets the npc to idling.
        SetNPCState(NPCProtocol.IDLING);
    }
    // Update is called once per frame
    protected override void Update()
    {
        switch(CurrentState)
        {
            case NPCProtocol.IDLING:
                // When idling, the NPC alternates between walking to a random location within the idle area and standing still.
                if(PlannedActions.Count == 0)
                {
                    // If the NPC has somehow left the bounds of the area it is 
                    //   supposed to be idling in, we have it go 
                    //   straight to the center of the area (KnownLocation).
                    if (Sqr(body.position.x - KnownLocation.x) + Sqr(body.position.y - KnownLocation.y) > idleRadius * idleRadius)
                    {
                        PlannedActions.Push(new NPCMoveAction(KnownLocation));
                        PlannedActions.Push(new NPCDelayAction(60));
                    }
                    else// We find a new destination within the idle area for this NPC to go to next.
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
                        PlannedActions.Push(new NPCMoveAction(destination));
                        PlannedActions.Push(new NPCDelayAction(60));
                    }
                }
                break;
            default:
                break;
        }
        // If there are any actions on the 'PlannedActions' stack, we perform the top one.
        if (PlannedActions.Count > 0)
            PlannedActions.Peek().ExecuteAction(PlannedActions, this);
        base.Update();   
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        // When a collision occurs, we have the current AtomicAction figure out how to respond.
        PlannedActions.Peek().HandleCollision(PlannedActions, this, collision);
    }
    // private helper functions
    /// <summary>
    /// Sometimes you just need a square.
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    private float Sqr(float n) => n * n;

}
