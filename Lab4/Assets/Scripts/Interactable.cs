//
//  How to create your own Interactable:
//  - Create a subclass of this class.
//  - Override any of the virtual methods.
//  - You can use attachedController.transform to get the controller's position
//  - You can also use attachedController.input to query for input and send haptic pulses.
//
//  Your Interactable must have:
//  - at least one collider on any of its child objects with its layer set to "Interactable"
//  - A rigidbody on the same object as this script.
//
//  A user will begin interacting with this object if they place the controller on the its collider and press the trigger.
//  The interaction will not end until they release the trigger. You can manually stop it by calling DetachController().
//
//  To make sure we only collide with the actual handles and knobs, We'll use the collisionMatrix.
//  The controller's layers are set to "Controllers" and the Interactables' are set to "Interactables"
//  We can then restrict collisions in Edit->ProjectSettings->Physics 
//
//  See WandController.cs to see how this object is detected.
//

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Interactable : MonoBehaviour
{
    // This allows the control to be grabbed by another wand while it is being held.
    public bool Stealable = true;
    // We restrict the attachedController property so that it can only be set using the TryAttach method.
    public WandController attachedController { get; private set; }

    //Returns true if the controller was attached correctly.
    public bool TryAttach(WandController ctrl) {
        //Only attach if the controller is not holding something already.
        if (ctrl.heldItem != null) return false;
        //Steal object from other controller.
        if (attachedController != null)
        {
            if (!Stealable) return false;
            DetachController();
        }

        attachedController = ctrl;
        OnBeginInteraction();
        // Perhaps DetachController was called from OnBeginInteraction.
        if (attachedController == null) return false;
        return true;
    }


    public void DetachController()
    {
        OnEndInteraction();
        if (attachedController.heldItem == this) attachedController.OnItemDetach(this);
        else Debug.LogError("Controller state (HeldItem) was incorrect. Tried to detach " + this + " while holding " +attachedController.heldItem);
        attachedController = null;
    }



    //Called when the user begins interacting with this object. 
    protected virtual void OnBeginInteraction() { }

    //Called  when the user stops interacting with this object. 
    protected virtual void OnEndInteraction() { }

    //No need for OnInteractionStay. You can issue updates for your object in Update() and check if attachedController is null;

    //Called by WandController when an unattached controller overlaps this object's collider.
    public virtual void OnHoverEnter(WandController ctrl) { }

    //Called by WandController when an unattached controller overlaps this object's collider.
    public virtual void OnHoverExit(WandController ctrl) { }

    //Called by WandController on each frame an unattached controller overlaps this object's collider.
    public virtual void OnHoverStay(WandController ctrl) { }


}