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
                new int[] { -7 },
                0,
                Math.PI / 4
            );

            ArgumentException exepted = Assert.Throws<ArgumentException>(() => Moving.Move(spaceship));
            Assert.Equal(exepted.Message, "Не совпадают размерности векторов position и velocity");


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

            ArgumentException exepted = Assert.Throws<ArgumentException>(() => Moving.Move(spaceship));
            Assert.Equal(exepted.Message, "Не совпадают размерности векторов position и velocity");


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

            ArgumentException exepted = Assert.Throws<ArgumentException>(() => Moving.Move(spaceship));
            Assert.Equal(exepted.Message, "Не совпадают размерности векторов position и velocity");


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

            ArgumentException exepted = Assert.Throws<ArgumentException>(() => Moving.Move(spaceship));
            Assert.Equal(exepted.Message, "Не совпадают размерности векторов position и velocity");


        }
    }
}