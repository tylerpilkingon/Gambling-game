using System;

namespace Gamble_game {
    class Program {
        static void Main(string[] args) {
        #region Variables
            int userInput =0;
            int number = 0;
            Random rnd_maker = new Random();
            int guess = 0;
            double userWager = 0;
            int userGuess = 0;
            bool win = false;
            double userCash = 0;
            double userWin = 0;
            double gamesPlayed = 0;
            double winPercentage = 0.0;
            int round = 0;
            #endregion

            //Get users total amount he/she will be bringing
            Console.WriteLine("Welcome to Lucky 7!");
            Console.WriteLine("The goal of the game is to reach $10000 in 7 rounds");
            Console.WriteLine("Each round you can have a total of 8 guesses");
            Console.WriteLine("You can start with any amount of cash from $1 to $500");
            Console.WriteLine("The less money you start with the harder the challenge is so good luck");
            Console.WriteLine("--------------------------------------------------------");

            do {
                userCash =PromptDouble("Please enter the amount of cash you will be bringing to the table: $");
                if (userCash > 500) Console.WriteLine("(You can not have over $500 to start off with)");
                if (userCash < 1) Console.WriteLine("(You can not have under $1 to start off with)");
            }while (userCash > 500 || userCash < 1);    
            Console.WriteLine("--------------------------------------------------------");
            do{               
                number = rnd_maker.Next(1,101);
                round++;

                Console.WriteLine("ROUND {0}", round);
                //get users wager
                do{
                    userWager = PromptDouble("Please enter how much money you want to wager: $");
                    if (userWager > userCash) Console.WriteLine("(You have wagered more money than what you currently have)");
                    if (userWager <= 0) Console.WriteLine("(You need to enter a wager that is more than zero)");   
                } while (userWager > userCash || userWager <= 0);//end loop
                
                //get users amount of guesses
                do {
                    userGuess = PromptInt("Please enter how many guesses you would like to use: ");
                    if (userGuess > 8) Console.WriteLine("(You can not have more than 8 guesses)");
                    if (userGuess <= 0) Console.WriteLine("(Please enter a number between 1 and 8)");
                } while (userGuess > 8 || userGuess <= 0);
                
                //calculate usercash and guess
                userCash = userCash - userWager;
                guess = userGuess;
                win = false;
            
                //start outguess game
                while (win != true && guess > 0) {
                    
                    //validation loop
                    do {
                        userInput = PromptInt("\nEnter a number between 1-100: ");
                    } while (userInput < -1 || userInput > 101);//end loop   
                
                    //calculate if he/she won
                    if (userInput == number){    
                        win = true;
                        }else if(userInput < number){
                        guess = guess - 1;
                        win = false;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        if (guess > 1 || guess == 0) Console.WriteLine("Sorry too Low! you have {0} guesses remaining",guess);
                        else Console.WriteLine("Sorry too Low! you have {0} guess remaining",guess);
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }else if(userInput > number){
                        guess = guess - 1;
                        win = false;
                        Console.ForegroundColor = ConsoleColor.Red;
                        if (guess > 1 || guess == 0) Console.WriteLine("Sorry too High! you have {0} guesses remaining",guess);
                        else Console.WriteLine("Sorry too High! you have {0} guess remaining",guess);       
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }//end if
                }//end loop
            
            
                //OUTPUT For game
                Console.WriteLine("--------------------------------------------------------");
                if (userInput == number) {
                    userWager = CheckWinnings(userGuess,userWager);
                    userCash = userWager + userCash;
                    userWin = userWin + 1;
                    gamesPlayed = gamesPlayed + 1;
                    Console.WriteLine("Congrats!! you guessed the right number and won {0:C}", userWager);
                    
                }else{
                    gamesPlayed = gamesPlayed + 1;
                    Console.WriteLine("You Lose! the correct number was {0}",number);
                }//end if
                
                //see if he or shor has enough money and ask if he/she wants to play again
                if (userCash > 0.000000 && userCash < 10000 && round < 7) {
                    Console.WriteLine("Your balance is {0:C}",userCash);
                    Console.WriteLine("--------------------------------------------------------");
                } else if (userCash <= 0.00) {
                    Console.WriteLine("Sorry you have run out of money");
                    Console.WriteLine("--------------------------------------------------------");
                    
                } else if (userCash >= 10000) {
                    Console.ForegroundColor = ConsoleColor.Green;
                    if (userCash > 10000) Console.WriteLine("YOU HAVE MADE IT PAST $10,000!!!!! GREAT JOB");
                    if (userCash == 10000) Console.WriteLine("YOU HAVE MADE IT TO EXACTLY $10,000!!!!! GREAT JOB");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("--------------------------------------------------------");
                } else if (round == 7) {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("YOU LOSE");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("--------------------------------------------------------");
                }
            } while (userCash > 0 && userCash < 10000 && round < 7 );
            
            winPercentage = (userWin / gamesPlayed) * 100;
            Console.WriteLine("Your win percentage was {0:F2}%",winPercentage);
        }// end main

        #region Functions
        static double CheckWinnings(int userguess, double wager) {
            return wager*(9 - userguess);
        }
        static int PromptInt(string text) {
	        Console.Write(text);
	        return int.Parse(Console.ReadLine());
        }
        static double PromptDouble(string text) {
	        Console.Write(text);
	        return double.Parse(Console.ReadLine());
        }
        #endregion
    }
}
