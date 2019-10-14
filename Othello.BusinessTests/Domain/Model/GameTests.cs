using Microsoft.VisualStudio.TestTools.UnitTesting;
using Othello.Business.Domain.Model;
using Othello.Business.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.Business.Domain.Model.Tests
{
    [TestClass()]
    public class GameTests
    {
        [TestMethod()]
        public void PutStoneTest()
        {
            string borad =
                "____\n" +
                "_ab_\n" +
                "_ba_\n" +
                "____";

            Dictionary<char, StoneType> players;
            var game = new OthelloBoardFactoryFromString().Create(borad, 'a', out players);

            game.Board.ShowDebug();
            game.PutStone(new Position(0, 2), players['a']);
            game.Board.ShowDebug();
            game.PutStone(new Position(0, 3), players['b']);
            game.Board.ShowDebug();
            game.PutStone(new Position(3, 1), players['a']);
            game.Board.ShowDebug();
            game.PutStone(new Position(0, 1), players['b']);
            game.Board.ShowDebug();
        }
        [TestMethod()]
        public void PutStoneTest_IsEnd()
        {
            string borad = "___\n" +
                           "_a_\n" +
                           "_b_\n";

            Dictionary<char, StoneType> players;
            var game = new OthelloBoardFactoryFromString().Create(borad, 'b', out players);
            game.PutStone(new Position(1, 0), players['b']);
            game.Board.ShowDebug();
            Assert.AreEqual(true, game.IsEnd);
        }
        [TestMethod()]
        public void PutStoneTest_IsNotEnd()
        {
            string borad = "___\n" +
                           "_aa\n" +
                           "__b\n";

            Dictionary<char, StoneType> players;
            var game = new OthelloBoardFactoryFromString().Create(borad, 'b', out players);
            game.PutStone(new Position(2, 0), players['b']);
            game.Board.ShowDebug();
            Assert.AreEqual(false, game.IsEnd);
            Assert.AreEqual(players['b'], game.CurrentTurn);
        }
    }
}