using NVorbis;
using NVorbis.Contracts;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOG_Manager
{
    internal class OGG
    {
        protected int SampleRate { get; set; }
        protected long NumberOfSamples { get; set; }

        private byte[] AudioData;
        public OGG(byte[] AudioData)
        {
            this.AudioData = AudioData;
        }
        public bool Verify(int expectedSampleRate)
        {
            try
            {
                using (var memoryStream = new MemoryStream(AudioData))
                {
                    using (var vorbisReader = new VorbisReader(memoryStream))
                    {
                        if (vorbisReader.SampleRate == expectedSampleRate)
                        {
                            SampleRate = vorbisReader.SampleRate;
                            NumberOfSamples = vorbisReader.TotalSamples;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Convert(string outputPath)
        {
            using (var memoryStream = new MemoryStream(AudioData))
            {
                using (var reader = new VorbisReader(memoryStream))
                {
                    using (var fileStream = new FileStream(outputPath, FileMode.Create))
                    {
                        using (var bw = new BinaryWriter(fileStream))
                        {
                            // Write custom header, which are in little endian
                            //The first byte is always the same signature 0x004D5243
                            bw.Write(Encoding.ASCII.GetBytes("CRM\0"));

                            //The second byte is the total number of samples multiplied by 4
                            bw.Write((int)(reader.TotalSamples * 4));

                            //After the header, it comes the original ogg file
                            fileStream.Write(AudioData, 0, AudioData.Length);
                        }
                    }
                }
            }
        }
    }
}
