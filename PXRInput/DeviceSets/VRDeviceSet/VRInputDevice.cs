using Polyglot;
using PXRInput.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.XR;
using static PXRInput.IInputDevice;
using static UnityEngine.RectTransform;

namespace PXRInput.VRDeviceSet
{
    public class VRInputDevice : IInputDevice
    {
        public VRInputDevice(DeviceInputGroupBase _deviceGroup, PXRDeviceType _deviceKey, InputDevice _device, XRNode _deviceXRNode)
        {
            device = _device;
            deviceXRNode = _deviceXRNode;
            DeviceType = _deviceKey;
            DeviceInfo = _deviceXRNode == XRNode.LeftHand ? "Left Controller" : "Right Controller";
            DeviceGroup = _deviceGroup;
        }
        public DeviceInputGroupBase DeviceGroup { get; internal set; }
        public PXRDeviceType DeviceType { get; private set; }
        public string DeviceName { get { return deviceName; } private set { deviceName = value; deviceNameLower = value.ToLower(); } }
        private string deviceName = string.Empty;
        private string deviceNameLower = string.Empty;
        public string DeviceInfo { get; private set; } 
        public bool ReportBatteryLife => false;
        public float BatteryLife { get { device.TryGetFeatureValue(CommonUsages.batteryLevel, out var batteryLevel); return batteryLevel; } }
        public bool IsValid { get; private set; } = false;
        public PXRButton ButtonBitmask { get; internal set; } = PXRButton.None;
        public PXRButton ButtonDownBitmask { get; internal set; } = PXRButton.None;
        public PXRTouch TouchBitmask { get; internal set; } = PXRTouch.None;
        public PXRTouch TouchDownBitmask { get; internal set; } = PXRTouch.None;


        private XRNode deviceXRNode;
        private InputDevice device;

        public void Initialize()
        {
            if (deviceXRNode == XRNode.Head)
            {
                DeviceName = "Headset";
            }
            else
            {
                DeviceName = device.name.Replace(" -", "").Replace(" OpenXR", "");
            }
            IsValid = true;
        }

        void HandleButtonBitmasks()
        {
            PXRButton newButtonBitmask = 0;
            PXRButton newButtonDownBitmask = 0;
            foreach (PXRButton buttonMask in InputDeviceExtensions.AllPXRButtons)
            {
                bool buttonValue = false;
                switch (buttonMask)
                {
                    case PXRButton.PrimaryButton:
                        device.TryGetFeatureValue(CommonUsages.primaryButton, out buttonValue);
                        break;
                    case PXRButton.SecondaryButton:
                        device.TryGetFeatureValue(CommonUsages.secondaryButton, out buttonValue);
                        break;
                    case PXRButton.MenuButton:
                        device.TryGetFeatureValue(CommonUsages.menuButton, out buttonValue);
                        break;
                    case PXRButton.PrimaryGripButton:
                        device.TryGetFeatureValue(CommonUsages.gripButton, out buttonValue);
                        break;
                    case PXRButton.PrimaryTriggerButton:
                        device.TryGetFeatureValue(CommonUsages.triggerButton, out buttonValue);
                        break;
                    case PXRButton.Primary2DAxisClick:
                        device.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out buttonValue);
                        break;
                    case PXRButton.Secondary2DAxisClick:
                        device.TryGetFeatureValue(CommonUsages.secondary2DAxisClick, out buttonValue);
                        break;
                }
                if (buttonValue) //if the button is down
                {
                    newButtonBitmask |= buttonMask; //add it to the bitmask
                }

                if ((ButtonBitmask & buttonMask) == 0 && buttonValue) //if this button was up on the last frame and the button is down now
                {
                    newButtonDownBitmask |= buttonMask;
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
                bool touchValue = false;
                switch (touchMask)
                {
                    case PXRTouch.PrimaryTouch:
                        device.TryGetFeatureValue(CommonUsages.primaryTouch, out touchValue);
                        break;
                    case PXRTouch.SecondaryTouch:
                        device.TryGetFeatureValue(CommonUsages.secondaryTouch, out touchValue);
                        break;
                    case PXRTouch.Primary2DAxisTouch:
                        device.TryGetFeatureValue(CommonUsages.primary2DAxisTouch, out touchValue);
                        break;
                    case PXRTouch.Secondary2DAxisTouch:
                        device.TryGetFeatureValue(CommonUsages.secondary2DAxisTouch, out touchValue);
                        break;
                }
                if (touchValue) //if the touch is down
                {
                    newTouchBitmask |= touchMask; //add it to the bitmask
                }

                if ((TouchBitmask & touchMask) == 0 && touchValue) //if this touch was up on the last frame and the button is down now
                {
                    newTouchDownBitmask |= touchMask;
                }
            }

            TouchBitmask = newTouchBitmask;
            TouchDownBitmask = newTouchDownBitmask;
        }

