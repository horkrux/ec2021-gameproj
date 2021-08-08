using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEncounterManager : MonoBehaviour
{
    public RandomEncounterZone prefabZone;

    // Start is called before the first frame update
    void Start()
    {
        //They might overlap hihihi

        float minX = 158.9f;
        float maxX = 464.8f;
        float minZ = 90.24f;
        float maxZ = 388.3f;

        for (int i = 0; i < 5; i++)
        {
            float x = Random.Range(minX, maxX);
            float z = Random.Range(minZ, maxZ);

            Instantiate(prefabZone, new Vector3(x, 5.18f, z), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
