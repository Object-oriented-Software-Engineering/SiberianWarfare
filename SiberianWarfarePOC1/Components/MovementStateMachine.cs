using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiberianWarfarePOC1.GameObjectMoveStates;
using SiberianWarfarePOC1.GameObjects;
using SiberianWarfarePOC1.Interfaces;

namespace SiberianWarfarePOC1.Components {
    public class MovementStateMachine : IComponent {
        public enum EMovementState {
            STATIONARY,
            MOVABLE,
            HIDE
        }

        private Dictionary<EMovementState, MovementState> m_states =
            new Dictionary<EMovementState, MovementState>();

        private MovementState m_currentState;
        EMovementState m_movementState;


        public MovementState MCurrentState {
            get => m_currentState;
            set => m_currentState = value;
        }

        public MovementStateMachine(SWGameObject host,EMovementState state) {
            // Create StateMachine States
            m_states[EMovementState.STATIONARY] = new StationaryState(host);
            m_states[EMovementState.MOVABLE] = new MovableState(host);
            m_states[EMovementState.HIDE] = new HideState(host);

            m_currentState = m_states[state];
            m_movementState = state;
        }

        public IReadOnlyList<CAction> GetAvailableActions() {
            return m_states[m_movementState].MActions;
        }

        public void ExecuteAction(CAction action, ActionArgs args) {
            action.execute(args);
        }



    }
}
