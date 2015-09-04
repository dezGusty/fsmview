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

        MyGraph g = new MyGraph();

        public MainWindow()
        {
            fi = CreateMachine();
            g.CreateNodesFromStateMachine(fi,"aaa");
            this.DataContext = g;
            InitializeComponent();
        }

        public StateMachine CreateMachine()
        {
            var Q = new List<string> { "q0", "q1", "q2" };
            var Sigma = new List<char> { 'a' };
            var Delta = new List<Transition>{
                    new Transition("q0", 'a', "q1"),
                     new Transition("q1", 'a', "q2"),
                     new Transition("q2", 'a', "q1")
                };
            var Q0 = "q0";
            var F = new List<string> { "q0", "q2", "q1" };
            var dFSM = new StateMachine(Q, Sigma, Delta, Q0, F);
            return dFSM;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;

            var vertex = menuItem.Tag as SampleVertex;

            vertex.Change();
        }


    }
}
