﻿using AutumnBox.CoreModules.Attribute;
using AutumnBox.CoreModules.Lib;
using AutumnBox.OpenFramework.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.CoreModules.Extensions.Poweron
{
    [ExtName("激活第二空间伪装版")]
    [ExtIcon("Icons.anzenbokusufake.png")]
    [ExtRegions("zh-CN")]
    [ExtRequiredDeviceStates(Basic.Device.DeviceState.Poweron)]
    [ExtAppProperty(PKG_NAME)]
    [DpmReceiver(RECEIVER_NAME)]
    class EHideAnzen : EDpmSetterBase
    {
        public const string PKG_NAME = "com.hld.danzenbokusu";
        public const string RECEIVER_NAME = "com.hld.danzenbokusu/.receiver.DPMReceiver";
    }
}