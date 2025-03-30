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

    /// <summary>
    ///  ACtion is an abstract class that represents an action
    /// that can be performed on a GameObject.  
    /// </summary>
    public abstract class ACommand {

        protected ICommandReceiver m_receiver;

        public ACommand(ICommandReceiver receiver) {
            m_receiver = receiver;
        }

        public abstract void execute(CommandArgs args);
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

        public MoveCommand(Vector3 newPosiiton, ICommandReceiver receiver): base(receiver) {
            m_targetPosition = new MoveArgs(){MNewPosition = newPosiiton};
        }

        public override void execute(CommandArgs args) {
            if (args is MoveArgs moveArgs) {
                m_receiver.ExecuteCommand(moveArgs);
            }
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

            public LocationArgs() {
               m_Position = Vector3.Zero;  
            }
        }
        public LocateCommand(ICommandReceiver receiver) :
            base(receiver) {
        }

        public override void execute(CommandArgs args) {
            if (args is LocationArgs locationArgs) {
                m_receiver.ExecuteCommand(locationArgs);
            }
        }
    }

    public class HideCommand : ACommand {
        public HideCommand(ICommandReceiver receiver) :
            base(receiver) {
        }
        public override void execute(CommandArgs args) {
            m_receiver.ExecuteCommand(new CommandArgs());
        }
    }

}
