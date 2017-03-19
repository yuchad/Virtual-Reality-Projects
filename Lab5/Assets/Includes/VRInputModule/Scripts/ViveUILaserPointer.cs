using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

namespace Wacki {

    public class ViveUILaserPointer : IUILaserPointer {

        public const bool USE_MINIMAL_KEYBOARD_MODE = false;

        public static InputField currentInput;
        public static int SkipUntilFrame;
        

        public EVRButtonId button = EVRButtonId.k_EButton_SteamVR_Trigger;

        private SteamVR_TrackedObject _trackedObject;
        private bool _connected = false;
        private int _index;



        protected override void Initialize()
        {
            base.Initialize();
            //Debug.Log("Initialize");

            var trackedObject = GetComponent<SteamVR_TrackedObject>();

            if(trackedObject != null) {
                _index = (int)trackedObject.index;
                _connected = true;
            }
        }

        public override bool ButtonDown()
        {
            if(!_connected)
                return false;

            var device = SteamVR_Controller.Input(_index);
            if(device != null) {
                var result = SkipUntilFrame < Time.frameCount && device.GetPressDown(button);
                return result;
            }

            return false;
        }

        public override bool ButtonUp()
        {
            if(!_connected)
                return false;

            var device = SteamVR_Controller.Input(_index);
            if(device != null)
                return device.GetPressUp(button);

            return false;
        }
        
        public override void OnEnterControl(GameObject control)
        {
            var sel = control.GetComponent<Selectable>();
            
            if (sel)
            {
                var device = SteamVR_Controller.Input(_index);
                device.TriggerHapticPulse(600);
            }
        }

        public override void OnExitControl(GameObject control)
        {
            if (control)
            {
                var sel = control.GetComponent<Selectable>();

                if (sel)
                {
                    var device = SteamVR_Controller.Input(_index);
                    device.TriggerHapticPulse(200);
                }
            }
            
        }

        public override void OnTargetPressDown(GameObject control)
        {
            if (!currentInput && control)
            {
                currentInput = control.GetComponent<InputField>();
                if (currentInput)
                {

                    var inputType = currentInput.inputType == InputField.InputType.Password ? EGamepadTextInputMode.k_EGamepadTextInputModePassword : EGamepadTextInputMode.k_EGamepadTextInputModeNormal;
                    var multiline = currentInput.multiLine ? EGamepadTextInputLineMode.k_EGamepadTextInputLineModeMultipleLines : EGamepadTextInputLineMode.k_EGamepadTextInputLineModeSingleLine;
                    var placeholder = currentInput.placeholder;
                    string hintText = (placeholder is Text) ? (placeholder as Text).text : "";
                    SteamVR.instance.overlay.ShowKeyboard((int)inputType, (int)multiline, hintText, (uint)currentInput.characterLimit, currentInput.text, USE_MINIMAL_KEYBOARD_MODE, 0);

                }
            }
        }
        
        private void OnDestroy()
        {
            currentInput = null;
        }
        protected override void Update()
        {
            base.Update();
            if (currentInput) {
                int capacity = currentInput.characterLimit == 0 ? 10000 : currentInput.characterLimit;
                StringBuilder sb = new StringBuilder(capacity);
                SteamVR.instance.overlay.GetKeyboardText(sb, (uint) capacity);
                string txt = sb.ToString();

                if (currentInput.text != txt)
                {
                    currentInput.text = txt;
                    currentInput = null;
                }
            }
        }
        

    }

}