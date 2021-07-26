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
    public GameObject mobsterPrefab;
    /// <summary>
    /// The cultist prefab to be used.
    /// </summary>
    public GameObject cultistPrefab;
    /// <summary>
    /// The undead friendly (friendly cultst) prefab to be used.
    /// </summary>
    public GameObject undeadFriendlyPrefab;
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
    static void SpawnEnemies(int mobsterCount, int cultustCount, float undeadSpawnProbability)
    {
        if(Spawners.Count == 0)
        {
            Debug.LogWarning("You have no spawners, is this intentional?");
            return;
        }
        int undeadCount = PlayerPrefs.GetInt("Undead Friendly Count", 0);
        undeadCount = GetBinomialSample(undeadCount, undeadSpawnProbability);
        SpawnEnemies(mobsterCount, cultustCount, undeadCount);
    }
    static void SpawnEnemies(int mobsterCount, int cultistCount, int undeadFriendlyCount)
    {
        int count = mobsterCount + cultistCount + undeadFriendlyCount;
        int[] partitions = new int[count];
        int i;
        for(i = 1; i < Spawners.Count; i++)
            partitions[Random.Range(0, partitions.Length)]++;
        print(30);
        i = 0;
        foreach(int b in partitions)
        {
            i += b;
            Spawners[i].SpawnEnemy(ref mobsterCount, ref cultistCount, ref undeadFriendlyCount, count);
            count--;
        }
    }
    void SpawnEnemy(ref int mobsterCount, ref int cultistCount, ref int undeadFriendlyCount, int count)
    {
        int ran = Random.Range(0, count);
        if (ran < mobsterCount)
        {
            mobsterCount--;
            Instantiate(mobsterPrefab).transform.position = transform.position;
        }
        else if (ran < mobsterCount + cultistCount)
        {
            cultistCount--;
            Instantiate(cultistPrefab).transform.position = transform.position;
        }
        else
        {
            undeadFriendlyCount--;
            Instantiate(undeadFriendlyPrefab).transform.position = transform.position;
        }
    }
    void Start()
    {
        Spawners.Add(this);
        Destroy(GetComponent<SpriteRenderer>());
    }
    // Binomial random variable generator
    static int GetBinomialSample(int n, float p)
    {
        // Monte-Carlo
        float PDF(int x)
        {
            float value = 1;
            for (int i = 2; i <= x; i++)
                value *= i;
            value *= Mathf.Pow(p, x);
            value *= Mathf.Pow(1 - p, n - x);
            return value;
        }
        float c = PDF(n >> 1) * (n + 1), U;
        int canidate;
        do
        {
            canidate = Random.Range(0, n);
            U = Random.Range(0f, c);

        }
        while (U > PDF(canidate) * (n + 1));
        return canidate;
    }
}
