using System;

namespace EscapeRoom
{
    class Program
    {
        static void Main()
        {
            ItemService service = new ItemService();


            // ------------------------------------------------------------------------
            // WELCOME MESSAGE
            // ------------------------------------------------------------------------

            Console.WriteLine("Welcome to a generic text-based puzzle adventure game!\n");

            Console.WriteLine("To look at something, type \"lookat\", \"look\", or simply \"l\" and the name of an item.");
            Console.WriteLine("You can also type \"look around\" to, you guessed it, look around the room!");
            Console.WriteLine("Ex.: \"look around\", \"l photo\"\n");

            Console.WriteLine("To use something, type \"use\" or \"u\" and an item, and specify what other item to use it on.");
            Console.WriteLine("This command also allows you to go through doors.");
            Console.WriteLine("Ex.: \"use button\", \"use key on door\"\n");

            //Console.WriteLine("To move between rooms, type \"go\" or \"g\" and a direction.");
            //Console.WriteLine("Available directions are \"west\", \"east\", \"north\", \"south\", \"up\", \"down\".");
            //Console.WriteLine("Ex.: \"go west\"\n");

            Console.WriteLine("To put an item into your inventory, type \"take\", \"t\", \"pickup\" or \"p\" and an item.");
            Console.WriteLine("Not all items can be taken, but those that can will always have an important use!");
            Console.WriteLine("Ex.:\"take note\", \"p note\"\n");

            Console.WriteLine("To see the contents of your inventory, type \"inventory\", \"inv\" or \"i\".\n");

            Console.WriteLine("Use simple one-word names! Even if you find a \"fancy blue necklace\", just type \"necklace\".");
            Console.WriteLine("Don't use uppercase letters or else the commands won't work.\n");

            Console.Write("Press Enter to begin!");
            Console.ReadLine();

            Console.Clear();

            Console.WriteLine($"{service.enterRoom()}");

            // ------------------------------------------------------------------------
            // MAIN GAME LOOP
            // ------------------------------------------------------------------------
            while (true)
            {
                Console.Write("\n>> ");
                Console.WriteLine(service.ReadCommand(Console.ReadLine()));
            }
        }
    }
}
