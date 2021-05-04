using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public GameObject boss;
    public AudioClip[] sources;
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<AudioSource>().Play();
        if(player.GetComponent<CharacterInfo>().isDead){
            GetComponent<AudioSource>().clip = sources[1];
            GetComponent<AudioSource>().Play();
        }
        else if(boss.GetComponent<EnemyInfo>().health < 500){
            GetComponent<AudioSource>().clip = sources[1];
        }
        else {
            GetComponent<AudioSource>().clip = sources[2];
        }
    }
}
