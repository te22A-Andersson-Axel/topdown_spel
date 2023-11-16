﻿using Raylib_cs;
using System.Numerics;

Raylib.InitWindow(1600, 1300, "Hello");
Raylib.SetTargetFPS(60);

Vector2 movement = new Vector2(0, 0);

Rectangle doorRect = new Rectangle(1400, 100, 32, 32);

Rectangle characterRect = new Rectangle(300, 400, 206, 236);
Texture2D characterImage = Raylib.LoadTexture("gubbe.png");

float speed = 15;

string scene = "start";

int points = 0;

while (!Raylib.WindowShouldClose())
{
  if (scene == "game")
  {
    movement = Vector2.Zero;

    if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
    {
      movement.Y = -1;
    }
    else if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
    {
      movement.Y = 1;
    }
    if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
    {
      movement.X = -1;
    }
    else if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
    {
      movement.X = 1;
    }

    if (movement.Length() > 0)
    {
      movement = Vector2.Normalize(movement) * speed;
    }
    characterRect.x += movement.X;
    characterRect.y += movement.Y;

    if (characterRect.x > 1600 - 206 || characterRect.x < -50)
    {
      characterRect.x -= movement.X;
    }

    if (characterRect.y > 1054 || characterRect.y < -10)
    {
      characterRect.y -= movement.Y;
    }

    if (Raylib.CheckCollisionRecs(characterRect, doorRect))
    {
      scene = "ny";
      points++;
    }

  }
  else if (scene == "start")
  {
    if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
    {
      scene = "game";
    }
  }

  else if (scene == "ny")
  {
    Raylib.ClearBackground(Color.BEIGE);
    Raylib.DrawText("Du förlorade!!!!", 450, 620, 100, Color.BROWN);
  }

  Raylib.BeginDrawing();
  if (scene == "game")
  {
    Raylib.ClearBackground(Color.DARKGREEN);

    Raylib.DrawRectangleRec(doorRect, Color.PURPLE);

    Raylib.DrawTexture(characterImage, (int)characterRect.x, (int)characterRect.y, Color.WHITE);

    Raylib.DrawText(points.ToString(), 10, 10, 32, Color.WHITE);
  }

  else if (scene == "start")
  {
    Raylib.ClearBackground(Color.BLACK);
    Raylib.DrawText("Play space to start!", 350, 620, 100, Color.WHITE);
  }
  
  else if (scene == "finished")
  {
    Raylib.ClearBackground(Color.YELLOW);
  }

  Raylib.EndDrawing();
}