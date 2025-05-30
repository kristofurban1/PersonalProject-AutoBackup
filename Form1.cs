using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AutoBackup
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //publikus változók
        public string FromPath;
        public string ToPath;
        public string OriginalName;
        public int Counting = 0;
        public int setTime;
        public int ticks = 0;
        public string Temp3;
        public string name = "";
        public bool error = false;

        //indításkor//
        private void Form1_Load(object sender, EventArgs e)
        {
            //nem elérhető interakciók kikapcsolása
            groupBox2.Enabled = false;
            groupBox1.Enabled = false;

            groupBox4.Enabled = false;
            groupBox4.Visible = false;

            textBox3.Enabled = false;
            radioButton3.Enabled = false;
            radioButton4.Enabled = false;

            button4.Enabled = false;
            button5.Enabled = false;

            button2.Enabled = false;

            checkBox1.Enabled = false;

            //alapértezett értékek
            radioButton1.Checked = true;
            radioButton3.Checked = true;
        }
        //--indításkor--//


        //file-direcroty//
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            button4.Enabled = true;
        }
        private void radioButton2_CheckedChanged_1(object sender, EventArgs e) { }
        //--file-directory--//

        //from, to//
        private void button4_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                string filePath = "";
                string fm;
                if (openFileDialog.ShowDialog() != DialogResult.OK)
                {
                    filePath = Microsoft.VisualBasic.Interaction.InputBox("Kérem adja meg manuálisan a Fálj vagy mappa elérését.", "Utvonál manuális megadása.");
                }

                if (filePath != "" || openFileDialog.FileName != "")
                {
                    if (radioButton1.Checked/*file*/ == true)
                    {
                        fm = "fájl";
                        if (filePath == "")
                        {
                            filePath = openFileDialog.FileName;
                        }

                        string[] Temp1 = filePath.Split('\\');

                        OriginalName = Temp1[Temp1.Length - 1];
                    }
                    else
                    {
                        
                        fm = "mappa";
                        if (filePath == "")
                        {
                            filePath = openFileDialog.FileName;
                        }
                        

                        string[] Temp1 = filePath.Split('\\');

                        if (Temp1[Temp1.Length - 1].Contains('.') == true)
                        {
                            filePath = "";
                            for (int i = 0; i < Temp1.Length - 2; i++)
                            {
                                filePath = filePath + Temp1[i] + @"\";
                            }
                            filePath = filePath + Temp1[Temp1.Length - 2];

                            OriginalName = Temp1[Temp1.Length - 2];
                        }
                        else
                        {
                            OriginalName = Temp1[Temp1.Length - 1];
                        }
                    }
                    DialogResult correctFile = MessageBox.Show($" \"{filePath}\" ez a helyes {fm}?", "", MessageBoxButtons.YesNo);
                    if (correctFile == DialogResult.Yes)
                    {
                        FromPath = filePath;
                        errorProvider1.Clear();
                        button5.Enabled = true;
                    }
                    else
                    {
                        FromPath = Microsoft.VisualBasic.Interaction.InputBox("Kérem adja meg manuálisan a Fálj vagy mappa elérését.", "Utvonál manuális megadása.");
                        button5.Enabled = true;
                        if (FromPath == "")
                        {
                            errorProvider1.SetError(button4, "Álítson be egy elérési útvonalat!");
                        }
                    }

                }
                else
                {
                    errorProvider1.SetError(button4, "Álítson be egy elérési útvonalat!");
                }


            }

        }

        private void button5_Click(object sender, EventArgs e)
        {

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                string filePath = "";
                string fm;
                if (openFileDialog.ShowDialog() != DialogResult.OK)
                {
                    filePath = Microsoft.VisualBasic.Interaction.InputBox("Kérem adja meg manuálisan a Fálj vagy mappa elérését.", "Utvonál manuális megadása.");
                }

                if (filePath != "" || openFileDialog.FileName != "")
                {
                    if (radioButton1.Checked/*file*/ == true)
                    {
                        fm = "fájl";
                        if (filePath == "")
                        {
                            filePath = openFileDialog.FileName;
                        }

                        string[] Temp1 = filePath.Split('\\');

                        if (Temp1[Temp1.Length-1].Contains('.') == true)
                        {
                            filePath = "";
                            for (int i = 0; i < Temp1.Length - 2; i++)
                            {
                                filePath = filePath + Temp1[i] + @"\";
                            }
                            filePath = filePath + Temp1[Temp1.Length - 2];
                        }
                        
                    }
                    else
                    {
                        fm = "mappa";
                        if (filePath == "")
                        {
                            filePath = openFileDialog.FileName;
                        }

                        string[] Temp1 = filePath.Split('\\');
                        if (Temp1[Temp1.Length - 1].Contains('.') != false)
                        {
                            filePath = "";
                            for (int i = 0; i < Temp1.Length - 2; i++)
                            {
                                filePath = filePath + Temp1[i] + @"\";
                            }
                            filePath = filePath + Temp1[Temp1.Length - 2];
                        }
                    }
                    DialogResult correctFile = MessageBox.Show($" \"{filePath}\" ez a helyes {fm}?", "", MessageBoxButtons.YesNo);
                    if (correctFile == DialogResult.Yes)
                    {
                        ToPath = filePath;
                        errorProvider1.Clear();
                        button2.Enabled = true;
                    }
                    else
                    {
                        filePath = Microsoft.VisualBasic.Interaction.InputBox("Kérem adja meg manuálisan a Fálj vagy mappa elérését.", "Utvonál manuális megadása.");
                    }

                }
                else
                {
                    errorProvider1.SetError(button5, "Álítson be egy elérési útvonalat!");
                }


            }
            if (FromPath != "")
            {

            }
        }
        //--from, to--//


        //lapozó ablak//
        private void button2_Click(object sender, EventArgs e)
        {
            //éppen látszódó eltüntetése
            groupBox3.Enabled = false;
            groupBox3.Visible = false;

            //Háttullévő megjelenítése
            groupBox4.Enabled = true;
            groupBox4.Visible = true;

            //Kötelező beálítások beálítva
            label7.Visible = false; label7.Enabled = false;
            label8.Visible = false; label8.Enabled = false;
            groupBox2.Enabled = true;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            //éppen látszódó eltüntetése
            groupBox4.Enabled = false;
            groupBox4.Visible = false;

            //Háttullévő megjelenítése
            groupBox3.Enabled = true;
            groupBox3.Visible = true;
        }
        //--lapozó ablak--//


         //Egyéb beálítások engedélyezése
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Enabled == true)
            {
                textBox3.Enabled = true;
                radioButton3.Enabled = true;
                radioButton4.Enabled = true;
            }
            else
            {
                textBox3.Enabled = false;
                radioButton3.Enabled = false;
                radioButton4.Enabled = false;

                textBox3.Text = "";
                radioButton3.Checked = true;
                radioButton4.Checked = false;
            }
        }



        //manuális mentés//
        private void button1_Click(object sender, EventArgs e)
        {
            bool error = false;
            if (radioButton3.Checked == true)
            {
                Counting++;

                
                string Cfiller = "";
                
                if(Counting < 10)
                {
                    Cfiller = "0";
                }
 

                Temp3 = Cfiller + Counting.ToString();
            }
            else
            {
                var time = DateTime.Now;
                string formattedTime = time.ToString("MM,dd,HH,mm,ss");
                
                string[] cuttedTime = formattedTime.Split(',');

                Temp3 = $"{cuttedTime[0]}-{cuttedTime[1]}--{cuttedTime[2]}-{cuttedTime[3]}-{cuttedTime[4]}";
            }

            
            name = textBox3.Text;
            if (name.Contains(" ") == true || name.Contains("\\") == true || name.Contains("/") == true || name.Contains(":") == true || name.Contains("*") == true || name.Contains("?") == true || name.Contains("\"") == true || name.Contains(@"<") == true || name.Contains(@">") == true || name.Contains(@"|") == true)
            {
                MessageBox.Show("A fáljnév nem tartalmazhatja a következő karakterek eggyikét sem: \" \\ / : * ? \" < > | \" ");
                error = true;
                errorProvider1.SetError(textBox3, "A fáljnév nem tartalmazhatja a következő karakterek eggyikét sem: \" \\ / : * ? \" < > | \" ");
            }

            //file
            if (radioButton1.Checked == true)
            {
                string[] cuttedON = OriginalName.Split('.');
                string format = "." + cuttedON[cuttedON.Length - 1];
                
                
                if (textBox3.Text == "")
                {
                    for (int i = 0; i < cuttedON.Length - 1; i++)
                    {
                        name = name + cuttedON[i];
                    }
                }

                if (error == false)
                {
                    errorProvider1.Clear();
                    if (Counting < 100)
                    {
                        File.Copy(FromPath, $@"{ToPath}\{name}_{Temp3}{format}");

                        //auto backup engedélyezés
                        label9.Visible = false;
                        groupBox1.Enabled = true;
                    }
                    else
                    {
                        button1.Enabled = false;
                        errorProvider1.SetError(button1, "A Számláló elérte a maximum értéket (99), ha tövábbra is a számlálót szeretné használni kattintson a \"Beálítások\" panelen a \"reset\" gombra, És törölje ki a eddig létregozott mentéseket vagy válasszon másik célt.");
                    }
                }
            }
            //mappa
            else
            {
                if (textBox3.Text != "")
                {
                    name = textBox3.Text;
                }
                else
                {
                    name = OriginalName;
                }

                DirectoryCopy(FromPath, $@"{ToPath}\{name}_{Temp3}", true);

                //auto backun engedélyezés
                label9.Visible = false;
                groupBox1.Enabled = true;
            }
        }
        //--manuális mentés--\\
        //autómatikus mentés\\

        

        private void trackBar1_Scroll(object sender, EventArgs e) 
        {
            if (checkBox3.Checked == true)
            {
                //debugmode
                errorProvider2.SetError(checkBox3, "A debug mode használatával az autómatikus mentés időtartalma lecsökkent 5 mp-re.");

                label3.Text = "00:05";
                setTime = 5;
            }
            else
            {
                switch (trackBar1.Value)
                {
                    case 0: label3.Text = "05:00"; setTime = 300; break;
                    case 1: label3.Text = "10:00"; setTime = 600; break;
                    case 2: label3.Text = "15:00"; setTime = 900; break;
                    case 3: label3.Text = "20:00"; setTime = 1200; break;
                    case 4: label3.Text = "25:00"; setTime = 1500; break;
                    case 5: label3.Text = "30:00"; setTime = 1800; break;
                    case 6: label3.Text = "35:00"; setTime = 2100; break;
                    case 7: label3.Text = "40:00"; setTime = 2400; break;
                    case 8: label3.Text = "45:00"; setTime = 2700; break;
                }
            }
            checkBox1.Enabled = true;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            //debugmode
            errorProvider2.SetError(checkBox3, "A debug mode használatával az autómatikus mentés időtartalma lecsökkent 5 mp-re.");

            label3.Text = "00:05";
            setTime = 5;
            checkBox1.Enabled = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {


            if (checkBox1.Checked == true)
            {
                name = textBox3.Text;
                if (name.Contains(" ") == true || name.Contains("\\") == true || name.Contains("/") == true || name.Contains(":") == true || name.Contains("*") == true || name.Contains("?") == true || name.Contains("\"") == true || name.Contains(@"<") == true || name.Contains(@">") == true || name.Contains(@"|") == true)
                {
                    MessageBox.Show("A fáljnév nem tartalmazhatja a következő karakterek eggyikét sem: \" \\ / : * ? \" < > | \" ");
                    error = true;
                    errorProvider1.SetError(textBox3, "A fáljnév nem tartalmazhatja a következő karakterek eggyikét sem: \" \\ / : * ? \" < > | \" ");
                }


                if (radioButton1.Checked == true)
                {
                    string[] cuttedON = OriginalName.Split('.');
                    string format = "." + cuttedON[cuttedON.Length - 1];
                }

                timer1.Start();

                groupBox2.Enabled = false;
                groupBox3.Enabled = false;
                groupBox4.Enabled = false;

                trackBar1.Enabled = false;
                checkBox3.Enabled = false;
            }
            else
            {
                timer1.Stop();
                ticks = 0;
                label3.Text = "00:00";

                groupBox2.Enabled = true;
                groupBox3.Enabled = true;
                groupBox4.Enabled = true;

                trackBar1.Enabled = true;
                checkBox3.Enabled = true;
            }
        }

        

        private void timer1_Tick(object sender, EventArgs e)
        {
            int tickLeft = setTime - ticks;

            int minLeft0 = tickLeft / 60;
            int secLeft0 = tickLeft % 60;
            string minLeft;
            string secLeft;

            if (minLeft0 < 10)
            {
                minLeft = "0" + minLeft0.ToString();
            }
            else 
            {
                minLeft = minLeft0.ToString();
            }

            if (secLeft0 < 10)
            {
                secLeft = "0" + secLeft0.ToString();
            }
            else
            {
                secLeft = secLeft0.ToString();
            }

            label3.Text = $"{minLeft}:{secLeft}";

            if (ticks == setTime)
            {
                if (radioButton3.Checked == true)
                {
                    Counting++;


                    string Cfiller = "";

                    if (Counting < 10)
                    {
                        Cfiller = "0";
                    }


                    Temp3 = Cfiller + Counting.ToString();
                }
                else
                {
                    var time = DateTime.Now;
                    string formattedTime = time.ToString("MM,dd,HH,mm,ss");

                    string[] cuttedTime = formattedTime.Split(',');

                    Temp3 = $"{cuttedTime[0]}-{cuttedTime[1]}--{cuttedTime[2]}-{cuttedTime[3]}-{cuttedTime[4]}";
                }

                //file 

                if (radioButton1.Checked == true)
                {
                    string[] cuttedON = OriginalName.Split('.');
                    string format = "." + cuttedON[cuttedON.Length - 1];

                    
                    if (textBox3.Text == "")
                    {
                        name = "";
                        for (int i = 0; i < cuttedON.Length - 1; i++)
                        {
                            name = name + cuttedON[i];
                        }
                    }

                    if (error == false)
                    {
                        errorProvider1.Clear();
                        if (Counting < 100)
                        {
                            File.Copy(FromPath, $@"{ToPath}\{name}_{Temp3}{format}");
                        }
                        else
                        {
                            button1.Enabled = false;
                            errorProvider1.SetError(button1, "A Számláló elérte a maximum értéket (99), ha tövábbra is a számlálót szeretné használni kattintson a \"Beálítások\" panelen a \"reset\" gombra, És törölje ki a eddig létregozott mentéseket vagy válasszon másik célt.");
                        }
                    }
                }
                else //mappa
                {
                    if (textBox3.Text != "")
                    {
                        name = textBox3.Text;
                    }
                    else
                    {
                        name = OriginalName;
                    }

                    DirectoryCopy(FromPath, $@"{ToPath}\{name}_{Temp3}", true);
                }
                ticks = 0;
            }
            ticks++;
        }

        //--autómatikus mentés--\\




        //funkció//


        //Mappa mentés//
        /// ez a kódrészlet nem az enyém. a mappa másolása funkció nincs benne a c#-ban, ezért kellet ezt használnom. 
        ///Igy használat és értelmezgetés után se biztos hogy utánozni tudnám.
        public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }


        //--funkció--//




        





        /*no*/private void groupBox4_Enter(object sender, EventArgs e) { }/*no*/
        /*no*/private void radioButton2_CheckedChanged(object sender, EventArgs e) { }/*no*/
        /*no*/private void radioButton3_CheckedChanged(object sender, EventArgs e) { }/*no*/
        /*no*/private void radioButton4_CheckedChanged(object sender, EventArgs e) { }/*no*/
        /*no*/private void textBox3_TextChanged(object sender, EventArgs e) { }/*no*/
        /*no*/private void trackBar1_ValueChanged(object sender, EventArgs e) { } /*no*/

        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("idő szűkében vagyok :/, most ez a leghatásosabb");
            Close();
        }
    }
}
