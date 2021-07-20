using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.AI
{
    class GoToPositionAction : IAtomicNPCAction
    {
        /// <summary>
        /// The destionation being travelled to.
        /// </summary>
        Vector2 Destination { get; }
        /// <summary>
        /// A move action for an NPC.
        /// </summary>
        public GoToPositionAction(Vector2 destination)
        {
            Destination = destination;
        }
        public void ExecuteAction(Stack<IAtomicNPCAction> actionStack, Enemy c)
        {
            // current position
            var pos = c.body.position;
            var diff = new Vector2(Destination.x - pos.x, Destination.y - pos.y);
            // If the distance between 'c' and the destination is less then the distance travelled
            //   in a unit of time, then we consider this action complete and pop it from the stack.
            if (diff.sqrMagnitude < 0.1)
                actionStack.Pop();
            // If 'c' is not already moving, we have it move towards the destionation.
            else if (!c.IsMoving)
                c.WalkInDirection(new Vector2(Destination.x - pos.x, Destination.y - pos.y).normalized);

        }

        public void HandleCollision(Stack<IAtomicNPCAction> actionStack, Enemy thisNPC, Collision2D col)
        {
            MonoBehaviour.print("Handling collision");
            // Checking if it just another character because 
            //   if that is the case, we just proceed without making any changes.
            var otherm = col.otherCollider.GetComponentInParent<MovableCharacter>();
            if(otherm != null)
            {
                // we now know that parts of the map are in our way 
                //   so we need to generate a proper path to the destination.
                // removing old 'GoToPositionAction' (aka this)
                actionStack.Pop();
                Vector2Int VectorFToI(Vector2 original) => 
                    new Vector2Int(Mathf.FloorToInt(original.x), Mathf.FloorToInt(original.y));
                var origin = thisNPC.Manager.MapOffset;
                var from = VectorFToI(thisNPC.body.position - origin);
                Vector2Int[] gridPositions = PathFinder.GeneratePath(
                    from, 
                    VectorFToI(Destination - origin), 
                    thisNPC.Manager.PathMap);
                // If it is impossible to get to the desired location (as indicated by a null value),
                //   we just pop off of the stack and let the higher portion of the AI handle this.
                if(gridPositions == null)
                {
                    actionStack.Pop();
                    return;
                }
                // building path plan based on grid positions.
                foreach (var p in gridPositions)
                    actionStack.Push(new GoToPositionAction(p + origin));
                // finally, we will go to the center of the tile we are currently on
                // to ensure movements are smooth
                actionStack.Push(new GoToPositionAction(from + origin));
            }
        }
    }
}
