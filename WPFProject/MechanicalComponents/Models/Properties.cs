using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalComponents.Models
{
    public interface IProperties
    {
        string Brand { get; }
        string Model { get; }
        double? Price { get; }
    }

    public class MultiChildrenNodeProperties : IProperties
    {
        public MultiChildrenNodeProperties()
        { }

        public string Brand { get; set; }
        public string Model { get; set; }
        public double? Price { get; set; }
        public int? FreeMaintenance { get; set; }
    }

    public class SingleChildrenNodeProperties : IProperties
    {
        public SingleChildrenNodeProperties()
        { }

        public string Brand { get; set; }
        public string Model { get; set; }
        public double? Price { get; set; }
        public int? WarrantyPeriod { get; set; }
    }

    public class NullChildrenNodeProperties : IProperties
    {
        public NullChildrenNodeProperties()
        { }

        public string Brand { get; set; }
        public string Model { get; set; }
        public double? Price { get; set; }
        public string Material { get; set; }
        public int? Year { get; set; }
    }
}
