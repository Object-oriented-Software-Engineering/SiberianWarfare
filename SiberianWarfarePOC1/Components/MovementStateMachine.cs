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
        SWGameObject m_unit;
        public AKineticState(SWGameObject unit) {
            m_unit = unit;
        }
        public abstract List<Actions> GetAvailableActions();
        public abstract void ExecuteCommand(CommandArgs commandArguments);

    }

    public class MovingKineticState : AKineticState {
        public MovingKineticState(SWGameObject unit) : base(unit) {
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
        public StoppedKineticState(SWGameObject unit) : base(unit) {
        }
        public override List<Actions> GetAvailableActions() {
            return new List<Actions> { Actions.MOVE,  Actions.LOCATE };
        }

        public override void ExecuteCommand(CommandArgs commandArguments) {
            if (commandArguments is MoveCommand.MoveArgs moveArgs) {
                // Move the unit to the new position
            }
        }
    }

    public class StaticKineticState : AKineticState {
        public StaticKineticState(SWGameObject unit) : base(unit) {
        }
        public override List<Actions> GetAvailableActions() {
            return new List<Actions> { Actions.LOCATE };
        }

        public override void ExecuteCommand(CommandArgs commandArguments) {
            if (commandArguments is LocateCommand.LocationArgs moveArgs) {
                // Acquire the location of the unit
            }
        }
    }
    

    public class KineticStateMachine :IStateMachine, IComponent, ICommandReceiver {
        private Dictionary<LocationState, IState> mStates;
        private IState mCurrentState;

        public KineticStateMachine(SWGameObject unit)
        {
            mStates = new Dictionary<LocationState, IState> {
                {LocationState.MOVING, new MovingKineticState(unit)},
                {LocationState.STOPPED, new StoppedKineticState(unit)},
                {LocationState.STATIC, new StaticKineticState(unit)}
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
