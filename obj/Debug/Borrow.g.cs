﻿#pragma checksum "..\..\Borrow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "ED9933AB4306D6F29A8E8DCF38C074159D6726337BC619513B7C4A2547D638F5"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using LibraryManageSystem;
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


namespace LibraryManageSystem {
    
    
    /// <summary>
    /// Borrow
    /// </summary>
    public partial class Borrow : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\Borrow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GroupBox result;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\Borrow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox list;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\Borrow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button addBook;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\Borrow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button canBorrow;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\Borrow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Bookname;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\Borrow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Writer;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\Borrow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button inquery;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\Borrow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox list1;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\Borrow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button delete;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\Borrow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button borrow;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\Borrow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button showall;
        
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
            System.Uri resourceLocater = new System.Uri("/LibraryManageSystem;component/borrow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Borrow.xaml"
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
            this.result = ((System.Windows.Controls.GroupBox)(target));
            return;
            case 2:
            this.list = ((System.Windows.Controls.ListBox)(target));
            return;
            case 3:
            this.addBook = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\Borrow.xaml"
            this.addBook.Click += new System.Windows.RoutedEventHandler(this.addBook_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.canBorrow = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\Borrow.xaml"
            this.canBorrow.Click += new System.Windows.RoutedEventHandler(this.canBorrow_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Bookname = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.Writer = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.inquery = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\Borrow.xaml"
            this.inquery.Click += new System.Windows.RoutedEventHandler(this.inquery_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.list1 = ((System.Windows.Controls.ListBox)(target));
            return;
            case 9:
            this.delete = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\Borrow.xaml"
            this.delete.Click += new System.Windows.RoutedEventHandler(this.delete_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.borrow = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\Borrow.xaml"
            this.borrow.Click += new System.Windows.RoutedEventHandler(this.borrow_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.showall = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\Borrow.xaml"
            this.showall.Click += new System.Windows.RoutedEventHandler(this.showall_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

