using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game.Test
{
    [TestClass]
    public class GameTest
    {
        [TestMethod]
        public void Coordinates_Method_isAdjacent_Return_True()
        {
            Coordinates a = new Coordinates { X = 1, Y = 2 };
            Coordinates b = new Coordinates { X = 2, Y = 2 };

            bool result = a.isAdjacent(b);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void Coordinates_Method_isAdjacent_Return_False()
        {
            Coordinates a = new Coordinates { X = 1, Y = 2 };
            Coordinates b = new Coordinates { X = 2, Y = 3 };

            bool result = a.isAdjacent(b);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Too few arguments.")]
        public void Game_Should_ThrowException_When_GameHasLessThenFourTiles()
        {
            Game game = new Game(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Field isn't squared.")]
        public void Game_Should_ThrowException_When_FieldIsNotSquared()
        {
            Game game = new Game(1,2,3,4,0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Number on some tile isn't correct.")]
        public void Game_Should_ThrowException_When_NumberOnTileMoreThenSizeOfField()
        {
            Game game = new Game(1, 4, 3, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Number on some tile isn't correct.")]
        public void Game_Should_ThrowException_When_NumberIsRepeated()
        {
            Game game = new Game(1, 1, 3, 0);
        }

        [TestMethod]
        public void Game_Indexator_Should_Return_Correct_Value()
        {
            Game game = new Game(1, 2, 3, 0);
            int result = game[1,0];
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void Game_GetLocation_Should_Return_Correct_Value()
        {
            Game game = new Game(1, 2, 3, 0);
            Coordinates result = game.GetLocation(2);
            Assert.AreEqual(0, result.X);
            Assert.AreEqual(1, result.Y);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Element not adjacent to zero.")]
        public void Game_Shift_Should_ThrowException_When_ElementCanNotBeMoved()
        {
            Game game = new Game(1, 2, 3, 0);
            game.Shift(1);
        }

        [TestMethod]
        public void Game_Shift_Should_Move_Element_Correctly()
        {
            Game game = new Game(1, 2, 3, 0);
            game.Shift(2);
            Coordinates resultCoordinatesOfZero = game.GetLocation(0);
            int shiftedTile= game[1, 1];
            Assert.AreEqual(2, shiftedTile);
            Assert.AreEqual(0, resultCoordinatesOfZero.X);
            Assert.AreEqual(1, resultCoordinatesOfZero.Y);
        }

        [TestMethod]
        public void ImmutableGame_Should_BeImmutable()
        {
            ImmutableGame game = new ImmutableGame(1, 2, 3,0);
            game.Shift(2);
            Coordinates resultCoordinatesOfZero = game.GetLocation(0);
            int shiftedTile = game[0, 1];
            Assert.AreEqual(2, shiftedTile);
            Assert.AreEqual(1, resultCoordinatesOfZero.X);
            Assert.AreEqual(1, resultCoordinatesOfZero.Y);
        }

        [TestMethod]
        public void ImmutableGame_Shift_Should_CreateNewImmutableGameCorrectly()
        {
            ImmutableGame game = new ImmutableGame(1, 2, 3, 0);
            game = game.Shift(2);
            Coordinates resultCoordinatesOfZero = game.GetLocation(0);
            int shiftedTile = game[1, 1];
            Assert.AreEqual(2, shiftedTile);
            Assert.AreEqual(0, resultCoordinatesOfZero.X);
            Assert.AreEqual(1, resultCoordinatesOfZero.Y);
        }
    }
}
