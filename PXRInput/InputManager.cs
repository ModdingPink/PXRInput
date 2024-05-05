using PXRInput.FPFCDeviceSet;
using PXRInput.VRDeviceSet;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Management;
using Zenject;
using static PXRInput.IInputDevice;

namespace PXRInput
{
    public class InputManager : IInitializable, IDisposable, ITickable
    {
        public List<DeviceInputGroupBase> InputGroups { get; private set; } = new List<DeviceInputGroupBase>();
        //public DeviceInputGroupBase.XRType XRLocation { get => CurrentInputGroup.XRLocation; }
        public Action<IInputDevice, PXRButton> GetAnyButtonDown { get; set; }
        public Action<IInputDevice, PXRTouch> GetAnyTouch { get; set; }

        public void AddInputGroup(DeviceInputGroupBase inputGroup)
        {
            InputGroups.Add(inputGroup);
            inputGroup.Initialize();
        }

        public void Initialize()
        {
            if (Environment.GetCommandLineArgs().Where(n => n.Contains("fpfc")).Count() > 0) {
                AddInputGroup(new FPFCInputGroup(this));
            }
            else
            {
                var loader = XRGeneralSettings.Instance.Manager.activeLoader;
                if (loader != null)
                {
                    string managerName = XRGeneralSettings.Instance.Manager.activeLoader.name.ToLower().Replace(" ", "");
                    if (managerName.Contains("openxr"))
                    {
                        AddInputGroup(new VRInputGroup(XRLocation.OpenXR, this));
                    }
                    else
                    {
                        AddInputGroup(new VRInputGroup(XRLocation.OVR, this));
                    }
                }
            }
        }

        public void Dispose()
        {
            foreach (var group in InputGroups)
            {
                group.Dispose();
            }
        }

        public void Tick()
        {
            foreach (var group in InputGroups)
            {
                group.Tick();
            }
        }
    }
}
