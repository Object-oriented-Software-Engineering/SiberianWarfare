using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using SiberianWarfarePOC1.Components;
using static SiberianWarfarePOC1.GameObjects.MoveAction;

namespace SiberianWarfarePOC1.GameObjects
{
    public class ActionArgs {

    }

    // CAction is an abstract class that represents an action
    // that can be performed on a GameObject. Specific actions
    // exist on the relevant GameObject states and are the hooks
    // that allow the GameObject to change or query its state.
    public abstract class CAction {
        // Game object that the action is performed on 
        protected SWGameObject m_GameObject;

        public CAction(SWGameObject gameObject) {
            m_GameObject = gameObject;
        }
        // Execute is a method that performs the action on the GameObject
        public abstract void execute(ActionArgs input);
    }

    public class MoveAction : CAction {

        // MoveArgs is a subclass of ActionArgs that contains a Vector3 field
        // that represents the new position of the GameObject after the move 
        public class MoveArgs : ActionArgs {
            private Vector3 m_newPosition;

            public Vector3 MNewPosition {
                get => m_newPosition;
                set => m_newPosition = value;
            }
        }

        // IMutablePosition is an interface that allows the GameObject
        // to have a mutable position.
        // The GameObject must have a TransformComponent.
        private IMutablePosition m_position;
        
        public MoveAction(SWGameObject targetGameObject) : base(targetGameObject){
            // GetComponent is a method that returns the first component of the specified type
            // that is attached to the GameObject. If no such component exists, it throws an exception.
            // IMutablePosition is an interface of the TransformComponent class that describes
            // a mutable position of the GameObject.
            m_position = m_GameObject.GetComponent<IMutablePosition>() ??
                         throw new NotSupportedException(); 
        }

        public override void execute(ActionArgs input) {
            // MoveArgs is a subclass of ActionArgs that contains a Vector3 field
            // that represents the new position of the GameObject after the move
            MoveArgs moveVector = input as MoveArgs ?? throw new NullReferenceException();
            // Set the new position of the GameObject.
            m_position.M_Position = moveVector.MNewPosition;
        }
    }

    // LocateAction is an action that locates the position of the GameObject
    // and returns it as a Vector3 through the MoveArgs class.
    public class LocateAction : CAction {
        // IImmutablePosition is an interface that allows the GameObject 
        // to have an immutable position.
        private IImmutablePosition m_position;
        // Position is a property that returns the position of the GameObject
        // as a Vector3 struct.
        public Vector3 Position => m_position.M_Position;

        public LocateAction(SWGameObject targetGameObject) : base(targetGameObject) {
            // GetComponent is a method that returns the first component of the specified type
            // that is attached to the GameObject. If no such component exists, it throws an exception.
            // IImmutablePosition is an interface of the TransformComponent class that describes
            // an immutable position of the GameObject.
            m_position = m_GameObject.GetComponent<IImmutablePosition>();
        }

        // Execute is a method that performs the action on the GameObject. In this case,
        // it returns the position of the GameObject as a Vector3 struct.
        public override void execute(ActionArgs input) {
            // MoveArgs is a subclass of ActionArgs that contains a Vector3 field
            MoveArgs moveVector = input as MoveArgs ?? throw new NullReferenceException();
            // Return the position of the GameObject by setting the new position in the MoveArgs class.
            moveVector.MNewPosition = Position;
        }
    }

    // HideAction is an action that hides the GameObject.It makes its position
    // unreachable through a LocateAction. 
    public class HideAction : CAction {
        public HideAction(SWGameObject targetGameObject) :
            base(targetGameObject) {
        }

        public override void execute(ActionArgs input) {
            throw new NotImplementedException();
        }
    }
}
