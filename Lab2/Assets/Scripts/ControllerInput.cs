using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInput : MonoBehaviour {


    public Rigidbody redBalloon;
    public Rigidbody blueBalloon;
    public Rigidbody greenBalloon;
    public Rigidbody yellowBalloon;
    public float startTime;

    private float initialScale;
    private float maxSize;
    private Vector3 baseScale;
    private Vector3 finalScale;
    private bool blowUpFinished;
    private Rigidbody balloonInstance;
    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device contDevice;
    // Use this for initialization
    void Start() {
        startTime = Time.time;
        trackedObj = this.GetComponent<SteamVR_TrackedObject>();
        contDevice = SteamVR_Controller.Input((int)trackedObj.index);
        maxSize = 0.12f;
        initialScale = 0.005f;
    }


    void FixedUpdate() {
       
    }

    void Update()
    {
        if (contDevice.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Vector2 touchpad = (contDevice.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad));

            //control up on trackpad
            if (touchpad.y > 0.65f)
            {
                makeBalloon(redBalloon);
                // releaseBaloon();

            }

            //control down on trackpad
            else if (touchpad.y < -0.65f)
            {
                makeBalloon(blueBalloon);
                //releaseBaloon();

            }

            //Control left on the touchpad
            else if (touchpad.x < 0.65f)
            {
                makeBalloon(greenBalloon);
               // releaseBaloon();

            }

            //Control right on the touchpad
            else if (touchpad.x > -0.65f)
            {
                makeBalloon(yellowBalloon);
                //releaseBaloon();


            }

        }

        if (contDevice.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            releaseBaloon();
            

        }

        if ((contDevice.GetPress(SteamVR_Controller.ButtonMask.Touchpad)))
        {
            if(balloonInstance.transform.localScale.x < maxSize) {
                balloonInstance.transform.localScale += new Vector3(initialScale, initialScale, initialScale);
            }
            
        }


    }



      

    void makeBalloon(Rigidbody balloon)
    {
        balloonInstance = Instantiate(balloon);
        balloonInstance.transform.parent = this.transform;
        balloonInstance.transform.position = this.transform.position + this.transform.forward * 0.09f;
        

    }

    void releaseBaloon()
    {
        if (balloonInstance != null)
        {
            balloonInstance.transform.parent = null;
            balloonInstance.GetComponent<ConstantForce>().enabled = true;
            balloonInstance = null;
          


        }
       
    }

 







}
