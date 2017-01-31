using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInput : MonoBehaviour {


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
        if (contDevice.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Vector2 touchpad = (contDevice.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad));
            print(touchpad);

            //control up on trackpad
            if(touchpad.y > 0.65f)
            {
                Rigidbody redBalloonInstance = Instantiate(redBalloon);
                redBalloonInstance.transform.parent = this.transform;
                redBalloonInstance.transform.position = this.transform.position;

            }

            //control down on trackpad
            else if (touchpad.y < -0.65f)
            {
                Rigidbody blueBalloonInstance = Instantiate(blueBalloon);
                blueBalloonInstance.transform.parent = this.transform;
                blueBalloonInstance.transform.position = this.transform.position;
            }

            //Control left on the touchpad
            else if (touchpad.x < 0.65f)
            {
                Rigidbody greenBalloonInstance = Instantiate(greenBalloon);
                greenBalloonInstance.transform.parent = this.transform;
                greenBalloonInstance.transform.position = this.transform.position;
            }

            //Control right on the touchpad
            else if (touchpad.x > -0.65f)
            {
                Rigidbody yellowBalloonInstance = Instantiate(yellowBalloon);
                yellowBalloonInstance.transform.parent = this.transform;
                yellowBalloonInstance.transform.position = this.transform.position;
            }

        }

        
    }
}
