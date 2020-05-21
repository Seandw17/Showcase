using System;
using System.Collections.Generic;
using UnityEngine;

// Author: Alec

// TODO rename class once completed
public class w_CSVLoader
{
    /// <summary>
    /// Load A CSV containing the question data, and return
    /// as a List
    /// </summary>
    /// <param name="fileName"> the file name of the CSV </param>
    /// <returns> A List containing sets of questions </returns>
    public List<List<KeyValuePair<e_identifier, s_questionData>>>
        ReadCSV(string fileName)
    {
        Debug.Log("Starting Read of CSV: " + fileName);

        List<List<KeyValuePair<e_identifier, s_questionData>>> returnList =
            new List<List<KeyValuePair<e_identifier, s_questionData>>>();

        // load the file as a text asset
        fileName = "Conversation/" + fileName;
        TextAsset file = Resources.Load<TextAsset>(fileName);

        Debug.Assert(file, "File: " + fileName + " cannot be found");
        Debug.Log("File: " + fileName + " Has Been found");

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
    KeyValuePair<e_identifier, s_questionData> LoadQuestion(string QuestionLine)
    {
        // creating a new question
        s_questionData question = new s_questionData();
        question.options = new List<s_response>();

        // loading in the actual question
        string[] data = QuestionLine.Split(',');
        question.question = data[1];

        //loading in the options
        for (int iterator = 2; iterator < data.Length; iterator++)
        {
            string[] optionAndFeel = data[iterator].Split('|');
            s_response temp = new s_response();
            temp.response = optionAndFeel[0];

            e_connotes feel = (e_connotes) Enum.Parse(typeof(e_connotes),
                optionAndFeel[1]);

            // loop through the remaining - 1 feels if there is any other
            for (int index = 2; index < optionAndFeel.Length - 1; index++)
            {
                feel |= (e_connotes)Enum.Parse(typeof(e_connotes),
                    optionAndFeel[index]);
            }
            temp.feel = feel;

            // parse the unlock value
            temp.unlockCriteria =
                (e_unlockFlag) Enum.Parse(typeof(e_unlockFlag),
                optionAndFeel[optionAndFeel.Length - 1]);
            question.options.Add(temp);
        }
        
        // return appropriate values
        return new KeyValuePair<e_identifier, s_questionData>
            ((e_identifier) Enum.Parse(typeof(e_identifier),
            data[0]),question);
    }
}