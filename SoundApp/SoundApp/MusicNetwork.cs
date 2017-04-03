﻿using System;
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

            //Rock Songs
            trainingSet.Add(new SoundSnippet("Sweet Emotion") { Genre = new double[4] { 1, 0, 0, 0 } });
            trainingSet.Add(new SoundSnippet("Runnin' With The Devil") { Genre = new double[4] { 1, 0, 0, 0 } });
            trainingSet.Add(new SoundSnippet("Highway To Hell") { Genre = new double[4] { 1, 0, 0, 0 } });
            trainingSet.Add(new SoundSnippet("Smells Like Teen Spirit") { Genre = new double[4] { 1, 0, 0, 0 } });
            trainingSet.Add(new SoundSnippet("Paradise City") { Genre = new double[4] { 1, 0, 0, 0 } });

            //Classical Songs
            trainingSet.Add(new SoundSnippet("Swan Lake Waltz") { Genre = new double[4] { 0, 1, 0, 0 } });
            trainingSet.Add(new SoundSnippet("Op. 67, Fate") { Genre = new double[4] { 0, 1, 0, 0 } });
            trainingSet.Add(new SoundSnippet("Op. 8, Spring") { Genre = new double[4] { 0, 1, 0, 0 } });
            trainingSet.Add(new SoundSnippet("Piano Concerto No. 21") { Genre = new double[4] { 0, 1, 0, 0 } });
            trainingSet.Add(new SoundSnippet("Ride Of The Valkyries") { Genre = new double[4] { 0, 1, 0, 0 } });

            //Jazz Songs
            trainingSet.Add(new SoundSnippet("Blue In Green") { Genre = new double[4] { 0, 0, 1, 0 } });
            trainingSet.Add(new SoundSnippet("My Funny Valentine") { Genre = new double[4] { 0, 0, 1, 0 } });
            trainingSet.Add(new SoundSnippet("Fly Me To The Moon") { Genre = new double[4] { 0, 0, 1, 0 } });
            trainingSet.Add(new SoundSnippet("Blue Moon") { Genre = new double[4] { 0, 0, 1, 0 } });
            trainingSet.Add(new SoundSnippet("God Bless The Child") { Genre = new double[4] { 0, 0, 1, 0 } });

            //Hip-Hop Songs
            trainingSet.Add(new SoundSnippet("Stan") { Genre = new double[4] { 0, 0, 0, 1 } });
            trainingSet.Add(new SoundSnippet("In Da Club") { Genre = new double[4] { 0, 0, 0, 1 } });
            trainingSet.Add(new SoundSnippet("Juicy") { Genre = new double[4] { 0, 0, 0, 1 } });
            trainingSet.Add(new SoundSnippet("Six In The Morning") { Genre = new double[4] { 0, 0, 0, 1 } });
            trainingSet.Add(new SoundSnippet("The Message") { Genre = new double[4] { 0, 0, 0, 1 } });

            foreach (SoundSnippet snippet in trainingSet)
            {
                snippet.FromFile();
            }
        }

        public void Train (int cycleCount)
        {
            if (trainingSet.Count == 0) Initialize();
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
            network.Save("MusicNetwork");
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
