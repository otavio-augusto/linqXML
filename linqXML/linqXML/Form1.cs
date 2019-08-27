using System;
using System.Text;
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
            foreach (XElement xelement in XElement.Load(fileName).Elements("produto"))
            {
                if (xelement.Attribute("nome").Value == query)
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

        }
        private void Button1_Click_1(object sender, EventArgs e)
        {
            string[] nome = new string[] { "id", "nome", "valor", "quantidade" };
            string[] valor = new string[] { textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text };
            escreveXML("produto.xml", "root", nome, valor);
        }
    }
}
