using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

public class CSVReader
{
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };

    public static List<Dictionary<string, object>> Read(string file)
    {
        var list = new List<Dictionary<string, object>>();
        string[] lines;

        if (File.Exists(Application.streamingAssetsPath + "/" + file + ".csv"))
        {
            string source;
            StreamReader sr = new StreamReader(Application.streamingAssetsPath + "/" + file + ".csv");
            source = sr.ReadToEnd();
            sr.Close();

            lines = Regex.Split(source, LINE_SPLIT_RE);

            Debug.Log(file + ".csv Load");
        }
        else
        {
            return null;
        }

        if (lines.Length <= 1) return list;

        var header = Regex.Split(lines[0], SPLIT_RE);
        for (var i = 1; i < lines.Length; i++)
        {

            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            var entry = new Dictionary<string, object>();
            for (var j = 0; j < header.Length && j < values.Length; j++)
            {
                string value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                value = value.Replace("<br>", "\n");
                value = value.Replace("<c>", ",");
                object finalvalue = value;
                int n;
                float f;
                if (int.TryParse(value, out n))
                {
                    finalvalue = n;
                }
                else if (float.TryParse(value, out f))
                {
                    finalvalue = f;
                }
                entry[header[j]] = finalvalue;
            }
            list.Add(entry);
        }
        return list;
    }
}

///// <summary>
/////     사용법
/////     CSVReader csvReaders = new CSVReader("YourFileName");
/////     string[] Datas = csvReaders.getDatas(num);
///// 
///// 
///// </summary>

//public class CSVReader 
//{
//    private string filePath;
//    private Dictionary<string, String[]> CSVData;
//    private string[] lines;

//    public CSVReader(string fileName)
//    {
//        // CSV 파일의 경로 설정
//        filePath = Application.streamingAssetsPath + $"/{fileName}.csv";
//        lines = File.ReadAllLines(filePath);
//       // PrintLines();
//        MakeDiction();

//    }
//    private void PrintLines()
//    {
//        foreach (var line in lines)
//        {
//            Debug.Log($"Line: {line}");
//        }
//    }
//    private void PrintDictionary(Dictionary<string, string[]> dictionary)
//    {
//        foreach (var kvp in dictionary)
//        {
//            string header = kvp.Key;
//            string[] data = kvp.Value;

//            Debug.Log($"Header: {header}, Data: {string.Join(", ", data)}");
//        }
//    }

//    public void MakeDiction()
//    {
//        // 헤더 처리
//        CSVData = new Dictionary<string, String[]>();
//        string[] headers = lines[0].Split(',');

//        // 각 헤더에 대한 빈 데이터 배열 생성

//        // 데이터 처리
//        for (int i = 1; i < headers.Length - 1; i++)
//        {
//            Debug.Log(headers.Length - 1);
//            List<string> dataset = new List<string>();
//            for (int j = 1;j<lines.Length;j++)
//            {
//                String[] temp = lines[j].Split(',');
//                Debug.Log("돌입");
//                dataset.Add(temp[i]);
//            }

//            Debug.Log(string.Join(",", dataset));
//            CSVData.Add(headers[i], dataset.ToArray());


//        }
//    }
//    private string[] SplitCsvLine(string line)
//    {
//        List<string> values = new List<string>();
//        int startIndex = 0;

//        for (int i = 0; i < line.Length; i++)
//        {
//            if (line[i] == ',')
//            {
//                values.Add(line.Substring(startIndex, i - startIndex).Trim());
//                startIndex = i + 1;
//            }
//            else if (i == line.Length - 1)
//            {
//                values.Add(line.Substring(startIndex).Trim());
//            }
//        }

//        return values.ToArray();
//    }
//    //변조필요함
//    public string[] getDatas(int num)
//    {
//        if (num >= 0 && num < CSVData.Count)
//        {


//            return CSVData["wave"+ num.ToString()];
//        }
//        else
//        {
//            Debug.LogError("Invalid index for Wavenum method.");
//            return null;
//        }
//    }

//}
