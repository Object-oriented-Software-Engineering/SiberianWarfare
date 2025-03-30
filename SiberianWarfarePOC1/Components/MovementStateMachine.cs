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

        class MovementStateMachine_InitArgs : InitializationArgs {
            public EMovementState state;
        }

        private Dictionary<EMovementState, MovementState> m_states =
            new Dictionary<EMovementState, MovementState>();

        private MovementState m_currentState;

        public MovementState MCurrentState {
            get => m_currentState;
            set => m_currentState = value;
        }

        public MovementStateMachine(SWGameObject host,EMovementState state) {
            // Create StateMachine States
            m_states[EMovementState.STATIONARY] = new StationaryState(host);
            m_states[EMovementState.MOVABLE] = new MovableState(host);
            m_states[EMovementState.HIDE] = new HideState(host);

            Initialize(new MovementStateMachine_InitArgs(){state = state});
        }

        public void Initialize(InitializationArgs init)
        {
            if (init is MovementStateMachine_InitArgs args) {
                SetState(args.state);
            }
        }

        public void SetState(EMovementState state) {
            m_currentState = m_states[state];
        }

        public IReadOnlyList<CAction> GetAvailableActions() {
            return m_states[m_movementState].MActions;
        }

        public void ExecuteAction(CAction action, ActionArgs args) {
            action.execute(args);
        }
    }

    public abstract class MovementState {

        MovementStateMachine.EMovementState m_movementState;

        protected List<CAction> m_Actions = new List<CAction>();
        public IReadOnlyList<CAction> MActions => m_Actions;
        private SWGameObject m_gameObject;

        protected MovementState(SWGameObject mGameObject,
            MovementStateMachine.EMovementState movementState) {
            m_gameObject = mGameObject;
            m_movementState = movementState;
        }
    }


    public class StationaryState : MovementState {
        public StationaryState(SWGameObject context) : 
            base(context,MovementStateMachine.EMovementState.STATIONARY) {
            m_Actions.Add(new LocateAction(context));
        }
    }

    public class MovableState : MovementState {
        public MovableState(SWGameObject mGameObject) : 
            base(mGameObject, MovementStateMachine.EMovementState.MOVABLE) {
            m_Actions.Add(new LocateAction(mGameObject));
            m_Actions.Add(new MoveAction(mGameObject));
        }
    }

    public class HideState : MovementState {
        public HideState(SWGameObject mGameObject) : 
            base(mGameObject, MovementStateMachine.EMovementState.HIDE) {
            m_Actions.Add(new MoveAction(mGameObject));
        }
    }
}
