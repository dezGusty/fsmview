namespace FSMControl.DomainModel.Model.Interfaces
{
  public interface ISequenceConfigInterface<T>
  {
    void LoadFromXMLFileAbsolute(string absoluteFilePath);

    T LoadFromXML(string xmlContent);
  }
}