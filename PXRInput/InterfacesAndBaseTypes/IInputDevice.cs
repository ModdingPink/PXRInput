using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.XR;

namespace PXRInput
{

    public struct DeviceButtonInfo
    {
        public DeviceInputGroupBase groupBase;
        public PXRDeviceType deviceType;
    }
    public enum XRLocation
    {
        OpenXR,
        OVR,
        Controllable,
        FPFC,
        Unknown
    }

    public enum PXRDeviceType
    {
        Unknown = 0,
        LeftController = 1,
        RightController = 2,
        LeftHand = 3,
        RightHand = 4,
        FPFC = 5,
        Gamepad = 6,
        FootPedal = 7,
        MidiDevice = 8,
        RhythmAddon = 9
    }

    public enum PXRButton
    {
        None = 0,
        PrimaryButton = 1,
        SecondaryButton = 1 << 1,
        TertiaryButton = 1 << 2,
        QuaternaryButton = 1 << 3,
        MenuButton = 1 << 4,
        PrimaryGripButton = 1 << 5,
        SecondaryGripButton = 1 << 6,
        PrimaryTriggerButton = 1 << 7,
        SecondaryTriggerButton = 1 << 8,
        Primary2DAxisClick = 1 << 9,
        Secondary2DAxisClick = 1 << 10,
        DPadNorth = 1 << 11,
        DPadSouth = 1 << 12,
        DPadEast = 1 << 13,
        DPadWest = 1 << 14,
        SelectButton = 1 << 15
    }
    public enum PXRTouch
    {
        None = 0,
        PrimaryTouch = 1,
        SecondaryTouch = 2,
        Primary2DAxisTouch = 4,
        Secondary2DAxisTouch = 8
    }
    public enum PXR2DAxis
    {
        None = 0,
        Primary2DAxis = 1,
        Secondary2DAxis = 2
    }
    public enum PXRAnalogInput
    {
        None = 0,
        Trigger = 1,
        Grip = 2
    }

    public interface IInputDevice
    {
        public void Initialize();
        public void Tick();
        public void Dispose();
        public PXRDeviceType DeviceType { get; }
        public PXRButton ButtonBitmask { get; }
        public PXRButton ButtonDownBitmask { get; }
        public PXRTouch TouchBitmask { get; }
        public PXRTouch TouchDownBitmask { get; }
        public string GetButtonTranslation(PXRButton button, Polyglot.Language language = Polyglot.Language.English);
        public string GetTouchTranslation(PXRTouch touch, Polyglot.Language language = Polyglot.Language.English);
        public string Get2DAxisTranslation(PXR2DAxis axis, Polyglot.Language language = Polyglot.Language.English);
        public string GetAnalogInputTranslation(PXRAnalogInput input, Polyglot.Language language = Polyglot.Language.English);
        public Vector2 Get2DAxis(PXR2DAxis axis);
        public float GetAnalogInput(PXRAnalogInput axis);
        public string DeviceName { get; }
        public string DeviceInfo { get; }
        public bool ReportBatteryLife { get; }
        public float BatteryLife { get; } 
        public bool IsValid { get; }
        public DeviceInputGroupBase DeviceGroup { get; }
    }
}
