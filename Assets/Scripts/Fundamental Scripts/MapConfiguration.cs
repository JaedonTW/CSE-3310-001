using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
/// <summary>
/// A configuration file which should appear for each map.
/// </summary>
class MapConfiguration : MonoBehaviour
{
    /// <summary>
    /// The number of mobsters to be spawned in the level.
    /// </summary>
    public int mobsterSpawnCount;
    /// <summary>
    /// The number of cultists to be spawned in the level.
    /// </summary>
    public int cultistSpawnCount;
    /// <summary>
    /// The probability for a given undead friendly to appear in a level.
    /// On average, the number of these to appear is this value multiplied by the "Undead Friendly Count" counter.
    /// </summary>
    public float undeadFriendlySpawnProbability;
}
