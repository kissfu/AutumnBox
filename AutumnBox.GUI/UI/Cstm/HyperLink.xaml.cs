﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutumnBox.GUI.UI.Cstm
{
    /// <summary>
    /// HyperLink.xaml 的交互逻辑
    /// </summary>
    public partial class HyperLink : UserControl
    {
        public static readonly DependencyProperty HyperlinkProperty =
            DependencyProperty.Register("Hyperlink", typeof(string), typeof(HyperLink),
                new PropertyMetadata("http://atmb.top"));
        public string Hyperlink
        {
            get
            {
                return (string)GetValue(HyperlinkProperty);
            }
            set
            {
                SetValue(HyperlinkProperty, value);
            }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(HyperLink),
                new PropertyMetadata("Hyper Link"));
        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }

        public HyperLink()
        {
            InitializeComponent();
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            TBMain.TextDecorations = TextDecorations.Underline;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            TBMain.TextDecorations = null;
        }

        private void TBMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Hyperlink);
        }
    }
}
