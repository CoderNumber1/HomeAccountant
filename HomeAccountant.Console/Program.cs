using HomeAccountant.Core;
using HomeAccountant.Core.DataServices;
using HomeAccountant.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccountant
{
    class Program
    {
        static Account selectedAccount = null;

        static void Main(string[] args)
        {
            Bootstrapper.Init();
            DisplayMainMenu();
        }

        public static void DisplayMainMenu()
        {
            string inputString;

            while (true)
            {
                Console.WriteLine("1: Display main accounts");
                Console.WriteLine();
                inputString = Console.ReadLine();

                switch (inputString)
                {
                    case "1":
                        DisplayMainAccounts();
                        break;
                }
            }
        }

        public static void DisplayMainAccounts()
        {
            using (IHomeAccountantDataService dataService = DependencyContainer.Instance.Resolve<IHomeAccountantDataService>())
            {
                List<Account> mainAccounts = dataService.GetAccounts(permanentAccountsOnly: true).ToList();
                for(int i = 0; i < mainAccounts.Count(); i++)
                {
                    Console.WriteLine("{0}: {1}", i + 1, mainAccounts[i].Name);
                }

                Console.WriteLine();
                Console.Write("Select an account to manage: ");
                string input = Console.ReadLine();

                int accountOption;
                if(int.TryParse(input, out accountOption) && accountOption >= 1 && accountOption <= mainAccounts.Count())
                {
                    selectedAccount = mainAccounts[accountOption - 1];
                    DisplayAccountMenu();
                }
            }
        }

        public static void DisplayAccounts(int parentAccountId)
        {
            using (IHomeAccountantDataService dataService = DependencyContainer.Instance.Resolve<IHomeAccountantDataService>())
            {
                List<Account> mainAccounts = dataService.GetAccounts(parentAccount: parentAccountId, searchDate: DateTime.Now).ToList();
                if (mainAccounts.Count > 0)
                {
                    for (int i = 0; i < mainAccounts.Count(); i++)
                    {
                        Console.WriteLine("{0}: {1}", i + 1, mainAccounts[i].Name);
                    }

                    Console.WriteLine();
                    Console.Write("Select an account to manage: ");
                    string input = Console.ReadLine();

                    int accountOption;
                    if (int.TryParse(input, out accountOption) && accountOption >= 1 && accountOption <= mainAccounts.Count())
                    {
                        selectedAccount = mainAccounts[accountOption - 1];
                        DisplayAccountMenu();
                    }
                }
                else
                    DisplayAccountMenu();
            }
        }

        public static void DisplayAccountMenu()
        {
            Console.WriteLine("Account Management - {0}", selectedAccount.Name);
            Console.WriteLine("1: Create a Child Account.");
            Console.WriteLine("2: Manage Child Accounts.");
            Console.WriteLine("3: Close Account");
            Console.WriteLine("0: Back");
            Console.WriteLine();
            string input = Console.ReadLine();

            switch(input)
            {
                case "1":
                    CreateAccount();
                    DisplayAccountMenu();
                    break;
                case "2":
                    DisplayAccounts(selectedAccount.Id);
                    break;
                case "3":
                    CloseAccount();
                    return;
                case "0":
                    return;
            }
        }

        public static void CloseAccount()
        {
            using (IHomeAccountantDataService dataService = DependencyContainer.Instance.Resolve<IHomeAccountantDataService>())
            {
                dataService.CloseAccount(selectedAccount, DateTime.Now);
                dataService.Save();
            }
        }

        public static void CreateAccount()
        {
            Console.Write("What is the name of this account: ");
            string accountName = Console.ReadLine();
            Console.Write("When should the account be opened: ");
            string openDateString = Console.ReadLine();

            using(IHomeAccountantDataService dataService = DependencyContainer.Instance.Resolve<IHomeAccountantDataService>())
            {
                HomeAccountant.Core.Models.AccountType type = dataService.GetAccountType(selectedAccount);
                Account account = dataService.OpenAccount(accountName, type, DateTime.Parse(openDateString), selectedAccount);
                dataService.Save();

                selectedAccount = account;
            }
        }
    }
}
