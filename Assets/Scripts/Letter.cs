using System;

[Serializable]
public struct Letter
{
    public LetterStatus status;
    public char character;
    public int score;

    public Letter(char character, LetterStatus status, int score)
    {
        this.character = character;
        this.status = status;
        this.score = score;
    }
}