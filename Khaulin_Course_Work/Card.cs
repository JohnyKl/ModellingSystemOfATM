using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khaulin_Course_Work
{
    class Card
    {
        Random rand;
        string number;
        string code;

        float currentExpense;
        float creditExpense;

        bool locked;

        string lastOperationValue;

        public Card(string number, string code)
        {
            this.number = number;
            this.code = code;

            rand = new Random();

            locked = false;

            currentExpense = (float) rand.Next(1000) + 100;
            creditExpense = (float)rand.Next(3000) + 500;

            lastOperationValue = "0";
        }

        public string getNumber()
        {
            return number;
        }

        public bool isLocked()
        {
            return locked;
        }

        public string getLastOperationValue()
        {
            return lastOperationValue;
        }

        public string getCheckInfo()
        {
            string info =   "--------------------------------\nНомер банківського рахунку - {0}\n" +
                            "Дата проведення операції - {1}\n--------------------------------\n" +
                            "Баланс поточного рахунку - {2}\nБаланс кредитного рахунку - {3}\n";

            return String.Format(info,number,DateTime.Now.ToString(),currentExpense.ToString(),creditExpense.ToString());
        }

        public string getCurrentBalance()
        {
            return currentExpense.ToString();
        }

        public string getCreditBalance()
        {
            return creditExpense.ToString();
        }

        public bool getMoney(string value)
        {            
            bool operationResult = false;
            float amountOfMoney = (float)Convert.ToDouble(value); 
            lastOperationValue = amountOfMoney.ToString("C", System.Globalization.CultureInfo.CurrentCulture);

            if(currentExpense >= amountOfMoney)
            {
                currentExpense -= amountOfMoney;

                operationResult = true;
            }
            else if (creditExpense >= amountOfMoney)
            {
                creditExpense -= amountOfMoney;

                operationResult = true;
            }            

            return operationResult;
        }

        public bool checkCode(string code)
        {
            if(this.code == code)
            {
                return true;
            }
            return false;
        }

        public const int TYPE_CURRENT = 0;
        public const int TYPE_CREDIT = 1;
    }
}
