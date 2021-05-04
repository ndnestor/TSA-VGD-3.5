using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSpawnpoint : MonoBehaviour
{
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Friendly")
        {
            other.GetComponent<CharacterInfo>().spawnPoint = transform.position;

        }

    }
}
