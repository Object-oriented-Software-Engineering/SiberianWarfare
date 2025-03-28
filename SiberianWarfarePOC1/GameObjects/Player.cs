﻿using SiberianWarfarePOC1.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SiberianWarfarePOC1.GameObjects
{
    internal class Player : SWGameObject {
        List<SWGameObject> m_units = new List<SWGameObject>();

        public void AddUnit(SWGameObject unit) {
            m_units.Add(unit);
        }

        public void RemoveUnit(SWGameObject unit) {
            m_units.Remove(unit);
        }

        public void Scenario() {
            var infantry = m_units.Where(unit =>
                unit.GetComponent<IImmutableName>().M_Name == "Infantry").FirstOrDefault();

            var availableActions = infantry?.GetComponent<MovementStateMachine>().GetAvailableActions();

            var moveaction = availableActions.OfType<MoveAction>();

            var moveArgs = new MoveAction.MoveArgs() {
                MNewPosition = new Vector3(2, 2, 2)
            };

            moveaction.First().execute(moveArgs);
        }
    }
}
