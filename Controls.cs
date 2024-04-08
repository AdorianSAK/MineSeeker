using System;
using System.Numerics;

public partial class MainGameCode
{
	static void Control()
	{
		control = Console.ReadKey().KeyChar;
		PlayerMove(control);
	}

	static void PauseControl()
	{
		control = Console.ReadKey().KeyChar;
		switch(control)
		{
			case '\n':
				return;
			case 'q':
				gameIsOn = false;
				return;
			default:
				PauseScreen();	break;
		}
	}

	static void PlayerMove(char playerInput)
	{
		int x = 0;
		int y = 0;
		int posibleInput = 0;

		if(int.TryParse(playerInput.ToString(), out posibleInput))
		{
			BannerPlacer(posibleInput);
			return;
		}

		switch(playerInput)
		{
			case 'w':
				if((DirectionFourSensor(playerX, playerY) & 1) == 0 || activePickaxe)
				{
					y = -1;
				}
				break;
			case 'd':
				if((DirectionFourSensor(playerX, playerY) & 2) == 0 || activePickaxe)
				{
					x = 1;
				}
				break;
			case 's':
				if((DirectionFourSensor(playerX, playerY) & 4) == 0 || activePickaxe)
				{
					y = 1;
				}
				break;
			case 'a':
				if((DirectionFourSensor(playerX, playerY) & 8) == 0 || activePickaxe)
				{
					x = -1;
				}
				break;
			case ' ':
				if(pickaxe > 0)
				{
					activePickaxe = !activePickaxe;
					Console.Beep(4000, 100);
					Console.Beep(2000, 50);
				} return;
			case 'p':
				PauseScreen();
				return;
		}

		localX += x;
		localY += y;
		playerX += x;
		playerY += y;

		if(world[playerX, playerY] == 3)
		{
			TreasureCollect();
		}else if(world[playerX, playerY] == 2 || world[playerX, playerY] == 7 || world[playerX, playerY] == 8)
		{
			StepOnMine();
		}else if(world[playerX, playerY] == 1)
		{
			MinedObstacle();
		}
	}

	static int DirectionFourSensor(int x, int y)
	{
		int direction = 0;
		direction = world[x + 0, y - 1] == 1? direction | 1 : direction ;
		direction = world[x + 1, y - 0] == 1? direction | 2 : direction ;
		direction = world[x + 0, y + 1] == 1? direction | 4 : direction ;
		direction = world[x - 1, y - 0] == 1? direction | 8 : direction ;
		return direction;
	}

	static void BannerPlacer(int playerInput)
	{
		int x = 0;
		int y = 0;
		switch(playerInput)
		{
			case 1:
				x = -1;	y = 1;	break;
			case 2:
				x = 0;	y = 1;	break;
			case 3:
				x = 1;	y = 1;	break;
			case 4:
				x = -1;	y = 0;	break;
			case 6:
				x = 1;	y = 0;	break;
			case 7:
				x = -1;	y = -1;	break;
			case 8:
				x = 0;	y = -1;	break;
			case 9:
				x = 1;	y = -1;	break;
			default:
				return;
		}

		int markerX = playerX + x;
		int markerY = playerY + y;

		if(world[markerX, markerY] == 0 || world[markerX, markerY] == 2)
		{
			world[markerX, markerY] = world[markerX, markerY] == 0? 5 : 7;
		}else if(world[markerX, markerY] == 5 || world[markerX, markerY] == 7)
		{
			world[markerX, markerY] = world[markerX, markerY] == 5? 6 : 8;
		}else if(world[markerX, markerY] == 6 || world[markerX, markerY] == 8)
		{
			world[markerX, markerY] = world[markerX, markerY] == 6? 0 : 2;
		}
	}
}