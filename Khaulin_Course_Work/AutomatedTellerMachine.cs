using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Khaulin_Course_Work
{
    class AutomatedTellerMachine
    {
        public enum menuStates { ENTER_PASSW, START_MENU, SECOND_MENU, THIRD_MENU, ENTER_CARD_NUM, ENTER_VALUE, ERROR_PASW, GET_MONEY, UNABLE };
        public enum enteredValueType { PASSW, CARD_NUMBER, MONEY };
        public enum menuType { ENTERING, SELECTING };
        public bool checkInfo;

        public AutomatedTellerMachine(Form1 form)
        {
            this.form = form;
            currentMenuType = (int)menuType.ENTERING;
            currentCard = null;
            operationList = new ArrayList();
            cards = new ArrayList();
            menu = new ArrayList();
            destinationOfPaying = "";
            checkInfo = false;

            createCards();
            createMenu();
        }

        public ArrayList getCardsNumbers()//отримати доступни номера карток
        {
            ArrayList cardsNumbers = new ArrayList();//строковий масив, який буде заповнено номерами карток

            for (int i = 0; i < cards.Count; i++)
            {
                cardsNumbers.Add(((Card)cards[i]).getNumber());//з двомірного масиву отримати номера доступних карток та додати в строковий масив ArrayList
            }

            return cardsNumbers;
        }

        public string execOperation(string code)
        {
            string nextMenuItem = "";

            switch (valueType)
            {
                case (int)enteredValueType.PASSW:
                    {
                        if (checkPin(code))
                        {
                            nextMenuItem = getStartMenuText();
                        }
                        else
                        {
                            nextMenuItem = MenuTexts.wrongPasswordMessage;

                            form.takeCard();
                        }
                    } break;
                case (int)enteredValueType.MONEY:
                    {
                        if (currentCard.getMoney(code))
                        {
                            nextMenuItem = "Операція виконана успішно";
                        }
                        else
                        {
                            nextMenuItem = "Помилка при переказі коштів";
                        }
                        form.takeCheck();
                        form.takeMoney();
                    } break;
                case (int)enteredValueType.CARD_NUMBER:
                    {
                        destinationOfPaying += "Переказ на карту:" + code;

                        nextMenuItem = formatMenu(MenuTexts.sentMoneyMenuSecond);
                    } break;
            }

            return nextMenuItem;
        }

        public string toNextMenu(int indexOfFunctionalButton)
        {
            currentMenuType = (int)menuType.SELECTING;

            string nextFormatMenu = "";

            string[] currentMenu = (string[])operationList[operationList.Count - 1];

            string[] nextMenu = findMenuItemInArray(currentMenu[indexOfFunctionalButton]);

            addComandToCommandList(nextMenu);

            if (nextMenu == null)
            {
                nextFormatMenu = "Помилка при виконанні операції";
                form.takeCard();
            }
            else if (nextMenu[1].Contains("Введіть"))
            {
                currentMenuType = (int)menuType.ENTERING;
                if (nextMenu[1].Contains("суму"))
                {
                    valueType = (int)enteredValueType.MONEY;
                }
                else if (nextMenu[1].Contains("номер"))
                {
                    valueType = (int)enteredValueType.CARD_NUMBER;
                }
            }

            nextFormatMenu = formatMenu(nextMenu);

            return nextFormatMenu;
        }

        string[] findMenuItemInArray(string header)
        {
            if(header == "20" || header == "50" || header == "100" || header == "200" || header == "500")
            {
                if (currentCard.getMoney(header))
                {
                    form.takeCheck();
                    form.takeMoney();

                    return new string[]{"Зняття коштів","",""};
                }
            }
            else if (MenuTexts.payModilePhoneMenu.Contains<String>(header) || MenuTexts.payServiceMenu.Contains<String>(header))
            {
                return MenuTexts.sentMoneyMenuSecond;
            }
            foreach (string[] item in menu)
            {
                if (item[0].Contains(header))
                {
                    if(!containMenuLevel(item))
                    {
                        return item;
                    }
                }
            }

            return null;
        }

        bool containMenuLevel(string[] nextItem)
        {
            bool result = true;

            for (int k = 0; k < operationList.Count && result; k++)
            {
                string[] item = (string[])operationList[k];
                bool isEqual = true;

                for (int i = 0; i < item.Length && isEqual; i++)
                {
                    if (item[i] != nextItem[i])
                    {
                        isEqual = false;
                    }
                }
                result = isEqual;
            }

            return result;
        }

        

        public void createMenu()
        {
            menu.Add(MenuTexts.startMenu);
            menu.Add(MenuTexts.getMoneyMenu);
            menu.Add(MenuTexts.getMoneyMenuSecond);
            menu.Add(MenuTexts.payModilePhoneMenu);
            menu.Add(MenuTexts.payServiceMenu);
            menu.Add(MenuTexts.payUtilitiesMenu);
            menu.Add(MenuTexts.seeBalanceMenu);
            menu.Add(MenuTexts.sentMoneyMenu);
            menu.Add(MenuTexts.sentMoneyMenuSecond);
        }

        public string getCheckInfo()
        {
            return currentCard.getCheckInfo() + destinationOfPaying;
        }

        public void findCurrentCard(string number)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                Card nextCard = (Card)cards[i];
                if (nextCard.getNumber() == number)
                {
                    currentCard = nextCard;
                }
            }
        }

        public bool checkPin(string code)//перевірити введений пін-код
        {
            return currentCard.checkCode(code);
        }

        private string getStartMenuText()
        {
            operationList.Add((string[])menu[0]);

            return formatMenu((string[])menu[0]);
        }

        private string formatMenu(string[] currentMenu)
        {
            string menuText = "";

            menuText += currentMenu[0] + "\n\n\n\n";

            int len = (currentMenu.Length - 1) / 2;
            for (int i = 1; i < len + 1; i++)
            {
                menuText += String.Format("{0,-22} {1,22}\n\n\n", currentMenu[i], currentMenu[len + i]);
            }

            return menuText;
        }

        public int menuPosition
        {
            get
            {
                return currentMenuType;
            }
            set
            {
                currentMenuType = value;
            }
        }

        public int evType
        {
            get
            {
                return valueType;
            }
            set
            {
                valueType = value;
            }
        }

        public void addComandToCommandList(string[] command)
        {
            operationList.Add(command);
        }

        /*private void createCards()
        {
            Random rand = new Random();

            for (int i = 0; i < cardsCount; i++)
            {
                string cardNumber = "";
                for (int j = 0; j < 4; j++)
                {
                    cardNumber += rand.Next(9999).ToString("D4");
                }
                
                string cardCode = rand.Next(9999).ToString("D4");

                cards.Add(new Card(cardNumber,cardCode));
            }
        }*/

        private void createCards()
        {
            cards.Add(new Card("9087659484308281", "1234"));
            cards.Add(new Card("3891464658322045", "9909"));
            cards.Add(new Card("9757647292927846", "4321"));
            cards.Add(new Card("0197456219101834", "6667"));
            cards.Add(new Card("9018474774448399", "0909"));
        }

        public string getLastOperationValue()
        {
            return currentCard.getLastOperationValue();
        }

        public string returnCard()
        {
            string number = currentCard.getNumber();
            currentCard = null;
            currentMenuType = (int)menuStates.UNABLE;
            destinationOfPaying = "";

            operationList.Clear();

            return "Отримайте карту: " + number;
        }

        private int valueType;

        private Form1 form;
        private string destinationOfPaying;
        private int currentMenuType;
        private ArrayList operationList;
        private ArrayList menu;
        private ArrayList cards;
        private Card currentCard;
        private const int cardsCount = 20;
    }
}
