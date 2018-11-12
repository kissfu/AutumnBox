﻿/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/10 17:36:13 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Exceptions;
using AutumnBox.CoreModules.Aspect;
using AutumnBox.CoreModules.Attribute;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Wrapper;
using System;

namespace AutumnBox.CoreModules.Lib
{
    [ExtDesc("使用奇淫技巧暴力设置设备管理员,\n注意:使用此模块前,必须先移除屏幕锁,指纹锁等,否则将可能导致不可预见的后果", "en-us:Use the sneaky skills to set up the device administrator, \n Note: Before using this module, you must first remove the screen lock, fingerprint lock, etc., otherwise it may lead to unforeseen consequences")]
    [UserAgree("EGodPowerWarning")]
    [ExtIcon("Icons.nuclear.png")]
    [DpmReceiver(null)]
    internal abstract class EDpmSetterBase : OfficialVisualExtension
    {
        private string _cn;
        private CstmDpmCommander dpmCommander;
        protected override void OnCreate(ExtensionArgs args)
        {
            base.OnCreate(args);
            ClassExtensionScanner scanner = new ClassExtensionScanner(GetType());
            scanner.Scan(ClassExtensionScanner.ScanOption.Informations);
            var infos = scanner.Informations;
            _cn = infos[DpmReceiverAttribute.KEY].Value as string;
        }
        protected sealed override int VisualMain()
        {
            WriteInitInfo();
            dpmCommander = new CstmDpmCommander(this, TargetDevice)
            {
                CmdStation = this.CmdStation
            };
            dpmCommander.To(OutputPrinter);
            dpmCommander.Extract();
            Progress = 20;

            WriteLineAndSetTip(Res("EGodPowerExtractingApk"));
            dpmCommander.Extract();
            ThrowIfCanceled();
            Progress = 40;

            WriteLineAndSetTip(Res("EGodPowerPushingApk"));
            dpmCommander.PushToDevice();
            ThrowIfCanceled();
            Progress = 50;

            WriteLineAndSetTip(Res("EGodPowerRmUser"));
            dpmCommander.RemoveUsers();
            ThrowIfCanceled();
            Progress = 60;

            WriteLineAndSetTip(Res("EGodPowerRmAcc"));
            dpmCommander.RemoveAccounts();
            ThrowIfCanceled();
            Progress = 70;

            WriteLineAndSetTip(Res("DPMSetting"));
            ThrowIfCanceled();
            Progress = 80;
            try
            {
                dpmCommander.SetDeviceOwner(_cn);
                return 0;
            }
            catch (AdbShellCommandFailedException ex)
            {
                WriteLine(ex.Message);
                return ex.ExitCode ?? 1;
            }
            finally
            {
                Progress = 100;
            }
        }
        protected override void OnFinish(ExtensionFinishedArgs args)
        {
            base.OnFinish(args);
            try
            {
                switch (args.ExitCode)
                {
                    case CstmDpmCommander.OKAY:
                        Tip = CoreLib.Current.Languages.Get("EDpmSetterBaseTipSuccessed");
                        WriteLine(CoreLib.Current.Languages.Get("EDpmSetterBaseMsgSuccessed"));
                        break;
                    case CstmDpmCommander.ERR:
                        Tip = CoreLib.Current.Languages.Get("EDpmSetterBaseTipError");
                        WriteLine(CoreLib.Current.Languages.Get("EDpmSetterBaseMsgError"));
                        break;
                    case CstmDpmCommander.ERR_EXIST_OTHER_ACC:
                        Tip = CoreLib.Current.Languages.Get("EDpmSetterBaseTipErrOtherAccounts");
                        WriteLine(CoreLib.Current.Languages.Get("EDpmSetterBaseMsgErrOtherAccounts"));
                        break;
                    case CstmDpmCommander.ERR_EXIST_OTHER_USER:
                        Tip = CoreLib.Current.Languages.Get("EDpmSetterBaseTipErrOtherUsers");
                        WriteLine(CoreLib.Current.Languages.Get("EDpmSetterBaseMsgErrOtherUsers"));
                        break;
                    case CstmDpmCommander.ERR_MIUI_SEC:
                        Tip = CoreLib.Current.Languages.Get("EDpmSetterBaseTipErrMiuiSec");
                        WriteLine(CoreLib.Current.Languages.Get("EDpmSetterBaseMsgErrMIUISec"));
                        break;
                }
            }
            catch (Exception e)
            {
                Logger.Warn("Can not set tip and message on EDpmSetterBase.OnFinish()", e);
            }
        }
    }
}
