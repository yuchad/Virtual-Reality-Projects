using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInput : MonoBehaviour {


    public Rigidbody redBalloon;
    public Rigidbody blueBalloon;
    public Rigidbody greenBalloon;
    public Rigidbody yellowBalloon;

    private bool blowUpFinished;
    private Rigidbody balloonInstance;
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

            //control up on trackpad
            if(touchpad.y > 0.65f)
            {
                makeBalloon(redBalloon);
                StartCoroutine(ScaleOverTime(1));
                blowUpFinished = true;
            }

            //control down on trackpad
            else if (touchpad.y < -0.65f)
            {
                makeBalloon(blueBalloon);
                StartCoroutine(ScaleOverTime(1));
                blowUpFinished = true;
            }

            //Control left on the touchpad
            else if (touchpad.x < 0.65f)
            {
                makeBalloon(greenBalloon);
                StartCoroutine(ScaleOverTime(1));
                blowUpFinished = true;
            }

            //Control right on the touchpad
            else if (touchpad.x > -0.65f)
            {
                makeBalloon(yellowBalloon);
                StartCoroutine(ScaleOverTime(1));
                blowUpFinished = true;
            }

        }

        if (contDevice.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            if(blowUpFinished == true)
            {
                releaseBaloon();
                blowUpFinished = false;
            }
            
        }



    }

    void makeBalloon(Rigidbody balloon)
    {
        balloonInstance = Instantiate(balloon);
        balloonInstance.transform.parent = this.transform;
        balloonInstance.transform.position = this.transform.position + this.transform.forward * 0.12f;
        
    }

    void releaseBaloon()
    {
        if (balloonInstance != null)
        {
            balloonInstance.transform.parent = null;
            balloonInstance = null;
        }
    }

    IEnumerator ScaleOverTime(float time)
    {
        Vector3 originalScale = balloonInstance.transform.localScale;
        Vector3 endScale = new Vector3(0.4f, 0.4f, 0.4f);
        float currentTime = 0.0f;

  
       
    if(currentTime <= time)
        {
            balloonInstance.transform.localScale = Vector3.Lerp(originalScale, endScale, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        }
        
    }


}
