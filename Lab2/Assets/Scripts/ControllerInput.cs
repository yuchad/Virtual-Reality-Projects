using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInput : MonoBehaviour {


    public Transform top;
    public Rigidbody redBalloon;
    public Rigidbody blueBalloon;
    public Rigidbody greenBalloon;
    public Rigidbody yellowBalloon;

    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device contDevice;
    // Use this for initialization
    void Start() {
        trackedObj = this.GetComponent<SteamVR_TrackedObject>();
        contDevice = SteamVR_Controller.Input((int)trackedObj.index);

    }


    void FixedUpdate() {
       
    }

    void Update()
    {
        if (contDevice.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Vector2 touchpad = (contDevice.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad));
            print(touchpad);

            //control up on trackpad
            if(touchpad.y > 0.65f)
            {
                print("Touching Up");
            }

            //control down on trackpad
            else if (touchpad.y < -0.65f)
            {
                print("Touching down");
            }

            //Control left on the touchpad
            else if (touchpad.x < 0.65f)
            {
                print("Touching Left");
            }

            //Control right on the touchpad
            else if (touchpad.x > -0.65f)
            {
                print("Touching Right");
            }

        }

        
    }
}
