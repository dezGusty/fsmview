namespace FSMControl.DomainModel.Model.Interfaces
{
  public interface ITriggerInterface<T>
  {
    bool CompareTo(T trigger);
  }
}