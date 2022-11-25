using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BME.GameLoop;
using BME;

namespace TicTacToe
{
    class Program
    {
        public static void Main() {
            Game _game = new TicTacToeGame(800,600, "Game");
            _game.Run();
        }    

    }
}
