﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VitSettings.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.8.0.0")]
    public sealed partial class ControlsSettings : global::System.Configuration.ApplicationSettingsBase {
        
        private static ControlsSettings defaultInstance = ((ControlsSettings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new ControlsSettings())));
        
        public static ControlsSettings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool flowLayoutPanelUserMenu {
            get {
                return ((bool)(this["flowLayoutPanelUserMenu"]));
            }
            set {
                this["flowLayoutPanelUserMenu"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool flowLayoutPanelAdminMenu {
            get {
                return ((bool)(this["flowLayoutPanelAdminMenu"]));
            }
            set {
                this["flowLayoutPanelAdminMenu"] = value;
            }
        }
    }
}
