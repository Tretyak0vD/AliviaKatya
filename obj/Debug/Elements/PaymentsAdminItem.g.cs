﻿#pragma checksum "..\..\..\Elements\PaymentsAdminItem.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "43AB4B51E2BDD613964E361A19677F54239EF31D971841DB8FB1D884DFC57E17"
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
    /// PaymentsAdminItem
    /// </summary>
    public partial class PaymentsAdminItem : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\..\Elements\PaymentsAdminItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border border;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\Elements\PaymentsAdminItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label idorder_payment;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\Elements\PaymentsAdminItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label fullcost_payment;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\Elements\PaymentsAdminItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label paymentdate_payment;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\Elements\PaymentsAdminItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label paymentstatus_payment;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\Elements\PaymentsAdminItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label paymentmethod_payment;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\Elements\PaymentsAdminItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label idclient_payment;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\Elements\PaymentsAdminItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonAdd;
        
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
            System.Uri resourceLocater = new System.Uri("/Фотосалон;component/elements/paymentsadminitem.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Elements\PaymentsAdminItem.xaml"
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
            this.idorder_payment = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.fullcost_payment = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.paymentdate_payment = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.paymentstatus_payment = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.paymentmethod_payment = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.idclient_payment = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.buttonAdd = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\..\Elements\PaymentsAdminItem.xaml"
            this.buttonAdd.Click += new System.Windows.RoutedEventHandler(this.Click_remove);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

