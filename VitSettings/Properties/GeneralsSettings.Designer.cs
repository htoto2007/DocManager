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
    public sealed partial class GeneralsSettings : global::System.Configuration.ApplicationSettingsBase {
        
        private static GeneralsSettings defaultInstance = ((GeneralsSettings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new GeneralsSettings())));
        
        public static GeneralsSettings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string programPath {
            get {
                return ((string)(this["programPath"]));
            }
            set {
                this["programPath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string repositiryPayh {
            get {
                return ((string)(this["repositiryPayh"]));
            }
            set {
                this["repositiryPayh"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("r")]
        public string repositoryRootFolderName {
            get {
                return ((string)(this["repositoryRootFolderName"]));
            }
            set {
                this["repositoryRootFolderName"] = value;
            }
        }
    }
}
