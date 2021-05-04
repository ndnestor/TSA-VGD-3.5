using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossAI : MonoBehaviour
{
    // Start is called before the first frame update

    private Transform head;
    [SerializeField] private GameObject target;
    [SerializeField] private float fireRate;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Sprite[] faces;
    [SerializeField] private Vector3[] destinations;
    [SerializeField] private int driveSpeed;
    [SerializeField] private GameObject[] allies;
    [SerializeField] private Vector3 spawnLoc;
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject drop;

    private int injuryState = 0;
    private int driveTarget = 0;
    private Vector3 speed;
    private Vector3 look;
    private bool moving;
    private float fireTime;

    private float closestDistance;
    private float cooldown;

    public bool isDead;
    public bool isActive;

    void Start()
    {
        head = transform.Find("Head").transform;
        look = new Vector3(transform.position.x, 0.0f, transform.position.z);
        cooldown = Time.fixedTime;
        fireTime = Time.fixedTime;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(GetComponent<EnemyInfo>().health != GetComponent<EnemyInfo>().maxHealth){
            isActive = true;
        }
        


        if(Time.fixedTime-fireTime > 0 && !isDead && isActive){
            Instantiate(projectile, new Vector3(head.position.x, head.position.y+0.5f, head.position.z)+head.forward*-5.0f, Quaternion.Euler(head.eulerAngles.x, head.eulerAngles.y+180, head.eulerAngles.z));
            fireTime = Time.fixedTime + fireRate;
        }

        //At 75 % of health
        if(GetComponent<EnemyInfo>().health < (GetComponent<EnemyInfo>().maxHealth/10)*7.5){
            fireRate = 1f;
            if (injuryState < 1)
            {
                injuryState = 1;
                spawnWave(allies[0], 3, 1);

            }


        }
        else if(GetComponent<EnemyInfo>().health == GetComponent<EnemyInfo>().maxHealth){
            injuryState = 0;
        }
        else {
            injuryState = 1;
        }
        //50%
        if (GetComponent<EnemyInfo>().health < (GetComponent<EnemyInfo>().maxHealth / 10) * 5)
        {
            fireRate = 0.75f;
            if (injuryState < 2)
            {
                injuryState = 2;
                spawnWave(allies[0], 3, 2);
                driveSpeed = 8;
            }
        }
        //25%
        if (GetComponent<EnemyInfo>().health < (GetComponent<EnemyInfo>().maxHealth / 10) * 3.5)
        {
            fireRate = 0.5f;
            if (injuryState < 3)
            {
                injuryState = 3;
                spawnWave(allies[1], 3, 2);
                driveSpeed = 12;

            }
        }

        transform.Find("Head").Find("Sprite").gameObject.GetComponent<SpriteRenderer>().sprite = faces[injuryState];



        //Debug.DrawLine(transform.position, look);
        /*
        if (Mathf.Abs(transform.position.x-destinations[driveTarget].x) < 0.5f && Mathf.Abs(transform.position.z-destinations[driveTarget].z) < 0.5f ) {
            Debug.Log(true);
            look += Vector3.Normalize(new Vector3(destinations[driveTarget].x - transform.position.x, 0.0f, destinations[driveTarget].z - transform.position.z)*0.01f);
        }
        */
        if(Vector3.Distance(transform.position, destinations[driveTarget]) <= closestDistance){
            closestDistance = Vector3.Distance(transform.position, destinations[driveTarget]);
        }
        else {
            speed = new Vector3(0.0f, 0.0f, 0.0f);
            moving = false;

            if (driveTarget < destinations.Length-1)
            {
                driveTarget++;
                closestDistance = Vector3.Distance(transform.position, destinations[driveTarget]);
            }
            else
            {
                driveTarget = 0;
            }
        }

        if (!moving)
        {
            moving = true;

            cooldown = Time.fixedTime + 2.0f;
            speed = new Vector3(destinations[driveTarget].x-transform.position.x, 0.0f, destinations[driveTarget].z-transform.position.z);
        }

        if (Time.fixedTime > cooldown && !isDead && isActive)
        {
            transform.Translate(Vector3.Normalize(speed) * 0.01f * driveSpeed);
        }
        if(GetComponent<EnemyInfo>().health <= 0 && !isDead){
            isDead = true;
            //Reusing cooldown for duration of explosion
            cooldown = Time.fixedTime;

        }
        if(isDead) {
            injuryState = 4;
            GetComponent<Light>().color = Color.red;
            if(Time.fixedTime % 0.5 <= 0.01) {
                Instantiate(explosion, transform.position+Random.insideUnitSphere*2.0f, Quaternion.Euler(new Vector3(90.0f, 0.0f, 0.0f)));
            }
            if(Time.fixedTime-cooldown > 6) {
                Instantiate(drop, transform.position, transform.Find("Model").rotation);
                Destroy(gameObject);
            }
        }
        //Change text
        if (GetComponent<EnemyInfo>().health > 0)
        {
            //transform.Find("UI").Find("Healthbar").Find("Text").GetComponent<Text>().text = GetComponent<EnemyInfo>().health.ToString();
        }
        else {
            //transform.Find("UI").Find("Healthbar").Find("Text").GetComponent<Text>().text = "0";
        }
    }
    void spawnWave(GameObject type, int w, int h){
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                Instantiate(type, new Vector3(spawnLoc.x + i * 5.0f, spawnLoc.y, spawnLoc.z + j * 5.0f), transform.rotation);
            }
        }
    }


}
