using System;

class Program
{
    static void Main(string[] args)
    {
        Random randomGenerator = new Random();
        int magicNumber = randomGenerator.Next(1, 101);

        int guess = -1;
        int guessCount = 0;

        while (guess != magicNumber)
        {
            Console.Write("What is your guess? ");
            string guessInput = Console.ReadLine();
            guess = int.Parse(guessInput);
            guessCount++;

            if (guess < magicNumber)
            {
                Console.WriteLine("Higher");
            }
            else if (guess > magicNumber)
            {
                Console.WriteLine("Lower");
            }
            else
            {
                Console.WriteLine("You guessed it!");
                Console.WriteLine($"It took you {guessCount} guesses!");
            }
        }
    }
}
