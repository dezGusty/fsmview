namespace FSMControl.DomainModel.Model.Interfaces
{
    public interface IStateInterface<AT, T>
    {
        AT FoundTriggerInCureentState(T trig);
    }
}