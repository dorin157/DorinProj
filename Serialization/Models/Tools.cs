using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Tools
    {

        public Tools() { }

        static public List<string> GetParaneters(string s)
        {
            List<string> parameters = new List<string>();
            string param = "";
            string tag = "";
            int openTag = 0;
            foreach (char ch in s)
            {
                switch (ch)
                {
                    case ',':
                        if (tag.Equals(","))
                        {
                            param += tag + ch;
                            tag = "";
                        }
                        else if (tag.Equals("/"))
                        {
                            tag = "";
                            openTag--;
                            if (openTag == 0)
                            {
                                parameters.Add(param);
                                param = "";
                            }
                        }
                        else if (tag.Equals(""))
                        {
                            tag += ch;
                        }
                        break;
                    case '/':
                        if (tag.Equals(","))
                        {
                            tag = "";
                            openTag++;

                        }
                        else if (tag.Equals("/"))
                        {
                            param += tag + ch;
                            tag = "";
                        }
                        else if (tag.Equals(""))
                        {
                            tag += ch;
                        }
                        break;
                    default:
                        param += ch;
                        break;
                }
            }
            return parameters;
        }
    }
}
