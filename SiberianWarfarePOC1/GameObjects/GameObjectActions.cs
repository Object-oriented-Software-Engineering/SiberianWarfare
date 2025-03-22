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

    public abstract class CAction {
        protected SWGameObject m_GameObject;
        public abstract void execute(ActionArgs input);
    }

    public class MoveAction : CAction {

        public class MoveVector : ActionArgs {
            private Vector3 m_newPosition;

            public Vector3 MNewPosition {
                get => m_newPosition;
                set => m_newPosition = value;
            }
        }

        private IMutablePosition m_position;
        
        public MoveAction() {
            m_position = m_GameObject.GetComponent<IMutablePosition>(); 
        }

        public override void execute(ActionArgs input) {
            MoveVector moveVector = input as MoveVector ?? throw new NullReferenceException();
            m_position.M_Position = moveVector.MNewPosition;
        }
    }

    public class LocateAction : CAction {
        private IImmutablePosition m_position;
        public Vector3 Position => m_position.M_Position;

        public LocateAction() {
            m_position = m_GameObject.GetComponent<IImmutablePosition>();
        }
        public override void execute(ActionArgs input) {
            MoveVector moveVector = input as MoveVector ?? throw new NullReferenceException();
            moveVector.MNewPosition = Position;
        }
    }
    public class HideAction : Action {

    }

    public class DormantAction : Action {

    }
}
