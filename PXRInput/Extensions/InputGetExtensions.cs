using System;
using System.Collections.Generic;
using System.Text;
using static PXRInput.IInputDevice;
using UnityEngine.XR;

namespace PXRInput.Extensions
{
    public static class InputGetExtensions
    {

        #region Input Manager Button Getters With Out
        public static bool GetButtonDown(this InputManager manager, PXRButton button, out IInputDevice? _device)
        {
            foreach (var group in manager.InputGroups)
            {
                if (group.GetButtonDown(button, out var device))
                {
                    _device = device;
                    return true;
                }
            }
            _device = null;
            return false;
        }
        public static bool GetButtonDown(this InputManager manager, PXRButton button, PXRDeviceType deviceType, out IInputDevice? _device)
        {
            foreach (var group in manager.InputGroups)
            {
                if (group.GetButtonDown(button, deviceType, out var device))
                {
                    _device = device;
                    return true;
                }
            }
            _device = null;
            return false;
        }
        public static bool GetButton(this InputManager manager, PXRButton button, out IInputDevice? _device)
        {
            foreach (var group in manager.InputGroups)
            {
                if (group.GetButton(button, out var device))
                {
                    _device = device;
                    return true;
                }
            }
            _device = null;
            return false;
        }
        public static bool GetButton(this InputManager manager, PXRButton button, PXRDeviceType deviceType, out IInputDevice? _device)
        {
            foreach (var group in manager.InputGroups)
            {
                if (group.GetButton(button, deviceType, out var device))
                {
                    _device = device;
                    return true;
                }
            }
            _device = null;
            return false;
        }
        public static bool GetTouch(this InputManager manager, PXRTouch touch, out IInputDevice? _device)
        {
            foreach (var group in manager.InputGroups)
            {
                if (group.GetTouch(touch, out var device))
                {
                    _device = device;
                    return true;
                }
            }
            _device = null;
            return false;
        }
        public static bool GetTouch(this InputManager manager, PXRTouch touch, PXRDeviceType deviceType, out IInputDevice? _device)
        {
            foreach (var group in manager.InputGroups)
            {
                if (group.GetTouch(touch, deviceType, out var device))
                {
                    _device = device;
                    return true;
                }
            }
            _device = null;
            return false;
        }
        public static bool GetTouchDown(this InputManager manager, PXRTouch touch, out IInputDevice? _device)
        {
            foreach (var group in manager.InputGroups)
            {
                if (group.GetTouchDown(touch, out var device))
                {
                    _device = device;
                    return true;
                }
            }
            _device = null;
            return false;
        }
        public static bool GetTouchDown(this InputManager manager, PXRTouch touch, PXRDeviceType deviceType, out IInputDevice? _device)
        {
            foreach (var group in manager.InputGroups)
            {
                if (group.GetTouchDown(touch, deviceType, out var device))
                {
                    _device = device;
                    return true;
                }
            }
            _device = null;
            return false;
        }
        #endregion

        #region Input Manager Button Getters
        public static bool GetButtonDown(this InputManager manager, PXRButton button)
        {
            return manager.GetButtonDown(button, out var device);
        }
        public static bool GetButtonDown(this InputManager manager, PXRButton button, PXRDeviceType deviceType)
        {
            return manager.GetButtonDown(button, deviceType, out var device);
        }
        public static bool GetButton(this InputManager manager, PXRButton button)
        {
            return manager.GetButton(button, out var device);
        }
        public static bool GetButton(this InputManager manager, PXRButton button, PXRDeviceType deviceType)
        {
            return manager.GetButton(button, deviceType, out var device);
        }
        public static bool GetTouch(this InputManager manager, PXRTouch touch)
        {
            return manager.GetTouch(touch, out var device);
        }
        public static bool GetTouch(this InputManager manager, PXRTouch touch, PXRDeviceType deviceType)
        {
            return manager.GetTouch(touch, deviceType, out var device);
        }
        public static bool GetTouchDown(this InputManager manager, PXRTouch touch)
        {
            return manager.GetTouchDown(touch, out var device);
        }
        public static bool GetTouchDown(this InputManager manager, PXRTouch touch, PXRDeviceType deviceType)
        {
            return manager.GetTouchDown(touch, deviceType, out var device);
        }
        #endregion


