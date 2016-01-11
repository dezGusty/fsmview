using System.Windows;
using FSMControl.DomainModel.FirstVersion;
using FSMControl.DomainModel.SecondVersion;

namespace FSMControl.Windows
{
    /// <summary>
    /// Interaction logic for AddEdgeWindow.xaml
    /// </summary>
    public partial class AddEdgeWindow : Window
    {
        private StateMachine machine;

        public AddEdgeWindow()
        {
            this.InitializeComponent();
        }

        public AddEdgeWindow(StateMachine fsm)
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
            this.DataContext = this.machine.MyGraph;
        }

        private void BtnAddEdge(object sender, RoutedEventArgs e)
        {
            CustomVertex vertexFrom;
            CustomVertex vertexTo;
            if (sourceVeritces.SelectedIndex == -1 || targetVeritces.SelectedIndex == -1)
            {
                MessageBox.Show("You must select a source and a target vertex!");
            }
            else
            {
                vertexFrom = new CustomVertex();
                vertexFrom = (CustomVertex)sourceVeritces.SelectedItem;
                vertexTo = new CustomVertex();
                vertexTo = (CustomVertex)targetVeritces.SelectedItem;

                string trigger = txtTrigger.Text.Trim();

                if (this.machine is FirstStateMachine)
                {
                    ((FirstStateMachine)this.machine).AddNewEdge(vertexFrom, trigger, vertexTo);
                    this.DataContext = this.machine.MyGraph;
                }
                else
                {
                    ((SecondStateMachine)this.machine).AddNewEdge(vertexFrom, trigger, vertexTo);
                    this.DataContext = this.machine.MyGraph;
                }
            }
        }
    }
}