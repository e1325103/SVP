using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVP_DataConverter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Convert("D:\\WindData");
        }

        public static bool Convert(string path)
        {
            StringBuilder textU = new StringBuilder();
            StringBuilder textV = new StringBuilder();
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
                                float x1 = swapFloat(uReader.ReadBytes(4));
                                float y1 = swapFloat(vReader.ReadBytes(4));

                                if (x1 > 2000)
                                {
                                    x1 = 0;
                                }

                                if (y1 > 2000)
                                {
                                    y1 = 0;
                                }
                                textU.Append(x1 + ";");
                                textV.Append(y1 + ";");
                            }
                            textU.Remove(textU.Length - 1, 1);
                            textU.Append(Environment.NewLine);
                            textV.Remove(textV.Length - 1, 1);
                            textV.Append(Environment.NewLine);
                        }
                    }
                }
            }

            File.WriteAllText("u.csv", textU.ToString());
            File.WriteAllText("v.csv", textV.ToString());

            return true;
        }

        private static float swapFloat(byte[] floatBytes)
        {
            byte temp = floatBytes[0];
            floatBytes[0] = floatBytes[3];
            floatBytes[3] = temp;
            temp = floatBytes[1];
            floatBytes[1] = floatBytes[2];
            floatBytes[2] = temp;
            return BitConverter.ToSingle(floatBytes, 0);
        }
    }
}
