using java.io;
using org.apache.tika;
using org.apache.tika.io;
using org.apache.tika.metadata;
using System.IO;

namespace Tikadot6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var tika = new Tika();

            using (Stream inputStream = new FileStream(@"../../../../testdoc.txt", FileMode.Open, FileAccess.Read))
            {
                using (InputStream stream = SystemStreamFactory(inputStream))
                {
                    Metadata metadata = new Metadata();
                    Reader reader = tika.parse(stream, metadata);


                    foreach (string name in metadata.names())
                    {
                        System.Console.WriteLine($"{name}: {string.Join(", ", metadata.getValues(name))}");
                    }

                    char[] buffer = new char[256];
                    reader.read(buffer);
                    System.Console.WriteLine(new string(buffer));
                }
            }
        }

        private static InputStream SystemStreamFactory(Stream input)
        {
            var ioStream = new ikvm.io.InputStreamWrapper(input);
            var result = TikaInputStream.get(ioStream);
            return ioStream;
        }
    }
}
