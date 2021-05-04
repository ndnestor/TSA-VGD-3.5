using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour {
    


	void Update() {
        

        if (!transform.parent.GetComponent<CharacterInfo>().isDead)
        {
            transform.LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            transform.rotation = Quaternion.Euler(90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }
	}
}
