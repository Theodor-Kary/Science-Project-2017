using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using SoundTouch;
using SoundTouch.Utility;
using NAudio;
using NAudio.Wave;

namespace SoundApp
{
    public class SoundSnippet
    {
        public double[] Values = new double[4];
        double LengthSeconds;
        int SampleRate;
        double BPM;
        
        public void FromFile(string path)
        {
            AudioFileReader audioFileReader = new AudioFileReader(path);
            SampleRate = audioFileReader.WaveFormat.SampleRate;
            float[] soundSamples = new float[SampleRate*10];
            //audioFileReader.Read(soundSamples, Convert.ToInt32((audioFileReader.TotalTime.TotalSeconds/2)*SampleRate), SampleRate*10);
            audioFileReader.Read(new float[SampleRate*60], 0, 60 * SampleRate);
            audioFileReader.Read(soundSamples, 0, 10 * SampleRate);

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
        }
    }
}
