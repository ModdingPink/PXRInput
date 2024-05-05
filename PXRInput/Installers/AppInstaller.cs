using PXRInput.FPFCDeviceSet;
using PXRInput.VRDeviceSet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.XR.Management;
using Zenject;

namespace PXRInput.Installers
{
    class AppInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InputManager>().AsSingle().NonLazy();
        }
    }
}
