﻿#pragma checksum "..\..\..\Windows\AddEdgeToWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "805F167917787E2D040BEDFF128280CB"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
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


namespace FSMControl.Windows {
    
    
    /// <summary>
    /// AddEdgeToWindow
    /// </summary>
    public partial class AddEdgeToWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 19 "..\..\..\Windows\AddEdgeToWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox checkSN;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\Windows\AddEdgeToWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox checkSATN;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\Windows\AddEdgeToWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtFromVertex;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\Windows\AddEdgeToWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtToVertex;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\Windows\AddEdgeToWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox targetVeritces;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\Windows\AddEdgeToWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtTrigger;
        
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
            System.Uri resourceLocater = new System.Uri("/FSMControl;component/windows/addedgetowindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Windows\AddEdgeToWindow.xaml"
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
            this.checkSN = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 2:
            this.checkSATN = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 3:
            this.txtFromVertex = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.txtToVertex = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.targetVeritces = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.txtTrigger = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            
            #line 28 "..\..\..\Windows\AddEdgeToWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.BtnAddEdge);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
