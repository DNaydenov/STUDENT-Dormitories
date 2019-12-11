using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace GUI
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AddModifySensorWindow : Window
    {
        public AddModifySensorWindow()
        {
            InitializeComponent();
        }

        private void ConfirmAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClearAddData_Click(object sender, RoutedEventArgs e)
        {
            ClearUserInput();
        }

        private void CancelAdd_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void ClearUserInput()
        {
            this.Name0.Clear();
            this.Name1.Clear();
            this.Name2.Clear();
            this.Name3.Clear();
            this.Name4.Clear();
            this.Name5.Clear();
            this.Name6.Clear();
            this.Name7.Clear();
            this.Name8.Clear();
            this.Name9.Clear();
        }
    }
}
