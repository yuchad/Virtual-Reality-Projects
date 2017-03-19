using UnityEngine;

//This object is added to the controller network avatars when the program is running as a client.
[RequireComponent(typeof(Rigidbody), typeof(FixedJoint))]
public class NetworkInput : MonoBehaviour {
    
    public SteamVR_TrackedObject trackedObject;
    public SteamVR_Controller.Device input;
    private SteamVR_TrackedController controller;
    public LineRenderer laser;
    private RaycastHit hitInfo;
    private Ray laserBeam;
    public float maxDistLaser;
    public NetworkedCameraRig player;
    GameObject model;
    
    private void Start()
    {
        // Consider why our models are neither meshRenderers on the network avatars, nor child objects of them.
        // How come we have to use joints? 
        laser = trackedObject.GetComponent<LineRenderer>();
        laser.enabled = false;
        maxDistLaser = 100.0f;
        input = SteamVR_Controller.Input((int)trackedObject.index);
        controller = trackedObject.GetComponent<SteamVR_TrackedController>();
        model = transform.parent.FindChild(this.name + "Model").gameObject;
    }

    void Update () {
        if (trackedObject)
        {
            // disable hands if not tracked.
            model.SetActive(trackedObject.isValid);
            if (trackedObject.isValid) {
                // have the net avatars track the steamvr tracked-objects
                transform.position = trackedObject.transform.position;
                transform.rotation = trackedObject.transform.rotation;

                // Send input events to the NetworkedCameraRig instance. (This is our local player)
                var input = SteamVR_Controller.Input((int)trackedObject.index);

                //determines which controller is being interacted with
                bool isLeft = controller.GetComponentInParent<SteamVR_ControllerManager>().left.Equals(controller.gameObject);
         
                //If any controller is being interated with, enable laser and find if it has hit a player
                if (input.GetPressDown(SteamVR_Controller.ButtonMask.Trigger)) {
                    player.CmdLaserEnable(isLeft, true);
                    if (isHit()) {
                        player.CmdHasBeenHit(getPos());
                    }
                }

                //Turn off laser when trigger is not pressed.
                if (!controller.triggerPressed) {
                    player.CmdLaserEnable(isLeft, false);
                }


             

            } 
        }
    }

    /*
     * Determines if a player has shot an other player
     */ 
    public bool isHit() {
        laserBeam = new Ray(trackedObject.transform.position, trackedObject.transform.TransformDirection(Vector3.forward));

        if (Physics.Raycast(laserBeam, out hitInfo, maxDistLaser)) {

            return hitInfo.collider.tag.Equals("Player");
        }

        return false;
    }

    /*
     * Gets the point where the raycast has collided with the player
     */ 
    private Vector3 getPos() {
        return hitInfo.point;
    }

}
