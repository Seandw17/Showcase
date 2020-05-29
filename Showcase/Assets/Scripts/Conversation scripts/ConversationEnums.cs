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
    AWFUL = 0
}

/// <summary>
/// Enum for identifier
/// </summary>
public enum e_identifier
{
    START = 1,
    FOLLOWGREAT = 2,
    FOLLOWGOOD = 3,
    FOLLOWOK = 4,
    FOLLOWBAD = 5,
    FOLLOWAWFUL = 6
}

/// <summary>
/// Enum to represent Unlock flag
/// </summary>
[Flags]
public enum e_unlockFlag
{
    NONE = 1 << 0,
    LAPTOP = 1 << 1,
    BOOK = 1 << 2,
    FLYER = 1 << 3,
    PHONE = 1 << 4
}