using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConvertByteToString
{
    class Program
    {
        static void Main(string[] args)
        {
            OpenFile openFile = new OpenFile();
            Console.WriteLine(openFile.ReadBytes());
        }
    }

    class OpenFile
    {
        private string fileName = "in.txt";
        private string path = @"C:\Users\user\Desktop\";

        public string ReadBytes()
        {
            string result = null;
            if (CheckFile() == false)
            {
                Console.WriteLine("Can't Open File, or symbols in file is not lacks");
                return result;
            }
            else
            {
                using (FileStream fileStream = new FileStream(path + fileName, FileMode.Open, FileAccess.Read))
                {
                    StreamReader streamReader = new StreamReader(fileStream);
                    char[] temp = new char[fileStream.Length];
                    int t = streamReader.Read(temp, 0, (int)fileStream.Length);
                    if (t == fileStream.Length)
                        result = (charToString(temp));
                }
            }
            return result;
        }

        private bool CheckFile()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            FileInfo fileInfo = new FileInfo(path + fileName);
            try
            {
                FileStream fileStream = new FileStream(path + fileName, FileMode.Open, FileAccess.Read);
                bool directoryExsist = directoryInfo.Exists;
                bool fileExsistInDirectory = fileInfo.Exists;
                long readStringLenght = fileStream.Length;
                if (directoryExsist == true && fileExsistInDirectory == true && fileStream.Length % 4 == 0) return true;
                else return false;
            }
            catch (FileNotFoundException ex)
            {
                return false;
            }
        }
        private string charToString(char[] charArray)
        {
            int charLenght = charArray.Length;
            int j = 0;
            string res = null;
            string tmp = null;

            for (int i = 0; i < charArray.Length; i++)
            {
                tmp += charArray[i];
                j++;
                if (j == 4)
                {
                    res += Convert.ToString(Convert.ToChar(Convert.ToByte(tmp)));
                    tmp = null;
                    j = 0;
                }
            }
            return res;
        }
    }
}
