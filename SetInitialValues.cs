using System;

public partial class MainGameCode
{
	static void ApplyValues()
	{
		score = 0;
		pickaxe = 0;
		pickCharge = 0;
		activePickaxe = false;
		localX = worldCenter - (screenWidth / 2);
		localY = worldCenter - (screenHeigh / 2);
		playerX = localX + (screenWidth / 2);
		playerY = localY + (screenHeigh / 2);
	}
}