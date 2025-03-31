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
        protected SWGameObject m_unit;
        protected IStateMachine m_stateMachine;
        public AKineticState(SWGameObject unit, IStateMachine stateMachine) {
            m_unit = unit;
            m_stateMachine = stateMachine;
        }
        public abstract List<Actions> GetAvailableActions();
        public abstract void ExecuteCommand(CommandArgs commandArguments);

    }

    public class MovingKineticState : AKineticState {
        public MovingKineticState(SWGameObject unit, IStateMachine state) :
            base(unit,state) {
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
        public StoppedKineticState(SWGameObject unit,IStateMachine state) : 
            base(unit, state) {
        }
        public override List<Actions> GetAvailableActions() {
            return new List<Actions> { Actions.MOVE,  Actions.LOCATE };
        }

        public override void ExecuteCommand(CommandArgs commandArguments) {
            if (commandArguments is MoveCommand.MoveArgs moveArgs) {
                m_unit.GetComponent<IMutableTransform>().Position = moveArgs.MNewPosition;
                m_stateMachine.SetState(LocationState.MOVING);
            }
        }
    }

    public class StaticKineticState : AKineticState {
        public StaticKineticState(SWGameObject unit,IStateMachine state) : 
            base(unit, state) {
        }
        public override List<Actions> GetAvailableActions() {
            return new List<Actions> { Actions.LOCATE };
        }

        public override void ExecuteCommand(CommandArgs commandArguments) {
            if (commandArguments is LocateCommand.LocationArgs moveArgs)
            {
                moveArgs.MPosition = m_unit.GetComponent<IImmutableTransform>().Position;
            }
        }
    }

    public class KineticStateMachine :IStateMachine, IComponent, ICommandReceiver {
        private Dictionary<LocationState, IState> mStates;
        private IState mCurrentState;

        public KineticStateMachine(SWGameObject unit)
        {
            mStates = new Dictionary<LocationState, IState> {
                {LocationState.MOVING, new MovingKineticState(unit,this)},
                {LocationState.STOPPED, new StoppedKineticState(unit,this)},
                {LocationState.STATIC, new StaticKineticState(unit, this)}
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
