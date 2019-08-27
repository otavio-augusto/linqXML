using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace linqXML
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            string[] resultado = pesquisaXML("produto.xml", textBox3.Text).Split('\n');
            Console.WriteLine(resultado[0]);
            if (resultado.Length > 1)
            {
                textBox1.Text = resultado[0]; //MARCA
                textBox2.Text = resultado[1]; //ID
                textBox3.Text = resultado[2]; //PRODUTO
                textBox4.Text = resultado[3]; //VALOR
                textBox5.Text = resultado[4]; //ESTOQUE
            }
        }

        public String pesquisaXML(String fileName, String query)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (XElement xelement in XElement.Load(fileName).Elements("marca"))
            {
                foreach (XElement xelement2 in xelement.Elements("produto"))
                {
                    if (xelement2.Attribute("nome").Value == query)
                    {
                        stringBuilder.AppendLine(xelement.Attribute("nome").Value);
                        stringBuilder.AppendLine(xelement2.Attribute("id").Value);
                        stringBuilder.AppendLine(xelement2.Attribute("nome").Value);
                        stringBuilder.AppendLine(xelement2.Attribute("valor").Value);
                        stringBuilder.AppendLine(xelement2.Attribute("quantidade").Value);
                        break;
                    }

                }
            }
            return stringBuilder.ToString();
        }

        private void Button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }
    }
}
