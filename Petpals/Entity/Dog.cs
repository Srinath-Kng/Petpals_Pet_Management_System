using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petpals.Entity
{
    public class Dog : Pet
    {
        public string DogBreed { get; set; }

        public Dog(string name, int age, string breed, string dogBreed) : base(name, age, breed)
        {
            DogBreed = dogBreed;
        }

        public string GetDogBreed()
        {
            return DogBreed;
        }

        public void SetDogBreed(string dogBreed)
        {
            DogBreed = dogBreed;
        }
    }
}
