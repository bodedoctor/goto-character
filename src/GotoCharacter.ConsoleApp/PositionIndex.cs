internal class PositionIndex
{
    public int LineIndex { get; set; }
    public int CharacterIndex { get; set; }

    public static PositionIndex GetPosition(string content, int character)
    {
        var newLineChar = '\n';
        var lineIndex = content.Substring(0, character).Count(c => c.Equals(newLineChar));
        var lines = content.Split(newLineChar);
        var charCountToLine = 0;
        for (var i = 0; i < lineIndex; i++)
        {
            charCountToLine += lines[i].Length + 1;
        }
        var charIndex = character - charCountToLine;
        return new PositionIndex
        {
            LineIndex = lineIndex,
            CharacterIndex = charIndex
        };
    }
}
