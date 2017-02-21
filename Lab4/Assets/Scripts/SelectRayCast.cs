using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectRayCast : MonoBehaviour {

	public LayerMask selectableMask;
	public Material[] myMaterials;
	public GameObject mySphere;
	public GameObject myCube;
	public GameObject myCylinder;

	private LineRenderer laserLine;
	private SteamVR_TrackedObject trackedObj;
	private SteamVR_Controller.Device contDevice;
	private bool triggerDown;
	private MeshRenderer myR;
	private RaycastHit hit;
	private MeshRenderer sphereMesh;
	private MeshRenderer cubeMesh;
	private MeshRenderer cylMesh;
	private bool sphereSelected;
	private bool cubeSelected;
	private bool cylSelected;


	// Use this for initialization
	void Start () {
		trackedObj = this.GetComponent<SteamVR_TrackedObject> ();
		contDevice = SteamVR_Controller.Input((int)trackedObj.index);
		laserLine = GetComponent<LineRenderer>();
		sphereMesh = mySphere.GetComponent<MeshRenderer> ();
		cubeMesh = myCube.GetComponent<MeshRenderer> ();
		cylMesh = myCylinder.GetComponent<MeshRenderer> ();

	}
	
	// Update is called once per frame
	void Update () {

		/* Used for vive
		if (contDevice.GetTouch (SteamVR_Controller.ButtonMask.Touchpad)) {
			laserLine.enabled = true;
			laserLine.SetPosition (0, this.transform.position);
			RaycastHit hit;
			if (Physics.Raycast (trackedObj.transform.position, transform.forward, out hit, 100, selectableMask)) {
				laserLine.SetPosition (1, hit.point);
			}

		} 

		else if (contDevice.GetTouchUp (SteamVR_Controller.ButtonMask.Touchpad)) {
			laserLine.enabled = false;
		}*/

		if (contDevice.GetPress (SteamVR_Controller.ButtonMask.Touchpad)) {
			laserLine.enabled = true;
			laserLine.SetPosition (0, this.transform.position);
			laserLine.SetPosition(1, this.transform.position + this.transform.forward * 50);


			if (Physics.Raycast (trackedObj.transform.position, transform.forward, out hit, 100, selectableMask)) {
				laserLine.SetPosition (1, hit.point);
	
				if (hit.rigidbody != null) {
					triggerDown = true;
					 //myR = hit.rigidbody.GetComponent<MeshRenderer>();


				}
			}
		} 

		else if (contDevice.GetPressUp (SteamVR_Controller.ButtonMask.Touchpad)) {
			laserLine.enabled = false;
			triggerDown = false;
		}

		if (contDevice.GetPressDown (SteamVR_Controller.ButtonMask.Trigger) && triggerDown == true) {

			if (hit.collider != null) {
				if (hit.collider.name == "Sphere") {
					sphereMesh.sharedMaterial = myMaterials [1];
					cubeMesh.sharedMaterial = myMaterials [0];
					cylMesh.sharedMaterial = myMaterials [0];
					sphereSelected = true;
					cubeSelected = false;
					cylSelected = false;
				} else if (hit.collider.name == "Cube") {
					sphereMesh.sharedMaterial = myMaterials [0];
					cubeMesh.sharedMaterial = myMaterials [1];
					cylMesh.sharedMaterial = myMaterials [0];
					sphereSelected = false;
					cubeSelected = true;
					cylSelected = false;
				} else if (hit.collider.name == "Cylinder") {
					sphereMesh.sharedMaterial = myMaterials [0];
					cubeMesh.sharedMaterial = myMaterials [0];
					cylMesh.sharedMaterial = myMaterials [1];
					sphereSelected = false;
					cubeSelected = false;
					cylSelected = true;
				}
			} else if (hit.collider == null) {
				sphereMesh.sharedMaterial = myMaterials [0];
				cubeMesh.sharedMaterial = myMaterials [0];
				cylMesh.sharedMaterial = myMaterials [0];
				sphereSelected = false;
				cubeSelected = false;
				cylSelected = false;
			}


		}
	}
}
