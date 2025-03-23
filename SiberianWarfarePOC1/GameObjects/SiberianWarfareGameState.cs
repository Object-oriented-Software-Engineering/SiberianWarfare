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
        private List<SWGameObject> m_gameObjects = new List<SWGameObject>();

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
