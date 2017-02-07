using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInput : MonoBehaviour {

    public Transform cameraRigTransform;
    public GameObject teleporterPrefab;
    public Transform headTransform;
    public Vector3 teleporterOffset;
    public LayerMask teleporterMask;

    private LineRenderer laserLine;
    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device contDevice;
    private GameObject reticle;
    private Transform teleportReticleTransform;
    private bool canTeleport;
    private Vector3 hitpoint;
    // Use this for initialization
    void Start () {
        trackedObj = this.GetComponent<SteamVR_TrackedObject>();
        contDevice = SteamVR_Controller.Input((int)trackedObj.index);
        laserLine = GetComponent<LineRenderer>();
        reticle = Instantiate(teleporterPrefab);
        teleportReticleTransform = reticle.transform;
    }
	
	// Update is called once per frame
	void Update () {
        if (contDevice.GetTouch(SteamVR_Controller.ButtonMask.Touchpad)) {
            laserLine.enabled = true;
            laserLine.SetPosition(0, this.transform.position);
            laserLine.SetPosition(1, this.transform.position + this.transform.forward * 50);
            RaycastHit hit;
            if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 100)) { 
                hitpoint = hit.point;
                if (hit.collider.gameObject.tag == "floor") {
                    reticle.SetActive(true);
                    teleportReticleTransform.position = hitpoint + teleporterOffset;
                    canTeleport = true;

                }
                
            }
           
               
            
        }
        else if (contDevice.GetTouchUp(SteamVR_Controller.ButtonMask.Touchpad)) {
            laserLine.enabled = false;
            reticle.SetActive(false);
        }
        
	}

   
}
