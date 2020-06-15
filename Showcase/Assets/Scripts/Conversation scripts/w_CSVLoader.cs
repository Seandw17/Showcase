using System;
using System.Collections.Generic;
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
    public static List<s_questionData> LoadQuestionData(string _fileName)
    {
        Debug.Log("Starting Read of CSV: " + _fileName);

        TextAsset file = LoadInFile(_fileName);

        List<s_questionData> returnValue = new List<s_questionData>();
        s_questionData temp = new s_questionData();

        string[] lines = file.text.Split('\n');
        foreach(string line in lines)
        {
            if (!line[0].Equals('#'))
            {
                if (line.Equals("end"))
                {
                    returnValue.Add(temp);
                    temp = new s_questionData();
                }
                else
                {
                    string[] check = line.Split('$');
                    if (check[0].Equals("q"))
                    {
                        temp.questions = ReadQuestions(check[1]);
                    }
                    else if(check[0].Equals("r"))
                    {
                        temp.options = ReadOptions(check[1]);
                    }
                    else
                    {
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
    static List<s_questionVariations> ReadQuestions(string _questions)
    {
        List<s_questionVariations> returnList =
            new List<s_questionVariations>();

        Debug.Log(_questions);

        string[] data = _questions.Split(',');

        foreach (string dataSet in data)
        {
            string[] brokenUp = dataSet.Split('|');
            s_questionVariations question = new s_questionVariations();
            Debug.Log(brokenUp[0]);
            question.question = brokenUp[1];
            question.identifier =
                (e_identifier) Enum.Parse(typeof(e_identifier), brokenUp[0]);
            returnList.Add(question);
        }

        return returnList;
    }

    /// <summary>
    /// A function to read in all the options for a question
    /// </summary>
    /// <param name="_responses">the string to parse</param>
    /// <returns>a list of responses</returns>
    static List<s_Questionresponse> ReadOptions(string _responses)
    {
        List<s_Questionresponse> returnList = new List<s_Questionresponse>();

        Debug.Log(_responses);

        string[] data = _responses.Split(',');
        foreach (string dataSet in data)
        {
            string[] brokenUp = dataSet.Split('|');
            s_Questionresponse temp = new s_Questionresponse();
            temp.response = brokenUp[0];
            Debug.Log(brokenUp[0]);
            temp.rating = (e_rating) Enum.Parse(typeof(e_rating), brokenUp[1]);
            temp.unlockCriteria =
                (e_unlockFlag)Enum.Parse(typeof(e_unlockFlag), brokenUp[2]);
            returnList.Add(temp);
        }

        return returnList;
    }

    /// <summary>
    /// Load in the players questions for the interviewer
    /// </summary>
    /// <param name="_fileName">the name of the file</param>
    /// <returns>a list of questions</returns>
    static public List<KeyValuePair<e_unlockFlag, string>>
        LoadInPlayerQuestions(string _fileName)
    {
        Debug.Log("Loading in player questions");

        // instaniate appropriate lists
        List<KeyValuePair<e_unlockFlag, string>> returnValue =
            new List<KeyValuePair<e_unlockFlag, string>>();

        TextAsset file = LoadInFile(_fileName);

        // split line into files
        string[] lines = file.text.Split('\n');
        foreach(string data in lines)
        {
            // if not a comment
            if (!data[0].Equals('#'))
            {
                // split by comma
                string[] responses = data.Split(',');
                foreach(string response in responses)
                {
                    // split by | character
                    string[] keyValue = response.Split('|');
                    // load in
                    returnValue.Add(
                        new KeyValuePair<e_unlockFlag, string>
                        ((e_unlockFlag) Enum.Parse(typeof(e_unlockFlag),
                        keyValue[1]), keyValue[0]));
                    Debug.Log("Question added");
                }
            }
        }

        Debug.Log("Player question files loaded in");
        return returnValue;
    }

    /// <summary>
    /// Load in file as a text asset
    /// </summary>
    /// <param name="_name">name of file in "/Resources"</param>
    /// <returns>TextAsset of file</returns>
    static TextAsset LoadInFile(string _name)
    {
        _name = "Conversation/" + _name;
        TextAsset file = Resources.Load<TextAsset>(_name);

        Debug.Assert(file, "File: " + _name + " cannot be found");
        Debug.Log("File: " + _name + " Has Been found");

        return file;
    }
}