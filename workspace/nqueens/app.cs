#!/usr/bin/dotnet run

static void Solve(int n)
{
    int[] board = new int[n];
    List<int[]> solutions = [];
    PlaceQueen(board, 0, n, solutions);

    Console.WriteLine($"Total solutions for {n}-Queens: {solutions.Count}");
    foreach (var sol in solutions)
    {
        PrintBoard(sol, n);
        Console.WriteLine();
    }
}

static void PlaceQueen(int[] board, int row, int n, List<int[]> solutions)
{
    if (row == n)
    {
        solutions.Add([.. board]);
        return;
    }
    for (int col = 0; col < n; col++)
    {
        if (IsSafe(board, row, col))
        {
            board[row] = col;
            PlaceQueen(board, row + 1, n, solutions);
        }
    }
}

static bool IsSafe(int[] board, int row, int col)
{
    for (int i = 0; i < row; i++)
    {
        if (board[i] == col || Math.Abs(board[i] - col) == row - i)
            return false;
    }
    return true;
}

static void PrintBoard(int[] board, int n)
{
    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < n; j++)
        {
            Console.Write(board[i] == j ? "Q " : ". ");
        }
        Console.WriteLine();
    }
}
Solve(int.Parse(Console.In.ReadLine()!));