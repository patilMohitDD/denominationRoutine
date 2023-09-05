using System;
using System.Collections.Generic;
using System.Linq;

public class ATM
{
    public static List<List<int>> FindCombinations(int amount, List<int> denominations)
    {
        List<List<int>> combinations = new List<List<int>>();  // Store valid combinations

        Stack<Tuple<int, List<int>, List<int>>> stack = new Stack<Tuple<int, List<int>, List<int>>>();

        // Initialize stack for iterative DFS
        stack.Push(new Tuple<int, List<int>, List<int>>(amount, denominations, new List<int>()));

        while (stack.Count > 0)
        {
            Tuple<int, List<int>, List<int>> current = stack.Pop();
            int currentAmount = current.Item1;
            List<int> currentDenoms = current.Item2;
            List<int> currentCombination = current.Item3;

            if (currentAmount == 0)  // Found a valid combination
            {
                combinations.Add(currentCombination);
                continue;
            }

            if (currentAmount >= 0 && currentDenoms.Count > 0)
            {
                // Exclude the first denomination
                stack.Push(new Tuple<int, List<int>, List<int>>(currentAmount, currentDenoms.Skip(1).ToList(), currentCombination));

                // Include the first denomination
                List<int> newCombination = new List<int>(currentCombination);
                newCombination.Add(currentDenoms[0]);
                stack.Push(new Tuple<int, List<int>, List<int>>(currentAmount - currentDenoms[0], currentDenoms, newCombination));
            }
        }

        return combinations;
    }

    public static void Main(string[] args)
    {
        List<int> denominations = new List<int> { 10, 50, 100 };
        List<int> amounts = new List<int> { 30, 50, 60, 80, 140, 230, 370, 610, 980 };

        foreach (int amount in amounts)
        {
            Console.WriteLine($"For {amount} EUR, the available combinations are :- ");
            List<List<int>> combinations = FindCombinations(amount, denominations);

            if (combinations.Count == 0)
            {
                Console.WriteLine("No valid combinations found.");
            }
            else
            {
                foreach (List<int> combination in combinations)
                {
                    var counts = combination.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
                    Console.WriteLine(string.Join(" + ", counts.Select(kvp => $"{kvp.Value} x {kvp.Key} EUR")));
                   
                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}
