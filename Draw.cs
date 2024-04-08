using System;
using System.Numerics;

public partial class MainGameCode
{
	static void DrawMap()
	{
		Console.Clear();
		PositionMarker();
		SystemColorSelector();
		Console.ForegroundColor = systemColor;
		UpperMarginDrawer();
		ScreenDrawer();
		BottomMarginDrawer();
	}

	static void PositionMarker()
	{
		string chargeBar = "";
		Console.ResetColor();
		Console.Write("\n\t\tPosition: {0, 4}, {1, 4}\t", playerX + 1, playerY + 1);
		Console.Write("Score: {0,3}", score);
		if(activePickaxe)
		{
			Console.ForegroundColor = ConsoleColor.Green;
		}else
		{
			Console.ResetColor();
		}
		Console.Write(activePickaxe? "\t\t►►►" : "\t\t   ");
		Console.Write("⛏️  X {0, 3}", pickaxe);
		Console.Write(activePickaxe? "◄◄◄\n" : "\n");
		Console.ResetColor();
		Console.Write("\tCBV: " + DirectionFourSensor(playerX, playerY)); // Collision Binary Value
		Console.Write("\t\t\t\t\t\t\t[");
		Console.ForegroundColor = ConsoleColor.Green;
		for(int i = 0; i < pickCharge; i ++)
		{
			chargeBar += "█";
		}
		Console.Write("{0, -9}", chargeBar);
		Console.ResetColor();
		Console.WriteLine("]\n");
	}

	static void ScreenDrawer()
	{
		for(int y = 0; y < screenHeigh; y ++)
		{
			Console.Write("\t\t\t||");
			for(int x = 0; x < screenWidth; x ++)
			{
				if(x == screenWidth / 2 && y == screenHeigh / 2)
				{
					Console.ForegroundColor = ConsoleColor.Green;
					Console.Write("☻ ");
				}else if(world[localX + x, localY + y] == 6 || world[localX + x, localY + y] == 8)
				{
					Console.ForegroundColor = systemColor;
					Console.Write("? ");
				}else if(world[localX + x, localY + y] == 5 || world[localX + x, localY + y] == 7)
				{
					Console.Write("🚩");
				}else if(world[localX + x, localY + y] == 4)
				{
					Console.ForegroundColor = ConsoleColor.DarkGray;
					Console.Write("▒▒");
				}else if(world[localX + x, localY + y] == 3)
				{
					Console.Write("💎");
				}else if(world[localX + x, localY + y] == 1)
				{
					Console.ForegroundColor = systemColor;
					Console.Write("██");
				}/*else if(world[localX + x, localY + y] == 2)
				{
					Console.Write("💣");
				}*/else
				{
					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.Write("▒▒");	
				}
				Console.ForegroundColor = systemColor;
			}
			Console.Write("||");
			//	UI was here before
			Console.WriteLine();
		}
	}

	static void UpperMarginDrawer()
	{
		Console.Write("\t\t\t💀");
		for(int i = 0; i < screenWidth; i ++)
		{
			Console.Write("==");
		}
		Console.WriteLine("💀");
	}

	static void BottomMarginDrawer()
	{
		Console.Write("\t\t\t💀");
		BottomAssistDraw("==");
		Console.Write("💀======💀");
		BottomAssistDraw("==");
		Console.WriteLine("💀");
		Console.Write("\t\t\t  ");
		BottomAssistDraw("  ");
		Console.Write("||   " +  TotalDetected());
		Console.ForegroundColor = systemColor;
		Console.Write("  ||\n");
		Console.Write("\t\t\t  ");
		BottomAssistDraw("  ");
		Console.WriteLine("💀======💀");
	}

	static void BottomAssistDraw(string thingToDraw)
	{
		for(int i = 0; i < (screenWidth / 2) - 2; i ++)
		{
			Console.Write(thingToDraw);
		}
	}

	static void SystemColorSelector()
	{
		switch(TotalDetected())
		{
			case 1: 
				systemColor = ConsoleColor.Blue; break;
			case 2:
				systemColor = ConsoleColor.DarkGreen; break;
			case 3:
				systemColor = ConsoleColor.Red; break;
			case 4:
				systemColor = ConsoleColor.DarkYellow; break;
			case 5:
				systemColor = ConsoleColor.DarkRed; break;
			case 6:
				systemColor = ConsoleColor.DarkCyan; break;
			case 7:
				systemColor = ConsoleColor.Black; break;
			case 8:
				systemColor = ConsoleColor.DarkGray; break;
			default:
				systemColor = ConsoleColor.White; break;
		}
	}

	static int TotalDetected()
	{
		return EightSensor(playerX, playerY, 2) + EightSensor(playerX, playerY, 7) +
			EightSensor(playerX, playerY, 8);
	}

	static void PauseScreen()
	{
		Console.Clear();
		Console.WriteLine("\n\n\t\tGame Paused");
		Console.WriteLine("\n\t\t Press Enter to continue.");
		Console.WriteLine("\n\t\tPress Q to exit game");
		Console.WriteLine("\n\t\tPress S to see High Scores");
		PauseControl();
		return;
	}
}