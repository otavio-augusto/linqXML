using System;
using System.Collections;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace linqXML
{
    public partial class Form1 : Form
    {
        private int radio;

        public int Radio { get => radio; set => radio = value; }

        public Form1()
        {
            InitializeComponent();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            String querry = "";
            String field = "";
            switch (this.Radio)
            {
                case 0:
                    querry = textBox1.Text;
                    field = "id";
                    break;
                case 1:
                    querry = textBox2.Text;
                    field = "produto";
                    break;
                case 2:
                    querry = textBox3.Text;
                    field = "marca";
                    break;
                case 3:
                    querry = textBox4.Text;
                    field = "valor";
                    break;
                case 4:
                    querry = textBox5.Text;
                    field = "quantidade";
                    break;
            }

            string[] resultado = pesquisaXML("produto.xml", querry.ToString(), field).Split('\n');
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

        public String pesquisaXML(String fileName, String query, String field)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (XElement xelement in XElement.Load(fileName).Elements("produto"))
            {
                if (xelement.Attribute(field).Value == query)
                {
                    stringBuilder.AppendLine(xelement.Attribute("id").Value);
                    stringBuilder.AppendLine(xelement.Attribute("nome").Value);
                    stringBuilder.AppendLine(xelement.Attribute("marca").Value);
                    stringBuilder.AppendLine(xelement.Attribute("valor").Value);
                    stringBuilder.AppendLine(xelement.Attribute("quantidade").Value);
                    break;
                }
            }
            return stringBuilder.ToString();
        }

        public ArrayList lerXML(String fileName)
        {
            ArrayList resultado = new ArrayList();
            foreach (XElement xElement in XElement.Load(fileName).Elements("produto"))
            {
                resultado.Add(xElement.Attribute("nome").Value);
            }
            return resultado;
        }

        public void escreveXML(String fileName, String element, string[] nome, string[] valor)
        {
            object[] atributo = new object[nome.Length];
            for (int i = 0; i < nome.Length; i++)
            {
                atributo[i] = new XAttribute(nome[i], valor[i]);
            }
            XElement xelement = new XElement("produto", atributo);
            XDocument xDocument = XDocument.Load(fileName);
            xDocument.Root.Add(xelement);
            xDocument.Save(fileName);
            MessageBox.Show(xelement.ToString());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            updateList();
        }
        private void Button1_Click_1(object sender, EventArgs e)
        {
            string[] nome = new string[] { "id", "nome", "marca", "valor", "quantidade" };
            string[] valor = new string[] { textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text };
            escreveXML("produto.xml", "root", nome, valor);
            updateList();
        }

        private void Produtos_SelectedIndexChanged(object sender, EventArgs e)
        {
            String[] resultado = pesquisaXML("produto.xml", (String)Produtos.SelectedItem, "nome").Split('\n');
            textBox1.Text = resultado[0];
            textBox2.Text = resultado[1];
            textBox3.Text = resultado[2];
            textBox4.Text = resultado[3];
            textBox5.Text = resultado[4];
        }

        public void updateList()
        {
            Produtos.Items.Clear();
            foreach (String item in lerXML("produto.xml"))
            {
                Produtos.Items.Add(item);
            }
        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.Radio = 0;
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            this.Radio = 1;
        }

        private void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            this.Radio = 2;
        }

        private void RadioButton4_CheckedChanged(object sender, EventArgs e)
        {
            this.Radio = 3;
        }

        private void RadioButton5_CheckedChanged(object sender, EventArgs e)
        {
            this.Radio = 4;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }
    }
}
