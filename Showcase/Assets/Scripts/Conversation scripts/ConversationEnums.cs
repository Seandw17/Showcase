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
    AWFUL = 0
}

/// <summary>
/// Enum for identifier
/// </summary>
public enum e_identifier
{
    START = 0,
    FOLLOWINGGREAT = 1,
    FOLLOWINGGOOD = 2,
    FOLLOWINGOK = 3,
    FOLLOWINGBAD = 4,
    FOLLOWINGAWFUL = 5
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