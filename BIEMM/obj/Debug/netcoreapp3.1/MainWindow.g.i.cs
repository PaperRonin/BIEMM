﻿#pragma checksum "..\..\..\MainWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "DDFF8FADA74AFA191FE75D1204E30B9835F456DA"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using BIEMM;
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


namespace BIEMM {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 41 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CloseButton;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button MinimizeButton;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid ModMenu;
        
        #line default
        #line hidden
        
        
        #line 117 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button OpenModsButton;
        
        #line default
        #line hidden
        
        
        #line 118 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ReloadListButton;
        
        #line default
        #line hidden
        
        
        #line 119 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ApplyModsButton;
        
        #line default
        #line hidden
        
        
        #line 120 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button RunGameButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.8.1.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/BIEMM;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.8.1.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 9 "..\..\..\MainWindow.xaml"
            ((BIEMM.MainWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.MainWindow_OnLoaded);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 38 "..\..\..\MainWindow.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Header_OnMouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.CloseButton = ((System.Windows.Controls.Button)(target));
            
            #line 47 "..\..\..\MainWindow.xaml"
            this.CloseButton.Click += new System.Windows.RoutedEventHandler(this.CloseButton_OnClick);
            
            #line default
            #line hidden
            return;
            case 4:
            this.MinimizeButton = ((System.Windows.Controls.Button)(target));
            
            #line 56 "..\..\..\MainWindow.xaml"
            this.MinimizeButton.Click += new System.Windows.RoutedEventHandler(this.MinimizeButton_OnClick);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ModMenu = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 6:
            this.OpenModsButton = ((System.Windows.Controls.Button)(target));
            
            #line 117 "..\..\..\MainWindow.xaml"
            this.OpenModsButton.Click += new System.Windows.RoutedEventHandler(this.OpenModsButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ReloadListButton = ((System.Windows.Controls.Button)(target));
            
            #line 118 "..\..\..\MainWindow.xaml"
            this.ReloadListButton.Click += new System.Windows.RoutedEventHandler(this.ReloadListButton_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.ApplyModsButton = ((System.Windows.Controls.Button)(target));
            
            #line 119 "..\..\..\MainWindow.xaml"
            this.ApplyModsButton.Click += new System.Windows.RoutedEventHandler(this.ApplyModsButton_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.RunGameButton = ((System.Windows.Controls.Button)(target));
            
            #line 120 "..\..\..\MainWindow.xaml"
            this.RunGameButton.Click += new System.Windows.RoutedEventHandler(this.RunGameButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

