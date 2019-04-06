using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Демо
{
    public partial class Form1 : Form
    {
        DataClasses1DataContext context = new DataClasses1DataContext();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = from u in context.Пациент
                                       select u;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Пациент us = new Пациент()
            {
                Фамилия = textBox1.Text,
                Возраст = Convert.ToDecimal(textBox2.Text),
                
            };

            context.Пациент.InsertOnSubmit(us);
            
            try
            {
                context.SubmitChanges();
            }
            catch
            {
                context.SubmitChanges();
            }

            dataGridView1.DataSource = from u in context.Пациент
                                       select u;
            textBox1.Text = "";
            textBox2.Text = "";
        }

        
        private void button2_Click(object sender, EventArgs e)
        {
            int iid = 0;
            int rowindex = dataGridView1.CurrentRow.Index;
            iid = Convert.ToInt32(dataGridView1.Rows[rowindex].Cells[0].Value);
            var update = from s2 in context.Пациент
                         where s2.id_Пациента == iid
                         select s2;
            
            foreach (var us in update)
            {
                us.Фамилия = textBox1.Text;
                us.Возраст = Convert.ToDecimal(textBox2.Text);
            }
            
            try
            {
                context.SubmitChanges();
            }
            catch
            { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int iid = 0;
            int rowindex = dataGridView1.CurrentRow.Index;
            iid = Convert.ToInt32(dataGridView1.Rows[rowindex].Cells[0].Value);
            var delete = from s2 in context.Пациент
                         where s2.id_Пациента == iid
                         select s2;

            context.Пациент.DeleteAllOnSubmit(delete);
            context.SubmitChanges();
            rowindex = 0;

            dataGridView1.DataSource = from u in context.Пациент
                                       select u;
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[1].Value);
            textBox2.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[2].Value);
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text.Length == 1)
            {
                ((TextBox)sender).Text = ((TextBox)sender).Text.ToUpper();
                ((TextBox)sender).Select(((TextBox)sender).Text.Length, 0);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char l = e.KeyChar;
            if ((l < 'А' || l > 'я') && (l < 'A' || l > 'z') && l != '\b' && l != '-')
            {
                e.Handled = true;
            }
        }


        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }
    }
}

