﻿#pragma checksum "..\..\Guide.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "37CC113E9152791A9E5E5D213CD17257633CD2EB"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using BpNFCApp;
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


namespace BpNFCApp {
    
    
    /// <summary>
    /// Guide
    /// </summary>
    public partial class Guide : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\Guide.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TimerTextBlock;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\Guide.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image GuideImage;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\Guide.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label GuideLabel;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\Guide.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Done1Button;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\Guide.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Done2Button;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\Guide.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button StartButton;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\Guide.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button NewMeasurementButton;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\Guide.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button NewMeasurement2Button;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\Guide.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button TransferButton;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\Guide.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SupportButton;
        
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
            System.Uri resourceLocater = new System.Uri("/BpNFCApp;component/guide.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Guide.xaml"
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
            this.TimerTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.GuideImage = ((System.Windows.Controls.Image)(target));
            return;
            case 3:
            this.GuideLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.Done1Button = ((System.Windows.Controls.Button)(target));
            
            #line 19 "..\..\Guide.xaml"
            this.Done1Button.Click += new System.Windows.RoutedEventHandler(this.Done1Button_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Done2Button = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\Guide.xaml"
            this.Done2Button.Click += new System.Windows.RoutedEventHandler(this.Done2Button_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.StartButton = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\Guide.xaml"
            this.StartButton.Click += new System.Windows.RoutedEventHandler(this.StartButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.NewMeasurementButton = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\Guide.xaml"
            this.NewMeasurementButton.Click += new System.Windows.RoutedEventHandler(this.NewMeasurementButton_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.NewMeasurement2Button = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\Guide.xaml"
            this.NewMeasurement2Button.Click += new System.Windows.RoutedEventHandler(this.NewMeasurement2Button_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.TransferButton = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\Guide.xaml"
            this.TransferButton.Click += new System.Windows.RoutedEventHandler(this.TransferButton_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.SupportButton = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

