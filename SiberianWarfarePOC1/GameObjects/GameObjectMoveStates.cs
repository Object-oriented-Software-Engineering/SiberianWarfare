using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using SiberianWarfarePOC1.GameObjects;

namespace SiberianWarfarePOC1.GameObjectMoveStates
{
    public abstract class MovementState {

        public enum EMovementState {
            STATIONARY,
            MOVABLE,
            HIDE
        }

        protected List<CAction> m_Actions = new List<CAction>();
        public IReadOnlyList<CAction> MActions => m_Actions;
        private SWGameObject m_gameObject;
        
        protected MovementState(SWGameObject mGameObject) {
            m_gameObject = mGameObject;
        }
    }


    public class StationaryState : MovementState{
        public StationaryState(SWGameObject context) : base(context) {
            m_Actions.Add(new LocateAction(context));
        }
    }

    public class MovableState :MovementState{
        public MovableState(SWGameObject mGameObject) : base(mGameObject) {
            m_Actions.Add(new LocateAction(mGameObject));
            m_Actions.Add(new MoveAction(mGameObject));
        }
    }
    
    public class HideState : MovementState {
        public HideState(SWGameObject mGameObject) : base(mGameObject) {
            m_Actions.Add(new MoveAction(mGameObject));
        }
    }
}
