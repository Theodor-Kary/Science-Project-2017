using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using AForge.Neuro;
using AForge.Neuro.Learning;

namespace SoundApp
{
    class XOR
    {
        public static void RunXOR(string[] args)
        {
            string path = Environment.CurrentDirectory + @"\XORNetwork.txt";
            ActivationNetwork XORNetwork = new ActivationNetwork(new SigmoidFunction(1), 2, 2, 1);
            Console.WriteLine(path);
            int[] trainingSet = new int[8] { 0, 0, 0, 1, 1, 0, 1, 1 };
            int[] trainingAnswers = new int[4] { 0, 1, 1, 0 };

            Start:
            Console.WriteLine("What would you like to do?");
            switch (Console.ReadLine().ToLower())
            {
                case ("train"):
                    BackPropagationLearning teacher = new BackPropagationLearning(XORNetwork);
                    while (true)
                    {
                        int iter = 0;
                        for (iter = 0; iter < 100000000; iter++)
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                //Console.WriteLine(Convert.ToString(trainingSet[i * 2]) + " " + Convert.ToString(trainingSet[i * 2 + 1]));
                                //Console.WriteLine(Convert.ToString(teacher.Run(new double[2] { trainingSet[i * 2], trainingSet[i * 2 + 1] }, new double[1] { trainingAnswers[i] })));
                                teacher.Run(new double[2] { trainingSet[i * 2], trainingSet[i * 2 + 1] }, new double[1] { trainingAnswers[i] });
                            }
                            //Console.WriteLine(iter.ToString());
                        }
                        //if (Console.ReadLine() == "stop")
                        //{
                        Console.WriteLine("Done");
                            XORNetwork.Save(path);
                            goto Start;
                        //}
                    }
                case ("read"):
                    Network network;
                    if (File.Exists(path)) network = Network.Load(path);
                    else network = XORNetwork;

                    for (int i = 0; i < 4; i++)
                    {
                        Console.WriteLine(Convert.ToString(trainingSet[i * 2]) + " " + Convert.ToString(trainingSet[i * 2 + 1]));
                        Console.WriteLine(network.Compute((new double[2] { trainingSet[i * 2], trainingSet[i * 2 + 1] }))[0].ToString());
                        Console.WriteLine(Math.Round(network.Compute((new double[2] { trainingSet[i * 2], trainingSet[i * 2 + 1] }))[0]).ToString());
                    }
                    /*double[] userValues = new double[2];
                    userValues[0] = Convert.ToDouble(Console.ReadLine());
                    userValues[1] = Convert.ToDouble(Console.Read());
                    Console.WriteLine(XORNetwork.Compute(userValues)[0].ToString());*/
                    break;
                case "randomize":
                    XORNetwork.Randomize();
                    XORNetwork.Save(path);
                    Console.WriteLine("done");
                        break;
                case "status":
                    break;
            }
            goto Start;
        }
    }
}
