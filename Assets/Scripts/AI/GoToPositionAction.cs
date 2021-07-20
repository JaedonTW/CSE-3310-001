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
            MonoBehaviour.print("Creating GoToPositionAction");
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
                var from = thisNPC.Manager.Walls.WorldToCell(thisNPC.body.position);
                var to = thisNPC.Manager.Walls.WorldToCell(Destination);
                Vector2Int[] gridPositions = PathFinder.GeneratePath(
                    from, 
                    to, 
                    thisNPC.Manager.Walls);
                // If it is impossible to get to the desired location (as indicated by a null value),
                //   we just pop off of the stack and let the higher portion of the AI handle this.
                if(gridPositions == null)
                {
                    actionStack.Pop();
                    return;
                }
                // building path plan based on grid positions.
                foreach (var p in gridPositions)
                {
                    actionStack.Push(new GoToPositionAction(thisNPC.Manager.Walls.GetCellCenterWorld((Vector3Int)p)));
                }
                var last = thisNPC.Manager.Walls.GetCellCenterWorld((Vector3Int)gridPositions[0]);
                for (int i = 1; i < gridPositions.Length; i++)
                {
                    var current = thisNPC.Manager.Walls.GetCellCenterWorld((Vector3Int)gridPositions[i]);
                    Debug.DrawLine(last, current, Color.red, 50, false);
                    last = current;
                }
                Debug.DrawLine(last, thisNPC.body.position, Color.red, 50, false);
                // finally, we will go to the center of the tile we are currently on
                // to ensure movements are smooth
                actionStack.Push(new GoToPositionAction(thisNPC.Manager.Walls.GetCellCenterWorld((Vector3Int)from)));
            }
        }
    }
}
