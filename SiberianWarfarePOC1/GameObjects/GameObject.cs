using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiberianWarfarePOC1.Interfaces;

namespace SiberianWarfarePOC1.GameObjects
{
    public class SWGameObject {
        private List<IComponent> m_components = new List<IComponent>();

       public SWGameObject() {}

        public void AddComponent(IComponent component) {
            m_components.Add(component);
        }

        public void RemoveComponent(IComponent component) {
            m_components.Remove(component);
        }

        public T GetComponent<T>() where T : class {
            return m_components.OfType<T>().FirstOrDefault() ??
                   throw new NullReferenceException("Request of non-existing component");
        }
    }

    
}
