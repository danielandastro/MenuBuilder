using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.IO;

namespace MenuBuilder
{
    public class Builder
    {
        private Dictionary<string, string> _lookupTable = new Dictionary<string, string>();

        public string Lookup(string lookup)
        {
            string lookupReturnData = "";
            lookupReturnData = _lookupTable[lookup];
            return lookupReturnData;
        }

        public void Define(string key, string data)
        {
            _lookupTable[key] = data;
        }

        public void Reader(string path)
        {
            int counter = 0, errorcount = 0;
            var lineErrorStore = new int[10];
            using (var file = new StreamReader(path))
            {
                var line = file.ReadLine();
                while (line != null)
                {
                    counter++;
                    line = file.ReadLine();
                    try
                    {
                        Define(line.Split(',')[0], line.Split(',')[1]);
                    }
                    catch (Exception)
                    {
                        if (errorcount == 10)
                        {
                            Console.WriteLine("Import error count exceded acceptable limit");
                            return;
                        }

                        errorcount++;
                        lineErrorStore[errorcount] = counter;
                    }
                }
            }
        }
    }
}