        public void Tick()
        {
            HandleButtonBitmasks();
            HandleTouchBitmasks();
        }

        public Vector2 Get2DAxis(PXR2DAxis axis)
        {
            Vector2 axisValue = Vector2.zero;

            switch (axis)
            {
                case PXR2DAxis.Primary2DAxis:
                    device.TryGetFeatureValue(CommonUsages.primary2DAxis, out axisValue);
                    break;
                case PXR2DAxis.Secondary2DAxis:
                    device.TryGetFeatureValue(CommonUsages.secondary2DAxis, out axisValue);
                    break;

            }
            return axisValue;
        }

        public float GetAnalogInput(PXRAnalogInput axis)
        {
            float analogValue = 0;
            switch (axis)
            {
                case PXRAnalogInput.Trigger:
                    device.TryGetFeatureValue(CommonUsages.trigger, out analogValue);
                    break;
                case PXRAnalogInput.Grip:
                    device.TryGetFeatureValue(CommonUsages.grip, out analogValue);
                    break;
            }
            return analogValue;
        }

        public string GetButtonTranslation(PXRButton button, Language language = Language.English)
        {
            string defaultValue = button switch
            {
                PXRButton.PrimaryButton => "Primary Button",
                PXRButton.SecondaryButton => "Secondary Button",
                PXRButton.PrimaryGripButton => "Grip",
                PXRButton.MenuButton => "Menu Button",
                PXRButton.PrimaryTriggerButton => "Trigger",
                PXRButton.Primary2DAxisClick => "Joystick Click",
                PXRButton.Secondary2DAxisClick => "Joystick Click",
                _ => "N/A"
            };

            if (deviceNameLower.Contains("oculus"))
            {
                return button switch
                {
                    PXRButton.PrimaryButton => deviceXRNode == XRNode.RightHand ? "A Button" : deviceXRNode == XRNode.LeftHand ? "X Button" : defaultValue,
                    PXRButton.SecondaryButton => deviceXRNode == XRNode.RightHand ? "B Button" : deviceXRNode == XRNode.LeftHand ? "Y Button" : defaultValue,
                    _ => defaultValue
                } ;
            }else if (deviceNameLower.Contains("index"))
            {
                return button switch
                {
                    PXRButton.PrimaryButton => "A Button",
                    PXRButton.SecondaryButton => "B Button",
                    _ => defaultValue
                } ;
            }
            return defaultValue;

        }

        public string GetTouchTranslation(PXRTouch touch, Language language = Language.English)
        {
            var defaultValue = touch switch
            {
                PXRTouch.PrimaryTouch => "Primary Touch",
                PXRTouch.SecondaryTouch => "Secondary Touch",
                PXRTouch.Primary2DAxisTouch => "Joystick Touch",
                PXRTouch.Secondary2DAxisTouch => "Joystick Touch",
                _ => "N/A"
            };

            if (deviceNameLower.Contains("oculus"))
            {
                return touch switch
                {
                    PXRTouch.PrimaryTouch => deviceXRNode == XRNode.RightHand ? "A Button" : deviceXRNode == XRNode.LeftHand ? "X Button" : defaultValue,
                    PXRTouch.SecondaryTouch => deviceXRNode == XRNode.RightHand ? "B Button" : deviceXRNode == XRNode.LeftHand ? "Y Button" : defaultValue,
                    _ => defaultValue
                };
            }
            else if (deviceNameLower.Contains("index"))
            {
                return touch switch
                {
                    PXRTouch.PrimaryTouch => "A Button",
                    PXRTouch.SecondaryTouch => "B Button",
                    _ => defaultValue
                };
            }
            return defaultValue;

        }

        public string Get2DAxisTranslation(PXR2DAxis axis, Language language = Language.English)
        {
            return axis switch
            {
                PXR2DAxis.Primary2DAxis => "Joystick",
                PXR2DAxis.Secondary2DAxis => "Joystick",
                _ => "N/A"
            };
        }

        public string GetAnalogInputTranslation(PXRAnalogInput input, Language language = Language.English)
        {
            return input switch
            {
                PXRAnalogInput.Trigger => "Trigger",
                PXRAnalogInput.Grip => "Grip",
                _ => "N/A"
            };
        }

        public void Dispose()
        {
        }
    }
}
