using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebula._79Nebula.Models
{
    public class Weapon
    {

        /*
        Gloves  5-5-5
        Staff	6-5-4

        Dagger  3-8-4
        Sword	3-4-8

        Mace	8-1-6
        Hammer	6-0-9

        Pistol	1-6-8
        Rifle	6-1-8
         */

        public WeaponType Type { get; set; }
        public string Name { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Intelligence{ get; set; }

        public Weapon(WeaponType type)
        {
            Type = type;

            switch (type)
            {
                case WeaponType.Untyped:
                    Name = "Untyped";
                    Strength = 1;
                    Agility = 1;
                    Intelligence = 1;
                    break;
                case WeaponType.Gloves:
                    Name = "Gloves";
                    Strength = 5;
                    Agility = 5;
                    Intelligence = 5;
                    break;
                case WeaponType.Staff:
                    Name = "Staff";
                    Strength = 6;
                    Agility = 5;
                    Intelligence = 4;
                    break;
                case WeaponType.Dagger:
                    Name = "Dagger";
                    Strength = 3;
                    Agility = 8;
                    Intelligence = 4;
                    break;
                case WeaponType.Sword:
                    Name = "Sword";
                    Strength = 3;
                    Agility = 4;
                    Intelligence = 8;
                    break;
                case WeaponType.Mace:
                    Name = "Mace";
                    Strength = 8;
                    Agility = 1;
                    Intelligence = 6;
                    break;
                case WeaponType.Hammer:
                    Name = "Hammer";
                    Strength = 6;
                    Agility = 0;
                    Intelligence = 9;
                    break;
                case WeaponType.Pistol:
                    Name = "Pistol";
                    Strength = 1;
                    Agility = 6;
                    Intelligence = 8;
                    break;
                case WeaponType.Rifle:
                    Name = "Rifle";
                    Strength = 6;
                    Agility = 1;
                    Intelligence = 8;
                    break;
                default:
                    Name = "Untyped";
                    Strength = 1;
                    Agility = 1;
                    Intelligence = 1;
                    break;
            }
        }

        public override string ToString()
        {
            return Name;
        }

    }

    public enum WeaponType
    {
        Untyped,
        Gloves,
        Staff,
        Dagger,
        Sword,
        Mace,
        Hammer,
        Pistol,
        Rifle
    }
}
