using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SiberianWarfarePOC1
{
    internal abstract class ASWTransformComponent : SWGameObjectComponent {
        protected Vector3 _position;
        protected Vector3 _rotation;
        protected Vector3 _scale;

        protected ASWTransformComponent(Vector3 position, Vector3 rotation, Vector3 scale) {
            _position = position;
            _rotation = rotation;
            _scale = scale;
        }
    }

    public interface IImmutablePosition {
        Vector3 M_Position { get; }
    }
    public interface IMutablePosition {
        Vector3 M_Position { get; set; }
    }
    public interface IImmutableRotation {
        Vector3 M_Rotation { get; }
    }
    public interface IMutableRotation {
        Vector3 M_Rotation { get; set; }
    }
    public interface IImmutableScale {
        Vector3 M_Scale { get; }
    }
    public interface IMutableScale {
        Vector3 M_Scale { get; set; }
    }
    
    internal class TransformComponent :
        ASWTransformComponent, IImmutablePosition, IMutablePosition, IImmutableRotation, IMutableRotation, IImmutableScale, IMutableScale {
        public TransformComponent(
            Vector3 position, Vector3 rotation, Vector3 scale) :
            base(position, rotation, scale) {
        }

        Vector3 IImmutablePosition.M_Position => _position;
        Vector3 IMutablePosition.M_Position {
            get => _position;
            set => _position = value;
        }

        Vector3 IImmutableRotation.M_Rotation => _rotation;
        Vector3 IMutableRotation.M_Rotation {
            get => _rotation;
            set => _rotation = value;
        }

        Vector3 IImmutableScale.M_Scale => _scale;
        Vector3 IMutableScale.M_Scale {
            get => _scale;
            set => _scale = value;
        }
    }
}
