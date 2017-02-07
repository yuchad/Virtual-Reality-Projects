using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInput : MonoBehaviour {

    private LineRenderer laserLine;
    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device contDevice;
    // Use this for initialization
    void Start () {
        trackedObj = this.GetComponent<SteamVR_TrackedObject>();
        contDevice = SteamVR_Controller.Input((int)trackedObj.index);
        laserLine = GetComponent<LineRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        if (contDevice.GetTouch(SteamVR_Controller.ButtonMask.Touchpad)) {
            laserLine.enabled = true;
            laserLine.SetPosition(0, this.transform.position);
            RaycastHit hit;
            if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, 50)) {
                if(hit.collider.gameObject.tag == "floor") {
                    print("hello");
                }
            }
                laserLine.SetPosition(1, this.transform.position + this.transform.forward * 50);
        }
        else if (contDevice.GetTouchUp(SteamVR_Controller.ButtonMask.Touchpad)) {
            laserLine.enabled = false;
        }
        
	}
}
