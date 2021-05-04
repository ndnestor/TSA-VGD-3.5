

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Image img;
    private Image child;
    public float fadeIn;

    void Start()
    {
        img = gameObject.GetComponent<Image>();
        child = gameObject.transform.Find("Start Game").GetComponent<Image>();
        fadeIn = 0.0f;

    }

    // Update is called once per frame
    void Update()
    {
        img.color = new Color(img.color.r, img.color.g, img.color.b, fadeIn);
        child.color = new Color(child.color.r, child.color.g, child.color.b, fadeIn);
        if (player.GetComponent<CharacterInfo>().isDead)
        {

            fadeIn += 0.5f * Time.deltaTime;
        }
    }
}
