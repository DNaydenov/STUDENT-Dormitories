using DormitorySensor;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsPresentation;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            
            InitializeComponent();

            InitViewAllList(dataGrid);

            //this.DataContext = new MainWindowViewModel();

            
        }

        public void InitViewAllList(DataGrid dataGrid)
        {
           SensorList.AddSensor("asd", "asd", SensorType.humidity, 20, 20, new Tuple<double,double> (10,40)); 
           SensorList.AddSensor("asd", "asasdsd", SensorType.humidity, 20, 20, new Tuple<double, double>(10, 40)); 
           dataGrid.ItemsSource = SensorList.ListSensors;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            //(this.DataContext as MainWindowViewModel).AddSensor("asd2", "asasdsd", SensorType.humidity, 20, 20);
            AddModifySensorWindow AddSensorWindow = new AddModifySensorWindow((sender as Button).Content.ToString());
            AddSensorWindow.ShowDialog();
        }

        
        private void Modify_Click(object sender, RoutedEventArgs e)
        {
            var sensorToModify = (Sensor)dataGrid.SelectedItem;
            AddModifySensorWindow ModifySensorWindow = new AddModifySensorWindow((sender as Button).Content.ToString(), sensorToModify);
            
            ModifySensorWindow.txtName.Text = sensorToModify.Name;
            ModifySensorWindow.txtDescription.Text = sensorToModify.Description;
            ModifySensorWindow.txtType.Text = sensorToModify.Type.ToString();
            ModifySensorWindow.txtLatitude.Text = sensorToModify.Latitude.ToString();
            ModifySensorWindow.Name5.Text = sensorToModify.Longtitude.ToString();
            ModifySensorWindow.Name6.Text = sensorToModify.AcceptableValues.Item1.ToString();
            ModifySensorWindow.Name7.Text = sensorToModify.AcceptableValues.Item2.ToString();
            //TODO
            ModifySensorWindow.Name9.Text = sensorToModify.Description;
            ModifySensorWindow.Name0.Text = sensorToModify.Description;
            
            ModifySensorWindow.ShowDialog();
            dataGrid.Items.Refresh();


        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            SensorList.Remove((Sensor)dataGrid.SelectedItem);


            //dataGrid.Items.Refresh();

            //dataGrid.Items.Remove(dataGrid.SelectedItem);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //(this.DataContext as MainWindowViewModel).Load();
        }

        //private void Window_Closing(object sender, RoutedEventArgs e)
        //{
            
        //}
    }
}
