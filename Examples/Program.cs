using Raylib_cs;

namespace Examples;

class Program
{
    public static void Main()
    {
        Raylib.InitWindow(800, 480, "Hello World");

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Pink);
            for (var i = 0; i < Raylib.GetScreenWidth(); i += 50)
            {
                Raylib.DrawLine(i,0, (int)(i * (Math.Sin(Raylib.GetTime()) * 10)), Raylib.GetScreenHeight(), Color.White);
            }
            for (var i = 0; i < Raylib.GetScreenHeight(); i += 50)
            {
                Raylib.DrawLine(0,i, 
                    Raylib.GetScreenWidth(), (int)(i * (Math.Sin(Raylib.GetTime()) * 10)), Color.White);
            }
            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}