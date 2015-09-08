using FiniteStateMachine.DomainModel;
using FiniteStateMachine.Machine;
using FiniteStateMachineTest.DomainModel;
using FiniteStateMachineTest.Graph;
using FiniteStateMachineTest.Machine;
using QuickGraph;
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

namespace FiniteStateMachine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        StateMachine fi = new StateMachine();

        public MainWindow()
        {
          this.DataContext = fi.Graph;
          InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            var vertex = menuItem.Tag as SampleVertex;
            vertex.Change();
        }

        private void Show_Graph(object sender, RoutedEventArgs e)
        {
            this.DataContext = fi.Graph;
            console.Text = fi.RepresentThisMachine();
        }

        private void cmbBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FSMSequence s = new FSMSequence();
            if (cmbBox.SelectedIndex != -1)
            {
                s = (FSMSequence)cmbBox.SelectedItem;
            }
            console.Text = fi.RepresentOneSequence(s);

        }

}
}
