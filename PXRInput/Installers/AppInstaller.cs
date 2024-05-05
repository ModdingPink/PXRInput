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


            var loader = XRGeneralSettings.Instance.Manager.activeLoader;
            if (loader != null)
            {
                //string managerName = XRGeneralSettings.Instance.Manager.activeLoader.name.ToLower().Replace(" ", "");
                //if (managerName.Contains("openxr"))

                Container.BindInterfacesAndSelfTo<VRInputGroup>().AsSingle().NonLazy();
            }
        }
    }
}
