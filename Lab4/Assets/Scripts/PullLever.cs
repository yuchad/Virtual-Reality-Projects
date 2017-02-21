using System;
using UnityEngine;
using UnityEngine.Events;

public class PullLever : Interactable {

	public float Value;                 // Between -0.5 and 0.5
	[Header("Configuration")]
	[Range(0.01f, 1)]                   // This attribute makes the field underneath into a slider in the editor.
	public float pullDelay = .05f;      // The responsiveness with which the lever matches the controller's position.
	public float Accuracy = 0.01f;         
	public float BreakDistance = .4f;   // Distance in worldspace units at which the user is forced to let go of the lever.
	[Header("Spring")]
	public bool  isSpring = false;      // If set, the handle returns to a resting position
	public float SpringDelay = .05f;    // The responsiveness with which the lever returns to its restingValue.
	public float RestingXValue = 0f;    
	[Header("Haptics")]
	public float dragHaptics = .5f;     // Multiplier for the haptic feedback when dragging. 
	public float hoverHaptics = 1f;     // Multiplier for the haptic feedback when hovering. 

	// See end of file (below) for a thorough explination of this field.
	public FloatEvent OnValueChanged;

	// We assume that the lever is 1 unit long.
	const float leverLocalXRange = .5f;

	// References to child objects.
	private Transform handle;
	private Transform handleKnob;

	// We also have access (from the base class) to:
	// bool Stealable;
	// WandController attachedController;

	void Start () {
		handle = transform.FindChild("Handle");
		handleKnob = handle.FindChild("Sphere");
		Value = handle.localPosition.x;
	}

	// Update is called once per frame
	void Update () {
		
		float newValue = Value;
		if (attachedController != null)
		{
			Vector3 controllerPos = attachedController.transform.position;

			// check to see if controller is too far from lever handle.
			float distanceToHandleBar = Vector3.Distance(controllerPos, handleKnob.position);
			if (distanceToHandleBar > BreakDistance)
			{
				// Send a click to the controller if we disconnect.
				attachedController.input.TriggerHapticPulse(2999);
				DetachController();
				return;
			}

			// Find the controller coordinate in local space (position, orientation and scale independent);
			Vector3 localPos = this.transform.InverseTransformPoint(controllerPos);
			// Take only the movement along the lever's axis.
			float TargetX = localPos.x;

			// Limit the Lever's movent to its effective range.
			TargetX = Mathf.Clamp(TargetX, -leverLocalXRange, leverLocalXRange);

			// Find the difference of the coordinates. We use this to find out when to stop
			// animating the lever, as well as to provide haptic feedback to simulate resistance
			float xDistance = Mathf.Abs(TargetX - Value);
			if (xDistance > Accuracy)
			{

				float hapticStrength = 2999 * (xDistance + .1f) * dragHaptics;
				hapticStrength = Mathf.Clamp(hapticStrength, 0, 2999);
				attachedController.input.TriggerHapticPulse((ushort) hapticStrength);

				//This approximates our target in a smooth fashion.
				newValue = Mathf.Lerp(Value, TargetX, (1 / pullDelay) * Time.deltaTime);
			}
		}
		else if (isSpring)
		{
			// Reset to resting position when detached.
			newValue = Mathf.Lerp(Value, RestingXValue, (1 / SpringDelay) * Time.deltaTime);
		}
		// Call the method that is linked in the editor.
		if (newValue != Value) OnValueChanged.Invoke(Value);
		Value = newValue;

		// We need to copy the Vector because it is a property struct.
		Vector3 oldPos = handle.localPosition;
		oldPos.x = Value;
		handle.localPosition = oldPos;

	}

	public override void OnHoverEnter(WandController ctrl)
	{
		ctrl.input.TriggerHapticPulse((ushort)(500 * hoverHaptics));
		handle.GetComponent<MeshRenderer>();
	}
}

// A UnityEvent is a field that allows you to setup a callback using the inspector.
// However, if you want your event to take parameters, you have to make a subclass
// This is simply because the inspector doesn't support generics.
[Serializable] public class FloatEvent : UnityEvent<float> { }
