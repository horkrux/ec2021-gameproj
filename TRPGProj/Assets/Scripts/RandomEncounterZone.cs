using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomEncounterZone : MonoBehaviour
{
    public EnemyCharacter prefabEnemy;
    int _encounterId;
    public float encounterChance = 0.1f; //very high, so it happens right away

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnUnits()
    {
        Collider collider = gameObject.GetComponent<Collider>();
        Vector3 min = collider.bounds.min;
        Vector3 max = collider.bounds.max;

        var rand = new System.Random();

        int numEnemies = rand.Next(5, 11);

        for (int i = 0; i < numEnemies; i++)
        {
            float x = Random.Range(min.x + 1.0f, max.x - 1.0f);
            float z = Random.Range(min.z + 1.0f, max.z - 1.0f);

            float y = Terrain.activeTerrain.SampleHeight(new Vector3(x, 0.0f, z));
            NavMeshHit hit;

            if (NavMesh.SamplePosition(new Vector3(x, y, z), out hit, 2.0f, NavMesh.AllAreas))
            {
                EnemyCharacter enemy = Instantiate(prefabEnemy, hit.position, Quaternion.identity);

                enemy.gameObject.SetActive(true);
                enemy.AttackDmg = rand.Next(1, 6);
                enemy.Defense = rand.Next(1, 6);
            }
            else
            {
                Debug.LogError("Couldn't find spot on navimesh for enemy spawn location");
            }
        }

        

        


    }
}
