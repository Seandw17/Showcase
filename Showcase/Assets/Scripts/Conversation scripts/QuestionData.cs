using System.Collections.Generic;
using System;
/// <summary>
/// Class to hold a <see cref="question"/>
/// </summary>
public class QuestionData
{
    public int ID;
    public Dictionary<e_rating, string> questions;
    public List<Questionresponse> options;
    public e_tipCategories tip;
}
