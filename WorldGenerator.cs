using System;
using System.Numerics;

public partial class MainGameCode
{
	static void WorldGenerator()
	{
		//int wX = 0, mY = 0;
		for(int y = 0; y < world.GetLength(0) - 1; y ++)
		{
			for(int x = 0; x < world.GetLength(1) - 1; x ++)
			{
				world[x,y] = 0;
			}
		}

		//	==========	Obstaccle Generator	==========

		PorcentualGenerator(expectedObstacles, roll, 2, 1);

		//	==========	Rock Floor Generator	==========

		for(int y = 1; y < world.GetLength(0) - 2; y ++)
		{
			for(int x = 1; x < world.GetLength(1) - 2; x++)
			{
				if(world[x,y] == 1)
				{
					continue;
				}
				if(EightSensor(x, y, 1) >= 3)
				{
					world[x,y] = 4;
				}
			}
		}

		//	==========	Mine Generator	==========

		PorcentualGenerator(expectedMines, roll, 4, 2);

		//	==========	Treasure Generator	==========

		PorcentualGenerator(expectedGems, roll, 2, 3);

		//	==========	Generates Spawn Area	==========

		PlayerSpawnArea();
		
	}

	static void PorcentualGenerator(BigInteger expected, Random roll, int secondCheck, int painter)
	{
		int x, y;
		for(BigInteger i = 0; i < expected; i ++)
		{
			//		It may be confused but damn, it works.
			do
			{
				x = roll.Next(0, world.GetLength(1));
				y = roll.Next(0, world.GetLength(0));
				if(world[x,y] != 1 && world[x,y] != secondCheck)
				{
					world[x,y] = painter;
					break;
				}
			}while(true);
		}
	}

	static int EightSensor(int x, int y, int criteria)
	{
		int count = 0;
		count += world[x - 1, y - 1] == criteria? 1 : 0;
		count += world[x - 0, y - 1] == criteria? 1 : 0;
		count += world[x + 1, y - 1] == criteria? 1 : 0;
		count += world[x - 1, y - 0] == criteria? 1 : 0;
		count += world[x + 1, y - 0] == criteria? 1 : 0;
		count += world[x - 1, y + 1] == criteria? 1 : 0;
		count += world[x - 0, y + 1] == criteria? 1 : 0;
		count += world[x + 1, y + 1] == criteria? 1 : 0;
		return count;
	}

	static void PlayerSpawnArea()
	{
		int limits = (playerSpawnRatio * 2) + 1;
		int initialMeasure = worldCenter - playerSpawnRatio;

		for(int y = 0; y < limits + 1; y ++)
		{
			for(int x = 0; x < limits + 1; x ++)
			{
				if(IsIn(x, y, playerSpawnRatio))
				{
					if(roll.Next(1, 11) <= obstaclesInSpawn && !(x == playerSpawnRatio && y == playerSpawnRatio))
					{
						world[initialMeasure + x, initialMeasure + y] = 1;
					}else
					{
						world[initialMeasure + x , initialMeasure + y] = 4;
					}
				}		
			}
		}
	}

	static bool IsIn(int x, int y, int ratio)
	{
		int absoluteX = Math.Abs(ratio - x);
		absoluteX *= absoluteX;
		int absoluteY = Math.Abs(ratio - y);
		absoluteY *= absoluteY;
		ratio *= ratio;

		return absoluteX + absoluteY <= ratio + (ratio * 0.07);
	}
}