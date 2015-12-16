namespace FSMControl.DomainModel.Model.Interfaces
{
    public interface IConfigInterface<ALLOWT, CONFIG, TRIGGER, STATE, STEP>
    {
        void LoadFromXMLFileAbsolute(string absoluteFilePath);

        CONFIG LoadFromXML(string xmlContent);

        STATE FoundNextState(string stateName);

        TRIGGER FoundTriggerInList(STEP stepp);

        TRIGGER FoundTriggerInList(ALLOWT trig);
    }
}