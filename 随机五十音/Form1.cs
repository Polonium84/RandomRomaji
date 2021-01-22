using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 随机五十音
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            data = new string[,] {
                { "a","i","u","e","o"},
                {"ka","ki","ku","ke","ko" },
                {"sa","si","su","se","so" },
                {"ta","ti","tu","te","to" },
                {"na","ni","nu","ne","no" },
                {"ha","hi","hu","he","ho" },
                {"ma","mi","mu","me","mo" },
                {"ya","(yi)","yu","(ye)","yo" },
                {"ra","ri","ru","re","ro" },
                {"wa","(wi)","(wu)","(we)","wo" }};
            randoms = new int[50];
            for (int i = 0; i < 50; i++)
                randoms[i] = i;
        }
        string[,] data;
        int[] randoms;
        int[] line;
        int showcount;
        int maxcount;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)//全选和清空
        {
            int i = 0;
            if(cbSelectAll.Checked)
            {
                for (i = 0; i < checkedListBox1.Items.Count; i++)
                    checkedListBox1.SetItemChecked(i, true);
            }
            else
            {
                for (i = 0; i < checkedListBox1.Items.Count; i++)
                    checkedListBox1.SetItemChecked(i, false);
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            if (!(checkedListBox1.CheckedItems.Count > 0))
            {
                MessageBox.Show("请至少选择一行");
                return;
            }
            maxcount = checkedListBox1.CheckedItems.Count * 5;
            if (cbAll.Checked)
                showcount = maxcount;
            else
            {
                if (comboBox1.SelectedItem == null)
                {
                    MessageBox.Show("请选择随机数量");
                    return;
                }
                else
                    showcount = Convert.ToInt32(comboBox1.SelectedItem.ToString());
            }
            if (showcount > maxcount)
                showcount = maxcount;
            line = checkedListBox1.CheckedIndices.Cast<int>().ToArray();
            KeyValuePair<int, int>[] pairs = new KeyValuePair<int, int>[maxcount];
            int i = 0;
            while(true)
            {
                foreach (int j in line)
                {
                    for (int k = 0; k < 5; k++)
                    {
                        pairs[i] = new KeyValuePair<int, int>(j, k);
                        i++;
                        if (i >= maxcount)
                            goto Con;
                    }
                }
            }
            Con:
            Random random = new Random();
            for(i=0;i<maxcount;i++)
            {
                int location = random.Next(i, maxcount - 1);
                KeyValuePair<int, int> temp = pairs[i];
                pairs[i] = pairs[location];
                pairs[location] = temp;
            }
            StringBuilder sb = new StringBuilder(64);
            for(i=0;i<showcount;i++)
            {
                //char[] chars = new char[7];
                //string str = data[pairs[i].Key, pairs[i].Value];
                //str.CopyTo(0, chars, 0, str.Length);
                sb.Append(data[pairs[i].Key, pairs[i].Value]+"  ");
                if (i % 5 == 4)
                    sb.AppendLine();

            }
            textBox1.Text = sb.ToString();
        }
        
        private void cbAll_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAll.Checked)
                comboBox1.Enabled = false;
            else
                comboBox1.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }
    }
}
