﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //XOR.RunXOR(args);
            SoundSnippet soundSnippet = new SoundSnippet();

            soundSnippet.FromFile("Sweet Emotion.mp3");
        }
    }
}
