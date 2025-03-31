using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiberianWarfarePOC1.GameObjects;
using SiberianWarfarePOC1.Interfaces;

namespace SiberianWarfarePOC1.Components {

    public enum LocationState {
        MOVING,
        STOPPED,
        STATIC
    }

    public enum Actions {
        MOVE,
        STOP,
        LOCATE
    }

    public interface IState {
        List<Actions> GetAvailableActions();
        void ExecuteCommand(CommandArgs commandArguments);
    }

    public interface IStateMachine {
        void SetState(LocationState state);
    }

    public abstract class AKineticState : IState, ICommandReceiver {
        protected IComponentProvider m_unit;
        protected IStateMachine m_stateMachine;
        public AKineticState(IComponentProvider unit, IStateMachine stateMachine) {
            m_unit = unit;
            m_stateMachine = stateMachine;
        }
        public abstract List<Actions> GetAvailableActions();
        public abstract void ExecuteCommand(CommandArgs commandArguments);

    }

    public class MovingKineticState : AKineticState {
        public MovingKineticState(IComponentProvider unit, IStateMachine state) :
            base(unit, state) {
        }

        public override List<Actions> GetAvailableActions() {
            return new List<Actions> { Actions.STOP, Actions.LOCATE };
        }

        public override void ExecuteCommand(CommandArgs commandArguments) {
            if (commandArguments is MoveCommand.MoveArgs moveArgs) {
                // change course to the new position

            }
        }
    }

    public class StoppedKineticState : AKineticState {
        public StoppedKineticState(IComponentProvider unit, IStateMachine state) :
            base(unit, state) {
        }
        public override List<Actions> GetAvailableActions() {
            return new List<Actions> { Actions.MOVE, Actions.LOCATE };
        }

        public override void ExecuteCommand(CommandArgs commandArguments) {
            if (commandArguments is MoveCommand.MoveArgs moveArgs) {
                m_unit.GetComponent<IMutableTransform>().Position = moveArgs.MNewPosition;
                m_stateMachine.SetState(LocationState.MOVING);
            }
        }
    }

    public class StaticKineticState : AKineticState {
        public StaticKineticState(IComponentProvider unit, IStateMachine state) :
            base(unit, state) {
        }
        public override List<Actions> GetAvailableActions() {
            return new List<Actions> { Actions.LOCATE };
        }

        public override void ExecuteCommand(CommandArgs commandArguments) {
            if (commandArguments is LocateCommand.LocationArgs moveArgs) {
                moveArgs.MPosition = m_unit.GetComponent<IImmutableTransform>().Position;
            }
        }
    }

    public interface IStateFactory {
        IState CreateMovingState(IComponentProvider unit, IStateMachine stateMachine);
        IState CreateStoppedState(IComponentProvider unit, IStateMachine stateMachine);
        IState CreateStaticState(IComponentProvider unit, IStateMachine stateMachine);
    }

    public class MoveStateFactory : IStateFactory {
        public IState CreateMovingState(IComponentProvider unit,
            IStateMachine stateMachine) {
            return new MovingKineticState(unit, stateMachine);
        }

        public IState CreateStoppedState(IComponentProvider unit,
            IStateMachine stateMachine) {
            return new StoppedKineticState(unit, stateMachine);
        }

        public IState CreateStaticState(IComponentProvider unit,
            IStateMachine stateMachine) {
            return new StaticKineticState(unit, stateMachine);
        }
    }

    public class KineticStateMachine : IStateMachine, IComponent, ICommandReceiver {
        private Dictionary<LocationState, IState> mStates;
        private IState mCurrentState;

        public KineticStateMachine(IComponentProvider unit,IStateFactory factory) {
            mStates = new Dictionary<LocationState, IState> {
                {LocationState.MOVING, factory.CreateMovingState(unit,this)},
                {LocationState.STOPPED, factory.CreateStoppedState(unit,this)},
                {LocationState.STATIC, factory.CreateStaticState(unit, this)}
            };
            mCurrentState = mStates[LocationState.STATIC];
        }

        public void SetState(LocationState state) {
            mCurrentState = mStates[state];
        }

        public List<Actions> GetAvailableActions() {
            return mCurrentState.GetAvailableActions();
        }

        public void ExecuteCommand(CommandArgs commandArguments) {
            mCurrentState.ExecuteCommand(commandArguments);
        }
    }

}
