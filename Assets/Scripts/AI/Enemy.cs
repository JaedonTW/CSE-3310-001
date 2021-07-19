using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public enum EnemyState : byte
    {
        Pursuing = 0,
        Wandering,
        HasLineOfSight,
    }
    public abstract class Enemy : NPC
    {
        
        /// <summary>
        /// The max distance this enemy can see.
        /// </summary>
        public float viewDistance;
        public float optimalFightDistance;
        private float ViewDistanceSquared { get; set; }
        internal EnemyState State { get; set; }
        protected Vector2 KnownPlayerLocation { get; private set; }
        Hurtable MainCharacter { get; set; }
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            ViewDistanceSquared = viewDistance * viewDistance;
        }
        protected abstract void InitializeAttack();
        /// <summary>
        /// The current stack of atomic actions being performed by this NPC.
        /// Executed from top to bottom.
        /// </summary>
        protected Stack<IAtomicNPCAction> PlannedActions { get; } = new Stack<IAtomicNPCAction>();
        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
            if (ViewDistanceSquared >= (MainCharacter.body.position - body.position).sqrMagnitude)
            {
                // the player is within the view range, so we need to check if the player is within line of sight.
                var hit = Physics2D.Raycast(body.position, MainCharacter.body.position);
                if(hit.collider.gameObject.tag == "Player")
                {
                    // we have line of sight.
                    KnownPlayerLocation = MainCharacter.body.position;
                    // TODO have attacks take place
                    if (State != EnemyState.HasLineOfSight)
                    {
                        // Line of sight was just made, so we
                        //   1. Purged 'PlannedActions' so that the 
                        //      derivative class can decide how to proceed.
                        //   2. Update the state
                        //   3. Let the derivitive class decide how it would like to attack.
                        State = EnemyState.HasLineOfSight;
                        PlannedActions.Clear();
                        InitializeAttack();
                    }
                }
            }
            if (PlannedActions.Count > 0)
                PlannedActions.Peek().ExecuteAction(PlannedActions, this);
            else if(State == EnemyState.HasLineOfSight)
            {
                State = EnemyState.Pursuing;
                // In this case, we just lost line of sight and thus start pursuing.
                
                PlannedActions.Push(new GoToPositionAction(KnownPlayerLocation));
            }
        }
        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            // When a collision occurs, we have the current AtomicAction figure out how to respond.
            PlannedActions.Peek().HandleCollision(PlannedActions, this, collision);
        }
    }
}