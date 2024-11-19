namespace TowersOfHanoi;

class Program
{
    public static Stack<int>[] Towers = new Stack<int>[]
    {
        new Stack<int>(),
        new Stack<int>(),
        new Stack<int>(),
    };
    public static int towerHeight = 5;

    public static int moves = 0;
    public static int rings = 0;
    
    static void Main(string[] args)
    {
        
        Console.WriteLine("TOWERS OF HANOI");
        Console.Write("How many rings would you like? ");
        towerHeight = InputInt(1);
        Console.Clear();
        
        PopulateTower(Towers[0], towerHeight);

        DisplayBoards();
        while (!WinCheck())
        {
            Console.Write("Tower to move from: ");
            int moveTower = InputInt(1, 3);
            Console.Write("Tower to move to: ");
            int toTower = InputInt(1, 3);
            MoveRing(Towers[moveTower - 1], Towers[toTower - 1]);
            ClearDisplay();
            DisplayBoards();
        }

        Console.WriteLine("win with " + moves + " moves.");
        Console.ReadLine();
    }
    
    static void PopulateTower(Stack<int> stack, int maxNumber)
    {
        rings = maxNumber;
        for (int i = maxNumber; i > 0; i--)
        {
            stack.Push(i);
        }
    }
    
    static bool MoveRing(Stack<int> fromStack, Stack<int> toStack)
    {
        if (fromStack.Count == 0) return false;
        int moveRing = fromStack.Peek();
        if (toStack.Count != 0)
        {
            if (toStack.Peek() < moveRing) return false;
        }
        fromStack.Pop();
        toStack.Push(moveRing);
        moves++;
        return true;
    }

    static bool WinCheck()
    {
        if (Towers[2].Count == rings)
        {
            return true;
        }

        return false;
    }

    static void DisplayBoards()
    {
        int[][] towers = new int[3][];
        towers[0] = Towers[0].ToArray();
        towers[1] = Towers[1].ToArray();
        towers[2] = Towers[2].ToArray();

        int maxHeight = towerHeight + 1;
        int maxWidth = towers.SelectMany(t => t).DefaultIfEmpty().Max() * 2 + 1;

        for (int i = maxHeight - 1; i >= 0; i--)
        {
            for (int t = 0; t < 3; t++)
            {
                if (i < towers[t].Length)
                {
                    int width = towers[t][towers[t].Length - 1 - i] * 2 + 1;
                    int padding = (maxWidth - width) / 2;

                    Console.Write(new string(' ', padding));
                    Console.Write(new string('#', width));
                    Console.Write(new string(' ', padding));
                }
                else
                {
                    Console.Write(new string(' ', maxWidth / 2) + "|");
                    Console.Write(new string(' ', maxWidth / 2));
                }

                if (t < 2) Console.Write("   ");
            }
            Console.WriteLine();
        }
    }

    static void ClearDisplay()
    {
        Console.Clear();
    }

    static int InputInt(int? lower = null, int? upper = null)
    {
        int variable;
        while (!(int.TryParse(Console.ReadLine(), out variable) && (variable >= lower || lower == null) && (variable <= upper || upper == null)))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Input must be an integer");
            if (lower != null && upper == null)
            {
                Console.Write(" greater than " + (lower - 1));
            } else if (lower != null && upper != null)
            {
                Console.Write(" between " + lower + " and " + upper); 
            } else if (lower == null && upper != null)
            {
                Console.Write(" less than " + (lower + 1));
            }
            Console.Write(", try again: ");
            Console.ForegroundColor = ConsoleColor.White;
        }

        return variable;
    }
    
}