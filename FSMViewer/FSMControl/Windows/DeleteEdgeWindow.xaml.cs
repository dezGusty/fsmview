using FSMControl.DomainModel.FirstVersion;
using FSMControl.DomainModel.SecondVersion;
using System.Windows;

namespace FSMControl.Windows
{
    /// <summary>
    /// Interaction logic for DeleteEdgeWindow.xaml
    /// </summary>
    public partial class DeleteEdgeWindow : Window
    {
        private StateMachine machine;

        public DeleteEdgeWindow(StateMachine fsm)
            : this()
        {
            if (fsm is FirstStateMachine)
            {
                this.machine = new FirstStateMachine(fsm.CurrentVersion);
            }
            else
            {
                if (fsm is SecondStateMachine)
                {
                    this.machine = new SecondStateMachine(fsm.CurrentVersion);
                }
            }

            this.machine = fsm;
        }

        public DeleteEdgeWindow()
        {
            this.InitializeComponent();
        }

        private void BtnDeleteEdge(object sender, RoutedEventArgs e)
        {
            if (txtTo.Text.Equals(string.Empty) || txtFrom.Text.Equals(string.Empty))
            {
                MessageBox.Show("Invalid name!" + "\n" + "It cannot be empty!");
                return;
            }
            else
            {
                if (this.machine != null)
                {
                    if (this.machine is FirstStateMachine)
                    {
                        MessageBox.Show(((FirstStateMachine)this.machine).DeleteEdge(txtFrom.Text, txtTo.Text));
                    }
                    else
                    {
                        MessageBox.Show(((SecondStateMachine)this.machine).DeleteEdge(txtFrom.Text, txtTo.Text));
                    }

                    this.DataContext = this.machine.MyGraph;
                }
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}