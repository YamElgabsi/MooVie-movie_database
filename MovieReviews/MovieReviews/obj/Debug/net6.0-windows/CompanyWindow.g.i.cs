﻿#pragma checksum "..\..\..\CompanyWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "BC74A114F57B8ACF9AB86B5216AF2E75E3B032D3"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MovieReviews;
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


namespace MovieReviews {
    
    
    /// <summary>
    /// CompanyWindow
    /// </summary>
    public partial class CompanyWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 60 "..\..\..\CompanyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_create;
        
        #line default
        #line hidden
        
        
        #line 93 "..\..\..\CompanyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_editmode;
        
        #line default
        #line hidden
        
        
        #line 126 "..\..\..\CompanyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_saveChenge;
        
        #line default
        #line hidden
        
        
        #line 159 "..\..\..\CompanyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Minimize;
        
        #line default
        #line hidden
        
        
        #line 191 "..\..\..\CompanyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Close;
        
        #line default
        #line hidden
        
        
        #line 227 "..\..\..\CompanyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_image;
        
        #line default
        #line hidden
        
        
        #line 238 "..\..\..\CompanyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image image_view;
        
        #line default
        #line hidden
        
        
        #line 249 "..\..\..\CompanyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textbox_name;
        
        #line default
        #line hidden
        
        
        #line 276 "..\..\..\CompanyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textbox_date;
        
        #line default
        #line hidden
        
        
        #line 301 "..\..\..\CompanyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_delete;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.2.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/MovieReviews;component/companywindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\CompanyWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.2.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 14 "..\..\..\CompanyWindow.xaml"
            ((MovieReviews.CompanyWindow)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Window_MouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btn_create = ((System.Windows.Controls.Button)(target));
            
            #line 69 "..\..\..\CompanyWindow.xaml"
            this.btn_create.Click += new System.Windows.RoutedEventHandler(this.btn_create_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btn_editmode = ((System.Windows.Controls.Button)(target));
            
            #line 102 "..\..\..\CompanyWindow.xaml"
            this.btn_editmode.Click += new System.Windows.RoutedEventHandler(this.btn_editmode_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btn_saveChenge = ((System.Windows.Controls.Button)(target));
            
            #line 135 "..\..\..\CompanyWindow.xaml"
            this.btn_saveChenge.Click += new System.Windows.RoutedEventHandler(this.btn_saveChenge_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btn_Minimize = ((System.Windows.Controls.Button)(target));
            
            #line 167 "..\..\..\CompanyWindow.xaml"
            this.btn_Minimize.Click += new System.Windows.RoutedEventHandler(this.btn_Minimize_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btn_Close = ((System.Windows.Controls.Button)(target));
            
            #line 199 "..\..\..\CompanyWindow.xaml"
            this.btn_Close.Click += new System.Windows.RoutedEventHandler(this.btn_Close_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btn_image = ((System.Windows.Controls.Button)(target));
            
            #line 237 "..\..\..\CompanyWindow.xaml"
            this.btn_image.Click += new System.Windows.RoutedEventHandler(this.btn_image_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.image_view = ((System.Windows.Controls.Image)(target));
            return;
            case 9:
            this.textbox_name = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.textbox_date = ((System.Windows.Controls.TextBox)(target));
            return;
            case 11:
            this.btn_delete = ((System.Windows.Controls.Button)(target));
            
            #line 311 "..\..\..\CompanyWindow.xaml"
            this.btn_delete.Click += new System.Windows.RoutedEventHandler(this.btn_delete_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
