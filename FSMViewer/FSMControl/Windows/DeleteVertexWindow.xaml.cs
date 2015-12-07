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
  /// Interaction logic for DeleteVertexWindow.xaml
  /// </summary>
  public partial class DeleteVertexWindow : Window
  {

    private StateMachine machine;

    public DeleteVertexWindow(StateMachine fsm)
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

    public DeleteVertexWindow()
    {
      InitializeComponent();
    }

    private void txtDeleteVertex_TextChanged(object sender, TextChangedEventArgs e)
    {
      string typedString = txtDeleteVertex.Text;
      List<string> autoList = new List<string>();
      autoList.Clear();

      if (machine is FirstStateMachine)
      {
        foreach (var item in ((FirstStateMachine)machine).config.ArrayOfFSMState)
        {
          if (!string.IsNullOrEmpty(txtDeleteVertex.Text))
          {
            if (item.Name.ToLower().StartsWith(typedString))
            {
              autoList.Add(item.Name);
            }
          }
        }
      }
      else
      {
        foreach (var item in ((SecondStateMachine)machine).config.ArrayOfFSMVState)
        {
          if (!string.IsNullOrEmpty(txtDeleteVertex.Text))
          {
            if (item.Name.ToLower().StartsWith(typedString))
            {
              autoList.Add(item.Name);
            }
          }
        }
      }

      if (autoList.Count > 0)
      {
        lbSuggestion.ItemsSource = autoList;
        lbSuggestion.Focus();
        lbSuggestion.Visibility = Visibility.Visible;
      }
      else
      {
        if (txtDeleteVertex.Text.Equals(""))
        {
          lbSuggestion.Visibility = Visibility.Collapsed;
          lbSuggestion.ItemsSource = null;
        }
        else
        {
          lbSuggestion.Visibility = Visibility.Collapsed;
          lbSuggestion.ItemsSource = null;
        }
      }
    }

    private void btnDeleteVertex(object sender, RoutedEventArgs e)
    {

      string text = txtDeleteVertex.Text;
      if (string.IsNullOrEmpty(text))
      {
        MessageBox.Show("Invalid name!" + "\n" + "It cannot be empty!");
        Close();
      }
      else
      {
        if (machine is FirstStateMachine)
        {
          CustomVertex ctx = machine.MyGraph.GetVertexByName(text);
          if(ctx == null)
          {
            MessageBox.Show(string.Format("A vertex with name {0} doesn't exist in the graph!", text));
            return;
          }
          foreach (var item in ((FirstStateMachine)machine).config.ArrayOfFSMState)
          {
            if (item.Name == ctx.Text)
            {
              ((FirstStateMachine)machine).config.ArrayOfFSMState.Remove(item);
              break;
            }
          }
          machine.MyGraph.RemoveVertex(ctx);
          foreach (var item in ((FirstStateMachine)machine).config.ArrayOfFSMState)
          {
            foreach (var it in item.ArrayOfAllowedTrigger.ToList())
            {
              if (it.StateAndTriggerName == ctx.Text || it.StateName == ctx.Text)
              {
                item.ArrayOfAllowedTrigger.Remove(it);
              }
            }
          }
        }
        else
        {
          CustomVertex ctx = machine.MyGraph.GetVertexByName(text);
          if (ctx == null)
          {
            MessageBox.Show(string.Format("A vertex with name {0} doesn't exist in the graph!", text));
            return;
          }
          foreach (var item in ((SecondStateMachine)machine).config.ArrayOfFSMVState)
          {
            if (item.Name == ctx.Text)
            {
              ((SecondStateMachine)machine).config.ArrayOfFSMVState.Remove(item);
              break;
            }
          }
          machine.MyGraph.RemoveVertex(ctx);
          foreach (var item in ((SecondStateMachine)machine).config.ArrayOfFSMVState)
          {
            foreach (var it in item.ArrayOfAllowedTrigger.ToList())
            {
              if (it.StateAndTriggerName == ctx.Text || it.StateName == ctx.Text)
              {
                item.ArrayOfAllowedTrigger.Remove(it);
              }
            }
          }
        }
        DataContext = machine.MyGraph;
      }
    }

    private void Cancel(object sender, RoutedEventArgs e)
    {
      this.Close();
    }

    private void lbSuggestion_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      if (lbSuggestion != null)
      {
        lbSuggestion.Visibility = Visibility.Collapsed;
        txtDeleteVertex.TextChanged -= new TextChangedEventHandler(txtDeleteVertex_TextChanged);
        if (lbSuggestion.SelectedIndex != -1)
        {
          txtDeleteVertex.Text = lbSuggestion.SelectedItem.ToString();
        }
        txtDeleteVertex.TextChanged += new TextChangedEventHandler(txtDeleteVertex_TextChanged);
      }
    }

    private void txtDeleteVertex_LostFocus(object sender, RoutedEventArgs e)
    {
      if (string.IsNullOrEmpty(txtDeleteVertex.Text))
      {
        txtDeleteVertex.Visibility = System.Windows.Visibility.Collapsed;
        watermarkedText.Visibility = System.Windows.Visibility.Visible;
      }
    }

    private void watermarkedText_GotFocus(object sender, RoutedEventArgs e)
    {
      watermarkedText.Visibility = System.Windows.Visibility.Collapsed;
      txtDeleteVertex.Visibility = System.Windows.Visibility.Visible;
      txtDeleteVertex.Focus();
    }
  }
}
