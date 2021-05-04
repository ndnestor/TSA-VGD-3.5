using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDuration : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float lifespan;
    private float timeAlive;

    void Start()
    {
        timeAlive = Time.fixedTime;
    }
    void Update()
    {
        
        transform.eulerAngles = new Vector3(90.0f, transform.rotation.y, transform.rotation.z);
        if(Time.fixedTime > timeAlive+lifespan){
            Destroy(gameObject);
        }
    }
}
