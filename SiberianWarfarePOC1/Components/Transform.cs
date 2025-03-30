using SiberianWarfarePOC1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SiberianWarfarePOC1.Components
{
    internal abstract class Transform : IComponent {
        protected Vector3 _position;
        protected Vector3 _rotation;
        protected Vector3 _scale;

        protected Transform(Vector3 position, Vector3 rotation, Vector3 scale) {
            _position = position;
            _rotation = rotation;
            _scale = scale;
        }
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


    internal class TransformComponent :
        Transform, IImmutableTransform, IMutableTransform {
        public TransformComponent(
            Vector3 position, Vector3 rotation, Vector3 scale) :
            base(position, rotation, scale) {
        }

        Vector3 IImmutableTransform.Position => _position;
        Vector3 IImmutableTransform.Rotation => _rotation;
        Vector3 IImmutableTransform.Scale => _scale;

        Vector3 IMutableTransform.Position {
            get => _position;
            set => _position = value;
        }
        Vector3 IMutableTransform.Rotation {
            get => _rotation;
            set => _rotation = value;
        }
        Vector3 IMutableTransform.Scale {
            get => _scale;
            set => _scale = value;
        }
    }
}
