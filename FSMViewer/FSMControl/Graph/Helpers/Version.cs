using System;
using System.Collections.Generic;
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
    public String configValidationString
    {
      get;
      set;
    }

    /// <value>
    /// The sequence validation string.
    /// </value>
    public String sequenceValidationString
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
      Version A = new Version();
      A.ID = 1;
      A.configValidationString = @"..\..\..\FSMControl\Resources\Schemas\ConfigVersion1.xsd";
      A.sequenceValidationString = @"..\..\..\FSMControl\Resources\Schemas\SequenceVersion1.xsd";
      list.Add(A);

      Version B = new Version();
      B.ID = 2;
      B.configValidationString = @"..\..\..\FSMControl\Resources\Schemas\ConfigVersion2.xsd";
      B.sequenceValidationString = @"..\..\..\FSMControl\Resources\Schemas\SequenceVersion2.xsd";
      list.Add(B);
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

    /// <summary>
    /// Gets the version.
    /// </summary>
    public void GetVersion()
    {
      Version.GetListOfVersions();
      this.GetXmlPaths();
      if (this.Xml != null && this.XmlSeq != null)
      {
        XmlSchemaSet schema;
        foreach (Version v in Version.GetListOfVersions())
        {
          schema = new XmlSchemaSet();
          schema.Add(null, v.configValidationString);
          XDocument document = XDocument.Load(this.Xml);
          bool validationErrorConfig = true;
          document.Validate(schema, (s, ex) =>
          {
            validationErrorConfig = false;
          });
          schema = new XmlSchemaSet();
          schema.Add(null, v.sequenceValidationString);
          document = XDocument.Load(this.XmlSeq);
          bool validationErrorSeq = true;
          document.Validate(schema, (s, ex) =>
          {
            validationErrorSeq = false;
          });
          if (validationErrorConfig && validationErrorSeq)
          {
            this.ID = v.ID;
            this.sequenceValidationString = v.sequenceValidationString;
            this.configValidationString = v.configValidationString;
          }
        }
      }
    }
  }
}