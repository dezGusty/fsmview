using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMControl.DomainModel.Model.Interfaces
{
    public interface ITriggerInterface<T>
    {
        bool compareTo(T trigger);
    }
}
