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

[Flags]
public enum e_tipCategories
{
    NONE = 1 << 1,
    NOTASKING = 1 << 2,
    UNDEMANDING = 1 << 3,
    SOMETHINGBETTER = 1 << 4,
    ENTHUSIASM = 1 << 5,
    CRITICISM = 1 << 6,
    HARDWORK = 1 << 7,
    FORWARD = 1 << 8,
    UNIQUE = 1 << 9,
    PROBLEM = 1 << 10,
}