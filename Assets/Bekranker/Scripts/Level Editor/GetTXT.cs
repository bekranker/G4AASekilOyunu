using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
public static class GetTXT
{
    public static string GetText(string path)
    {
        return File.ReadAllText(path);
    }

    public static List<string> GetTexts(string path)
    {
        return File.ReadAllLines(path).ToList();
    }
}
