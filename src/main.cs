  using Game;
  using System.Diagnostics.CodeAnalysis;
  [ExcludeFromCodeCoverage]
  class Server
    {
      static void Main()
        {
            var spaceship = new Spaceship(new int[] { 12, 5 },new int[] { -7, 3 },45,90);
            Moving.Move(spaceship);
            Rotating.Rotate(spaceship);
            Console.WriteLine(spaceship.ToString());
        }
    }