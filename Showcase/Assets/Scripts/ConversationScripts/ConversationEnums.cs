using System;

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

public enum e_identifier
{
    START = 1,
    GOOD = 2,
    END = 3
}

public enum e_unlockFlag
{
    NONE = 0,
    LAPTOP = 1,
    BOOK = 2,
    FLYER = 3,
    PHONE = 4
}