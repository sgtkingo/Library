using System;
using System.IO;

namespace UniCtrlMod.FileControl
{
    public class FileManager
    {
        public static bool LoadFile(VirtualFile virtualFile){ 
            if(virtualFile == null)return false;

            virtualFile.clearFileData();
            string data = string.Empty;

            if (!File.Exists(virtualFile.FILEPATH))
            {
                Console.WriteLine($"File {virtualFile.FILEPATH} DOESNT exist!");
                return false;
            }
            try{ 
                using (FileStream fs = new FileStream(virtualFile.FILEPATH,FileMode.Open))
                {
                fs.Seek(0,SeekOrigin.Begin);
                    StreamReader sr=new StreamReader(fs);

                    while(!sr.EndOfStream){ 
                        data += sr.ReadLine();
                        data += "\n";
                    }
                    sr.Close();
                }               
            }catch (Exception e){ 
                Console.WriteLine($"Problem during loading file: {e.Message} | {e.Source} ");
                return false;
            }
            virtualFile.fillFile(data);
            Console.WriteLine("File "+virtualFile.FILENAME+" loaded...");
            return true;
        }

        public static bool SaveFile(VirtualFile virtualFile){
            if(virtualFile == null)return false;

            string filePath=(virtualFile.FILEPATH);
            try{ 
                using (FileStream fs = new FileStream(filePath,FileMode.Create))
                {
                fs.Seek(0,SeekOrigin.Begin);
                    StreamWriter sw=new StreamWriter(fs);
                    sw.Write(virtualFile.getFileData());
                    sw.Close();
                }               
            }catch (Exception e){
                Console.WriteLine($"Problem during saving file: {e.Message} | {e.Source} ");
                return false;
            }
            Console.WriteLine("File "+virtualFile.FILENAME+" saved!");
            return true;
        }

        public static bool SaveFile(string path, string data){
            try
            {
                File.WriteAllText(path, data);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Problem during saving file: {e.Message} | {e.Source} ");
                return false;
            }
            return true;
        }

        public static string LoadFile(string path){
            string data = string.Empty;
            try
            {
                data = File.ReadAllText(path);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Problem during loading file: {e.Message} | {e.Source} ");
            }
            return data;
        }
    }
}
