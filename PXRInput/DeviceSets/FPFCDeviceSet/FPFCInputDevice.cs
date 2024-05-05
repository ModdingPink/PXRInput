using Polyglot;
using System;
using System.Collections.Generic;
using System.Text;
using static PXRInput.IInputDevice;
using UnityEngine.XR;
using UnityEngine;
using PXRInput.Extensions;

namespace PXRInput.FPFCDeviceSet
{
    internal class FPFCInputDevice : IInputDevice
    {
        public FPFCInputDevice(DeviceInputGroupBase _deviceGroup) {
            DeviceGroup = _deviceGroup;
        }
        public PXRDeviceType DeviceType { get; private set; } = PXRDeviceType.FPFC;
        public string DeviceName { get; private set; } = "FPFC";
        public string DeviceInfo => "FPFC";
        public bool ReportBatteryLife => false;
        public float BatteryLife { get; private set; } = 0;
        public bool IsValid { get; private set; } = false;
        public PXRButton ButtonBitmask { get; internal set; } = PXRButton.None;
        public PXRButton ButtonDownBitmask { get; internal set; } = PXRButton.None;
        public PXRTouch TouchBitmask { get; internal set; } = PXRTouch.None;
        public PXRTouch TouchDownBitmask { get; internal set; } = PXRTouch.None;

        public DeviceInputGroupBase DeviceGroup { get; internal set; }

        public void Initialize()
        {

        }
        void HandleButtonBitmasks()
        {
            PXRButton newButtonBitmask = 0;
            PXRButton newButtonDownBitmask = 0;
            foreach (PXRButton buttonMask in InputDeviceExtensions.AllPXRButtons)
            {

                switch (buttonMask)
                {
                    case PXRButton.PrimaryButton:
                        if (Input.GetKey(KeyCode.Z)) newButtonBitmask |= buttonMask;
                        if (Input.GetKeyDown(KeyCode.Z)) newButtonDownBitmask |= buttonMask;
                        break;
                    case PXRButton.SecondaryButton:
                        if (Input.GetKey(KeyCode.X)) newButtonBitmask |= buttonMask;
                        if (Input.GetKeyDown(KeyCode.X)) newButtonDownBitmask |= buttonMask;
                        break;
                    case PXRButton.MenuButton:
                        if (Input.GetKey(KeyCode.I)) newButtonBitmask |= buttonMask;
                        if (Input.GetKeyDown(KeyCode.I)) newButtonDownBitmask |= buttonMask;
                        break;
                    case PXRButton.PrimaryGripButton:
                        if (Input.GetKey(KeyCode.O)) newButtonBitmask |= buttonMask;
                        if (Input.GetKeyDown(KeyCode.O)) newButtonDownBitmask |= buttonMask;
                        break;
                    case PXRButton.PrimaryTriggerButton:
                        if (Input.GetMouseButton(0)) newButtonBitmask |= buttonMask;
                        if (Input.GetMouseButtonDown(0)) newButtonDownBitmask |= buttonMask;
                        break;
                    case PXRButton.Primary2DAxisClick:
                        if (Input.GetKey(KeyCode.LeftCurlyBracket)) newButtonBitmask |= buttonMask;
                        if (Input.GetKeyDown(KeyCode.LeftCurlyBracket)) newButtonDownBitmask |= buttonMask;
                        break;
                }

            }
            ButtonBitmask = newButtonBitmask;
            ButtonDownBitmask = newButtonDownBitmask;
        }

        void HandleTouchBitmasks()
        {
            PXRTouch newTouchBitmask = 0;
            PXRTouch newTouchDownBitmask = 0;

            foreach (PXRTouch touchMask in InputDeviceExtensions.AllPXRTouch)
            {
                switch (touchMask)
                {
                    case PXRTouch.PrimaryTouch:
                        if (Input.GetKey(KeyCode.H)) newTouchBitmask |= touchMask;
                        if (Input.GetKeyDown(KeyCode.H)) newTouchDownBitmask |= touchMask;
                        break;
                    case PXRTouch.SecondaryTouch:
                        if (Input.GetKey(KeyCode.J)) newTouchBitmask |= touchMask;
                        if (Input.GetKeyDown(KeyCode.J)) newTouchDownBitmask |= touchMask;
                        break;
                    case PXRTouch.Primary2DAxisTouch:
                        if (Input.GetKey(KeyCode.K)) newTouchBitmask |= touchMask;
                        if (Input.GetKeyDown(KeyCode.K)) newTouchDownBitmask |= touchMask;
                        break;
                    case PXRTouch.Secondary2DAxisTouch:
                        if (Input.GetKey(KeyCode.L)) newTouchBitmask |= touchMask;
                        if (Input.GetKeyDown(KeyCode.L)) newTouchDownBitmask |= touchMask;
                        break;
                }
            }
            TouchBitmask = newTouchBitmask;
            TouchDownBitmask = newTouchDownBitmask;
        }

        public void Tick()
        {
            Debug.Log("Tick!");
            HandleButtonBitmasks();
            HandleTouchBitmasks();

        }


        public Vector2 Get2DAxis(PXR2DAxis axis)
        {
            return Vector2.zero;
        }

        public float GetAnalogInput(PXRAnalogInput axis)
        {
            return 0;
        }

        public string GetButtonTranslation(PXRButton button, Language language = Language.English)
        {
            return button switch
            {
                PXRButton.PrimaryButton => "Z Key",
                PXRButton.SecondaryButton => "X Key",
                PXRButton.MenuButton => "I Key",
                PXRButton.PrimaryGripButton => "O Key",
                PXRButton.PrimaryTriggerButton => "Mouse Click",
                PXRButton.Primary2DAxisClick => "{ Key",
                _ => "Unknown"
            };
        }

        public string GetTouchTranslation(PXRTouch touch, Language language = Language.English)
        {
            return touch switch
            {
                PXRTouch.PrimaryTouch => "H Key",
                PXRTouch.SecondaryTouch => "J Key",
                PXRTouch.Primary2DAxisTouch => "K Key",
                PXRTouch.Secondary2DAxisTouch => "L Key",
                _ => "Unknown"
            };
        }

        public string Get2DAxisTranslation(PXR2DAxis axis, Language language = Language.English) => "N/A";

        public string GetAnalogInputTranslation(PXRAnalogInput input, Language language = Language.English) => "N/A";

        public void Dispose()
        {
        }

    }
}