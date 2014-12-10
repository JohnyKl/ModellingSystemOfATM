using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khaulin_Course_Work
{
    class MenuTexts
    {
        public static string welcomeMessage = "Вітаємо у нашій мережі банкоматів.\n\nВставте картку, будь-ласка:";
        public static string askPassword = "Введіть пін-код:";
        public static string wrongPasswordMessage = "Не вірний пін-код. Картку заблоковано.";
        public static string[] startMenu = {"Початкове меню:",
                                            "Зняти готівку",
                                            "Перегляд балансу",
                                            "Переказ коштів",
                                            "Оплата послуг",
                                            "Мобільний зв'язок",
                                            "Оплата комунальних послуг" };
        public static string[] getMoneyMenu = { "Зняти готівку:",
                                                "20",
                                                "50",
                                                "100",
                                                "200",
                                                "500",
                                                "Інша сума" };
        public static string[] getMoneyMenuSecond = { "Зняти готівку:", enterAmountOfMoney, ""};
        public static string enterAmountOfMoney = "Введіть суму:";
        public static string[] seeBalanceMenu = {   "Перегляд балансу:",
                                                    "На екран",
                                                    "","",
                                                    "Чек", "", "" };
        public static string[] sentMoneyMenu = {   "Переказ коштів:",
                                                   "Введіть номер рахунку:", 
                                                   ""};
        public static string[] sentMoneyMenuSecond = {  "Переказ коштів:", enterAmountOfMoney, ""};
        public static string[] payServiceMenu = {   "Оплата послуг:",
                                                    "Інтернет",
                                                    "Телебачення",
                                                    "WebMoney",
                                                    "GooglePlay",
                                                    ""};
        public static string[] payModilePhoneMenu = {   "Мобільний зв'язок:",
                                                        "Life :)",
                                                        "Київстар",
                                                        "МТС",
                                                        "Білайн",
                                                        ""};
        public static string[] payUtilitiesMenu = { "Оплата комунальних послуг:",
                                                    "Введіть абонентський номер:",
                                                    ""};

    }
}
