using System;

namespace Plocha
{
    class Program
    {
        static int precSelector = 0; // select 0 - 2
        static int[] precSelection = { 100, 1000, 10000 };
        static double[] divSizes = new double[precSelection[precSelector]];

        static void Main(string[] args)
        {
            Division div = new Division(precSelection[precSelector]);
            double result = 0;

            for (int i = 0; i < precSelection[precSelector]; i++)
            {
                divSizes[i] = div.CalculateDiv();
                result += divSizes[i];
            }

            MonteCarlo mc = new MonteCarlo();

            Console.WriteLine("Metoda obdelniku: " + result);
            Console.WriteLine("Metoda Monte Carlo: " + mc.MCResult());
        }
    }

    public class Division
    {
        const int lengthX = 2;
        static double divX, currX;

        public Division(double selSize)
        {
            divX = lengthX / selSize;
            currX = divX/2;
        }

        public double CalculateDiv()
        {
            double divHeight = Math.Pow(currX, 3);
            currX += divX;
            return  divHeight * divX;
        }

    }

    public class MonteCarlo
    {
        Random rng = new Random();

        const int yMAX = 10, xMAX = 2;
        int guessesToTake, guessField;
        Guess[] guessList;

        public MonteCarlo()
        {
            guessesToTake = 10000;
            guessField = yMAX * xMAX;
            guessList = new Guess[guessesToTake];
        }

        public double MCResult()
        {
            int goodGuess = 0, badGuess = 0;
            for (int i = 0; i < guessesToTake; i++)
            {
                guessList[i] = new Guess(rng.NextDouble() + rng.Next(0,xMAX), rng.NextDouble() + rng.Next(0,yMAX));
                if (guessList[i].posY <= Math.Pow(guessList[i].posX, 3)) guessList[i].inFunction = true;
                else guessList[i].inFunction = false;
            }

            foreach (var item in guessList)
            {
                if (item.inFunction) goodGuess++;
                else badGuess++;
            }
            double retVal = (Convert.ToDouble(goodGuess) / Convert.ToDouble(guessesToTake) * guessField);
            return retVal; 
        }
    }

    public class Guess
    {
        public double posX, posY;
        public bool inFunction;

        public Guess(double x, double y)
        {
            posX = x;
            posY = y;
        }
    }
}
