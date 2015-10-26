﻿using QuickGraph.Serialization;
using System;
using System.Linq;
using System.Windows.Media;
using System.Xml;

namespace FiniteStateMachineViewer
{
    /// <summary>
    /// Serializes and deserializes a CustomGraph
    /// </summary>
    public static class SerializeHelper
  {
    /// <summary>
    /// Loads the graph.
    /// </summary>
    /// <param name="filename">The filename.</param>
    /// <returns></returns>
    public static CustomGraph LoadGraph(string filename)
    {
      //open the file of the graph
      var reader = XmlReader.Create(filename);

      //create the serializer
      var serializer = new GraphMLDeserializer<CustomVertex, CustomEdge, CustomGraph>();

      //graph where the vertices and edges should be put in
      var customGraph = new CustomGraph();

      //deserialize the graph
      serializer.Deserialize(reader, customGraph, id => new CustomVertex(id.Split(';').First(),
                                   (Color)ColorConverter.ConvertFromString(id.Split(';').ElementAt(1)),
                                   Double.Parse(id.Split(';').ElementAt(2)),
                                   Double.Parse(id.Split(';').ElementAt(3))),
                                   (source, target, name) => new CustomEdge(name, source, target, Colors.Black));
      return customGraph;
    }

    /// <summary>
    /// Saves the graph.
    /// </summary>
    /// <param name="graph">The graph.</param>
    /// <param name="filename">The filename.</param>
    public static void SaveGraph(CustomGraph graph, string filename)
    {
      //create the xml writer
      using (var writer = XmlWriter.Create(filename))
      {
        var serializer = new GraphMLSerializer<CustomVertex, CustomEdge, CustomGraph>();
        //serialize the graph
        serializer.Serialize(writer, graph, v => v.Text + ";" + v.BackgroundColor.ToString() + ";" + v.X.ToString() + ";" + v.Y.ToString(), e => e.Trigger);
      }
    }
  }
}