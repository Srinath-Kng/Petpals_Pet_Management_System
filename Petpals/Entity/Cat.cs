using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petpals.Entity
{
    public class Cat : Pet
    {

        public string CatColor { get; set; }


        public Cat(string name, int age, string breed, string catColor) : base(name, age, breed)
        {
            CatColor = catColor;
        }

        public string GetCatColor()
        {
            return CatColor;
        }

        public void SetCatColor(string catColor)
        {
            CatColor = catColor;
        }
    }
}
