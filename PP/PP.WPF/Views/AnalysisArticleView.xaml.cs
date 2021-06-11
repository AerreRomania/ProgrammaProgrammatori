using PP.Domain.Services;
using PP.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PP.WPF.Views
{
    /// <summary>
    /// Interaction logic for AnalysisArticle.xaml
    /// </summary>
    public partial class AnalysisArticleView 
    {
       
        public AnalysisArticleView()
        {
            InitializeComponent();
        }
        private void ArticleCB_DropDownOpened(object sender, EventArgs e)
        {
            ArticleCB.SelectedIndex = -1;
        }
    }
}
