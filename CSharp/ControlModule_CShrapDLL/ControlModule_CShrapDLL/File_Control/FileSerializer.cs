using System;
using System.IO;
using System.Security.Permissions;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace UniCtrlMod.FileControl
{
    public class FileSerializer
    {
        [SecurityPermissionAttribute(SecurityAction.PermitOnly, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public static void AllowSerializationPermission(){
            Console.WriteLine("Serialization Permission: PermitOnly!");
            TestPermission(new SecurityPermission(SecurityPermissionFlag.SerializationFormatter));
        }

        public static void TestPermission(SecurityPermission permission){
            try
            {
                //If demand fail, then throw Exception
                permission.Demand();
                Console.WriteLine($"Permission {permission.ToString()} demand status: OK!");
            }
            catch (Exception e) {
                Console.WriteLine($"Permission demand failed! -> {e.Message}|{e.Source}");
            }
        }
        //Using simples class with IFormatter interface
        public static void SerializeItem<T>(T serializableObject, string fileName)
        {
            IFormatter formatter = new BinaryFormatter();

            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Create))
                {
                    formatter.Serialize(fs, serializableObject);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error during serialize and saving object! Ex: {e.Message}|{e.Source}->{e.StackTrace}");
            }
        }

        public static T DeserializeItem<T>(string fileName)
        {
            IFormatter formatter = new BinaryFormatter();

            //Set T t to default value of type
            T t = default(T);

            try
            {
                using (FileStream s = new FileStream(fileName, FileMode.Open))
                {
                    t = (T)formatter.Deserialize(s);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error during deserialize and loading object! Ex: {e.Message}|{e.Source}->{e.StackTrace}");
            }
            return t;
        }
    }
    }
