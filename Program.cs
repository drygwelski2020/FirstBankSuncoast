
// P - Problem is to make an application to track a savings and checking account.
//
//Create an application that allows us to:
//
// - Load past transactions from a file (.csv in my case)
// - Display savings and checking acct balances upon start of program
// - Do not allow withdraw or deposit for more than allowed. No negative account balances.
// 	- Check each deposit or withdrawl amount to be sure it's positive
// - Ability to deposit money to savings or checking account
// - Ability to withdraw money from savings or checking account
// - List account balances
// - Quit the application
//
// - After each transaction, it should be written to the .csv file
//
// D - Data
//
// List of accounts
// Savings
// Checking
// .csv file to store and retrieve data
// Account class
// - Type - savings or checking - string
// - Balance - double
// - makeDeposit - method to add money to Balance - double
// - makeWithdrawl - method to subtract money from Balance - double
// - displayBal - method to display account balance - double
// - writeToFile - method to write each deposit/withdrawl to .csv file
// - readFromFile - method to read the .csv file
//
// E - Examples
//
// |    Type    |  Balance  | makeDeposit | makeWithdrawl | displayBal |
// | ---------- |  -------- | ----------- | ------------- | ---------- |
// | Savings    | $200.00   |	  x	  |	 x	  |	x      | 
// | Checking   | $100.00   | 	  x	  |	 x	  |	x      | 
//
// User can display balance of checking account
// User can display balance of savings account
// User can make a withdrawl from checking account
// User can make a deposit to checking account
// User can make a withdrawl from savings account
// User can make a deposit to savings account
// User can quit the application
//
// A - Algorithm
//
// Welcome the user to the application
//
// While the user hasn't quit the application:
//
// Display all current info from the .csv file

// Display a menu of options they can do
// 
// - Checking Account:
// - (S)how checking account balance
// - (D)eposit funds to checking account
// - (W)ithdraw funds from checking account
// - Saving Account:
// - (s)how savings account balance
// - (d)eposit funds to savings account
// - (w)ithdraw funds from savings account
// - (Q|q)uit the application
//
// Prompt for their choice
// Based their choice, do that function
//
// - If the user entered quit, then Quit the application
//  End the while loop

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using CsvHelper;
using System.Globalization;

namespace FirstBankSuncoast
{
    class Program
    {
        // Create the Account class
        class Account
        {
            public string Type { get; set; }
            public double Balance { get; set; }
        }

        // Create the menu to display to the user
        public static void Menu()
        {
            Console.WriteLine("");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("  Welcome to First Bank of Suncoast           ");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("Checking Account:                             ");
            Console.WriteLine("(S)how checking account balance               ");
            Console.WriteLine("(D)eposit funds to checking account           ");
            Console.WriteLine("(W)ithdraw funds from checking account        ");
            Console.WriteLine("");
            Console.WriteLine("Saving Account:                               ");
            Console.WriteLine("(s)how savings account balance                ");
            Console.WriteLine("(d)eposit funds to savings account            ");
            Console.WriteLine("(w)ithdraw funds from savings account         ");
            Console.WriteLine("(Q)uit the application                      ");
            Console.WriteLine("");

        }

        // Start the program
        static void Main(string[] args)
        {
            List<Account> account = new List<Account>();


            // Write all transactions to a .csv file
            void SaveAllAccounts()
            {
                StreamWriter writer = new StreamWriter("accounts.csv");
                CsvWriter csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);
                csvWriter.WriteRecords(account);
                writer.Close();
            }

            // Load file info from .csv file
            void LoadAllAccounts()
            {
                if (File.Exists("accounts.csv"))
                {
                    var reader = new StreamReader("accounts.csv");
                    var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
                    csvReader.Configuration.HasHeaderRecord = true;
                    account = csvReader.GetRecords<Account>().ToList();
                    reader.Close();
                }
                else
                {
                    // Assign a StringReader to read from an empty string
                    var reader = new StringReader("");
                }
            }

            //Create some Account objects
            var checking = new Account
            {
                Type = "Checking",
            };
            var savings = new Account
            {
                Type = "Savings",
            };

            account.Add(checking);
            account.Add(savings);

            Console.WriteLine("");
            Console.WriteLine($"account has a checking balance of {account[0].Balance} and savings balance of {account[1].Balance}");

