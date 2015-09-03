using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extensions
{
    public static class Extensions
    {
        static public List<string> GetProperties(this string s)
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
                            openTag--;
                            if (openTag == 0)
                            {
                                parameters.Add(param);
                                param = "";
                            }
                            else
                            {
                                param += tag + ch;
                            }
                            tag = "";
                        }
                        else if (tag.Equals(""))
                        {
                            tag += ch;
                        }
                        break;
                    case '/':
                        if (tag.Equals(","))
                        {
                            if (openTag != 0)
                            {
                                param += tag + ch;
                            }
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
