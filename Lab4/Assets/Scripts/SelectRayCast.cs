using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectRayCast : MonoBehaviour {

	public LayerMask selectableMask;
	public Material[] materials;

	private LineRenderer laserLine;
	private SteamVR_TrackedObject trackedObj;
	private SteamVR_Controller.Device contDevice;
	// Use this for initialization
	void Start () {
		trackedObj = this.GetComponent<SteamVR_TrackedObject> ();
		contDevice = SteamVR_Controller.Input((int)trackedObj.index);
		laserLine = GetComponent<LineRenderer>();
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

		if (contDevice.GetPress (SteamVR_Controller.ButtonMask.Trigger)) {
			laserLine.enabled = true;
			laserLine.SetPosition (0, this.transform.position);
			laserLine.SetPosition(1, this.transform.position + this.transform.forward * 50);
			RaycastHit hit;

			if (Physics.Raycast (trackedObj.transform.position, transform.forward, out hit, 100, selectableMask)) {
				laserLine.SetPosition (1, hit.point);
				if (hit.rigidbody != null) {
					MeshRenderer myR = hit.rigidbody.GetComponent<MeshRenderer> ();
					/*
					if (myR != null) {
						print ("test");
						print (materials [1]);
					}
					*/
				}
			}
		} else if (contDevice.GetPressUp (SteamVR_Controller.ButtonMask.Trigger)) {
			laserLine.enabled = false;
		}
	}
}
