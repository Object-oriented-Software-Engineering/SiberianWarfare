using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using SiberianWarfarePOC1.Components;

namespace SiberianWarfarePOC1.GameObjects
{
    public class CommandArgs {

    }

    public interface ICommandReceiver {
        void ExecuteCommand(CommandArgs commandArguments);
    }

    // CAction is an abstract class that represents an action
    // that can be performed on a GameObject. Specific actions
    // exist on the relevant GameObject states and are the hooks
    // that allow the GameObject to change or query its state.
    public abstract class ACommand {

        protected ICommandReceiver m_receiver;

        public ACommand(ICommandReceiver receiver) {
            m_receiver = receiver;
        }

        public abstract void execute();
    }

    public class MoveCommand : ACommand {

        // MoveArgs is a subclass of ActionArgs that contains a Vector3 field
        // that represents the new position of the GameObject after the move 
        public class MoveArgs : CommandArgs {
            private Vector3 m_newPosition;

            public Vector3 MNewPosition {
                get => m_newPosition;
                set => m_newPosition = value;
            }
        }
        private MoveArgs m_targetPosition;

        public MoveCommand(Vector3 newPosiiton, ICommandReceiver reciever): base(reciever) {
            m_targetPosition = new MoveArgs(){MNewPosition = newPosiiton};
        }

        public override void execute() {
            m_receiver.ExecuteCommand(m_targetPosition);
        }
    }

    // LocateAction is an action that locates the position of the GameObject
    // and returns it as a Vector3 through the MoveArgs class.
    public class LocateCommand : ACommand {
        public class LocationArgs : CommandArgs {
            private Vector3 m_Position;

            public Vector3 MPosition {
                get => m_Position;
                set => m_Position = value;
            }
        }
        public LocateCommand(ICommandReceiver receiver) :
            base(receiver) {
        }

        public override void execute() {
            m_receiver.ExecuteCommand(new LocationArgs());
        }
    }

    public class HideCommand : ACommand {
        public HideCommand(ICommandReceiver receiver) :
            base(receiver) {
        }
        public override void execute() {
            m_receiver.ExecuteCommand(new CommandArgs());
        }
    }

}
