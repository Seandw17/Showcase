using System;

// Author: Alec

/// <summary>
/// Enum to show connotation
/// </summary>
[Flags]
public enum e_connotes
{
    CONFIDENCE = 1 << 0,
    NERVOUSNESS = 1 << 1,
    SKILL = 1 << 2,
    LACKKNOWLEDGE = 1 << 3,
    HAVEKNOWLEDGE = 1 << 4,
    UNSURE = 1 << 5,
    BOASTFUL = 1 << 6
}

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
    GOOD = 2,
    END = 3
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