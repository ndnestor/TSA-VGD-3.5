using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject inventoryData;
    [SerializeField] private GameObject[] weaponTypes;
    [HideInInspector] public Sprite[] data;
    private Sprite imageData;
    private bool isOpen = false;

    void Start()
    {
        //Access player inventory
        data = inventoryData.GetComponent<CharacterInfo>().inventory;

    }
    void Update()
    {
        //Open and close inventory
        if (Input.GetKeyDown("tab"))
        {
            if (isOpen)
            {
                isOpen = false;
            }
            else
            {
                isOpen = true;
            }
        }
        animator.SetBool("Opened", isOpen);


        //Examine inventory items
        for (int i = 0; i < inventoryData.GetComponent<CharacterInfo>().inventory.Length; i++)
        {

            //Hide default white square when image is not present.
            if (gameObject.transform.GetChild(i).Find("Image Icon").GetComponent<Image>().sprite == null)
            {
                gameObject.transform.GetChild(i).Find("Image Icon").gameObject.SetActive(false);
            }
            else
            {
                gameObject.transform.GetChild(i).Find("Image Icon").gameObject.SetActive(true);
            }


            if (data[i] != null)
            {
                //Set the UI text and image to match the player inventory array data.
                gameObject.transform.GetChild(i).Find("Text").GetComponent<Text>().text = data[i].name;
                gameObject.transform.GetChild(i).Find("Image Icon").GetComponent<Image>().sprite = data[i];
            }

        }
        //Equipping items in the array



        if (Input.anyKeyDown)
        {

            //The number that is being pressed on the keyboard
            int canParse;

            //If the numbers 1-5 are being pressed on the keyboard
            if (int.TryParse(Input.inputString, out canParse) && canParse >= 1 && canParse <= 5)
            {

                //Runs code based on the type of item stored in the inventory
                switch (gameObject.transform.GetChild(canParse - 1).Find("Text").GetComponent<Text>().text)
                {
                    case "Cafeteria pizza":

                        //Deletes object and increases player health.
                        data[canParse - 1] = null;
                        if (inventoryData.GetComponent<CharacterInfo>().health <= inventoryData.GetComponent<CharacterInfo>().maxHealth-20)
                        {
                            inventoryData.GetComponent<CharacterInfo>().health += 20;
                        }
                        else {
                            inventoryData.GetComponent<CharacterInfo>().health = inventoryData.GetComponent<CharacterInfo>().maxHealth;
                        }
                        //Reset inventory slot for future use.
                        ResetBox(canParse - 1);
                        break;
                    case "Wrench":
                        //Sets the player's weapon as wrench.
                        inventoryData.GetComponent<CharacterInfo>().weapon = weaponTypes[0];
                        break;
                    case "Crowbar":
                        inventoryData.GetComponent<CharacterInfo>().weapon = weaponTypes[1];
                        break;
                    case "Claw Arm":
                        inventoryData.GetComponent<CharacterInfo>().weapon = weaponTypes[2];
                        break;

                }

            }

        }

    }

    public void RemoveKey()
    {
        for (int i = 0; i < data.Length; i++)
        {
            if (data[i] != null && (data[i].name == "Key" || data[i].name == "Gold Key"))
            {
                data[i] = null;
                ResetBox(i);
            }
        }
    }

    public void ResetBox(int box)
    {
        gameObject.transform.GetChild(box).Find("Text").GetComponent<Text>().text = "Item:";
        gameObject.transform.GetChild(box).Find("Image Icon").GetComponent<Image>().sprite = data[box];
    }
}
