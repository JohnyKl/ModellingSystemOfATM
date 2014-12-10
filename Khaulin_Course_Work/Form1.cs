using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Khaulin_Course_Work
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            functionalButtons = new List<Button>();
            atm = new AutomatedTellerMachine(this);
            chdDialog = null;
            enteredCodeBuffer = "";
            shutdownSequence = 0;
            solveEnteringCode = false;
            solveUseMenuButton = false;
            richTextBoxText = "";
            savedText = "";

            addToArrayAllButtons();
        }

        private void addToArrayAllButtons()//функцція, що додає усі кнопки з форми до списку
        {
            foreach (var ctrl in pnlDisplay.Controls)//для кожного контрола (елемента) панелі дисплею банкомата на формі
            {
                if (ctrl is Button)//якщо це кнопка
                {
                    functionalButtons.Add((Button)ctrl);//додати у список кнопку
                }
            }
        }

        private void keyboard_Click(object sender, EventArgs e)
        {
            if (solveEnteringCode)
            {
                enteredCodeBuffer += ((Button)sender).Text;
                richTextBoxText += ((Button)sender).Text;
            }

            refreshRichTextBox();
        }

        private void btnInsertCard_Click(object sender, EventArgs e)
        {
            if (chdDialog == null)
            {
                chdDialog = new ChooseCardDialog(this, atm.getCardsNumbers());
                chdDialog.Show();
            }
        }

        public void askPasswMessage()
        {
            shutdownSequence++;

            solveEnteringCode = true;
            richTextBoxText = "\n\n\n" + MenuTexts.askPassword + "\n\t";
            atm.menuPosition = (int)AutomatedTellerMachine.menuStates.ENTER_PASSW;

            //atm.addComandToCommandList(MenuTexts.askPassword);

            savedText = richTextBoxText;

            restartSession();
            refreshRichTextBox();
        }

        private void btnGetCheck_Click(object sender, EventArgs e)
        {
            string message = atm.getCheckInfo();

            showMessage(message, "Отримання чеку");

            btnGetCheck.Enabled = false;

            takeCard();
        }

        private void btnGetMoney_Click(object sender, EventArgs e)
        {
            string message = atm.getLastOperationValue();

            showMessage(message, "Отримання купюр");

            btnGetMoney.Enabled = false;

            takeCard();
        }

        private void btnGetCard_Click(object sender, EventArgs e)
        {
            string message = atm.returnCard();

            solveEnteringCode = false;
            btnGetCard.Enabled = false;
            btnGetMoney.Enabled = false;
            btnGetCheck.Enabled = false;

            displayHelloMessage();
            showMessage(message, "Отримання карти");
        }

        private void functionalButtons_Click(object sender, EventArgs e)
        {
            if (solveUseMenuButton)
            {
                int index = functionalButtons.IndexOf((Button)sender);

                richTextBoxText = atm.toNextMenu(index + 1);

                if (atm.menuPosition == (int)AutomatedTellerMachine.menuType.ENTERING)
                {
                    solveEnteringCode = true;
                }
            }

            refreshRichTextBox();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            solveEnteringCode = false;
            solveUseMenuButton = true;

            richTextBoxText = atm.execOperation(enteredCodeBuffer);

            refreshRichTextBox();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            solveEnteringCode = false;
            solveUseMenuButton = false;

            enteredCodeBuffer = "";

            btnGetCard.PerformClick();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            enteredCodeBuffer = "";
            richTextBoxText = savedText;

            refreshRichTextBox();
        }

        private DialogResult showMessage(string message, string caption)
        {
            return MessageBox.Show(message, caption, MessageBoxButtons.OK);
        }

        private void restartSession()
        {
            switch (shutdownSequence)
            {
                case 0:
                    {
                        btnGetCard.Enabled = false;
                        btnGetCheck.Enabled = false;
                        btnGetMoney.Enabled = false;

                        //TODO: restart
                    } break;
                case 1:
                    {
                        btnGetCard.Enabled = true;
                        btnGetCheck.Enabled = false;
                        btnGetMoney.Enabled = false;
                        //TODO: wait for getting a card
                    } break;

                case 2:
                    {
                        btnGetCard.Enabled = true;
                        btnGetCheck.Enabled = false;
                        btnGetMoney.Enabled = true;
                        //TODO: wait for getting a card and money
                    } break;
                case 3:
                    {
                        btnGetCard.Enabled = true;
                        btnGetCheck.Enabled = true;
                        btnGetMoney.Enabled = true;
                        //TODO: wait for getting a card, a check and money
                    } break;
            }
        }

        public void selectCard(string cardNumber)
        {
            atm.findCurrentCard(cardNumber);

            chdDialog = null;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            displayHelloMessage();
        }

        private void displayHelloMessage()
        {
            richTextBoxText = "\n\n\n" + MenuTexts.welcomeMessage;

            refreshRichTextBox();
        }

        private void refreshRichTextBox()
        {
            richTextBox1.Text = richTextBoxText;
        }

        public void takeCheck()
        {
            btnGetCheck.Enabled = true;
        }

        public void takeMoney()
        {
            btnGetMoney.Enabled = true;
        }

        public void takeCard()
        {
            if (!btnGetMoney.Enabled && !btnGetCheck.Enabled)
            {
                btnGetCard.Enabled = true;

                richTextBoxText = "Отримайте карту:";
            }
            else
            {
                richTextBoxText = "Заберіть чек та гроші:";
            }

            solveUseMenuButton = false;
            solveEnteringCode = false;

            enteredCodeBuffer = "";
            savedText = "";

            refreshRichTextBox();
        }

        private ChooseCardDialog chdDialog;
        private AutomatedTellerMachine atm;
        private List<Button> functionalButtons;
        private bool solveEnteringCode;
        private bool solveUseMenuButton;
        private string enteredCodeBuffer;
        private string richTextBoxText;
        private string savedText;
        private int shutdownSequence;


    }
}
