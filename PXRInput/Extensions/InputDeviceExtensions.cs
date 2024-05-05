using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.XR;
using static PXRInput.IInputDevice;

namespace PXRInput.Extensions
{
    public static class InputDeviceExtensions
    {
        public static PXRButton[] AllPXRButtons = (PXRButton[])Enum.GetValues(typeof(PXRButton));
        public static PXRTouch[] AllPXRTouch = (PXRTouch[])Enum.GetValues(typeof(PXRTouch));
        public static PXR2DAxis[] AllPXR2DAxis = (PXR2DAxis[])Enum.GetValues(typeof(PXR2DAxis));
        public static PXRAnalogInput[] AllPXRAnalogInput = (PXRAnalogInput[])Enum.GetValues(typeof(PXRAnalogInput));
        public static PXRDeviceType[] AllDeviceTypes = (PXRDeviceType[])Enum.GetValues(typeof(PXRDeviceType));
        public static XRNode[] AllXRNodes = (XRNode[])Enum.GetValues(typeof(XRNode));

        public static bool GetButton(this IInputDevice device, PXRButton button)
        {
            return (device.ButtonBitmask & button) != 0;
        }        
        public static bool GetButtonDown(this IInputDevice device, PXRButton button)
        {
            return (device.ButtonDownBitmask & button) != 0;
        }
        public static bool GetTouch(this IInputDevice device, PXRTouch touch)
        {
            return (device.TouchBitmask & touch) != 0;
        }
        public static bool GetTouchDown(this IInputDevice device, PXRTouch touch)
        {
            return (device.TouchDownBitmask & touch) != 0;
        }
    }
}
