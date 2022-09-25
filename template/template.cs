namespace oihelper.template{
    public class Template{
        public static Func<string,string> freopenadd=(name)=>{
            string f="""

            int main()
            {

            """;
            string l="""
            
                return 0;
            }
            """;
            return string.Format("{0}    freopen(\"{1}\",\"r\",stdin);\r\n    freopen(\"{2}\",\"w\",stdout);\r\n    fclose(stdout);\r\n    fclose(stdin);{3}",f,name+".in",name+".out",l); 
        };
    }
}