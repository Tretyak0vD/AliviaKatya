﻿#pragma checksum "..\..\..\Elements\UserUserItem.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "978DE250D84F0E6BDCF51F892F0943F6741CB4651B378DC31D720CF5B1314FCF"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using Фотосалон.Elements;


namespace Фотосалон.Elements {
    
    
    /// <summary>
    /// UserUserItem
    /// </summary>
    public partial class UserUserItem : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\..\Elements\UserUserItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border border;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\Elements\UserUserItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label fio_client;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\Elements\UserUserItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label email_client;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\Elements\UserUserItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label password_client;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\Elements\UserUserItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonRedac;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\Elements\UserUserItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonExitAccount;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Фотосалон;component/elements/useruseritem.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Elements\UserUserItem.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.border = ((System.Windows.Controls.Border)(target));
            return;
            case 2:
            this.fio_client = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.email_client = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.password_client = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.buttonRedac = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\..\Elements\UserUserItem.xaml"
            this.buttonRedac.Click += new System.Windows.RoutedEventHandler(this.Click_redact);
            
            #line default
            #line hidden
            return;
            case 6:
            this.buttonExitAccount = ((System.Windows.Controls.Button)(target));
            
            #line 19 "..\..\..\Elements\UserUserItem.xaml"
            this.buttonExitAccount.Click += new System.Windows.RoutedEventHandler(this.Click_exitAccount);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

