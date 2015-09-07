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

        MyGraph g = new MyGraph();

        //se retine configurarea FSM, starile si trigarele
        FSMVConfig config = new FSMVConfig();

        //se citesc secventele
        FSMSequenceConfig seq = new FSMSequenceConfig();

        private string xml = "../../statemachinecfg.xml";
        private string xmlSeq = "../../statemachinecfgsequences.xml";

        public MainWindow()
        {
            seq = seq.LoadFromXML(xmlSeq);
            config = config.LoadFromXML(xml);
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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //for console displaying
            StringBuilder str = new StringBuilder();

            //current step
            FSMStep stepp = new FSMStep();

            //first sequence to be represented
            FSMSequence sequence = new FSMSequence();
            sequence = seq.ArrayOfSequence.ElementAt(0);

            //first step
            stepp = sequence.ArrayOfStep.First();
            str.Append("First step: " + stepp.Name+"\n");
            console.Text = str.ToString();

            //founds the trigger in the config file
            FSMVTrigger tr = new FSMVTrigger();
            tr = config.ArrayOfFSMVTrigger.Where(trig => trig.SequenceID == stepp.Name).FirstOrDefault();
            if (tr.Name.Length != 0)
            {
                str.Append("Found trigger: " + tr.Name+"\n");
                console.Text = str.ToString();
            }
            FSMVState initialState = new FSMVState();
            FSMVState otherState = new FSMVState();

            //Initial state
            initialState = config.ArrayOfFSMVState.FirstOrDefault();
            str.Append("Initial state is always: " + initialState.Name + "\n");
            console.Text = str.ToString();

            //founds the trigger in current state
            string newState = initialState.FoundTriggerInCureentState(tr);
            SampleVertex node1 = new SampleVertex(newState);
            g.Graph.AddVertex(node1); //adds a new node to graph

            //switch to new state
            otherState = config.ArrayOfFSMVState.Where(state => state.Name == newState).FirstOrDefault();
            str.Append("Next state is:" + otherState.Name + " Allowed triggers: " + otherState.ArrayOfAllowedTrigger.Count.ToString() + "\n");
            console.Text = str.ToString();

            for (int i=1;i<sequence.ArrayOfStep.Count;i++)
            {
                stepp = sequence.ArrayOfStep.ElementAt(i);
                str.Append("\nNext step: " + stepp.Name + "\n");
                console.Text = str.ToString();
                 tr = config.ArrayOfFSMVTrigger.Where(trig => trig.SequenceID == stepp.Name).FirstOrDefault();
                 str.Append("Found trigger: " + tr.Name + "\n");
                console.Text = str.ToString();
                 SampleVertex node2=new SampleVertex("");
                 newState = "";
                 newState = otherState.FoundTriggerInCureentState(tr);
            if (newState.Length == 0)
            {
                str.Append("The trigger does not exist in current state!" + "\n");
                console.Text = str.ToString();
            }
            else
            {
                node2.Text=newState;
                g.Graph.AddVertex(node2);
                g.Graph.AddEdge(new Edge<object>(node1, node2));
                node1 = node2;
                node2 = new SampleVertex("");
                initialState = otherState;
                otherState = config.ArrayOfFSMVState.Where(state => state.Name == newState).FirstOrDefault();
                str.Append("Next state is:" + otherState.Name + " Allowed triggers: " + otherState.ArrayOfAllowedTrigger.Count.ToString() + "\n");
                console.Text = str.ToString();
            }
            }
        }
    }
}
