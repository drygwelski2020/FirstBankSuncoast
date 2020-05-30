
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

    class Transaction
    {
        public int Amount { get; set; }
        public string Type { get; set; } // Deposit or Withdrawl
        public string Account { get; set; } // Checking or savings
    }
    class Program
    {
        // Create the menu to display to the user
        public static void Menu()
        {
            Console.WriteLine("");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("  Welcome to First Bank of Suncoast           ");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("Checking Account:                             ");
            Console.WriteLine("(D)eposit funds to checking account           ");
            Console.WriteLine("(W)ithdraw funds from checking account        ");
            Console.WriteLine("(d)eposit funds to savings account            ");
            Console.WriteLine("(w)ithdraw funds from savings account         ");
            Console.WriteLine("(S)how checking account balance               ");
            Console.WriteLine("(Q)uit the application                      ");
            Console.WriteLine("");
        }

        static int TransactionsTotal(List<Transaction> transactions, string account, string type)
        {
            var total = transactions.
                Where(transaction => transaction.Account == account && transaction.Type == type).
                Sum(transaction => transaction.Amount);
            return total;
        }
        private static int CheckingBalance(List<Transaction> transactions)
        {
            var checkingDepositSum = TransactionsTotal(transactions, "Checking", "Deposit");
            var checkingWithdrawSum = TransactionsTotal(transactions, "Checking", "Withdraw");
            var checkingBalance = checkingDepositSum - checkingWithdrawSum;
            return checkingBalance;
        }

        private static int SavingsBalance(List<Transaction> transactions)
        {
            var savingsDepositSum = TransactionsTotal(transactions, "Savings", "Deposit");
            var savingsWithdrawSum = TransactionsTotal(transactions, "Savings", "Withdraw");
            var savingsBalance = savingsDepositSum - savingsWithdrawSum;
            return savingsBalance;
        }
        // Start the program
        static void Main(string[] args)
        {
            var streamReader = new StreamReader("transactions.csv");
            var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
            var transactions = csvReader.GetRecords<Transaction>().ToList();
            streamReader.Close();

            // Set initial value for the 'choice' variable
            string choice = "";

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
                            int savingsBalance = SavingsBalance(transactions);
                            var checkingBalance = CheckingBalance(transactions);
                            Console.WriteLine($"You're checking balance is ${checkingBalance} and savings balance is ${savingsBalance}");
                            break;
                        }

                    // Deposit funds into checking acct and display the current balance
                    case "D":
                        {
                            Console.WriteLine("");

                            Console.Write("Enter deposit amount for checking: ");
                            var amount = Int32.Parse(Console.ReadLine());
                            if (amount <= 0)
                            {
                                Console.WriteLine("You must enter a number greater than zero.");
                                break;
                            }
                            else
                            {
                                var newTransaction = new Transaction { Type = "Deposit", Account = "Checking", Amount = amount };
                                transactions.Add(newTransaction);

                                Console.WriteLine("");
                                break;
                            }
                        }

                    // Withdraw funds from checking acct and display the current balance
                    case "W":
                        {
                            Console.WriteLine("");

                            Console.Write("Enter withdraw amount for checking: ");
                            var amount = Int32.Parse(Console.ReadLine());

                            //CheckingBalance(transactions);
                            if (amount > CheckingBalance(transactions))
                            {
                                Console.WriteLine("You cannot withdraw more than your current balance.");
                                break;
                            }
                            else if (amount <= 0)
                            {
                                Console.WriteLine("You must enter a number greater than zero.");
                                break;
                            }
                            else
                            {

                                var newTransaction = new Transaction { Type = "Withdraw", Account = "Checking", Amount = amount };
                                transactions.Add(newTransaction);

                                Console.WriteLine("");
                                break;
                            }
                        }

                    // Deposit funds into savings acct and display the current balance
                    case "d":
                        {
                            Console.WriteLine("");

                            Console.Write("Enter deposit amount for savings: ");
                            var amount = Int32.Parse(Console.ReadLine());
                            if (amount <= 0)
                            {
                                Console.WriteLine("You must enter a number greater than zero.");
                                break;
                            }
                            else
                            {
                                var newTransaction = new Transaction { Type = "Deposit", Account = "Savings", Amount = amount };
                                transactions.Add(newTransaction);

                                Console.WriteLine("");
                                break;
                            }
                        }

                    // Withdraw funds from savings acct and display the current balance
                    case "w":
                        {
                            Console.WriteLine("");

                            Console.Write("Enter withdraw amount for savings: ");
                            var amount = Int32.Parse(Console.ReadLine());
                            if (amount > SavingsBalance(transactions))
                            {
                                Console.WriteLine("You cannot withdraw more than your current balance.");
                                break;
                            }
                            else if (amount <= 0)
                            {
                                Console.WriteLine("You must enter a number greater than zero.");
                                break;
                            }
                            else
                            {
                                var newTransaction = new Transaction { Type = "Withdraw", Account = "Savings", Amount = amount };
                                transactions.Add(newTransaction);
                                Console.WriteLine("");
                                break;
                            }

                        }

                    // End the program
                    case "Q":
                        break;
                }
            }

            var streamWriter = new StreamWriter("transactions.csv");
            var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);
            csvWriter.WriteRecords(transactions);
            streamWriter.Close();

        }
    }
}
