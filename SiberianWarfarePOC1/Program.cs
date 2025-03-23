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
            var gameState = new SiberianWarfareGameState();
            gameState.Initialize();
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

        public void Scenario() {
            var infantry = m_units.Where(unit => 
                unit.GetComponent<IImmutableName>().M_Name == "Infantry").FirstOrDefault();

            var availableActions = infantry?.GetComponent<MovementStateMachine>().GetAvailableActions();

            var moveaction = availableActions.OfType<MoveAction>();

            var moveArgs = new MoveAction.MoveArgs() {
                MNewPosition = new Vector3(2, 2, 2)
            };

            moveaction.First().execute(moveArgs);



        }
    }

    internal class SiberianWarfareGameState {
        private List<SWGameObject> m_gameObjects = new List<SWGameObject>();

        public void RegisterGameObject(SWGameObject gameObject) {
            m_gameObjects.Add(gameObject);
        }

        public void Initialize() {
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
            gameObject.AddComponent(
                new MovementStateMachine(gameObject,
                    MovementStateMachine.EMovementState.MOVABLE));
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
            warFactory.AddComponent(new MovementStateMachine(warFactory,
                MovementStateMachine.EMovementState.STATIONARY));
            RegisterGameObject(warFactory);
            
            var player = new Player();
            player.AddUnit(gameObject);
            player.AddUnit(warFactory);

            player.Scenario();


        }
    }
}
