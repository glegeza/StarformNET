namespace DLS.StarformNET.Console
{
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.IO;

    class Program
    {
        private string SYSTEM_NAME = "test";
        private string SYSTEM_FILE = "testsystem.bin";

        static void Main(string[] args)
        {
            Utilities.InitRandomSeed(0);
            var system = Generator.GenerateStellarSystem("test");
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("testsystem.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, system);
            stream.Close();
        }
    }
}
