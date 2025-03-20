using System.Numerics;
using System.Reflection.Metadata.Ecma335;

namespace SiberianWarfarePOC1 {
    internal class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello, World!");
        }
    }

    internal class SWGameObjectComponent {

    }

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


    class SWTransformComponent :
           ASWTransformComponent, IImmutablePosition, IMutablePosition, IImmutableRotation, IMutableRotation {
        public SWTransformComponent(
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



    internal class SWGameObjectTypeComponent : SWGameObjectComponent {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }

    internal class SWGameObject {
        private List<SWGameObjectComponent> m_components = new List<SWGameObjectComponent>();

        public void AddComponent(SWGameObjectComponent component) {
            m_components.Add(component);
        }

        public void RemoveComponent(SWGameObjectComponent component) {
            m_components.Remove(component);
        }

        public T GetComponent<T>() where T : class {
            return m_components.OfType<T>().FirstOrDefault() ??
                   throw new NullReferenceException("Request of non-existing component");
        }
    }

    internal class Player {
        List<SWGameObject> m_units = new List<SWGameObject>();

        public void AddUnit(SWGameObject unit) {
            m_units.Add(unit);
        }

        public void RemoveUnit(SWGameObject unit) {
            m_units.Remove(unit);
        }

        public void MoveUnit(SWGameObject unit, Vector3 newposition) {
            var transform = unit.GetComponent<SWTransformComponent>();
            transform.Position = newposition;
        }
    }

    internal class SiberianWarfareGameState {
        private List<SWGameObject> m_gameObjects = new List<SWGameObject>();

        public void RegisterGameObject(SWGameObject gameObject) {
            m_gameObjects.Add(gameObject);
        }

        void Initialize() {
            var gameObject = new SWGameObject();
            gameObject.AddComponent(new SWTransformComponent { Position = new Vector3(0, 0, 0) });
            gameObject.AddComponent(
                new SWGameObjectTypeComponent {
                    Type = "Army Personnel",
                    Name = "Infantry",
                    Description = "Basic infantry unit"
                });
            RegisterGameObject(gameObject);

            var warFactory = new SWGameObject();
            warFactory.AddComponent(new SWTransformComponent { Position = new Vector3(10, 0, 0) });
            warFactory.AddComponent(
                new SWGameObjectTypeComponent {
                    Type = "Building",
                    Name = "Heavy War Factory",
                    Description = "Facility for producing heavy equipment"
                });
            RegisterGameObject(warFactory);
        }
    }
}
