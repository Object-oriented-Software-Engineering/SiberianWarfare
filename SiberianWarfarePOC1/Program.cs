using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;
using SiberianWarfarePOC1.Components;
using SiberianWarfarePOC1.GameObjects;
using SiberianWarfarePOC1.Interfaces;

namespace SiberianWarfarePOC1 {
    internal class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello, World!");
            var gameState = new SiberianWarfareGameState();
            gameState.Initialize();
        }
    }


    

    
}
