using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAndRelease : MonoBehaviour {

    private GameObject collidingObject;
    private GameObject objectInHand;
    private SteamVR_Controller.Device contDevice;
    private SteamVR_TrackedObject trackedObj;

    // Use this for initialization
    void Start () {
        trackedObj = this.GetComponent<SteamVR_TrackedObject>();
        contDevice = SteamVR_Controller.Input((int)trackedObj.index);
    }
	
	// Update is called once per frame
	void Update () {

        if (contDevice.GetPressDown(SteamVR_Controller.ButtonMask.Trigger)) {
            if (collidingObject) {
                GrabObject();
            }
        }
        else if (contDevice.GetPressUp(SteamVR_Controller.ButtonMask.Trigger)) {
            if (objectInHand) {
                ReleaseObject();
            }
        }


    }

    private void SetCollidingObject(Collider col) {
        if (collidingObject || !col.GetComponent<Rigidbody>()) {
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
        fx.breakTorque = 20000;
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
    /*
    public void OnTriggerStay(Collider other) {
        SetCollidingObject(other);
    }*/

    public void OnTriggerExit(Collider other) {
        if (!collidingObject) {
            return;
        }

        collidingObject = null;
    }

}
