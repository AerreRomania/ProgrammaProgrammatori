﻿#pragma checksum "..\..\..\..\Views\AnalisiOperatoreView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0AB315E86139D69F9CF9E51D81CD22231E82D143"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using DevExpress.Mvvm;
using DevExpress.Mvvm.UI;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Mvvm.UI.ModuleInjection;
using DevExpress.Xpf.DXBinding;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.DataPager;
using DevExpress.Xpf.Editors.DateNavigator;
using DevExpress.Xpf.Editors.ExpressionEditor;
using DevExpress.Xpf.Editors.Filtering;
using DevExpress.Xpf.Editors.Flyout;
using DevExpress.Xpf.Editors.Popups;
using DevExpress.Xpf.Editors.Popups.Calendar;
using DevExpress.Xpf.Editors.RangeControl;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Editors.Settings.Extension;
using DevExpress.Xpf.Editors.Validation;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.ConditionalFormatting;
using DevExpress.Xpf.Grid.LookUp;
using DevExpress.Xpf.Grid.TreeList;
using PP.WPF.Behaviors;
using PP.WPF.Converters;
using PP.WPF.Views;
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


namespace PP.WPF.Views {
    
    
    /// <summary>
    /// AnalisiOperatoreView
    /// </summary>
    public partial class AnalisiOperatoreView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 113 "..\..\..\..\Views\AnalisiOperatoreView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CbAnno;
        
        #line default
        #line hidden
        
        
        #line 120 "..\..\..\..\Views\AnalisiOperatoreView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CbClient;
        
        #line default
        #line hidden
        
        
        #line 158 "..\..\..\..\Views\AnalisiOperatoreView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox AngajatiCB;
        
        #line default
        #line hidden
        
        
        #line 182 "..\..\..\..\Views\AnalisiOperatoreView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.GridControl AnalysisReport;
        
        #line default
        #line hidden
        
        
        #line 392 "..\..\..\..\Views\AnalisiOperatoreView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.GridControl AnalysisReportPercent;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.6.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/PP.WPF;component/views/analisioperatoreview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\AnalisiOperatoreView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.6.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.CbAnno = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 2:
            this.CbClient = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.AngajatiCB = ((System.Windows.Controls.ComboBox)(target));
            
            #line 162 "..\..\..\..\Views\AnalisiOperatoreView.xaml"
            this.AngajatiCB.DropDownOpened += new System.EventHandler(this.ComboBox_DropDownOpened);
            
            #line default
            #line hidden
            return;
            case 4:
            this.AnalysisReport = ((DevExpress.Xpf.Grid.GridControl)(target));
            return;
            case 5:
            this.AnalysisReportPercent = ((DevExpress.Xpf.Grid.GridControl)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

