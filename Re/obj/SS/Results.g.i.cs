﻿#pragma checksum "C:\Users\Administrator\Desktop\Repositories\Re.WP8\Re\Results.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "3BC6644CA94124675A195480EAA33A72"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18033
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Advertising.Mobile.UI;
using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace Re {
    
    
    public partial class Results : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal Microsoft.Advertising.Mobile.UI.AdControl AdUnit;
        
        internal System.Windows.Controls.TextBlock searchTerms;
        
        internal Microsoft.Phone.Controls.LongListMultiSelector lbxOutput;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/Re;component/Results.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.AdUnit = ((Microsoft.Advertising.Mobile.UI.AdControl)(this.FindName("AdUnit")));
            this.searchTerms = ((System.Windows.Controls.TextBlock)(this.FindName("searchTerms")));
            this.lbxOutput = ((Microsoft.Phone.Controls.LongListMultiSelector)(this.FindName("lbxOutput")));
        }
    }
}

