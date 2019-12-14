using DormitorySensor;
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

            SensorList.AddSensor(Name1.Text, Name2.Text,SensorType.noiseSensor, 0, 0);
            ClearUserInput();
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
            Name0.Clear();
            Name1.Clear();
            Name2.Clear();
            Name3.Clear();
            Name4.Clear();
            Name5.Clear();
            Name6.Clear();
            Name7.Clear();
            Name8.Clear();
            Name9.Clear();
        }
    }
}
