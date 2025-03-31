using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SiberianWarfarePOC1.Interfaces
{
    public class InitializationArgs {
        
    }

    public interface IComponent
    {
        
    }

    public interface IImmutableTransform {
        Vector3 Position { get; }
        Vector3 Rotation { get; }
        Vector3 Scale { get; }
    }

    public interface IMutableTransform {
        Vector3 Position { get; set; }
        Vector3 Rotation { get; set; }
        Vector3 Scale { get; set; }
    }
}
