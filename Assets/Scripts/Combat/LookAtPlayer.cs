using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;
    public Vector3 playerCoords;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!transform.parent.gameObject.GetComponent<BossAI>().isDead)
        {
            playerCoords = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
            transform.LookAt(new Vector3(transform.position.x, transform.position.y, transform.position.z) * 2 - playerCoords);
        }
    }
}
