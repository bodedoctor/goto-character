namespace GotoCharacter.ConsoleApp
{
    internal class Program
    {
        private const ConsoleColor HighlightColour = ConsoleColor.Red;
        private const ConsoleColor LowLightColour = ConsoleColor.DarkGray;

        static void Main(string[] args)
        {
            string? filenameInput;
            if (args.Length > 0)
            {
                filenameInput = args[0];
            }
            else
            {
                Console.WriteLine("Please specify filename:");
                filenameInput = Console.ReadLine();
                while (string.IsNullOrEmpty(filenameInput))
                {
                    Console.WriteLine($"Invalid filename '{filenameInput}'.");

                    Console.WriteLine("Please specify filename:");
                    filenameInput = Console.ReadLine();
                }
            }

            string? characterInput;
            if (args.Length > 1)
            {
                characterInput = args[1];
            }
            else
            {
                Console.WriteLine("Please specify character number:");
                characterInput = Console.ReadLine();

                while (string.IsNullOrEmpty(characterInput))
                {
                    Console.WriteLine($"Invalid character number '{characterInput}'.");

                    Console.WriteLine("Please specify character number:");
                    characterInput = Console.ReadLine();
                }
            }

            if (!File.Exists(filenameInput))
            {
                Console.WriteLine($"Could not find file '{filenameInput}'.");
                return;
            }

            if (int.TryParse(characterInput, out var character))
            {
                var fileContent = File.ReadAllText(filenameInput);
                if (character > fileContent.Length)
                {
                    Console.WriteLine($"Character number {characterInput} was beyond the length of the file.");
                    return;
                }
                HighlightCharacterPosition(fileContent, character);
            }
            else
            {
                Console.WriteLine($"Invalid argument for character number: '{characterInput}'.");
            }
        }

        private static void HighlightCharacterPosition(string fileContent, int character, int previewLines = 3)
        {
            var originalColour = Console.ForegroundColor;
            Console.WriteLine($"Preview:");
            Console.WriteLine();

            var position = PositionIndex.GetPosition(fileContent, character);

            var lines = fileContent.Split('\n');
            var startLine = Math.Max(position.LineIndex - previewLines, 0);
            var endLine = Math.Min(position.LineIndex + previewLines + 1, lines.Length - 1);
            for (var i = startLine; i < endLine; i++)
            {
                if (i == position.LineIndex)
                {
                    Console.ForegroundColor = originalColour;
                    Console.Write($" {i}:\t{lines[i].Substring(0, position.CharacterIndex)}");
                    Console.ForegroundColor = HighlightColour;
                    Console.Write(lines[position.LineIndex][position.CharacterIndex]);
                    Console.ForegroundColor = originalColour;
                    Console.Write(lines[i].Substring(position.CharacterIndex + 1, lines[i].Length - position.CharacterIndex - 1));
                    Console.WriteLine();
                }
                else
                {
                    Console.ForegroundColor = LowLightColour;
                    Console.WriteLine($" {i}:\t{lines[i]}");
                    Console.ForegroundColor = originalColour;
                }
            }

            Console.WriteLine();
            Console.Write("Found character ");
            Console.ForegroundColor = HighlightColour;
            Console.Write(character);
            Console.ForegroundColor = originalColour;
            Console.Write(" on line ");
            Console.ForegroundColor = HighlightColour;
            Console.Write(position.LineIndex);
            Console.ForegroundColor = originalColour;
            Console.Write(".");
            Console.WriteLine();
        }
    }
}