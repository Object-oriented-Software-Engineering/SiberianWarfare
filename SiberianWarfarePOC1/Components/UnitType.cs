using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiberianWarfarePOC1.Interfaces;

namespace SiberianWarfarePOC1.Components
{
    public abstract class UnitType : IComponent {
        protected string _type;
        protected string _name;
        protected string _description;

        protected UnitType(string type,
            string name, string description) {
            _type = type;
            _name = name;
            _description = description;
        }
    }

    public interface IImmutableType {
        string M_Type { get; }
    }
    public interface IMutableType {
        string M_Type { get; set; }
    }
    public interface IImmutableName {
        string M_Name { get; }
    }
    public interface IMutableName {
        string M_Name { get; set; }
    }
    public interface IImmutableDescription {
        string M_Description { get; }
    }
    public interface IMutableDescription {
        string M_Description { get; set; }
    }

    public class UnitTypeCompoment:
        UnitType, IImmutableType, IMutableType,
        IImmutableName, IMutableName, IImmutableDescription,
        IMutableDescription {

        public UnitTypeCompoment(string type, string name, string description) :
            base(type, name, description) {
        }

        string IImmutableType.M_Type => _type;
        string IMutableType.M_Type {
            get => _type;
            set => _type = value;
        }
        string IImmutableName.M_Name => _name;
        string IMutableName.M_Name {
            get => _name;
            set => _name = value;
        }
        string IImmutableDescription.M_Description => _description;
        string IMutableDescription.M_Description {
            get => _description;
            set => _description = value;
        }
    }
}
