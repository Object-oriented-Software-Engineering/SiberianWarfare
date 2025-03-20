using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;

namespace SiberianWarfarePOC1 {
    internal class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello, World!");
        }
    }

    public class SWGameObjectComponent {

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
    }

    internal class SiberianWarfareGameState {
        private List<SWGameObject> m_gameObjects = new List<SWGameObject>();

        public void RegisterGameObject(SWGameObject gameObject) {
            m_gameObjects.Add(gameObject);
        }

        void Initialize() {
            var gameObject = new SWGameObject();
            gameObject.AddComponent(new TransformComponent(
                new Vector3(0,0,0),
                new Vector3(0,0,0),
                new Vector3(1,1,1)));
            gameObject.AddComponent(
                new UnitTypeCompoment(
                    "Army Personnel",
                    "Infantry",
                    "Basic infantry unit"));
            RegisterGameObject(gameObject);

            var warFactory = new SWGameObject();
            warFactory.AddComponent(new TransformComponent(
                new Vector3(10, 0, 0),
                new Vector3(0, 0, 0),
                new Vector3(1, 1, 1)));
            warFactory.AddComponent(
                new UnitTypeCompoment(
                    "Building",
                    "Heavy War Factory",
                    "Facility for producing heavy equipment"
                ));
            RegisterGameObject(warFactory);

            var player = new Player();
            player.AddUnit(gameObject);
            player.AddUnit(warFactory);

        }
    }
}
