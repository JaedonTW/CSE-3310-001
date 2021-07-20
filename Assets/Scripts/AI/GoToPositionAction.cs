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
        internal virtual Vector2 Destination { get; }
        /// <summary>
        /// A move action for an NPC.
        /// </summary>
        public GoToPositionAction(Vector2 destination)
        {
            Destination = destination;
        }
        /// <summary>
        /// A move action for an NPC.
        /// </summary>
        public GoToPositionAction()
        { }
        public void ExecuteAction(Stack<IAtomicNPCAction> actionStack, Enemy c)
        {
            // current position
            var pos = c.body.position;
            var diff = new Vector2(Destination.x - pos.x, Destination.y - pos.y);
            // If the distance between 'c' and the destination is less then the distance travelled
            //   in a unit of time, then we consider this action complete and pop it from the stack.
            if (diff.sqrMagnitude < 0.01)
                actionStack.Pop();
            // If 'c' is not already moving, we have it move towards the destionation.
            else
            {
                Debug.DrawRay(pos, diff, Color.blue, 1, false);
                c.WalkInDirection(diff.normalized);
            }
        }

        public void HandleCollision(Stack<IAtomicNPCAction> actionStack, Enemy thisNPC, Collision2D col)
        {
            // Checking if it just another character because 
            //   if that is the case, we just proceed without making any changes.
            var otherm = col.gameObject.tag;
            if (otherm == "View Blocker")
            {
                MonoBehaviour.print("Hit a wall");
                // we now know that parts of the map are in our way 
                //   so we need to generate a proper path to the destination.
                // removing old 'GoToPositionAction' (aka this)
                actionStack.Pop();
                var from = thisNPC.Manager.Walls.WorldToCell(thisNPC.GetComponent<Collider2D>().transform.position);
                if (thisNPC.Manager.Walls.HasTile(from))
                    from -= new Vector3Int(0, 1, 0);
                var to = thisNPC.Manager.Walls.WorldToCell(Destination);
                Vector2Int[] gridPositions = PathFinder.GeneratePath(
                    from,
                    to,
                    thisNPC.Manager.Walls);
                // If it is impossible to get to the desired location (as indicated by a null value),
                //   we just pop off of the stack and let the higher portion of the AI handle this.
                if (gridPositions == null)
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
                    Debug.DrawLine(last, current, Color.red, 10, false);
                    last = current;
                }
                Debug.DrawLine(last, thisNPC.body.position, Color.red, 10, false);
                // finally, we will go to the center of the tile we are currently on
                // to ensure movements are smooth
                actionStack.Push(new GoToPositionAction(thisNPC.Manager.Walls.GetCellCenterWorld((Vector3Int)from)));
            }/*
            else
            {
                MonoBehaviour.print("Hit something else idk");
                // we go in the opposite direction that we have been moving a distance
                var dir = (thisNPC.body.position - Destination).normalized*0.01f;
                actionStack.Push(new GoToPositionAction(dir));
            }*/
        }
    }
}
