﻿#pragma checksum "..\..\..\Windows\GenerateSequenceWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "927D418D87877F9D36C8EF6848D5BBF1"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
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
    /// GenerateSequenceWindow
    /// </summary>
    public partial class GenerateSequenceWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\Windows\GenerateSequenceWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock firstStep;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\Windows\GenerateSequenceWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox veritces;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\Windows\GenerateSequenceWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button undo;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\Windows\GenerateSequenceWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button done;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\Windows\GenerateSequenceWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button reset;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\Windows\GenerateSequenceWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox seqName;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\Windows\GenerateSequenceWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox seqFinalDesc;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\Windows\GenerateSequenceWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox seqDesc;
        
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
            System.Uri resourceLocater = new System.Uri("/FSMControl;component/windows/generatesequencewindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Windows\GenerateSequenceWindow.xaml"
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
            this.firstStep = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.veritces = ((System.Windows.Controls.ComboBox)(target));
            
            #line 11 "..\..\..\Windows\GenerateSequenceWindow.xaml"
            this.veritces.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.Veritces_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.undo = ((System.Windows.Controls.Button)(target));
            
            #line 12 "..\..\..\Windows\GenerateSequenceWindow.xaml"
            this.undo.Click += new System.Windows.RoutedEventHandler(this.Undo_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.done = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\..\Windows\GenerateSequenceWindow.xaml"
            this.done.Click += new System.Windows.RoutedEventHandler(this.Done_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.reset = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\..\Windows\GenerateSequenceWindow.xaml"
            this.reset.Click += new System.Windows.RoutedEventHandler(this.Reset_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.seqName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.seqFinalDesc = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.seqDesc = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

