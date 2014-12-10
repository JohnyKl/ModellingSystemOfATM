using System;
using System.Collections;
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
    public partial class ChooseCardDialog : Form
    {
        Form1 mainForm;

        public ChooseCardDialog()
        {
            InitializeComponent();
        }

        public ChooseCardDialog(Form1 form, ArrayList cards)
        {
            InitializeComponent();

            mainForm = form;

            setCardsList(cards);
        }

        private void setCardsList(ArrayList cards)
        {
            comboBox1.DataSource = cards;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string card = (string) comboBox1.SelectedItem;

            mainForm.selectCard(card);

            mainForm.askPasswMessage();

            this.Dispose();
        }        
    }
}
