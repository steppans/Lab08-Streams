using Animals;
using System.Linq.Expressions;
using System.Net;
using System.Xml;
using System.Xml.Serialization;

Lion Alex = new Lion("USA", new List<Animal>(0), "Alex");
Pig Anatoliy = new Pig("Brazil", new List<Animal>(0), "Anatoliy");
Pig Koba = new Pig("Uruguay", new List<Animal> { Anatoliy }, "Koba");
Cow Marina = new("Russia", new List<Animal> { Alex, Koba }, "Marina");

List<Animal> animals = new List<Animal>()
{
    Alex,
    Anatoliy,
    Koba,
    Marina,
};

Animal animalToSave = EnterYourChoice(animals);

CreateXmlFile(animalToSave, animalToSave.Name + ".xml");

Console.WriteLine("Data saved!");

void CreateXmlFile(Animal animal, string filename)
{
    XmlWriterSettings xmlWriterSettings = new XmlWriterSettings() { Indent = true };
    XmlSerializer serializer = new XmlSerializer(typeof(Animal), new Type[] { typeof(Cow), typeof(Lion), typeof(Pig) });
    using (XmlWriter writer = XmlWriter.Create(filename, xmlWriterSettings))
    {
        serializer.Serialize(writer, animal);
    }
}
;

Animal EnterYourChoice(List<Animal> animals)
{
    Console.WriteLine("Enter, what information you want to save about");
    int choice = 0;
    while (choice < 1 || choice > animals.Count)
    {
        int i = 1;
        foreach(Animal animal in animals)
        {
            Console.WriteLine($"\t{i++}. {animal.Name}({animal.WhatAnimal})");
        }

        choice = Convert.ToInt32(Console.ReadLine());
    }

    return animals[choice - 1];
}

