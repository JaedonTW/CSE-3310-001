using Assets.Scripts.AI;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

static class InkSpawner
{
    const int SpawnFrequency = 60 * 30;
    static int TicksUntilNextSpawn = 0;
    public static int CurrentInkCount { get; set; } = 0;
    /// <summary>
    /// To be called as soon as sanity drops to 50 or below.
    /// </summary>
    /// <param name="character">The 'player' object.</param>
    public static void Start(MainCharacter character) => CurrentInkCount = 0;
    /// <summary>
    /// This is to be called whenever the player's sanity drops to 50 or below.
    /// </summary>
    /// <param name="character">The 'player' object.</param>
    public static void Update(MainCharacter character, Tilemap walls, Inkie ink)
    {
        if (TicksUntilNextSpawn > 0)
            TicksUntilNextSpawn--;
        else
        {
            int inksNeeded = (55 - character.Sanity) / 5 - CurrentInkCount;
            if (inksNeeded > 0)
            {
                TicksUntilNextSpawn = SpawnFrequency;
                var pos = walls.WorldToCell(character.body.position);
                // spawn inks
                for (int i = 0; i < inksNeeded; i++)
                {
                    // To handle not having enough room, if we cannot
                    //   spawn within 5 tries, we will give up and try
                    //   again on the next epoch.
                    int j = 0;
                    for(; j < 5; j++)
                    {
                        var spawnPos = new Vector3Int(pos.x + Random.Range(-3, 3), pos.y + Random.Range(-3, 3), pos.z);
                        if(!walls.HasTile(spawnPos))
                        {
                            MonoBehaviour.Instantiate<Inkie>(ink, walls.CellToWorld(spawnPos), Quaternion.identity, character.transform);
                            break;
                        }
                    }
                    if (j == 5)
                        return;
                }
            }
        }
    }
}
