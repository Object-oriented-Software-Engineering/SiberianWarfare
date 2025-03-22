using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;
using SiberianWarfarePOC1.Components;
using SiberianWarfarePOC1.GameObjects;
using SiberianWarfarePOC1.Interfaces;

namespace SiberianWarfarePOC1 {
    internal class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello, World!");
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
            gameObject.MState = new MovableState(gameObject);
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
            gameObject.MState = new StationaryState(warFactory);
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
            warFactory.MState

            var player = new Player();
            player.AddUnit(gameObject);
            player.AddUnit(warFactory);

        }
    }
}
