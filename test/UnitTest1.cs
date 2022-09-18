
namespace Game.Tests;

public class UnitTest1
{
   [Fact]
        public void Mov()
        {

            var spaceship = new Spaceship(
                new int[] { 12, 5 },
                new int[] { -7 },
                0,
                Math.PI / 4
            );
            string exepted1 = "Не совпадают размерности векторов position и velocity";
            ArgumentException exepted = Assert.Throws<ArgumentException>(() => Moving.Move(spaceship));
            Assert.Equal(exepted1,exepted.Message);


        }
        [Fact]
        public void Mov1()
        {

            var spaceship = new Spaceship(
                new int[] { 12, 5 },
                new int[] { -7, 3 },
                0,
                Math.PI / 4
            );
            Moving.Move(spaceship);
        }
        [Fact]
        public void Mov2()
        {

            var spaceship = new Spaceship(
                new int[] { },
                new int[] { -7, 3 },
                0,
                Math.PI / 4
            );
            string exepted1 = "Не совпадают размерности векторов position и velocity";
            ArgumentException exepted = Assert.Throws<ArgumentException>(() => Moving.Move(spaceship));
            Assert.Equal(exepted.Message, exepted1);


        }
        [Fact]
        public void Mov3()
        {

            var spaceship = new Spaceship(
                new int[] { 5 },
                new int[] { -7, 3 },
                0,
                Math.PI / 4
            );
            string exepted1 = "Не совпадают размерности векторов position и velocity";
            ArgumentException exepted = Assert.Throws<ArgumentException>(() => Moving.Move(spaceship));
            Assert.Equal(exepted.Message, exepted1);


        }
        [Fact]
        public void Rot()
        {

            var spaceship = new Spaceship(
                new int[] { 12, 5 },
                new int[] { -7, 3 },
                45,
                90
            );


        }
    }


