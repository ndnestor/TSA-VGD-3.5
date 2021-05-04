using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour {
	
	[SerializeField] private GameObject selectionMarker;
	[SerializeField] private GameObject infoDisplay;

	private GameObject currMarker;
	private GameObject selection;
	private RaycastHit rayHit;
	private bool hasSelection;
	private bool hasHit;
	private Ray ray;

	void Update() {

		//Select object on left click and set a waypoint on right click
		if(Input.GetMouseButtonDown(0)) {
			selectObject();
		} else if(Input.GetMouseButtonDown(1) && selection != null && selection.tag == "Friendly") {
			setWaypoint();
		}
	}

	//Determine if the player has left clicked on something selectable
	void selectObject() {

		//Gather information on where the player clicked
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		ray.origin = transform.position;
		hasHit = Physics.Raycast(ray, out rayHit, Mathf.Infinity, ~0);

		//Select the object if appropriate
		if(!hasSelection && hasHit && isSelectable(rayHit.collider.gameObject)) {
			currMarker = Instantiate(selectionMarker, new Vector3(rayHit.collider.transform.position.x, -0.002f,
			rayHit.collider.transform.position.z), Quaternion.identity, rayHit.collider.transform);

			selection = rayHit.collider.gameObject;
			hasSelection = true;

			showInfo(selection);

		//If not appropriate to select the object, deselect anything that has been selected
		} else if(hasSelection && (hasHit && !isSelectable(rayHit.collider.gameObject)) || !hasHit) {
			Destroy(currMarker);

			selection = null;
			hasSelection = false;

			showInfo(null);
		}
	}

	//Returns true if go has the tag "Friendly"
	bool isSelectable(GameObject go) {
		return go.tag == "Friendly";
	}

	//Make a waypoint for the selected character
	void setWaypoint() {
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		ray.origin = transform.position;
		Physics.Raycast(ray, out rayHit, Mathf.Infinity, ~0);
		selection.GetComponent<NavMeshAgent>().destination = rayHit.point;
	}

	//Display UI info on go which is the selected object
	void showInfo(GameObject go) {
		if(go != null) {
			infoDisplay.SetActive(true);
			infoDisplay.GetComponent<InfoDisplay>().label.text = go.name + " (" + go.tag + ")";
			infoDisplay.GetComponent<InfoDisplay>().health.text = go.GetComponent<CharacterInfo>().maxHealth + " / " + go.GetComponent<CharacterInfo>().health;
		} else {
			infoDisplay.SetActive(false);
		}
	}
}
