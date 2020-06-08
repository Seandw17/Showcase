using System;
// Author: Alec

/// <summary>
/// the rating of how good the response
/// </summary>
public enum e_rating
{
    GREAT = 4,
    GOOD = 3,
    OK = 2,
    BAD = 1,
    AWFUL = 0,
    NONE
}

/// <summary>
/// Enum to represent Unlock flag
/// </summary>
public enum e_unlockFlag
{
    NONE = 0,
    FIRST = 2,
    SECOND = 3,
    THIRD = 4,
    FOURTH = 5
}

//TODO figure out what my tip categories are
[Flags]
public enum e_tipCategories
{
    NONE
}