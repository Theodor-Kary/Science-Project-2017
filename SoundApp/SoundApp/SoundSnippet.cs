using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using RZP;
using NAudio;
using NAudio.Wave;

namespace SoundApp
{
    public class SoundSnippet
    {
        public double[] Values = new double[4];
        public int LengthSeconds;
        public double BPM;

        //for possible later use
        //public double Tonality = 0;
        public string Name;

        /// <summary>
        /// [0] = Rock, [1] = Classical, [2] = Jazz, [3] = Hip-Hop.
        /// </summary>
        public double[] Genre = new double[4];

        /// <summary>
        /// File name of song
        /// </summary>
        /// <param name="name"></param>
        public SoundSnippet(string name)
        {
            this.Name = name;
        }

        public void FromFile()
        {
            string mp3Path = @"MP3\" + Name + ".mp3";
            string wavePath = @"WAV\" + Name + ".wav";

            if (!File.Exists(Environment.CurrentDirectory + wavePath))
            {
                using (Mp3FileReader reader = new Mp3FileReader(mp3Path))
                {
                    using (WaveStream pcmStream = WaveFormatConversionStream.CreatePcmStream(reader))
                    {
                        WaveFileWriter.CreateWaveFile(wavePath, pcmStream);
                    }
                }
            }

            //Read audio file
            AudioFileReader audioFileReader = new AudioFileReader(mp3Path);
            LengthSeconds = Convert.ToInt32(audioFileReader.TotalTime.TotalSeconds);
            int SampleRate = audioFileReader.WaveFormat.SampleRate;
            float[] soundSamples = new float[SampleRate*10];
            audioFileReader.Read(new float[SampleRate*(LengthSeconds/2)], 0, SampleRate * (LengthSeconds / 2));
            audioFileReader.Read(soundSamples, 0, 10 * SampleRate);

            //Calculate average for samples
            for (int i = 0; i < SampleRate * 2.5; i++)
            {
                Values[0] = +soundSamples[i];
            }
            Values[0] = Values[0] / SampleRate * 2.5;
            for (int i = Convert.ToInt32(Math.Round(SampleRate * 2.5)); i < SampleRate * 5; i++)
            {
                Values[1] = +soundSamples[i];
            }
            Values[1] = Values[0] / SampleRate * 2.5;
            for (int i = (SampleRate * 5); i < SampleRate * 7.5; i++)
            {
                Values[2] = +soundSamples[i];
            }
            Values[2] = Values[0] / SampleRate * 2.5;
            for (int i = Convert.ToInt32(Math.Round(SampleRate * 7.5)); i < SampleRate * 10; i++)
            {
                Values[3] = +soundSamples[i];
            }
            Values[3] = Values[0] / SampleRate * 2.5;

            
            BPMDetector tempoDetector = new BPMDetector(wavePath);
            BPM = tempoDetector.getBPM();
        }
    }
}
