using IPA;
using IPA.Config.Stores;
using IPA.Loader;
using SiraUtil.Zenject;
using IPALogger = IPA.Logging.Logger;
using HarmonyLib;
using System.Reflection;
using System.IO;
using UnityEngine;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.IO.Compression;
using PXRInput.Installers;
using PXRInput;
using UnityEngine.XR.Management;
using System.Linq;

namespace PXRInput
{
    [Plugin(RuntimeOptions.DynamicInit), NoEnableDisable]
    public class Plugin
    {
        [Init]
        public Plugin(IPALogger logger, Zenjector zenjector)
        { 
            zenjector.UseLogger(logger);
            zenjector.Install<AppInstaller>(Location.App);
        }
    }
}
