using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.AI
{
    public enum EnemyState : byte
    {
        Pursuing = 0,
        Wandering,
        HasLineOfSight,
        Guarding,
    }
    public abstract class Enemy : NPC
    {
        /// <summary>
        /// The prefered distance to be from the player when fighting.
        /// </summary>
        internal virtual float OptimalFightDistance { get; }
        /// <summary>
        /// The current state of the AI at this moment, see UML for details
        /// </summary>
        public EnemyState state = EnemyState.Wandering;
        protected Vector2 KnownPlayerLocation { get; private set; }
        /// <summary>
        /// The max distance this enemy can see.
        /// </summary>
        const int ViewDistance = 10;
        Hurtable MainCharacter => Manager.player;
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            weapon = Instantiate(weapon);
            weapon.body = body;
            DamageGroup = DamegeGroups.Enemy;
            weapon.ignoring = DamegeGroups.Enemy;
            Manager.EnemyCount++;
        }
        public override void OnDeath()
        {
            base.OnDeath();
            Manager.EnemyCount--;
        }
        /// <summary>
        /// Called when this enemy gains line of sight with the player.
        /// Initializes the attack as defined by the attack protocol in the derivative class.
        /// </summary>
        protected abstract void InitializeAttack();
        /// <summary>
        /// The current stack of atomic actions being performed by this NPC.
        /// Executed from top to bottom.
        /// </summary>
        internal Stack<IAtomicNPCAction> PlannedActions { get; } = new Stack<IAtomicNPCAction>();
        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
            if (state != EnemyState.Guarding)
            {
                // the player is within the view range, so we need to check if the player is within line of sight.
                var dx = MainCharacter.body.position - body.position;
                var hits = Physics2D.RaycastAll(body.position, dx, ViewDistance);
                foreach (var hit in hits)
                {
                    if (hit.collider.gameObject.tag == "View Blocker")
                    {
                        if (state == EnemyState.HasLineOfSight)
                        {
                            state = EnemyState.Pursuing;
                            // In this case, we just lost line of sight and thus start pursuing.
                            print("Looking for player");
                            PlannedActions.Push(new GoToPositionAction(KnownPlayerLocation));
                        }
                        break;
                    }
                    if (hit.collider.gameObject.tag == "Player")
                    {
                        //print("Using weapon");
                        weapon.AttemptUse(Mathf.Atan2(dx.y, dx.x), MainCharacter);
                        // we have line of sight.
                        KnownPlayerLocation = MainCharacter.body.position;
                        // TODO have attacks take place
                        if (state != EnemyState.HasLineOfSight)
                        {
                            // Line of sight was just made, so we
                            //   1. Purged 'PlannedActions' so that the 
                            //      derivative class can decide how to proceed.
                            //   2. Update the state
                            //   3. Let the derivitive class decide how it would like to attack.
                            state = EnemyState.HasLineOfSight;
                            print("Player spotted!");
                            PlannedActions.Clear();
                            InitializeAttack();
                        }
                        break;
                    }
                }
                if (PlannedActions.Count > 0)
                    PlannedActions.Peek().ExecuteAction(PlannedActions, this);
                else
                {
                    //print("wandering");
                    state = EnemyState.Wandering;
                    PlannedActions.Push(new RestAction(60));
                    PlannedActions.Push(new GoToPositionAction(Random.insideUnitCircle + body.position));
                }
            }
        }
        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            if(PlannedActions.Count > 0)
                // When a collision occurs, we have the current AtomicAction figure out how to respond.
                PlannedActions.Peek().HandleCollision(PlannedActions, this, collision);
        }
    }
}