using System.Collections.Generic;
using System.Windows;
using System.Xml.Linq;
using System.Xml.Schema;

namespace FSMControl
{
  public class Version
  {
    public string Xml
    {
      get;
      set;
    }

    public string XmlSeq
    {
      get;
      set;
    }

    /// <value>
    /// Number of version.
    /// </value>
    public int ID
    {
      get;
      set;
    }

    /// <value>
    /// The configuration validation string.
    /// </value>
    public string ConfigValidationString
    {
      get;
      set;
    }

    /// <value>
    /// The sequence validation string.
    /// </value>
    public string SequenceValidationString
    {
      get;
      set;
    }

    /// <summary>
    /// Gets the list of versions.
    /// </summary>
    /// <returns></returns>
    public static List<Version> GetListOfVersions()
    {
      List<Version> list = new List<Version>();
      Version versionA = new Version();
      versionA.ID = 1;
      versionA.ConfigValidationString = @"..\..\..\FSMControl\Resources\Schemas\ConfigVersion1.xsd";
      versionA.SequenceValidationString = @"..\..\..\FSMControl\Resources\Schemas\SequenceVersion1.xsd";
      list.Add(versionA);

      Version versionB = new Version();
      versionB.ID = 2;
      versionB.ConfigValidationString = @"..\..\..\FSMControl\Resources\Schemas\ConfigVersion2.xsd";
      versionB.SequenceValidationString = @"..\..\..\FSMControl\Resources\Schemas\SequenceVersion2.xsd";
      list.Add(versionB);
      return list;
    }

    /// <summary>
    /// Gets the XML paths.
    /// </summary>
    public void GetXmlPaths()
    {
      this.Xml = Utilities.GetPath("Select machine configuration");
      this.XmlSeq = Utilities.GetPath("Select machine sequnces");
    }

    public void GetXmlPathsAtOnce()
    {
      string[] xmls = Utilities.GetMultiplePaths("Select both files");
      if (xmls == null)
      {
        return;
      }

      if (xmls.Length != 2)
      {
        return;
      }

      this.Xml = xmls[0];
      this.XmlSeq = xmls[1];
    }

    /// <summary>
    /// Gets the version.
    /// </summary>
    public void GetVersion()
    {
      this.GetXmlPaths();
      if (this.Xml != null && this.XmlSeq != null)
      {
        XmlSchemaSet schema;
        foreach (Version v in Version.GetListOfVersions())
        {
          schema = new XmlSchemaSet();
          schema.Add(null, v.ConfigValidationString);
          XDocument document = XDocument.Load(this.Xml);
          bool validationErrorConfig = true;
          document.Validate(schema, (s, ex) => { validationErrorConfig = false; });
          schema = new XmlSchemaSet();
          schema.Add(null, v.SequenceValidationString);
          document = XDocument.Load(this.XmlSeq);
          bool validationErrorSeq = true;
          document.Validate(schema, (s, ex) => { validationErrorSeq = false; });
          if (validationErrorConfig && validationErrorSeq)
          {
            this.ID = v.ID;
            this.SequenceValidationString = v.SequenceValidationString;
            this.ConfigValidationString = v.ConfigValidationString;
            return;
          }
        }
      }
    }

    public void GetVersionMultiple()
    {
      this.GetXmlPathsAtOnce();
      if (this.Xml != null && this.XmlSeq != null)
      {
        XmlSchemaSet schema;
        foreach (Version v in Version.GetListOfVersions())
        {
          schema = new XmlSchemaSet();
          schema.Add(null, v.ConfigValidationString);
          XDocument document = XDocument.Load(this.Xml);
          bool validationErrorConfig = true;
          document.Validate(schema, (s, ex) => { validationErrorConfig = false; });
          schema = new XmlSchemaSet();
          schema.Add(null, v.SequenceValidationString);
          document = XDocument.Load(this.XmlSeq);
          bool validationErrorSeq = true;
          document.Validate(schema, (s, ex) => { validationErrorSeq = false; });
          if (validationErrorConfig && validationErrorSeq)
          {
            this.ID = v.ID;
            this.SequenceValidationString = v.SequenceValidationString;
            this.ConfigValidationString = v.ConfigValidationString;
            return;
          }
        }
      }

      string aux = this.Xml;
      this.Xml = this.XmlSeq;
      this.XmlSeq = aux;
      if (this.Xml != null && this.XmlSeq != null)
      {
        XmlSchemaSet schema;
        foreach (Version v in Version.GetListOfVersions())
        {
          schema = new XmlSchemaSet();
          schema.Add(null, v.ConfigValidationString);
          XDocument document = XDocument.Load(this.Xml);
          bool validationErrorConfig = true;
          document.Validate(schema, (s, ex) => { validationErrorConfig = false; });
          schema = new XmlSchemaSet();
          schema.Add(null, v.SequenceValidationString);
          document = XDocument.Load(this.XmlSeq);
          bool validationErrorSeq = true;
          document.Validate(schema, (s, ex) => { validationErrorSeq = false; });
          if (validationErrorConfig && validationErrorSeq)
          {
            this.ID = v.ID;
            this.SequenceValidationString = v.SequenceValidationString;
            this.ConfigValidationString = v.ConfigValidationString;
            return;
          }
        }
      }
    }
  }
}