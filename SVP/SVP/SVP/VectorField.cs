using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SVP
{
    public class VectorField
    {
        private float[, ,] u;
        private float[, ,] v;

        public bool import(string path)
        {
            u = new float[500, 500, 48];
            v = new float[500, 500, 48];

            for (int i = 1; i <= 48; i++)
            {
                string filename = i.ToString();
                if (i < 10)
                {
                    filename = "0" + filename;
                }
                using (BinaryReader uReader = new BinaryReader(new FileStream(path + "\\Uf" + filename + ".bin", FileMode.Open)))
                {
                    using (BinaryReader vReader = new BinaryReader(new FileStream(path + "\\Vf" + filename + ".bin", FileMode.Open)))
                    {
                        for (int x = 0; x < 500; x++)
                        {
                            for (int y = 0; y < 500; y++)
                            {
                                u[x, y, (i - 1)] = swapFloat(uReader.ReadBytes(4));
                                v[x, y, (i - 1)] = swapFloat(vReader.ReadBytes(4));
                            }
                        }
                    }
                }
            }
            return true;
        }

        private float swapFloat(byte[] floatBytes)
        {
            byte temp = floatBytes[0];
            floatBytes[0] = floatBytes[3];
            floatBytes[3] = temp;
            temp = floatBytes[1];
            floatBytes[1] = floatBytes[2];
            floatBytes[2] = temp;
            return BitConverter.ToSingle(floatBytes, 0);
        }

        public BitmapSource createImage(float minValue, float maxValue)
        {
            byte[] pixelData = new byte[500 * 500 * 4];
            int pixelCount = 0;

            for (int x = 0; x < 500; x++)
            {
                for (int y = 0; y < 500; y++)
                {
                    float value = u[x, y, 1];
                    byte intensity = (byte)(value >= minValue && value <= maxValue ? ((float)value / (float)(maxValue - minValue) * 255.0f) : 0);
                    pixelData[pixelCount++] = intensity;
                    pixelData[pixelCount++] = intensity;
                    pixelData[pixelCount++] = intensity;
                    pixelData[pixelCount++] = (byte)255;
                    value = v[x, y, 1];
                    intensity = (byte)(value >= minValue && value <= maxValue ? ((float)value / (float)(maxValue - minValue) * 255.0f) : 0);
                    pixelData[pixelCount - 2] = intensity;
                }
            }

            int stride = 500 * PixelFormats.Bgr32.BitsPerPixel / 8;
            return BitmapSource.Create(500, 500, 96, 96, PixelFormats.Bgr32, null, pixelData, stride);
        }

    }
}
