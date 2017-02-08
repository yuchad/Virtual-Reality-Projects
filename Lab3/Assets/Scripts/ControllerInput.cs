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
    private Vector3 movePoint;
    private bool isTracking;
    private GameObject collidingObject;
    private GameObject objectInHand;



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

       
        if (contDevice.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && canTeleport) {
            SteamVR_Fade.View(Color.black, 2f);
            movePoint = hitpoint;
            Invoke("Teleport",2f);           
        }

        if (contDevice.GetPressDown(SteamVR_Controller.ButtonMask.Trigger)) {
            if (collidingObject) {
                GrabObject();
                print("grab");
            }
        }
        else if (contDevice.GetPressUp(SteamVR_Controller.ButtonMask.Trigger)) {
            if (objectInHand) {
                ReleaseObject();
                print("release");
            }
        }

        
        
	}

    private void Teleport() {
            
            canTeleport = false;
            reticle.SetActive(false);
            Vector3 difference = cameraRigTransform.position - headTransform.position;
            difference.y = 0;
            cameraRigTransform.position = movePoint + difference;
            SteamVR_Fade.View(new Color(0, 0, 0, 0), 2f);
        
    }

    private void SetCollidingObject(Collider col) {
        if(collidingObject || !col.GetComponent<Rigidbody>()) {
            return;
        }

        collidingObject = col.gameObject;
    }

    private void GrabObject() {
        objectInHand = collidingObject;
        collidingObject = null;
        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
        
    }

    private FixedJoint AddFixedJoint() {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 2000;
        return fx;
    }

    private void ReleaseObject() {
        if (GetComponent<FixedJoint>()) {

            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());

            objectInHand.GetComponent<Rigidbody>().velocity = contDevice.velocity;
            objectInHand.GetComponent<Rigidbody>().angularVelocity = contDevice.angularVelocity;
        }

        objectInHand = null;
    }

    public void OnTriggerEnter(Collider other) {
        SetCollidingObject(other);
    }

    public void OnTriggerStay(Collider other) {
        SetCollidingObject(other);
    }

    public void OnTriggerExit(Collider other) {
        if (!collidingObject) {
            return;
        }

        collidingObject = null;
    }




}
