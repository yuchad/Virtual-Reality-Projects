using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInput : MonoBehaviour {


    public Rigidbody redBalloon;
    public Rigidbody blueBalloon;
    public Rigidbody greenBalloon;
    public Rigidbody yellowBalloon;
    public float startTime;

    public int gunDamage = 1;
    public float fireRate = 0.25f;
    public float weaponRange = 50f;//50 units of range
    public float hitForce = 100f;

    private WaitForSeconds shotDuration = new WaitForSeconds(0.7f);//determine how long to keep in game view
    private LineRenderer laserLine;
    private float nextFire;//Time player will be allowed to fire
    private Camera fpsCam;

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
        trackedObj = this.GetComponent<SteamVR_TrackedObject>();
        contDevice = SteamVR_Controller.Input((int)trackedObj.index);
        maxSize = 0.12f;
        initialScale = 0.005f;
        laserLine = GetComponent<LineRenderer>();
    }


    void FixedUpdate() {

    }

    void Update() {
        if (contDevice.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad)) {
            Vector2 touchpad = (contDevice.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad));

            //control up on trackpad
            if (touchpad.y > 0.65f) {
                makeBalloon(redBalloon);
            }

            //control down on trackpad
            else if (touchpad.y < -0.65f) {
                makeBalloon(blueBalloon);
            }

            //Control left on the touchpad
            else if (touchpad.x < 0.65f) {
                makeBalloon(greenBalloon);
            }

            //Control right on the touchpad
            else if (touchpad.x > -0.65f) {
                makeBalloon(yellowBalloon);
            }

        }

        if (contDevice.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad)) {
            releaseBaloon();
        }

        if ((contDevice.GetPress(SteamVR_Controller.ButtonMask.Touchpad))) {
            if (balloonInstance.transform.localScale.x < maxSize) {
                balloonInstance.transform.localScale += new Vector3(initialScale, initialScale, initialScale);
            }

        }

        if ((contDevice.GetPress(SteamVR_Controller.ButtonMask.Trigger))) {


            DestroyBaloon health;
            StartCoroutine(ShotEffect());
            laserLine.SetPosition(0, this.transform.position);
            RaycastHit hit;
            if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, weaponRange)) {
                health = hit.collider.gameObject.GetComponent<DestroyBaloon>();
                laserLine.SetPosition(1, hit.point);
                if (health != null) {
                    // Call the damage function of that script, passing in our gunDamage variable
                    health.Damage(gunDamage);
                }
            }
            else {
                laserLine.SetPosition(1, this.transform.position + this.transform.forward * weaponRange);
            }


        }

        if ((contDevice.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))) {
            laserLine.enabled = false;
        }

        if ((contDevice.GetPressDown(SteamVR_Controller.ButtonMask.Grip))) {
            print("you presssed grip");
        }



    }





    void makeBalloon(Rigidbody balloon) {
        balloonInstance = Instantiate(balloon);
        balloonInstance.transform.parent = this.transform;
        balloonInstance.transform.position = this.transform.position + this.transform.forward * 0.09f;


    }

    void releaseBaloon() {
        if (balloonInstance != null) {
            balloonInstance.transform.parent = null;
            balloonInstance.GetComponent<ConstantForce>().enabled = true;
            balloonInstance = null;
        }

    }

    private IEnumerator ShotEffect() {
        laserLine.enabled = true;
        yield return null;


    }









}
