using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.AI;
using System.Collections;
using UnityEngine;
/// <summary>
/// To be attached to any spawner item.  Will make spawner invisible when the game starts.
/// </summary>
class Spawner : MonoBehaviour
{
    /// <summary>
    /// The mobster prefab to be used.
    /// </summary>
    public Enemy mobsterPrefab;
    /// <summary>
    /// The cultist prefab to be used.
    /// </summary>
    public Enemy cultistPrefab;
    /// <summary>
    /// The undead friendly (friendly cultst) prefab to be used.
    /// </summary>
    public Enemy undeadFriendlyPrefab;
    /// <summary>
    /// Current count for turned friendlies not in level.
    /// </summary>
    public static int UndeadCount { get; private set; }
    /// <summary>
    /// The list of spawners that are currently in the level.
    /// This needs to be cleared by the end of the level.
    /// </summary>
    public static List<Spawner> Spawners { get; } = new List<Spawner>();
    /// <summary>
    /// A method for placing enemies in the level via 'Spawners'.
    /// </summary>
    /// <param name="config">The configuration that specifies numbers for how many enemies are to be spawned of each type.</param>
    public static void SpawnEnemies(MapConfiguration config) =>
        SpawnEnemies(config.mobsterSpawnCount, config.cultistSpawnCount, config.undeadFriendlySpawnProbability);
    /// <summary>
    /// Spawns the specified counts of enemies.
    /// </summary>
    /// <param name="mobsterCount">Number of mobsters to be spawned.</param>
    /// <param name="cultustCount">Number of cultists to be spawned.</param>
    /// <param name="undeadSpawnProbability">Probability for each turned friendly to be spawned.</param>
    /// <param name="returnRefs">If references to each spawned enemies are desired.</param>
    /// <returns></returns>
    public static List<Enemy> SpawnEnemies(int mobsterCount, int cultustCount, float undeadSpawnProbability, bool returnRefs = false)
    {
        if(Spawners.Count == 0)
        {
            Debug.LogWarning("You have no spawners, is this intentional?");
            return returnRefs? new List<Enemy>() : null;
        }
        if (UndeadCount == -1)
            UndeadCount = PlayerPrefs.GetInt("friendly_counter", 0);

        int undeadCount = GetBinomialSample(UndeadCount, undeadSpawnProbability);
        UndeadCount -= undeadCount;
        return SpawnEnemies(mobsterCount, cultustCount, undeadCount,returnRefs);
    }
    static List<Enemy> SpawnEnemies(int mobsterCount, int cultistCount, int undeadFriendlyCount, bool returnRefs)
    {
        int count = mobsterCount + cultistCount + undeadFriendlyCount;
        if(count == 0)
        {
            Debug.LogWarning("You are calliny \"SpawnEnemies(int, int, int, bool)\", but asking for zero enemies to be spawned.  Is this intentional?");
            return returnRefs ? new List<Enemy>(0) : null;
        }
        List<Enemy> refs = returnRefs ? new List<Enemy>(count) : null;
        int[] partitions = new int[count];
        int i;
        for(i = 1; i < Spawners.Count; i++)
            partitions[Random.Range(0, partitions.Length)]++;
        i = 0;
        foreach(int b in partitions)
        {
            i += b;
            var enemy = Spawners[i].SpawnEnemy(ref mobsterCount, ref cultistCount, ref undeadFriendlyCount, count);
            if (returnRefs)
                refs.Add(enemy);
            count--;
        }
        return refs;
    }
    Enemy SpawnEnemy(ref int mobsterCount, ref int cultistCount, ref int undeadFriendlyCount, int count)
    {
        int ran = Random.Range(0, count);
        Enemy prefab;
        if (ran < mobsterCount)
        {
            mobsterCount--;
            prefab = mobsterPrefab;
        }
        else if (ran < mobsterCount + cultistCount)
        {
            cultistCount--;
            prefab = cultistPrefab;
        }
        else
        {
            undeadFriendlyCount--;
            prefab = undeadFriendlyPrefab;
        }
        var ret = Instantiate(prefab);
        ret.transform.position = transform.position;
        return ret;
    }
    void Start()
    {
        if (Spawners.Count == 0)
            UndeadCount = -1;
        Spawners.Add(this);
        Destroy(GetComponent<SpriteRenderer>());
    }
    // Binomial random variable generator
    static int GetBinomialSample(int n, float p)
    {
        // Monte-Carlo
        float ProbabilityDistributionFunction(int x)
        {
            float value = 1;
            for (int i = 2; i <= x; i++)
                value *= i;
            value *= Mathf.Pow(p, x);
            value *= Mathf.Pow(1 - p, n - x);
            return value;
        }
        float c = ProbabilityDistributionFunction(n >> 1) * (n + 1), U;
        int canidate;
        do
        {
            canidate = Random.Range(0, n);
            U = Random.Range(0f, c);

        }
        while (U > ProbabilityDistributionFunction(canidate) * (n + 1));
        return canidate;
    }
}
