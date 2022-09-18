using Xunit;
using System;
using ship;
namespace test
{
    public class Movetest
    {
        [Fact]
        public void Mov()
        {
            var spaceship = new Spaceship(
                new int[] { 12, 5 },
                new int[] { -7, 3 },
                0,
                Math.PI / 4
            );
            Moving.Move(spaceship);
            
        }


    }
}