using FiniteStateMachineViewer.DomainModel;
using FiniteStateMachineViewer.ViewModel;
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

namespace FiniteStateMachineViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// The machine
        /// </summary>
        public StateMachine machine;

        public MainWindow()
        {        
            machine = new StateMachine();
            machine.ViewMachineConfiguration();
            this.DataContext = machine.Graph;
            InitializeComponent();
        }

        /// <summary>
        /// Handles the StateMachine event of the Draw control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Draw_StateMachine(object sender, RoutedEventArgs e)
        {
            machine.RepresentThisMachine(Colors.Yellow);
            this.DataContext = machine.Graph;
        }

        /// <summary>
        ///View the entire state machine configuration.
        /// </summary>
        private void View_Configuration(object sender, RoutedEventArgs e)
        {
             machine = new StateMachine();
             machine.ViewMachineConfiguration();
             this.DataContext = machine.Graph;
        }

        /// <summary>
        /// Represents the selected sequence from combobox
        /// </summary>
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FSMSequence sequence = new FSMSequence();
            OperationResult result=new OperationResult();
            if (cmbBox.SelectedIndex != -1)
            {
                sequence = (FSMSequence)cmbBox.SelectedItem;
                result=machine.RepresentOneSequence(sequence,Colors.Yellow);
              if(!result.Succes)
              {
                  MessageBox.Show(result.Message);
                  machine.RepresentOneSequence(sequence, Colors.Red);
              }
                this.DataContext = machine.Graph;
            }
        }
    }
}
