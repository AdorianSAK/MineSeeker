using System;
using System.Numerics;

public partial class MainGameCode
{
	static void TreasureCollect()
	{
		world[playerX, playerY] = 0;
		score ++;
		pickCharge ++;
		Console.Beep(1500, 200);
		if(pickCharge == 10)
		{
			pickaxe ++;
			pickCharge = 0;
			Console.Beep(1500, 7);
			Console.Beep(1200, 7);
			Console.Beep(1700, 7);
			Console.Beep(1800, 7);
			Console.Beep(1500, 7);
			Console.Beep(2000, 7);
		}

		//	Check if floor is StepOnMine ==========
		if(EightSensor(playerX, playerY, 1) >= 3)
		{
			world[playerX, playerY] = 4;
		}
	}

	static void StepOnMine()
	{
		Console.Clear();
		Console.WriteLine("\n\n\t\tGame Over");
		for(int y = 0; y < world.GetLength(0); y ++)
		{
			for(int x = 0; x < world.GetLength(1); x ++)
			{
				score += world[x, y] == 7? 1 : 0;
				score -= world[x, y] == 5? 1 : 0;
			}
		}
		Console.WriteLine("\n\n\t\tYour Score is: " + score);

		if(IsHighScore(score))
		{
			RegisterPlayer(UserUI.LimitStrRecept(16), score);
			ScoreRecorder();
		}

		ApplyValues();
		WorldGenerator();
		Console.WriteLine("\n\t\tPress any key to restart.");
		Console.ReadKey();
	}

	static void MinedObstacle()
	{
		world[playerX, playerY] = 4;
		pickaxe --;
		activePickaxe = pickaxe == 0? false : true;
		Console.Beep(350, 7);
		Console.Beep(450, 7);
		Console.Beep(250, 7);
	}
}

//	This is a comment