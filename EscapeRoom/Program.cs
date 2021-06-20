using System;

namespace EscapeRoom2
{
    class Program
    {
        static void Main()
        {
            ItemService service = new ItemService();

            Console.WriteLine("Welcome to a generic text-based puzzle adventure game!\n");

            Console.WriteLine("To look at something, type \"lookat\", \"look\", or simply \"l\" and the name of an item.");
            Console.WriteLine("Use simple one-word names! Even if you find a \"fancy blue necklace\", just type \"necklace\".");
            Console.WriteLine("You can also type \"look around\" to, you guessed it, look around the room!");
            Console.WriteLine("Ex.: \"look around\", \"l photo\"\n");

            Console.WriteLine("To use something, type \"use\" or \"u\" and an item, and specify what other item to use it on.");
            Console.WriteLine("This command also allows you to go through doors.");
            Console.WriteLine("Ex.: \"use button\", \"use key on door\"\n");

            //Console.WriteLine("To move between rooms, type \"go\" or \"g\" and a direction.");
            //Console.WriteLine("Available directions are \"west\", \"east\", \"north\", \"south\", \"up\", \"down\".");
            //Console.WriteLine("Ex.: \"go west\"\n");

            //Console.WriteLine("To pick up an item, type \"take\", \"t\", \"pickup\" or \"p\" and an item.");
            //Console.WriteLine("Some items can only be used if you're holding them first!");
            //Console.WriteLine("Ex.:\"take note\", \"p note\"\n");

            //Console.WriteLine("To check what items you're holding, type \"inventory\", \"inv\" or \"i\".\n");

            Console.WriteLine("Don't use uppercase letters or else the commands won't work.\n");

            Console.Write("Press Enter to begin!");
            Console.ReadLine();

            Console.Clear();

            Console.WriteLine($"{service.enterRoom()}");

            // main game loop
            while (true)
            {
                Console.Write("\n>> ");
                Console.WriteLine(service.ReadCommand(Console.ReadLine()));
            }
        }
    }
}
