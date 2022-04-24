using badlife;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Text;

namespace Test
{
    [TestClass]
    public class BadLifeTests
    {
        int ROWS = 10;
        int COLS = 10;

        [TestMethod]
        public void LiveCell_LessThanTwoLiveNeighbors_Dies()
        {
            //Business Logic Test

            int currentState = 1;
            int liveNeighbors = 1;

            int nextState = ConwaysGameOfLife.GetNewState(currentState, liveNeighbors);

            Assert.AreEqual(0, nextState);


            //Real Logic Test

            //INPUT
            /*
            
            _*_
            ___
            
             */


            //OUTPUT
            /*
            
            ___
            ___
            
             */

            char[][] world = new char[2][] { new char[3] {'_','*','_' }, new char[3] { '_', '*', '_' } };


            IGameOfLife obj = new ConwaysGameOfLife(world, 1);

            foreach (var output in obj.Evolve())
            {
                var result = Helper.GetWorld(output);

                Assert.AreEqual("   ", result[0]);
                Assert.AreEqual("   ", result[1]);
            }
        }

        [TestMethod]
        public void LiveCell_TwoOrThreeLiveNeighbors_Lives()
        {
            //Business Logic Test

            int currentState = 1;
            int liveNeighbors = 2;

            int nextState = ConwaysGameOfLife.GetNewState(currentState, liveNeighbors);

            Assert.AreEqual(1, nextState);

            currentState = 1;
            liveNeighbors = 3;

            nextState = ConwaysGameOfLife.GetNewState(currentState, liveNeighbors);

            Assert.AreEqual(1, nextState);


            //Real Logic Test

            //INPUT
            /*
            
            **_*
            ____
            
             */


            //OUTPUT
            /*
            
            *___
            *___
            
             */

            char[][] world = new char[2][] { new char[4] { '*', '*', '_','*' }, new char[4] { '_', '_', '_','_' } };


            IGameOfLife obj = new ConwaysGameOfLife(world, 1);

            foreach (var output in obj.Evolve())
            {
                var result = Helper.GetWorld(output);

                Assert.AreEqual("*   ", result[0]);
                Assert.AreEqual("*   ", result[1]);
            }
        }

        [TestMethod]
        public void DeadCell_ThreeLiveNeighbors_Lives()
        {
            //Business Logic Test

            int currentState = 0;
            int liveNeighbors = 3;

            int nextState = ConwaysGameOfLife.GetNewState(currentState, liveNeighbors);

            Assert.AreEqual(1, nextState);


            //Real Logic Test

            //INPUT
            /*
            
            *___
            _*__
            __*_
            
             */


            //OUTPUT
            /*
            
            _*__
            _*__
            _*__
            
             */

            char[][] world = new char[3][] { new char[4] { '*', '_', '_', '_' }, new char[4] { '_', '*', '_', '_' }, new char[4] { '_', '_', '*', '_' } };


            IGameOfLife obj = new ConwaysGameOfLife(world, 1);

            foreach (var output in obj.Evolve())
            {
                var result = Helper.GetWorld(output);

                Assert.AreEqual(" *  ", result[0]);
                Assert.AreEqual(" *  ", result[1]);
                Assert.AreEqual(" *  ", result[2]);
            }
        }

        [TestMethod]
        public void LiveCell_MoreThanThreeLiveNeighbors_Dies()
        {
            //Business Logic Test

            int currentState = 1;
            int liveNeighbors = 4;

            int nextState = ConwaysGameOfLife.GetNewState(currentState, liveNeighbors);

            Assert.AreEqual(0, nextState);

            //Real Logic Test

            //INPUT
            /*
            
            _*__
            _**_
            _**_
            
             */


            //OUTPUT
            /*
            
            *___
            *___
            *___
            
             */

            char[][] world = new char[3][] { new char[4] { '_', '*', '_', '_' }, new char[4] { '_', '*', '*', '_' }, new char[4] { '_', '*', '*', '_' } };


            IGameOfLife obj = new ConwaysGameOfLife(world, 1);

            foreach (var output in obj.Evolve())
            {
                var result = Helper.GetWorld(output);

                Assert.AreEqual("*   ", result[0]);
                Assert.AreEqual("*   ", result[1]);
                Assert.AreEqual("*   ", result[2]);
            }
        }

    
        //State blink repeatdely 
        [TestMethod]
        public void BlinkerPatternTest()
        {
            //INPUT
            /*
            _____
            __*__
            __*__
            __*__
            _____
            
             */


            //OUTPUT
            /*
            
            _____
            _____
            _***_
            _____
            _____
            
             */


            IGameOfLife obj = new ConwaysGameOfLife(@"Samples\\blinkerPattern.txt", 1);

            foreach (var output in obj.Evolve())
            {
                var result = Helper.GetWorld(output);

                Assert.AreEqual("     ", result[0]);
                Assert.AreEqual("     ", result[1]);
                Assert.AreEqual(" *** ", result[2]);
                Assert.AreEqual("     ", result[3]);
                Assert.AreEqual("     ", result[4]);
            }

            foreach (var output in obj.Evolve())
            {
                var result = Helper.GetWorld(output);

                Assert.AreEqual("     ", result[0]);
                Assert.AreEqual("  *  ", result[1]);
                Assert.AreEqual("  *  ", result[2]);
                Assert.AreEqual("  *  ", result[3]);
                Assert.AreEqual("     ", result[4]);
            }
        }

        //State never change
        [TestMethod]
        public void BlockPatternTest()
        {
            //INPUT
            /*
            **
            **
            */

            //OUTPUT
            /*
            **
            **
            */


            IGameOfLife obj = new ConwaysGameOfLife(@"Samples\\blockPattern.txt", 1);

            foreach (var output in obj.Evolve())
            {
                var result = Helper.GetWorld(output);

                Assert.AreEqual("**", result[0]);
                Assert.AreEqual("**", result[1]);
            }
        }

        [TestMethod]
        public void RandomPatternWithSingleStateChangeTest()
        {
            ROWS = 5;
            COLS = 5;

            IGameOfLife obj = new ConwaysGameOfLife(Helper.GenerateWorld(ROWS, COLS), 1, 0);

            foreach (var world in obj.Evolve())
            {
                DisplayWord(world);
            }

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void RandomPatternWithMultipleStateChangeTest()
        {
            ROWS = 5;
            COLS = 5;

            IGameOfLife obj = new ConwaysGameOfLife(Helper.GenerateWorld(ROWS, COLS), 3, 0);

            foreach (var world in obj.Evolve())
            {
                DisplayWord(world);
            }

            Assert.IsTrue(true);
        }


        [TestMethod]
        public void SampleFileTest()
        {

            IGameOfLife obj = new ConwaysGameOfLife(@"Samples\\sample_input.txt", 10);

            foreach (var world in obj.Evolve())
            {
                DisplayWord(world);
            }

            Assert.IsTrue(true);
        }


        public static void DisplayWord(Output world)
        {
            
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < world.MaxRows; i++)
            {
                sb.Clear();

                for (int j = 0; j < world.MaxCols; j++)
                {
                    if (world.LiveCells.ContainsKey($"{i}#{j}"))
                        sb.Append("*");
                    else
                        sb.Append(" ");
                }

                Trace.WriteLine(sb.ToString());

            }

            Trace.WriteLine("");
        }
    }
}
