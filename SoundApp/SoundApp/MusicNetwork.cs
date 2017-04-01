using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using AForge.Neuro.Learning;
using AForge.Neuro;

namespace SoundApp
{
    class MusicNetwork
    {
        List<SoundSnippet> trainingSet = new List<SoundSnippet>();
        Network network;
        string networkPath = Environment.CurrentDirectory + @"\MusicNetwork.txt";

        void Initialize()
        {
            if (File.Exists(networkPath)) network = Network.Load(networkPath);
            else network = new ActivationNetwork(new SigmoidFunction(2), 6, 6, 4, 4);
            trainingSet.Add(new SoundSnippet("Sweet Emotion") { Genre = new double[4] { 1, 0, 0, 0 }  });
            foreach (SoundSnippet snippet in trainingSet)
            {
                snippet.FromFile();
            }
        }

        public void train (int cycleCount)
        {
            ActivationNetwork trainingNetwork = new ActivationNetwork(new SigmoidFunction(2), 6, 6, 4, 4);
            BackPropagationLearning teacher = new BackPropagationLearning(trainingNetwork);

            for (int i = 0; i < cycleCount; i++)
            {
                foreach (SoundSnippet snippet in trainingSet)
                {
                    teacher.Run(new double[] {
                    snippet.BPM, snippet.LengthSeconds, snippet.Values[0], snippet.Values[1], snippet.Values[2], snippet.Values[3]
                    }, snippet.Genre);
                }
            }

            network = trainingNetwork;
            Console.WriteLine("Done!");
        }

        public double[] Run(SoundSnippet song)
        {
            if (network == null) Initialize();
            return network.Compute(new double[]{
                song.BPM, song.LengthSeconds, song.Values[0], song.Values[1], song.Values[2], song.Values[3]
            });
        }
    }
}
