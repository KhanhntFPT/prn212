﻿#pragma checksum "..\..\..\..\CustomerSide\PaymentPopup.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "614F8232767E9DEA97BAD40499E69C03F2D69E2B"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Project.CustomerSide;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace Project.CustomerSide {
    
    
    /// <summary>
    /// PaymentPopup
    /// </summary>
    public partial class PaymentPopup : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\..\..\CustomerSide\PaymentPopup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock NameText;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\..\CustomerSide\PaymentPopup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock bienso;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\..\CustomerSide\PaymentPopup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock giovao;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\..\CustomerSide\PaymentPopup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock giora;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\..\CustomerSide\PaymentPopup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ParkingFeeText;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\..\CustomerSide\PaymentPopup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock BalanceText;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\..\CustomerSide\PaymentPopup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button PayButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Project;component/customerside/paymentpopup.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\CustomerSide\PaymentPopup.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.NameText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.bienso = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.giovao = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.giora = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.ParkingFeeText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.BalanceText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.PayButton = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\..\..\CustomerSide\PaymentPopup.xaml"
            this.PayButton.Click += new System.Windows.RoutedEventHandler(this.PayButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

