﻿using DormitorySensor;
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
using Microsoft.Maps.MapControl.WPF;

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
            InitMap();
            InitViewAllList(dataGrid);

            //this.DataContext = new MainWindowViewModel();

            //Http client is initialized at the start of our app
            ApiHelper.InitializeClient();
        }
        public void InitMap()
        {
            
            Pushpin pin1 = new Pushpin();
            pin1.Location = new Location(42.698334, 23.319941);
            BingMap.Children.Add(pin1);
            BingMap.ZoomLevel = 9;
            //Sofia location
            BingMap.Center = new Location(42.698334, 23.319941);
        }

        public void InitViewAllList(DataGrid dataGrid)
        {
           //SensorList.AddSensor("asd", "asd", sensorType.humidity, (20, 20), (10,40)); 
           //SensorList.AddSensor("asd", "asasdsd", sensorType.humidity, (20, 20), (10, 40)); 
           dataGrid.ItemsSource = SensorList.ListSensors;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            //(this.DataContext as MainWindowViewModel).AddSensor("asd2", "asasdsd", sensorType.humidity, 20, 20, new Tuple<double, double>(0,50));
            AddModifySensorWindow AddSensorWindow = new AddModifySensorWindow((sender as Button).Content.ToString());
            AddSensorWindow.ShowDialog();
            //SensorList.ListSensors.Last();
        }

        
        private void Modify_Click(object sender, RoutedEventArgs e)
        {
            var sensorToModify = (Sensor)dataGrid.SelectedItem;
            AddModifySensorWindow ModifySensorWindow = new AddModifySensorWindow((sender as Button).Content.ToString(), sensorToModify);
            
            ModifySensorWindow.txtName.Text = sensorToModify.Name;
            ModifySensorWindow.txtDescription.Text = sensorToModify.Description;
            //To Fix enum types 
            ModifySensorWindow.CBoxType.SelectedItem = ModifySensorWindow.CBoxType.FindName(sensorToModify.Type.ToString());
            ModifySensorWindow.txtLatitude.Text = sensorToModify.Location.latitude.ToString();
            ModifySensorWindow.txtLongtitude.Text = sensorToModify.Location.longtitude.ToString();
            ModifySensorWindow.txtMinValue.Text = sensorToModify.AcceptableValues.min.ToString();
            ModifySensorWindow.txtMaxValue.Text = sensorToModify.AcceptableValues.max.ToString();
            
            
            ModifySensorWindow.ShowDialog();
            //dataGrid.Items.Refresh();


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

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private async void loadSensorInfo_Click(object sender, RoutedEventArgs e)
        {
            //var sensorInfo = await SensorProcessor.LoadSensorInfo();
            //sensorDateText.Text = $"Sensor date {sensorInfo.timeStamp}";
            ////sensorTypeText.Text = $"Sensor type {sensorInfo.valueType}";
            //sensorTypeText.Text = $"Sensor value {sensorInfo.value}";
            ////sunsetText.Text = $"Sunset is at {sunInfo.measure_type_of_value}";
        }
    }
}
