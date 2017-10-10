using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace twozerofoureight
{
    class TwoZeroFourEightModel : Model
    {
        protected int boardSize; // default is 4
        protected int[,] board;
        protected Random rand;

        protected int score = 0;    //declare score point
        protected bool isMove = true;

        public TwoZeroFourEightModel() : this(4)
        {
            // default board size is 4 
        }

        public int[,] GetBoard()
        {
            return board;
        }

        public int GetScore()   //score return function
        {
            return score;
        }

        public bool IsGameOver()
        {
            for(int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (board[i, j] == 0)   //can move to blank space
                    {
                        return false;
                    }
                }
            }
            //create buffer for check
            int[,] buffer = new int[boardSize + 2, boardSize + 2];
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    buffer[i + 1, j + 1] = board[i, j];
                }
            }
            //check all
            for (int i = 1; i <= boardSize; i++)
            {
                for (int j = 1; j <= boardSize; j++)
                {
                    if (buffer[i, j] == buffer[i, j - 1] ||
                        buffer[i, j] == buffer[i, j + 1] ||
                        buffer[i, j] == buffer[i + 1, j] ||
                        buffer[i, j] == buffer[i - 1, j])
                    {
                        return false;
                    }
                }
            }
            //check center
            /*for(int i = 1; i < boardSize - 1; i++)
            {
                for (int j = 1; j < boardSize - 1; j++)
                {
                    if (board[i, j] == board[i, j - 1]) //can up
                    {
                        return false;
                    }
                    if (board[i, j] == board[i, j + 1]) //can down
                    {
                        return false;
                    }
                    if (board[i, j] == board[i + 1, j]) //can right
                    {
                        return false;
                    }
                    if (board[i, j] == board[i - 1, j]) //can left siht
                    {
                        return false;
                    }
                }
            }
            //check 4 corner
            for(int i = 0; i < boardSize - 1; i++)
            {
                if (board[i, 0] == board[i + 1, 0])
                {
                    return false;
                }
            }
            for (int i = 0; i < boardSize - 1; i++)
            {
                if (board[i, boardSize - 1] == board[i + 1, boardSize - 1])
                {
                    return false;
                }
            }
            for (int j = 0; j < boardSize - 1; j++)
            {
                if (board[0, j] == board[0, j + 1])
                {
                    return false;
                }
            }
            for (int j = 0; j < boardSize - 1; j++)
            {
                if (board[boardSize - 1, j] == board[boardSize - 1, j + 1])
                {
                    return false;
                }
            }*/
            return true;
        }

        public TwoZeroFourEightModel(int size)
        {
            boardSize = size;
            board = new int[boardSize, boardSize];
            var range = Enumerable.Range(0, boardSize);
            foreach(int i in range) {
                foreach(int j in range) {
                    board[i,j] = 0;
                }
            }
            rand = new Random();
            board = Random(board);
            NotifyAll();
        }

        private int[,] Random(int[,] input)
        {
            while (true)
            {
                int x = rand.Next(boardSize);
                int y = rand.Next(boardSize);
                if (board[x, y] == 0)
                {
                    board[x, y] = 2;
                    break;
                }
            }
            return input;
        }

        public void PerformDown()
        {
            int[] buffer;
            int pos;
            int[] rangeX = Enumerable.Range(0, boardSize).ToArray();
            int[] rangeY = Enumerable.Range(0, boardSize).ToArray();
            Array.Reverse(rangeY);
            foreach (int i in rangeX)
            {
                pos = 0;
                buffer = new int[4];
                foreach (int k in rangeX)
                {
                    buffer[k] = 0;
                }
                //shift down
                foreach (int j in rangeY)
                {
                    if (board[j, i] != 0)
                    {
                        buffer[pos] = board[j, i];
                        pos++;
                    }
                }
                //check can down shift
                for (int j = rangeY.Length - 1; j != 0; j--)
                {
                    if (board[j, i] == 0 && board[j - 1, i] != 0)
                    {
                        isMove = true;
                    }
                }
                // check duplicate
                foreach (int j in rangeX)
                {
                    if (j > 0 && buffer[j] != 0 && buffer[j] == buffer[j - 1])
                    {
                        buffer[j - 1] *= 2;
                        buffer[j] = 0;
                        //increase score
                        score += buffer[j - 1];
                        isMove = true;
                    }
                }
                // shift down again
                pos = 3;
                foreach (int j in rangeX)
                {
                    if (buffer[j] != 0)
                    {
                        board[pos, i] = buffer[j];
                        pos--;
                    }
                }
                // copy back
                for (int k = pos; k != -1; k--)
                {
                    board[k, i] = 0;
                }
            }
            if (isMove) board = Random(board);
            isMove = false;
            NotifyAll();
        }

        public void PerformUp()
        {
            int[] buffer;
            int pos;

            int[] range = Enumerable.Range(0, boardSize).ToArray();
            foreach (int i in range)
            {
                pos = 0;
                buffer = new int[4];
                foreach (int k in range)
                {
                    buffer[k] = 0;
                }
                //shift left
                foreach (int j in range)
                {
                    if (board[j, i] != 0)
                    {
                        buffer[pos] = board[j, i];
                        pos++;
                    }
                }
                //check can up shift
                for (int j = 0; j < range.Length - 1; j++)
                {
                    if (board[j, i] == 0 && board[j + 1, i] != 0)
                    {
                        isMove = true;
                    }
                }
                // check duplicate
                foreach (int j in range)
                {
                    if (j > 0 && buffer[j] != 0 && buffer[j] == buffer[j - 1])
                    {
                        buffer[j - 1] *= 2;
                        buffer[j] = 0;
                        //increase score
                        score += buffer[j - 1];
                        isMove = true;
                    }
                }
                // shift left again
                pos = 0;
                foreach (int j in range)
                {
                    if (buffer[j] != 0)
                    {
                        board[pos, i] = buffer[j];
                        pos++;
                    }
                }
                // copy back
                for (int k = pos; k != boardSize; k++)
                {
                    board[k, i] = 0;
                }
            }
            if (isMove) board = Random(board);
            isMove = false;
            NotifyAll();
        }

        public void PerformRight()
        {
            int[] buffer;
            int pos;

            int[] rangeX = Enumerable.Range(0, boardSize).ToArray();
            int[] rangeY = Enumerable.Range(0, boardSize).ToArray();
            Array.Reverse(rangeX);
            foreach (int i in rangeY)
            {
                pos = 0;
                buffer = new int[4];
                foreach (int k in rangeY)
                {
                    buffer[k] = 0;
                }
                //shift left
                foreach (int j in rangeX)
                {
                    if (board[i, j] != 0)
                    {
                        buffer[pos] = board[i, j];
                        pos++;
                    }
                }
                //check can right shift
                for (int j = rangeX.Length-1; j != 0; j--)
                {
                    if (board[i, j] == 0 && board[i, j - 1] != 0)
                    {
                        isMove = true;
                    }
                }
                // check duplicate
                foreach (int j in rangeY)
                {
                    if (j > 0 && buffer[j] != 0 && buffer[j] == buffer[j - 1])
                    {
                        buffer[j - 1] *= 2;
                        buffer[j] = 0;
                        //increase score
                        score += buffer[j - 1];
                        isMove = true;
                    }
                }
                // shift left again
                pos = 3;
                foreach (int j in rangeY)
                {
                    if (buffer[j] != 0)
                    {
                        board[i, pos] = buffer[j];
                        pos--;
                    }
                }
                // copy back
                for (int k = pos; k != -1; k--)
                {
                    board[i, k] = 0;
                }
            }
            if (isMove) board = Random(board);
            isMove = false;
            NotifyAll();
        }

        public void PerformLeft()
        {
            int[] buffer;
            int pos;
            int[] range = Enumerable.Range(0, boardSize).ToArray();
            foreach (int i in range)
            {
                pos = 0;
                buffer = new int[boardSize];
                foreach (int k in range)
                {
                    buffer[k] = 0;
                }
                //shift left
                foreach (int j in range)
                {
                    if (board[i, j] != 0)
                    {
                        buffer[pos] = board[i, j];
                        pos++;
                    }
                }
                //check can left shift
                for (int j = 0; j < range.Length-1; j++)
                {
                    if (board[i, j] == 0 && board[i, j + 1] != 0)
                    {
                        isMove = true;
                    }
                }
                // check duplicate
                foreach (int j in range)
                {
                    if (j > 0 && buffer[j] != 0 && buffer[j] == buffer[j - 1])
                    {
                        buffer[j - 1] *= 2;
                        buffer[j] = 0;
                        //increase score
                        score += buffer[j - 1];
                        isMove = true;
                    }
                }
                // shift left again
                pos = 0;
                foreach (int j in range)
                {
                    if (buffer[j] != 0)
                    {
                        board[i, pos] = buffer[j];
                        pos++;
                    }
                }
                for (int k = pos; k != boardSize; k++)
                {
                    board[i, k] = 0;
                }
            }
            if(isMove) board = Random(board);
            isMove = false;
            NotifyAll();
        }
    }
}
