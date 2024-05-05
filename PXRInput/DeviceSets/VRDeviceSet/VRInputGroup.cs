using PXRInput.Extensions;
using PXRInput.VRDeviceSet;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.XR;
using static PXRInput.IInputDevice;

namespace PXRInput.VRDeviceSet
{
    internal class VRInputGroup : DeviceInputGroupBase
    {
        public VRInputGroup(XRLocation _loc, InputManager _inputManager) : base(_inputManager)
        {
            Location = _loc;
            inputManager = _inputManager;
        }

        //Yes this is very bad I know
        void ReinitialiseDevices(InputDevice _device = default)
        {
            ResetDeviceList();
            AddDeviceForXRNode(XRNode.LeftHand, PXRDeviceType.LeftController);
            AddDeviceForXRNode(XRNode.RightHand, PXRDeviceType.RightController);
        }

        void AddDeviceForXRNode(XRNode node, PXRDeviceType deviceName)
        {
            var inputDevice = InputDevices.GetDeviceAtXRNode(node);
            if (inputDevice != null)
            {
                AddDevice(new VRInputDevice(this, deviceName, inputDevice, node));
            }
        }

        public override void Initialize()
        {
            this.GetButton(PXRButton.None);

            base.Initialize();
            ReinitialiseDevices();
            InputDevices.deviceConfigChanged += ReinitialiseDevices;
            InputDevices.deviceDisconnected += ReinitialiseDevices;
            InputDevices.deviceConnected += ReinitialiseDevices;
        }
        public override void Dispose()
        {
            base.Dispose();
            InputDevices.deviceConfigChanged -= ReinitialiseDevices;
            InputDevices.deviceDisconnected -= ReinitialiseDevices;
            InputDevices.deviceConnected -= ReinitialiseDevices;
        }
    }
}
