using SiberianWarfarePOC1.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SiberianWarfarePOC1.GameObjects
{
    internal class SiberianWarfareGameState : SWGameObject {
        private static SiberianWarfareGameState _instance;
        private static readonly object _lock = new object();
        private List<SWGameObject> m_gameObjects = new List<SWGameObject>();

        private SiberianWarfareGameState() { }



        public static SiberianWarfareGameState Instance {
            get {
                lock (_lock) {
                    if (_instance == null) {
                        _instance = new SiberianWarfareGameState();
                    }
                    return _instance;
                }
            }
        }

        public void RegisterGameObject(SWGameObject gameObject) {
            m_gameObjects.Add(gameObject);
        }

        public void Initialize() {
            var gameObject = new SWGameObject();
            gameObject.AddComponent(new TransformComponent(
                new Vector3(0, 0, 0),
                new Vector3(0, 0, 0),
                new Vector3(1, 1, 1)));
            gameObject.AddComponent(
                new UnitTypeCompoment(
                    "Army Personnel",
                    "Infantry",
                    "Basic infantry unit"));
            gameObject.AddComponent(
                new KineticStateMachine(gameObject));
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
            warFactory.AddComponent(new KineticStateMachine(warFactory));
            RegisterGameObject(warFactory);

            var player = new Player();
            player.AddUnit(gameObject);
            player.AddUnit(warFactory);

            player.Scenario1_MoveUnit();
        }
    }
}
