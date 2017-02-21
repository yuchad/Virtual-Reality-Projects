using UnityEngine;
using Valve.VR;
[RequireComponent(typeof(SteamVR_TrackedObject))]
public class WandController : MonoBehaviour
{
    //Made Public so that it made be accessed from the Interactables
    public SteamVR_Controller.Device input;
    //Only one held item for now.
    public Interactable heldItem;

    private void Start()
    {
        
        //Get Input obj
        var trackedObj = this.GetComponent<SteamVR_TrackedObject>();
        input = SteamVR_Controller.Input((int)trackedObj.index);


        // To make sure we only collide with the actual handles and knobs, We'll use the collisionMatrix.
        // The controller's layers are set to "Controllers" and the Interactables' are set to "Interactables"
        // We can then restrict collisions in Edit->ProjectSettings->Physics 

        if (gameObject.layer != LayerMask.NameToLayer("Controllers")) Debug.LogError("Controllers should be in 'Controllers' Collision Layer");
    }

    private void Update()
    {
        //Let go of item. We don't clear heldItem here, as it's take care of in OnItemDetach.
        bool triggerReleased = input.GetPressUp(EVRButtonId.k_EButton_SteamVR_Trigger);
        if(heldItem && triggerReleased)
        {
            heldItem.DetachController();
        }
    }

    public void OnItemDetach(Interactable item)
    {
        heldItem = null;
    }



    //Send HoverEnter message to Interactable
    private void OnTriggerEnter(Collider other)
    {
        // The collider we collided with may not be the root object of the interactable.
        // We assume that the rigidbody is.
        if (other.attachedRigidbody == null) return;
        var interactable = other.attachedRigidbody.GetComponent<Interactable>();
        if (interactable == null) return;
        if (heldItem == interactable) return;
        interactable.OnHoverEnter(this);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.attachedRigidbody == null) return;
        var interactable = other.attachedRigidbody.GetComponent<Interactable>();
        if (interactable == null) return;
        if (heldItem == interactable) return;

        // Start Interacting 
        if (input.GetPressDown(EVRButtonId.k_EButton_SteamVR_Trigger))
        {
            bool didAttach = interactable.TryAttach(this);
            if (didAttach) heldItem = interactable;
        }
        else interactable.OnHoverStay(this);
    }
    //Send HoverExit message to Interactable
    private void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody == null) return;
        var interactable = other.attachedRigidbody.GetComponent<Interactable>();
        if (interactable == null) return;
        if (heldItem == interactable) return;
        interactable.OnHoverStay(this);
    }

}