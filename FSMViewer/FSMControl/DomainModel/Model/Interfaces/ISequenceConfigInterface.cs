using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMControl.DomainModel.Model.Interfaces
{
    public interface ISequenceConfigInterface<T>
    {
       void LoadFromXMLFileAbsolute(string absoluteFilePath);
       T LoadFromXML(string xmlContent);
    }
}
