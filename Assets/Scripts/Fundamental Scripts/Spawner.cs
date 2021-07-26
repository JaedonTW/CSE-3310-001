using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.AI;
using System.Collections;
using UnityEngine;

class Spawner : MonoBehaviour
{
    public GameObject mobsterPrefab;
    public GameObject cultistPrefab;
    public GameObject undeadFriendlyPrefab;
    public static List<Spawner> Spawners { get; } = new List<Spawner>();
    static void SpawnEnemies(int mobsterCount, int cultustCount, float undeadSpawnProbability)
    {
        int undeadCount = PlayerPrefs.GetInt("Undead Friendly Count", 0);
        undeadCount = GetBinomialSample(undeadCount, undeadSpawnProbability);
        SpawnEnemies(mobsterCount, cultustCount, undeadSpawnProbability);
    }
    static void SpawnEnemies(int mobsterCount, int cultistCount, int undeadFriendlyCount)
    {
        int count = mobsterCount + cultistCount + undeadFriendlyCount;
        int[] partitions = new int[count];
        int i;
        for(i = 1; i < partitions.Length; i++)
            partitions[Random.Range(0, partitions.Length)]++;
        i = 0;
        foreach(int b in partitions)
        {
            i += b;
            Spawners[i].SpawnEnemy(ref mobsterCount, ref cultistCount, ref undeadFriendlyCount, count);
            count--;
        }
    }
    public void SpawnEnemy(ref int mobsterCount, ref int cultistCount, ref int undeadFriendlyCount, int count)
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
    public void Start()
    {
        Spawners.Add(this);
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
