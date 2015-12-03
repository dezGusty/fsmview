using FSMControl.DomainModel.FirstVersion;
using FSMControl.DomainModel.SecondVersion;
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

namespace FSMControl.Windows
{
    /// <summary>
    /// Interaction logic for AddVertexWindow.xaml
    /// </summary>
    public partial class AddVertexWindow : Window
    {
        private StateMachine machine;

        public AddVertexWindow()
        {
            InitializeComponent();
        }

        public AddVertexWindow(StateMachine fsm)
            : this()
        {
            if(fsm is FirstStateMachine)
            {
                this.machine = new FirstStateMachine(fsm.Xml, fsm.XmlSeq, fsm.CurrentVersion);
            }
            else
            {
                if(fsm is SecondStateMachine)
                {
                    this.machine = new SecondStateMachine(fsm.Xml, fsm.XmlSeq, fsm.CurrentVersion);
                }
            }
            this.machine = fsm;
        }

        private void btnAddVertex(object sender, RoutedEventArgs e)
        {
            string text = txtAddVertex.Text;
            if (string.IsNullOrEmpty(text))
            {
                MessageBox.Show("Invalid name!" + "\n" + "It cannot be empty!");
                Close();
            }
            else
            {
                if(machine!=null)
                {
                    if ((machine.MyGraph.Vertices.Where(v => String.Compare(v.Text.ToLower(), txtAddVertex.Text.ToLower()) == 0).FirstOrDefault() != null))
                    {
                        MessageBox.Show("A vertex with the name << " + text + " >>  already exists in the graph");
                        Close();
                    }
                    else
                    {
                        CustomVertex vertex = new CustomVertex(text, Colors.Wheat);
                        if(machine is FirstStateMachine)
                        {
                            FSMState state = new FSMState();
                             state.Name = vertex.Text;
                                if (!string.IsNullOrEmpty(txt_DefaultHandler.Text))
                            {
                                state.DefaultHandler = txt_DefaultHandler.Text;
                            }

                            if (!string.IsNullOrEmpty(txt_Reentry.Text))
                            {
                                state.ReentryUsingTrigger = txt_Reentry.Text;
                            }
                            ((FirstStateMachine)machine).config.ArrayOfFSMState.Add(state);
                            this.DataContext = machine.MyGraph;
                        }
                        else
                        {
                            FSMVState state = new FSMVState();
                             state.Name = vertex.Text;
                                if (!string.IsNullOrEmpty(txt_DefaultHandler.Text))
                            {
                                state.DefaultHandler = txt_DefaultHandler.Text;
                            }

                            if (!string.IsNullOrEmpty(txt_Reentry.Text))
                            {
                                state.ReentryUsingTrigger = txt_Reentry.Text;
                            }
                            ((SecondStateMachine)machine).config.ArrayOfFSMVState.Add(state);
                        }
                        machine.MyGraph.AddVertex(vertex);
                        txtAddVertex.Text = string.Empty;
                        this.DataContext = machine.MyGraph;
                    }
                }
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
