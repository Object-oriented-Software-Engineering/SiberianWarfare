using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiberianWarfarePOC1.Interfaces
{
    public class InitializationArgs {
        
    }

    public interface IComponent
    {
        void Initialize(InitializationArgs init);
    }
}