            void MakeCheckingDeposit()
            {
                Console.Write("How much would you like to deposit?: ");
                double checkingDepAmt = Double.Parse(Console.ReadLine());
                if (checkingDepAmt < 0)
                {
                    Console.WriteLine("You cannot have a negative deposit amount.");
                }
                else
                {
                    checking.Balance += checkingDepAmt;
                    Console.WriteLine($"You're current checking account balance is {checking.Balance}");
                    SaveAllAccounts();
                    Console.WriteLine("");
                }
            }

            void MakeSavingsDeposit()
            {
                Console.Write("How much would you like to deposit?: ");
                double savingsDepAmt = Double.Parse(Console.ReadLine());
                if (savingsDepAmt < 0)
                {
                    Console.WriteLine("You cannot have a negative deposit amount.");
                }
                else
                {
                    savings.Balance += savingsDepAmt;
                    Console.WriteLine($"You're current savings account balance is {savings.Balance}");
                    SaveAllAccounts();
                    Console.WriteLine("");
                }
            }

            void MakeCheckingWithdrawl()
            {
                Console.Write("How much would you like to withdraw?: ");
                double checkingWithdrawAmt = Double.Parse(Console.ReadLine());
                if (checkingWithdrawAmt > checking.Balance)
                {
                    Console.WriteLine($"You're current balance is {checking.Balance}. You cannot have a negative balance.");
                }
                else
                {
                    checking.Balance -= checkingWithdrawAmt;
                    Console.WriteLine($"You're current checking account balance is {checking.Balance}");
                    //SaveAllAccounts();
                    Console.WriteLine("");
                }
            }

            void MakeSavingsWithdrawl()
            {
                Console.Write("How much would you like to withdraw?: ");
                double savingsWithdrawAmt = Double.Parse(Console.ReadLine());
                if (savingsWithdrawAmt > savings.Balance)
                {
                    Console.WriteLine($"You're current balance is {savings.Balance}. You cannot have a negative balance.");
                }
                else
                {
                    savings.Balance -= savingsWithdrawAmt;
                    Console.WriteLine($"You're current savings account balance is {savings.Balance}");
                    //SaveAllAccounts();
                    Console.WriteLine("");
                }
            }
            void DisplayCheckingBalance()
            {
                Console.WriteLine($"You're current checking account balance is {checking.Balance}");
                Console.WriteLine("");
            }

            void DisplaySavingsBalance()
            {
                Console.WriteLine($"You're current savings account balance is {savings.Balance}");
                Console.WriteLine("");
            }

            LoadAllAccounts();

            // Set initial value for the 'choice' variable
            string choice = "S";

            Menu();

            // Loop through the user's selections and perform actions based on those selections
            while (choice != "Q")
            {

                Console.Write("Please enter your choice: ");
                choice = Console.ReadLine();

                // Switch statement to determine action(s) to take based on user input
                switch (choice)
                {

                    // Display info regarding current checking acct balance
                    case "S":
                        {
                            Console.WriteLine("");

                            DisplayCheckingBalance();

                            Console.WriteLine("");
                            break;
                        }

                    // Deposit funds into checking acct and display the current balance
                    case "D":
                        {
                            Console.WriteLine("");
                            Console.WriteLine("You are depositing funds into your checking account:");

                            MakeCheckingDeposit();

                            Console.WriteLine("");
                            break;
                        }

                    // Withdraw funds from checking acct and display the current balance
                    case "W":
                        {
                            Console.WriteLine("");
                            Console.WriteLine("You are withdrawing funds from your checking account:");

                            MakeCheckingWithdrawl();


                            Console.WriteLine("");
                            break;
                        }

                    // Display info regarding current savings acct balance
                    case "s":
                        {
                            Console.WriteLine("");

                            DisplaySavingsBalance();

                            Console.WriteLine("");
                            break;
                        }

                    // Deposit funds into savings acct and display the current balance
                    case "d":
                        {
                            Console.WriteLine("");
                            Console.WriteLine("You are depositing funds into your savings account:");

                            MakeSavingsDeposit();

                            Console.WriteLine("");
                            break;
                        }

                    // Withdraw funds from savings acct and display the current balance
                    case "w":
                        {
                            Console.WriteLine("");
                            Console.WriteLine("You are withdrawing funds from your savings account:");

                            MakeSavingsWithdrawl();
                            Console.WriteLine("");
                            break;
                        }

                    // End the program

                    case "Q":
                        break;
                }

                SaveAllAccounts();

            }
        }
    }
}

