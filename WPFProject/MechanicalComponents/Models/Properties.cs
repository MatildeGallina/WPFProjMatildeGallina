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
        decimal? Price { get; }
        int? FreeMaintenance { get; }
        int? WarrantyPeriod { get; }
        string Material { get; }
        int? Year { get; }

        bool Savable { get; set; }
    }

    public class MultiChildrenNodeProperties : IProperties
    {
        public MultiChildrenNodeProperties()
        {
            Savable = false;
        }

        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal? Price { get; set; }
        public int? FreeMaintenance { get; set; }
        public bool Savable { get; set; }

        public int? WarrantyPeriod { get; }
        public string Material { get; }
        public int? Year { get; }
    }

    public class SingleChildrenNodeProperties : IProperties
    {
        public SingleChildrenNodeProperties()
        {
            Savable = false;
        }

        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal? Price { get; set; }
        public int? WarrantyPeriod { get; set; }
        public bool Savable { get; set; }

        public int? FreeMaintenance { get; }
        public string Material { get; }
        public int? Year { get; }
    }

    public class NullChildrenNodeProperties : IProperties
    {
        public NullChildrenNodeProperties()
        {
            Savable = false;
        }

        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal? Price { get; set; }
        public string Material { get; set; }
        public int? Year { get; set; }
        public bool Savable { get; set; }

        public int? FreeMaintenance { get; }
        public int? WarrantyPeriod { get; }
    }
}
