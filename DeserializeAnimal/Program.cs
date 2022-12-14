using Animals;
using System.Xml;
using System.Xml.Serialization;

XmlSerializer serializer = new(typeof(Animal), new Type[] { typeof(Cow), typeof(Lion), typeof(Pig) });

string filepath = @"C:\Users\stepp\source\repos\Lab08(Streams)\SerializeAnimal\bin\Debug\net6.0\";

Console.WriteLine("Enter name of the file to deserialize:");
string filename = filepath + Console.ReadLine();

FileStream file = new FileStream(filename, FileMode.Open);

Animal animal = (Animal) (serializer.Deserialize(file) ?? new object());

DisplayAnimal(animal);

 void DisplayAnimal(Animal animal)
{
    Console.WriteLine("/----------------------------------/");
    Console.WriteLine($"{animal.Name} ({animal.WhatAnimal}) from {animal.Country}");
    Console.WriteLine($"{animal.Name} is {animal.GetClassificationAnimal()}");
    Console.WriteLine($"{animal.Name} love {animal.GetFavouriteFood()}");
    Console.WriteLine($"{animal.Name} hides from animals:");

    if (animal.HideFromOtherAnimals.Count == 0)
    {
        Console.WriteLine("\tNobody. He's fearless!");
        return;
    }

    int i = 1;
    foreach (var enemy in animal.HideFromOtherAnimals)
    {
        Console.WriteLine($"\t{i++}. {enemy.Name}({enemy.WhatAnimal}) from {enemy.Country};");
    }
}
