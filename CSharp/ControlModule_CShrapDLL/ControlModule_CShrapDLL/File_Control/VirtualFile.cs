
using System;

namespace UniCtrlMod.FileControl
{
    public class VirtualFile
    {
        public string FILENAME{ get;set; }
        public string FILEPATH{ get;set; }
        public string FILETYPE{ get;set; }

        public string FILEDATA { get;set; }

        private const char splitCharName=(char)92;
        private const char splitCharType='.';

        private void ParseFileNameAndPath(string path){
            FILEPATH=path;
            for(int i=(path.Length-1);i>=0;i--){
                if(path[i]==splitCharName){
                    FILENAME=path.Substring(i+1);
                    break;
                }
            }
            for(int i=(FILENAME.Length-1);i>=0;i--){
                if(FILENAME[i]==splitCharType){
                    FILETYPE=FILENAME.Substring(i);
                    break;
                }
            }
            return;
        }

        public VirtualFile(){
            this.Clear();
        }

        public void appendFile(string path){
            this.Clear();
            ParseFileNameAndPath(path);
        }

        public void fillFile(string data){
            this.FILEDATA += data;
        }

        public void setLabels(string labels)
        {
            this.FILEDATA = labels + this.FILEDATA;
        }

        public void clearFileData(){
            FILEDATA = string.Empty;
        }
        public string getFileData()
        {
            return FILEDATA;
        }

        public void Clear(){ 
            FILEDATA="";
            FILENAME="";
            FILEPATH="";
            FILETYPE="";                
        }

        public string getFullFileName(){
            return (FILENAME+FILETYPE);
        }
    }
}
