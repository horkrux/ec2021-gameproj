using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerCharacter playerChr;
    bool isFollowPlayer = true;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.SetParent(playerChr.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (isFollowPlayer)
        {
            
            //gameObject.transform.position.x 
        }
    }
}
