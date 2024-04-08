using System;
using System.Numerics;
using System.Threading;

public partial class MainGameCode
{
	static void Main()
	{
		WorldGenerator();

		while(gameIsOn)
		{
			DrawMap();
			Control();	
		}

		Console.Clear();
		Console.WriteLine("\n\n\t\tThanks for playing.");
		Thread.Sleep(1000);
		Console.Clear();

		Console.ResetColor();
	}
}