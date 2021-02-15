using NawafizApp.Data;
using NawafizApp.Services.Interfaces;
using NawafizApp.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    class Program
    {
        IEquipmentService _EquipmentService;
        public Program(IEquipmentService EquipmentService)

        {
            _EquipmentService = EquipmentService;
        }
        static List ts()
        {
            Console.WriteLine(_EquipmentService.All(11));

        }
        static void Main(string[] args)
        {
            Console.WriteLine(_EquipmentService.All(11));
            Console.ReadLine();
        }
    }
}
