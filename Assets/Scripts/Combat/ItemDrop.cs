using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    
    private int arrayPosition = 0;
    //Sprite[] getArr;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Friendly"){
            //Get player's inventory
            Sprite[] getArr = other.gameObject.GetComponent<CharacterInfo>().inventory;

            //Search for an empty position in the array
            //Debug.Log(getArr[arrayPosition]);
            while(getArr[arrayPosition] != null && arrayPosition < getArr.Length - 1){
                arrayPosition++;

            }

            //If there is an open spot add it to the array
            if(!(arrayPosition == getArr.Length)){
                getArr[arrayPosition] = gameObject.transform.Find("Model").GetComponent<SpriteRenderer>().sprite;
            }

            //Delete drop
            Destroy(gameObject);

        }
        else {
            arrayPosition = 0;
        }
    }
}
