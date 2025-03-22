using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SiberianWarfarePOC1.GameObjects
{
    public abstract class MovementState {
        protected List<Action> m_Actions = new List<Action>();
        private SWGameObject m_gameObject;



        protected MovementState(SWGameObject mGameObject) {
            m_gameObject = mGameObject;
        }
    }


    public class StationaryState : MovementState{
        public StationaryState(SWGameObject context) : base(context) {
            m_Actions.Add(new LocateAction());
        }
    }

    public class MovableState :MovementState{
        public MovableState(SWGameObject mGameObject) : base(mGameObject) {
            m_Actions.Add(new LocateAction());
            m_Actions.Add(new MoveAction());
        }
    }
    
    public class HideState : MovementState {
        public HideState(SWGameObject mGameObject) : base(mGameObject) {
            m_Actions.Add(new MoveAction());
        }
    }
}
