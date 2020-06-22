using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Author: Alec

// TODO rename class once completed
static public class w_CSVLoader
{
    /// <summary>
    /// Function to load in interview questions
    /// </summary>
    /// <param name="_fileName">name of file</param>
    /// <returns>list of questions</returns>
    public static List<QuestionData> LoadQuestionData(string _fileName)
    {
        Debug.Log("Starting Read of CSV: " + _fileName);

        TextAsset file = LoadInFile("Conversation/" + _fileName);

        List<QuestionData> returnValue = new List<QuestionData>();
        QuestionData temp = new QuestionData();

        string[] lines = file.text.Split('\n');
        foreach (string line in lines)
        {
            if (!line[0].Equals('#'))
            {
                if (line.Equals("end"))
                {
                    returnValue.Add(temp);
                    temp = new QuestionData();
                }
                else
                {
                    string[] check = line.Split('$');
                    switch (check[0])
                    {
                        case "q":
                            temp.questions = ReadQuestions(check[1]);
                            break;
                        case "r":
                            temp.options = ReadOptions(check[1]);
                            break;
                        case "f":
                            temp.tip = (e_tipCategories)Enum.Parse(
                            typeof(e_tipCategories), check[1]);
                            break;
                        case "":
                            throw new Exception("Illegal index: " + check[0]);
                    }
                }
            }
        }

        Debug.Log("completed read of file: " + _fileName);

        return returnValue;
    }

    /// <summary>
    /// Parses a line of string into a set of question variations
    /// </summary>
    /// <param name="_questions"> the line</param>
    /// <returns>a list of variations</returns>
    static Dictionary<e_rating, string> ReadQuestions(string _questions)
    {
        Dictionary<e_rating, string> returnList =
            new Dictionary<e_rating, string>();

        string[] data = _questions.Split(',');

        foreach (string dataSet in data)
        {
            string[] brokenUp = dataSet.Split('|');
            returnList.Add((e_rating)Enum.Parse(typeof(e_rating),
                brokenUp[0]), brokenUp[1]);
        }

        return returnList;
    }

    /// <summary>
    /// A function to read in all the options for a question
    /// </summary>
    /// <param name="_responses">the string to parse</param>
    /// <returns>a list of responses</returns>
    static List<Questionresponse> ReadOptions(string _responses)
    {
        List<Questionresponse> returnList = new List<Questionresponse>();

        string[] data = _responses.Split(',');
        foreach (string dataSet in data)
        {
            string[] brokenUp = dataSet.Split('|');
            Questionresponse temp = new Questionresponse();
            temp.response = brokenUp[0];
            temp.rating = (e_rating)Enum.Parse(typeof(e_rating), brokenUp[1]);
            temp.unlockCriteria =
                (e_unlockFlag)Enum.Parse(typeof(e_unlockFlag), brokenUp[2]);
            returnList.Add(temp);
        }

        return returnList;
    }

    /// <summary>
    /// Loads player questions
    /// </summary>
    /// <param name="_fileName">name of file</param>
    /// <param name="_replies">list of replies to the questions</param>
    /// <param name="_questions">List of questions the player can ask</param>
    public static void LoadInPlayerQuestions(string _fileName,
        out List<s_playerQuestion> _list)
    {
        Debug.Log("Loading in player questions");

        TextAsset file = LoadInFile("Conversation/" + _fileName);

        _list = new List<s_playerQuestion>();

        string[] lines = file.text.Split('\n');

        // split line into files

        foreach (string data in lines)
        {
            // if not a comment
            if (!data[0].Equals('#'))
            {
                // split by comma
                string[] responses = data.Split(',');
                foreach (string response in responses)
                {
                    // split by | character
                    string[] keyValue = response.Split('|');
                    // load in
                    s_playerQuestion temp = new s_playerQuestion();
                    temp.question = keyValue[0];
                    temp.response = keyValue[2];
                    temp.flag = (e_unlockFlag)Enum.Parse(typeof(e_unlockFlag),
                        keyValue[1]);
                    _list.Add(temp);
                    Debug.Log("Player Question added");
                }
            }
        }

        Debug.Log("Player question files loaded in");
    }

    /// <summary>
    /// Load in file as a text asset
    /// </summary>
    /// <param name="_name">name of file in "/Resources"</param>
    /// <returns>TextAsset of file</returns>
    static TextAsset LoadInFile(string _name)
    {
        TextAsset file = Resources.Load<TextAsset>(_name);

        Debug.Assert(file, "File: " + _name + " cannot be found");
        Debug.Log("File: " + _name + " Has Been found");

        return file;
    }

    /// <summary>
    /// Function to load in the filler text
    /// </summary>
    /// <returns>List of filler text string</returns>
    public static List<string> LoadInFillerText()
    {
        List<string> returnList = new List<string>();

        TextAsset file = LoadInFile("Conversation/FillerText");
        string[] lines = file.text.Split('\n');

        foreach (string line in lines)
        {
            returnList.Add(line);
        }

        return returnList;
    }

    /// <summary>
    /// Function to load in tips from a text file
    /// </summary>
    /// <param name="_list">Dictionary you want to load the tips into</param>
    public static void LoadTips(out Dictionary<e_tipCategories, string> _list)
    {
        _list = new Dictionary<e_tipCategories, string>();

        TextAsset file = LoadInFile("Conversation/Tips");
        string[] lines = file.text.Split('\n');

        foreach (string line in lines)
        {
            if (!line[0].Equals('#'))
            {
                string[] data = line.Split(',');
                _list.Add((e_tipCategories)Enum.Parse(typeof(e_tipCategories),
                    data[0]), data[1]);
            }
        }
    }

    /// <summary>
    /// function to get intro text document
    /// </summary>
    /// <returns>string array</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string[] LoadIntroText() => LoadInFile("Conversation/intro")
        .text.Split('\n');

    /// <summary>
    /// function to get outro text document
    /// </summary>
    /// <returns>string array of outro text</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string[] LoadOutroText() => LoadInFile("Conversation/outro")
        .text.Split('\n');
}