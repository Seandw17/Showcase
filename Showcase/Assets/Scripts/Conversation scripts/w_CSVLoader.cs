using System;
using System.Collections.Generic;
using UnityEngine;

// Author: Alec

// TODO rename class once completed
static public class w_CSVLoader
{
    /// <summary>
    /// Load A CSV containing the question data, and return
    /// as a List
    /// </summary>
    /// <param name="_fileName"> the file name of the CSV </param>
    /// <returns> A List containing sets of questions </returns>
    static public List<List<KeyValuePair<e_identifier, s_questionData>>>
        ReadConversationCSV(string _fileName)
    {
        Debug.Log("Starting Read of CSV: " + _fileName);

        List<List<KeyValuePair<e_identifier, s_questionData>>> returnList =
            new List<List<KeyValuePair<e_identifier, s_questionData>>>();

        TextAsset file = LoadInFile(_fileName);

        List<KeyValuePair<e_identifier, s_questionData>> currentQuestionSet =
            new List<KeyValuePair<e_identifier, s_questionData>>();

        // parse over each line, recording data as we go
        string[] linesInFile = file.text.Split('\n');
        foreach (string line in linesInFile)
        {
            // if it is not a comment
            if (!line.ToLower()[0].Equals('#'))
            {
                // when we hit a end line, record it into the list and start new
                if (line.Equals("end"))
                {
                    returnList.Add(currentQuestionSet);
                    currentQuestionSet = new
                        List<KeyValuePair<e_identifier, s_questionData>>();
                    Debug.Log("Question set added");
                }

                // if else, record a question set
                else
                {
                    currentQuestionSet.Add(LoadQuestion(line));
                }
            }
        }

        Debug.Log("File Loading Complete");

        return returnList;
    }

    /// <summary>
    /// Load an individual question 
    /// </summary>
    /// <param name="QuestionLine"> the raw line of the question data </param>
    /// <returns> A question data key value pair </returns>
    static KeyValuePair<e_identifier, s_questionData>
        LoadQuestion(string QuestionLine)
    {
        // creating a new question
        s_questionData question = new s_questionData();
        question.options = new List<s_Questionresponse>();

        // loading in the actual question
        string[] data = QuestionLine.Split(',');
        question.question = data[1];

        //loading in the options
        for (int iterator = 2; iterator < data.Length; iterator++)
        {
            string[] optionAndFeel = data[iterator].Split('|');

            // parse data for each response
            s_Questionresponse temp = new s_Questionresponse();
            temp.response = optionAndFeel[0];
            temp.rating = (e_rating) Enum.Parse(
                typeof(e_rating),optionAndFeel[1]);
            temp.unlockCriteria = (e_unlockFlag)Enum.Parse(
                typeof(e_unlockFlag), optionAndFeel[2]);
            question.options.Add(temp);
        }
        
        // return appropriate values
        return new KeyValuePair<e_identifier, s_questionData>
            ((e_identifier) Enum.Parse(typeof(e_identifier),
            data[0]),question);
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
                        keyValue[0]), keyValue[1]));
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