using PXRInput.VRDeviceSet;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.XR;

namespace PXRInput.FPFCDeviceSet
{
    internal class FPFCInputGroup : DeviceInputGroupBase
    {
        public FPFCInputGroup(InputManager _inputManager) : base(_inputManager)
        {
            inputManager = _inputManager;
            Location = XRLocation.FPFC;
            AddDevice(new FPFCInputDevice(this));
        }
    }
}
