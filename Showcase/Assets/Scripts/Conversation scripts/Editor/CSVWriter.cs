using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;

/// <summary>
/// Class to manage all writing functions
/// </summary>
public static class CSVWriter
{
    static char[] illegalCharacters = { ',', '#', '$', '|' };

    /// <summary>
    /// Write back to intro or outro text file
    /// </summary>
    /// <param name="_fileName">name of file to write</param>
    /// <param name="_text">list of strings to write</param>
    public static void WriteIntroOutroText(string _fileName, List<string> _text)
    {
        string path = "Assets/Resources/Conversation/" + _fileName;

        StreamWriter writer = new StreamWriter(path, false);

        Debug.Log("Writing file:" + _fileName);

        foreach (string line in _text)
        {
            if (!line.Equals(""))
            {
                writer.WriteLine(line.Trim(new char[] { ',', '#', '$', '|' }));
                Debug.Log("Wrote Line: " + line);
            }
        }

        writer.Close();

        Debug.Log("Completed writing file:" + _fileName);
    }

    /// <summary>
    /// Function to write filler text to file
    /// </summary>
    /// <param name="_text">List of strings to write to file</param>
    public static void WriteFillerText(List<string> _text)
    {
        string path = "Assets/Resources/Conversation/FillerText.txt";

        StreamWriter writer = new StreamWriter(path, false);

        Debug.Log("Writing file: " + path);

        foreach(string line in _text)
        {
            if (!line.Equals(""))
            {
                writer.WriteLine(line.Trim(illegalCharacters));
                Debug.Log("Wrote Line " + line);
            }
        }

        writer.Close();

        Debug.Log("Completed writing file: " + path);
    }

    /// <summary>
    /// Write back to tips file
    /// </summary>
    /// <param name="_values">Dictionary of values</param>
    public static void WriteTips(Dictionary<e_tipCategories, string> _values)
    {
        string path = "Assets/Resources/Conversation/Tips.txt";

        StreamWriter writer = new StreamWriter(path, false);

        Debug.Log("Writing file: " + path);

        writer.WriteLine("# Format: [tipenum],[tiptext]");

        foreach(KeyValuePair<e_tipCategories, string> keyvalue in _values)
        {
            string lineToWrite = keyvalue.Key.ToString() + "," +
                keyvalue.Value.Trim(illegalCharacters);
            writer.WriteLine(lineToWrite);

            Debug.Log("Wrote Line: " + lineToWrite);
        }

        writer.Close();

        Debug.Log("Completed writing file: " + path);
    }

    /// <summary>
    /// Function to write back player questions
    /// </summary>
    /// <param name="_strings"></param>
    public static void WritePlayerQuestions(List<PlayerQuestion> _questions)
    {
        string path = "Assets/Resources/Conversation/PQuestions.txt";

        StreamWriter writer = new StreamWriter(path, false);

        writer.WriteLine("#");
        writer.WriteLine("# The format of this document is as follows ");
        writer.WriteLine("# [question]|[unlockFlag]|[reponse]|ID, ... , " +
            "[question]|[UnlockFlag]|[response]|ID");
        writer.WriteLine("#");

        Debug.Log("Writing File: " + path);

        string line = "";

        for (int index = 0; index < _questions.Count; index++)
        {
            line += _questions[index].question + "|" + _questions[index].flag
                + "|" + _questions[index].response + "|" + _questions[index].ID;

            if (index != _questions.Count - 1)
            {
                line += ",";
            }
        }

        writer.Write(line);

        writer.Close();

        Debug.Log("Completed Writing file: " + path);
    }

    /// <summary>
    /// Write back to the IQuestions.csv file
    /// </summary>
    /// <param name="_questions">list of questions</param>
    public static void WriteInterviewerQuestions(List<QuestionData> _questions)
    {

        string path = "Assets/Resources/Conversation/IQuestions.txt";

        StreamWriter writer = new StreamWriter(path, false);

        Debug.Log("Writing File: " + path);

        writer.WriteLine("# This file specifies the questions that will be " +
            "loaded into the game");
        writer.WriteLine("#");
        writer.WriteLine("# format is as follows");
        writer.WriteLine("# i$[ID]");
        writer.WriteLine("# q$[e_identifier]|[question text], ...");
        writer.WriteLine("# r$[response string]|[e_rating]|[e_unlockFlag]...");
        writer.WriteLine("# f$[e_tipcategories]");
        writer.WriteLine("#");

        foreach(QuestionData question in _questions)
        {
            writer.WriteLine("i$" + question.ID);
            Debug.Log("ID line :" + "i$" + question.ID + " written");

            // parsing question line
            string questionLine = "q$";
            int externalIndex = 0;
            foreach (e_rating rating in Enum.GetValues(typeof(e_rating)))
            {
                questionLine += rating + "|" +
                    question.questions[rating].Trim(illegalCharacters);
                if (externalIndex != question.questions.Count - 1)
                {
                    questionLine += ",";
                }
                externalIndex++;
            }
            writer.WriteLine(questionLine);
            Debug.Log("Question Line: " + questionLine +  " written");

            // Parsing response line
            string responseLine = "r$";

            for (int index = 0; index < question.options.Count; index++)
            {
                responseLine += question.options[index].response
                    .Trim(illegalCharacters) + "|"
                    + question.options[index].rating + "|" +
                    question.options[index].unlockCriteria;

                if (index != question.options.Count - 1)
                {
                    responseLine += ",";
                }
            }
            writer.WriteLine(responseLine);
            Debug.Log("Response Line: " + responseLine + " Written to file");

            // Parsing tip
            writer.WriteLine("f$" + question.tip.ToString());
            Debug.Log("Tip line: " + question.tip + " written");
        }

        writer.Close();

        Debug.Log("Completed Writing file: " + path);
    }
}
