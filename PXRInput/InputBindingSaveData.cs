using IPA.Config.Stores.Attributes;
using IPA.Config.Stores.Converters;
using ModestTree.Util;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine.XR;
using static PXRInput.IInputDevice;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

namespace PXRInput
{
    public class DeviceInfo
    {
        public DeviceInfo(PXRDeviceType key, int flags)
        {
            Key = key;
            Flags = flags;
        }

        //thanks BSIPA
        public DeviceInfo() { }
        public PXRDeviceType Key { get; set; }
        public int Flags { get; set; }
    }

    public interface IBindingSaveData
    {
        public bool SetFlag(IInputDevice device, int flag);
        public bool AddFlag(IInputDevice device, int flag);
        public bool RemoveFlag(IInputDevice device, int flag);
    }

    public class AnyDeviceBindingSaveData : IBindingSaveData
    {
        public int Flag = 0;
        public AnyDeviceBindingSaveData(int _flag)
        {
            Flag = _flag;
        }

        //Thanks BSIPA
        AnyDeviceBindingSaveData() { }

        public bool AddFlag(IInputDevice device, int _flag)
        {
            Flag |= _flag;
            return true;
        }

        public bool RemoveFlag(IInputDevice device, int _flag)
        {
            Flag &= ~_flag;
            return true;
        }

        public bool SetFlag(IInputDevice device, int _flag)
        {
            Flag = _flag;
            return true;
        }
    }
    
    public class DeviceBindingSaveData : IBindingSaveData
    {
        public DeviceBindingSaveData(DeviceInfo[] _devices) {
            if (_devices != null)
            {
                Devices = new List<DeviceInfo>(_devices);
            }
        }
        public DeviceBindingSaveData(DeviceInfo _device) {
            if (_device != null)
            {
                Devices.Add(_device);
            }
        }

        //Thanks BSIPA
        public DeviceBindingSaveData() {}
        
        [UseConverter(typeof(ListConverter<DeviceInfo>))]
        public List<DeviceInfo> Devices = new List<DeviceInfo>();
        public bool SetFlag(IInputDevice device, int buttonFlags)
        {
            var index = Devices.FindIndex(member => member.Key == device.DeviceType);
            if (index != -1) {
                Devices[index].Flags = buttonFlags;
                return true;
            }
            else
            {
                Devices.Add(new DeviceInfo(device.DeviceType, buttonFlags));
                return false;
            }
        }
        public bool AddFlag(IInputDevice device, int buttonFlags)
        {
            var index = Devices.FindIndex(member => member.Key == device.DeviceType);
            if (index != -1)
            {
                Devices[index].Flags |= buttonFlags;
            }
            else
            {
                Devices.Add(new DeviceInfo(device.DeviceType, buttonFlags));
            }
            return true;
        }
        public bool RemoveFlag(IInputDevice device, int buttonFlags)
        {
            var index = Devices.FindIndex(member => member.Key == device.DeviceType);
            if (index != -1)
            {
                int newFlags = Devices[index].Flags & ~buttonFlags;
                if (newFlags != 0)
                {
                    Devices[index].Flags = newFlags;
                }
                else
                {
                    Devices.RemoveAt(index);
                }
                return true;
            }
            return false;
        }

    }
}
