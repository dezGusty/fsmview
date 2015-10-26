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

namespace FSMViewControl.Windows
{
    /// <summary>
    /// Interaction logic for AddEdgeWindow.xaml
    /// </summary>
    public partial class AddEdgeWindow : Window
    {
        private StateMachine machine = new StateMachine();

        public AddEdgeWindow()
        {
            InitializeComponent();
        }

        public AddEdgeWindow(StateMachine machine)
            : this()
        {
            this.machine = machine;
        }

        private void add_edge_(object sender, RoutedEventArgs e)
        {
            string textFrom = from.Text.Trim();
            string textTo = to.Text.Trim();
            string trig = trigger.Text.Trim();

            if (String.IsNullOrEmpty(textFrom) || String.IsNullOrEmpty(textTo) || String.IsNullOrEmpty(trig))
            {
                MessageBox.Show("Invalid name!" + "\n" + "It cannot be empty!");
                Close();
            }
            else
            {
                if (!(machine.Graph.existingVertices.Exists(v => v.Text.ToLower() == textFrom.ToLower())))
                {
                    MessageBox.Show(textFrom + " doesn't exist!");
                    Close();
                }
                else
                {
                    if (!(machine.Graph.existingVertices.Exists(v => v.Text.ToLower() == textTo.ToLower())))
                    {
                        MessageBox.Show(textTo + " doesn't exist!");
                        Close();
                    }
                    else
                    {
                        CustomVertex vtxFrom = machine.Graph.GetVertexByName(textFrom);
                        CustomVertex vtxTo = machine.Graph.GetVertexByName(textTo);
                        if (String.IsNullOrEmpty(machine.Graph.AddNewEdge(trig, vtxFrom, vtxTo, Colors.Black).ToString()))
                            MessageBox.Show("Edge from " + vtxFrom + " to " + vtxTo + " added successfully!");
                    }
                }
            }
        }
    }
}
