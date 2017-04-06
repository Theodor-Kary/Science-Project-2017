using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundApp
{
    class Program
    {
        static void Main(string[] args)
        {
            MusicNetwork network = null;
            Console.WriteLine("What network do you wanna run?");
            switch (Console.ReadLine().ToLower())
            {
                case "xor":
                    XOR.RunXOR(args);
                    break;
                case "sound":
                    RunSound(network);
                    break;
            }
            SoundSnippet soundSnippet = new SoundSnippet("Blue Moon");
            soundSnippet.FromFile();
            Console.ReadKey();
        }

        static void RunSound(MusicNetwork network)
        {
            if (network == null) network = new MusicNetwork();

            Start:
            Console.WriteLine("What would you like to do?");
            switch (Console.ReadLine().ToLower())
            {
                case "train":
                    Console.WriteLine("How many times do you wanna train?");
                    network.Train(Convert.ToInt32(Console.ReadLine()));

                    break;
                case "run":
                    Console.WriteLine("What song would you like to run?");
                    var snippet = new SoundSnippet(Console.ReadLine());
                    double[] result = network.Run(snippet);
                    Console.WriteLine(result[0].ToString("G", CultureInfo.InvariantCulture) + " Rock \n" + result[1].ToString("G", CultureInfo.InvariantCulture) + " Classical \n" + result[2].ToString("G", CultureInfo.InvariantCulture) + " Jazz \n" + result[3].ToString("G", CultureInfo.InvariantCulture) + " Hip-Hop \n");

                    break;
                case "exit":
                    Main(null);
                    break;

            }
            goto Start;
        }
    }
}