        #region Device Group Button Getters With Out
        public static bool GetButtonDown(this DeviceInputGroupBase group, PXRButton button, out IInputDevice? _device)
        {
            foreach (var device in group.AllDevicesList)
            {
                if (device.GetButtonDown(button))
                {
                    _device = device;
                    return true;
                }
            }
            _device = null;
            return false;
        }

        public static bool GetButtonDown(this DeviceInputGroupBase group, PXRButton button, PXRDeviceType deviceType, out IInputDevice? _device)
        {
            foreach (var device in group.GetInputDevices(deviceType))
            {
                if (device.GetButtonDown(button))
                {
                    _device = device;
                    return true;
                }
            }
            _device = null;
            return false;
        }

        public static bool GetButton(this DeviceInputGroupBase group, PXRButton button, out IInputDevice? _device)
        {
            foreach (var device in group.AllDevicesList)
            {
                if (device.GetButton(button))
                {
                    _device = device;
                    return true;
                }
            }
            _device = null;
            return false;
        }

        public static bool GetButton(this DeviceInputGroupBase group, PXRButton button, PXRDeviceType deviceType, out IInputDevice? _device)
        {
            foreach (var device in group.GetInputDevices(deviceType))
            {
                if (device.GetButton(button))
                {
                    _device = device;
                    return true;
                }
            }
            _device = null;
            return false;
        }

        public static bool GetTouch(this DeviceInputGroupBase group, PXRTouch touch, out IInputDevice? _device)
        {
            foreach (var device in group.AllDevicesList)
            {
                if (device.GetTouch(touch))
                {
                    _device = device;
                    return true;
                }
            }
            _device = null;
            return false;
        }
        public static bool GetTouch(this DeviceInputGroupBase group, PXRTouch touch, PXRDeviceType deviceType, out IInputDevice? _device)
        {
            foreach (var device in group.GetInputDevices(deviceType))
            {
                if (device.GetTouch(touch))
                {
                    _device = device;
                    return true;
                }
            }
            _device = null;
            return false;
        }
        public static bool GetTouchDown(this DeviceInputGroupBase group, PXRTouch touch, out IInputDevice? _device)
        {
            foreach (var device in group.AllDevicesList)
            {
                if (device.GetTouchDown(touch))
                {
                    _device = device;
                    return true;
                }
            }
            _device = null;
            return false;
        }
        public static bool GetTouchDown(this DeviceInputGroupBase group, PXRTouch touch, PXRDeviceType deviceType, out IInputDevice? _device)
        {
            foreach (var device in group.GetInputDevices(deviceType))
            {
                if (device.GetTouchDown(touch))
                {
                    _device = device;
                    return true;
                }
            }
            _device = null;
            return false;
        }
        #endregion

        #region Device Group Button Getters
        public static bool GetButtonDown(this DeviceInputGroupBase group, PXRButton button)
        {
            return group.GetButtonDown(button, out var device);
        }
        public static bool GetButtonDown(this DeviceInputGroupBase group, PXRButton button, PXRDeviceType deviceType)
        {
            return group.GetButtonDown(button, deviceType, out var device);
        }
        public static bool GetButton(this DeviceInputGroupBase group, PXRButton button)
        {
            return group.GetButton(button, out var device);
        }
        public static bool GetButton(this DeviceInputGroupBase group, PXRButton button, PXRDeviceType deviceType)
        {
            return group.GetButton(button, deviceType, out var device);
        }
        public static bool GetTouch(this DeviceInputGroupBase group, PXRTouch touch)
        {
            return group.GetTouch(touch, out var device);
        }
        public static bool GetTouch(this DeviceInputGroupBase group, PXRTouch touch, PXRDeviceType deviceType)
        {
            return group.GetTouch(touch, deviceType, out var device);
        }
        public static bool GetTouchDown(this DeviceInputGroupBase group, PXRTouch touch)
        {
            return group.GetTouchDown(touch, out var device);
        }
        public static bool GetTouchDown(this DeviceInputGroupBase group, PXRTouch touch, PXRDeviceType deviceType)
        {
            return group.GetTouchDown(touch, deviceType, out var device);
        }
        #endregion
    }
}