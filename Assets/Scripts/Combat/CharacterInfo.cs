using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfo : MonoBehaviour
{
    //Info pertaining to a specific character in combat
    public GameObject weapon;

    public InfoDisplay infoDisplay;

    public int maxHealth;
    public int health;
    public int force;
    public bool isDead;
    public Vector3 spawnPoint;
    public int experience;
    public int level;

    public Sprite[] inventory;

    [SerializeField] private int dangerHeatlh;
    [SerializeField] private Sprite deathSprite;
    [SerializeField] private GameObject UI;

    private void FixedUpdate()
    {
        infoDisplay.health.text = health.ToString();
        if (health < dangerHeatlh)
        {
            infoDisplay.health.color = Color.red;
        }

        if (health <= 0 && !isDead)
        {
            Die();
        }
        if (isDead)
        {
            GetComponent<CharacterMovement>().canMove = false;

        }
        //Debug.LogError(experience);
    }

    void Die()
    {
        //Display a death animation or screens
        isDead = true;

        GetComponent<Animator>().SetBool("isDead", true);
        UI.transform.Find("Game Over").gameObject.SetActive(true);
        /*
        transform.Find("Model").GetComponent<SpriteRenderer>().sprite = deathSprite;
        Debug.Log(transform.Find("Model").GetComponent<SpriteRenderer>().sprite.name);
        */
    }
    public void Revive()
    {
        GetComponent<Animator>().Play("Idle");
        //transform.Find("Model").GetComponent<SpriteRenderer>().sprite = defaultSprite;
        isDead = false;
        GetComponent<Animator>().SetBool("isDead", false);
        GetComponent<Animator>().SetBool("isWalking", true);
        health = 100;

        transform.position = spawnPoint;
        UI.transform.Find("Info Display").Find("Health").GetComponent<Text>().color = Color.black;
        UI.transform.Find("Game Over").GetComponent<DeathScreen>().fadeIn = 0.0f;
        UI.transform.Find("Game Over").gameObject.SetActive(false);


    }
}
