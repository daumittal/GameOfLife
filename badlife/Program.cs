using System;

namespace badlife
{
    //Time Complexity: O(no of live cells) - Ignoring the extract initial state
    //Space Complexity: O(no of live cells)

    // LOGIC:
    // 1. Capturing only live cells from the file and finding the 8 neighbours only for live cells.
    // 2. Storing this locatio of live cells in dictionary gives constant space complexity for reading the cell
    // 3. Supporting processing very large file with finite no of live cells
    // 4. Reading large file line by line using parrallel computing and ignoring all DEAD cells
    // 5. Processing live cells in parallel
    // 6. Returing output for only live cells to the client application (Console) and client will draw both live and dead celll by calculating location. This saves lot of time 
    //    as we dont need to store the entire board in memory which is not feasible if the board of extreamly large
    // 7  Supports both file input and in memory matrics 
    // 8. Supports multiple state changes using function parameter
    // 9. Supports stop processing next generation if state is not changes
    // 10.Support both continous state generation or with delay using function parameter
    // 11.Unit test cases projecyt for business rules validation and some sample validation
    // 12.I have tested this for 500 MB file and processing and display is very quick in less than 1 second

    //Assumptios: I am assuming that for very large file, the live cells are finite which we can store in memory. My logic is based on this assumption
    //However for case when file is very large and many many live cells, I can design it differently when I will read 3 lines at a time from disk and apply the rules on those line and save the data back in disk
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.SetWindowSize(110, 60);
            Console.SetWindowPosition(0, 0);
            Console.CursorVisible = false;

            //Test 1: Using file
            IGameOfLife obj = new ConwaysGameOfLife(@"Samples\\sample_input.txt", delayInSeconds: 1);

            //Test 2: Using in memory two dimentional matrix
           
            //IGameOfLife obj = new ConwaysGameOfLife(Helper.GenerateWorld(10,10), 5000, 1);

            foreach (var world in obj.Evolve())
            {
                Helper.DisplayWord(world);
            }
        }
    }
}
