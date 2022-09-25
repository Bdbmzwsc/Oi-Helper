using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using oihelper.template;
namespace oihelper.problem
{
    public class Create
    {
        public static string main(string[] args)
        {
            Console.WriteLine("What is the problem id");
            string name = Console.ReadLine();
            if (File.Exists(name)) throw new Exception("Id is exists");

            Directory.CreateDirectory(name);
            File.Create(name + @"/" + name + ".in");
            File.Create(name + @"/" + name + ".out");

            JObject job = JObject.Parse(File.ReadAllText("config/config.json"));
            JObject t = job;
            string inclu = "";
            JArray jar = JArray.Parse(job["include"].ToString());

            for (int i = 0; i < jar.Count; i++) inclu = inclu + "#include <" + jar[i].ToString() + ">\r\n";
            jar = JArray.Parse(job["namespace"].ToString());
            for (int i = 0; i < jar.Count; i++) inclu = inclu + "using namespace " + jar[i].ToString() + ";\r\n";
            jar = JArray.Parse(job["const"].ToString());
            for (int i = 0; i < jar.Count; i++)
            {
                job = JObject.Parse(jar[i].ToString());
                inclu = string.Format("{0}const {1} {2} = {3} ;\r\n", inclu, job["type"].ToString(), job["name"].ToString(), job["value"].ToString());
            }

            inclu = inclu + t["other"].ToString();
            inclu = inclu + Template.freopenadd(name);
            
            File.WriteAllText(string.Format("{0}/{1}.cpp", name, name), inclu);
            Console.WriteLine("Create successlly");

            return null;
        }
    }
}