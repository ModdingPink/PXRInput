using ModestTree.Util;
using PXRInput.Extensions;
using SiraUtil.Zenject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.XR;
using static PXRInput.IInputDevice;

namespace PXRInput
{
    public class DeviceInputGroupBase
    {
        internal InputManager inputManager;
        public DeviceInputGroupBase(InputManager _inputManager)
        {
            inputManager = _inputManager;
        }

        public List<IInputDevice> AllDevicesList = new List<IInputDevice>();
        internal Dictionary<PXRDeviceType, List<IInputDevice>> deviceTypeDict = new Dictionary<PXRDeviceType, List<IInputDevice>>();
        public XRLocation Location { get; set; }

        public string GroupName;

        public virtual void Initialize()
        {
            ResetDeviceList();
        }
        public virtual void Dispose()
        {
            foreach (var device in AllDevicesList)
            {
                device.Dispose();
            }
        }
        public virtual void Tick()
        {
            foreach (var device in AllDevicesList)
            {
                device.Tick();

                foreach (PXRButton buttonkey in InputDeviceExtensions.AllPXRButtons)
                {
                    if (device.GetButtonDown(buttonkey))
                    {
                        inputManager.GetAnyButtonDown?.Invoke(device, buttonkey);
                    }
                }

                foreach (PXRTouch touchkey in InputDeviceExtensions.AllPXRTouch)
                {
                    if (device.GetTouchDown(touchkey))
                    {
                        inputManager.GetAnyTouch?.Invoke(device, touchkey);
                    }
                }
            }
        }

        public virtual bool AddDevice(IInputDevice device)
        {
            if (AllDevicesList.Contains(device))
            {
                return false;
            }
            deviceTypeDict[device.DeviceType].Add(device);
            AllDevicesList.Add(device);
            return true;
        }  
        public virtual bool RemoveDevice(IInputDevice device)
        {
            if (AllDevicesList.Contains(device))
            {
                deviceTypeDict[device.DeviceType].Remove(device);
                AllDevicesList.Remove(device);
            }
            return true;
        }
        internal void ResetDeviceList()
        {
            AllDevicesList.Clear();
            deviceTypeDict.Clear();
            foreach (var deviceType in InputDeviceExtensions.AllDeviceTypes)
            {
                deviceTypeDict.Add(deviceType, new List<IInputDevice>());
            }
        }
        public virtual List<IInputDevice> GetInputDevices(PXRDeviceType key)
        {
            return deviceTypeDict[key];
        }

    }
}
