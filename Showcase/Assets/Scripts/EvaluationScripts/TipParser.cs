using System.Collections.Generic;
using System;
using static w_CSVLoader;

static public class TipParser 
{
    static public List<string> GenerateTips()
    {
        e_tipCategories tips = ConversationStore.GetPlayerTips();

        List<string> returnList = new List<string>();

        Dictionary<e_tipCategories, string> tipsList;

        LoadTips(out tipsList);

        foreach(e_tipCategories indvTips in Enum.GetValues(tips.GetType()))
        {
            if (tips.HasFlag(indvTips) && indvTips != e_tipCategories.NONE)
            {
                returnList.Add(tipsList[indvTips]);
            }
        }

        return returnList;
    }
}
