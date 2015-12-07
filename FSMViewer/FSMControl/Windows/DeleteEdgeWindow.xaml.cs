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
        this.machine = new FirstStateMachine(fsm.Xml, fsm.XmlSeq, fsm.CurrentVersion);
      }
      else
      {
        if (fsm is SecondStateMachine)
        {
          this.machine = new SecondStateMachine(fsm.Xml, fsm.XmlSeq, fsm.CurrentVersion);
        }
      }
      this.machine = fsm;
    }

    public DeleteEdgeWindow()
    {
      InitializeComponent();
    }

    private void btnDeleteEdge(object sender, RoutedEventArgs e)
    {
      if (txtTo.Text.Equals(string.Empty) || txtFrom.Text.Equals(string.Empty))
      {
        MessageBox.Show("Invalid name!" + "\n" + "It cannot be empty!");
        return;
      }

      if (machine is FirstStateMachine)
      {
        CustomVertex vtxTo = machine.MyGraph.GetVertexByName(txtTo.Text);
        CustomVertex vtxFrom = machine.MyGraph.GetVertexByName(txtFrom.Text);

        if (vtxFrom == null)
        {
          MessageBox.Show(string.Format("Vertex with name {0} doesn't exist!", txtFrom.Text));
          return;
        }

        if (vtxTo == null)
        {
          MessageBox.Show(string.Format("Vertex with name {0} doesn't exist!", txtTo.Text));
          return;
        }

        //machine.MyGraph.DeleteEdge(vtxFrom, vtxTo);
        machine.MyGraph.RemoveEdgeIf(v => v.Source.Text.Equals(vtxFrom.Text) && v.Target.Text.Equals(vtxTo.Text));

        foreach (var item in ((FirstStateMachine)machine).config.ArrayOfFSMState)
        {
          if (item.Name == vtxFrom.Text)
          {
            foreach (var it in item.ArrayOfAllowedTrigger.ToList())
            {
              if (it.StateAndTriggerName == vtxTo.Text || it.StateName == vtxTo.Text)
              {
                item.ArrayOfAllowedTrigger.Remove(it);
              }
            }
          }
        }
      }
      else
      {
        CustomVertex vtxTo = machine.MyGraph.GetVertexByName(txtTo.Text);
        CustomVertex vtxFrom = machine.MyGraph.GetVertexByName(txtFrom.Text);

        if (vtxFrom == null)
        {
          MessageBox.Show(string.Format("Vertex with name {0} doesn't exist!", txtFrom.Text));
          return;
        }

        if (vtxTo == null)
        {
          MessageBox.Show(string.Format("Vertex with name {0} doesn't exist!", txtTo.Text));
          return;
        }

        //machine.MyGraph.DeleteEdge(vtxFrom, vtxTo);
        machine.MyGraph.RemoveEdgeIf(v => v.Source.Text.Equals(vtxFrom.Text) && v.Target.Text.Equals(vtxTo.Text));

        foreach (var item in ((SecondStateMachine)machine).config.ArrayOfFSMVState)
        {
          if (item.Name == vtxFrom.Text)
          {
            foreach (var it in item.ArrayOfAllowedTrigger.ToList())
            {
              if (it.StateAndTriggerName == vtxTo.Text || it.StateName == vtxTo.Text)
              {
                item.ArrayOfAllowedTrigger.Remove(it);
              }
            }
          }
        }
      }
      this.DataContext = machine.MyGraph;
    }

    private void Cancel(object sender, RoutedEventArgs e)
    {
      this.Close();
    }
  }
}
