using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace WindowsApplication1
{
    public partial class userId : Form
    {
        public userId()
        {
            InitializeComponent();
            System.IO.DirectoryInfo DataDir = new System.IO.DirectoryInfo(Application.StartupPath + "\\idData");
            if (!DataDir.Exists)
            {
                DataDir.Create();
            }
        }

        bool newSaving;
        string filePassword = "";
        bool editing;
        string PPath = "";
        static string onlyaDot = ".";
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (userPass.Text == "")
            {
                MessageBox.Show("Enter Password of your user?", "User", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (userName.Text == "" | userPass.Text == "")
                {
                    MessageBox.Show("Select a User from list or add a new.", "User", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (!System.IO.File.Exists(Application.StartupPath + "\\idData\\" + userName.Text + onlyaDot + userPass.Text))
                    {
                        MessageBox.Show("Your Password is not currect. Enter Currect Password?", "User", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        this.Hide();
                        frmMain frmMain = new frmMain();
                        frmMain.UserFileName = userName.Text;
                        frmMain.UserFilePass = userPass.Text;
                        frmMain.Show();
                        frmMain.Activate();
                    }
                }
            }

        }
        public void NamesAndPass()
        {
            try
            {
                int fileCount = 0;
                int counter = 0;
                string name = "";
                bool Dot = false;
                string pass = "";
                /////////////////////////////////////////////////////////////////////
                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Application.StartupPath + "\\idData\\");
                foreach (System.IO.FileInfo file in dir.GetFiles("*.*"))
                {
                    fileCount += 1;
                }
                string[] filesList = new string[fileCount];
                string[] filesNames = new string[fileCount];
                string[] filesPass = new string[fileCount];
                fileCount = 0;
                /////////////////////////////////////////////////////////////////////
                foreach (System.IO.FileInfo file in dir.GetFiles("*.*"))
                {
                    if (file.Name == "")
                    { }
                    else
                    {
                        filesList[fileCount] = (file.Name);
                        fileCount += 1;
                    }
                }
                /////////////////////////////////////////////////////////////////////
                do
                {
                    foreach (char ch in filesList[counter])
                    {
                        if (ch != '.')
                        { name += ch; }
                        else
                        { ; break; }
                    }
                    filesNames[counter] = name;
                    name = "";
                    counter += 1;
                }
                while (counter < filesList.Length);
                fileListCombo.Items.Clear();
                fileListCombo.Items.AddRange(filesNames);
                fileListCombo.SelectedIndex = 0;
                //////////////////////////////////////////////////////////////////////
                counter = 0;
                do
                {
                    foreach (char ch in filesList[counter])
                    {
                        if (ch != '.')
                        {
                            if (Dot)
                                pass += ch;
                        }
                        else
                        { Dot = true; }
                    }
                    filesPass[counter] = pass;
                    pass = "";
                    counter += 1;
                }
                while (counter < filesList.Length);
                fileCount = 0;
            }
            catch
            {
            }
            ////////////////////////////////////////////////////////
        }
        private void userId_Load(object sender, EventArgs e)
        {
            try
            {
                NamesAndPass();
                fileListCombo.SelectedIndex = 0;
            }
            catch
            {

            }
            userName.Enabled = false;
            newSaving = true;
            editing = false;

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (newSaving)
            {
                newSaving = false;
                userName.Enabled = true;
                userPass.Enabled = true;
                userName.Text = "";
                userPass.Text = "";
                btnNew.Text = "&Save";
                btnDelete.Enabled = false;
                btnEdit.Enabled = false;
            }
            else
            {
                if (userName.Text == "" | userPass.Text == "")
                {                ///////////////////////
                    MessageBox.Show("Enter name and password.", "User", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    newSaving = true;
                    userName.Enabled = false;

                    btnNew.Text = "&New";
                    btnDelete.Enabled = true;
                    btnEdit.Enabled = true;
                    ////////////////////////


                    string filefullName = userName.Text + onlyaDot + userPass.Text;

                    string filePath = Application.StartupPath + "\\idData\\" + filefullName;
                    if (!System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Create(filePath);
                    }
                    else
                    {
                        if (MessageBox.Show("File exists do you want to overrite the file?", "File Exists", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            System.IO.File.Create(filePath);
                        }
                    }
                    userPass.Text = "";
                    NamesAndPass();
                }

            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (newSaving == false)
            {
                newSaving = true;
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                btnNew.Text = "&New";
                userName.Enabled = false;
                userPass.Text = "";
                NamesAndPass();
            }
            if (editing)
            {
                editing = false;
                btnEdit.Text = "&Edit";
                btnDelete.Enabled = true;
                btnNew.Enabled = true;
                userName.Enabled = false;
                userPass.Text = "";
                NamesAndPass();
            }
        }

        private void fileListCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int fileCount1 = 0;
            int counter1 = 0;
            bool Dot1 = false;
            string pass1 = "";
            /////////////////////////////////////////////////////////////////////
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Application.StartupPath + "\\idData\\");
            foreach (System.IO.FileInfo file1 in dir.GetFiles("*.*"))
            {
                fileCount1 += 1;
            }
            try
            {
                string[] filesList1 = new string[fileCount1];
                string[] filesPass1 = new string[fileCount1];
                fileCount1 = 0;
                /////////////////////////////////////////////////////////////////////
                foreach (System.IO.FileInfo file in dir.GetFiles("*.*"))
                {
                    if (file.Name == "")
                    { }
                    else
                    {
                        filesList1[fileCount1] = (file.Name);
                        fileCount1 += 1;
                    }
                }
                ///////////////////////////////////////////
                counter1 = 0;
                do
                {
                    foreach (char ch1 in filesList1[counter1])
                    {
                        if (ch1 != '.')
                        {
                            if (Dot1)
                            {
                                pass1 += ch1;
                            }
                        }
                        else
                        { Dot1 = true; }
                    }
                    filesPass1[counter1] = pass1;
                    pass1 = "";
                    Dot1 = false;
                    counter1 += 1;
                }
                while (counter1 < filesList1.Length);
                fileCount1 = 0;
                ////////////////////////////////////////////////////////
                userName.Text = fileListCombo.SelectedItem.ToString();
                filePassword = filesPass1[fileListCombo.SelectedIndex].ToString();
            }
            catch (Exception exC)
            {
                MessageBox.Show(exC.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!System.IO.File.Exists(Application.StartupPath + "\\idData\\" + userName.Text + onlyaDot + userPass.Text))
            {
                MessageBox.Show("Your Password is not currect. Enter currect Password?", "User Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                if (MessageBox.Show("The Record will be deleted.\nAre You Sure to Delete?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string FileNameDelete = userName.Text + onlyaDot + userPass.Text;
                    string filePathDelete = Application.StartupPath + "\\idData\\" + FileNameDelete;
                    System.IO.File.Delete(filePathDelete);

                    try
                    {
                        fileListCombo.Items.Clear();
                        NamesAndPass();
                        fileListCombo.SelectedIndex = 0;
                    }
                    catch
                    {
                        MessageBox.Show("All records have been deleted.", "User", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        fileListCombo.Text = "";
                    }
                    userName.Text = "";
                    userPass.Text = "";
                    NamesAndPass();
                }
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {

            if (editing)
            {
                if (userName.Text == "" | userPass.Text == "")
                {
                    MessageBox.Show("Please Enter New Name And Password.", "Edit User", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    btnDelete.Enabled = true;
                    btnNew.Enabled = true;
                    btnEdit.Text = "&Edit";
                    editing = false;

                    System.IO.File.Move(PPath, Application.StartupPath + "\\idData\\" + userName.Text + onlyaDot + userPass.Text);
                    userPass.Text = "";
                    userName.Enabled = false;
                    NamesAndPass();
                }
            }
            else
            {
                if (!System.IO.File.Exists(Application.StartupPath + "\\idData\\" + userName.Text + onlyaDot + userPass.Text))
                {
                    MessageBox.Show("Enter Currect File Name and Password.", "Edit User", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    PPath = Application.StartupPath + "\\idData\\" + userName.Text + onlyaDot + userPass.Text;
                    btnDelete.Enabled = false;
                    btnNew.Enabled = false;
                    btnEdit.Text = "&Save";
                    userName.Enabled = true;
                    editing = true;
                    userName.Text = "";
                    userPass.Text = "";
                }
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to exit?", "Application", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        private void userPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (newSaving)
                    btnOK_Click(sender, e);
                else
                    btnNew_Click(sender, e);
            }
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (fileListCombo.SelectedIndex > -1 && userPass.Text != "" && fileListCombo.SelectedItem.ToString() != "")
                {
                    if (File.Exists(Application.StartupPath + "\\idData\\" + fileListCombo.SelectedItem.ToString() + "." + userPass.Text))
                    {

                        SaveFileDialog SaveFile = new SaveFileDialog();
                        SaveFile.InitialDirectory = "C:\\";
                        SaveFile.Filter = "Phone Directory File (*.ph)| *.ph";
                        string FilePath = Application.StartupPath + "\\idData\\";
                        string OldfileName = fileListCombo.SelectedItem.ToString();
                        SaveFile.ShowDialog();
                        string saveResult = SaveFile.FileName;
                        int index = saveResult.LastIndexOf('\\') + 1;

                        string ExPath = saveResult.Substring(0, index);
                        string OldFile = OldfileName + "." + filePassword;
                        index = saveResult.LastIndexOf('\\') + 1;
                        string ExpFileName = saveResult.Substring(index);

                        if (!System.IO.File.Exists(ExPath + ExpFileName))
                            System.IO.File.Copy(FilePath + OldFile, ExPath + ExpFileName);
                        else
                        {
                            if (MessageBox.Show(
                                "File Already exists. Sure to Export?\r\nExisting Data will be erased.",
                                "Export",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question
                           ) == DialogResult.Yes)
                            {
                                File.Delete(ExPath + ExpFileName);
                                File.Copy(FilePath + OldFile, ExPath + ExpFileName);
                            }
                        }

                        List<string> fileChs = new List<string>();
                        string Text = "";
                        foreach (string str in File.ReadAllLines(ExPath + ExpFileName))
                        {
                            fileChs.Add(str);
                        }

                        fileChs.Add('n' + userName.Text);
                        fileChs.Add('x' + userPass.Text);
                        for (int i = 0; i < fileChs.Count; i++)
                        {
                            foreach (char ch in fileChs[i])
                            {
                                Text += (17 * (12 * (8 * (5 * (3 * (2 * (Int32)ch)))))) + (' '.ToString());
                                //int toGain = (((((((Int32.Parse(Text)) / 17) / 12) / 8) / 5) / 3) / 2);
                                //char chr = char.Parse(char.ConvertFromUtf32(toGain));
                            }
                            Text += '~';
                        }
                        new FileInfo(ExPath + ExpFileName).Create().Close();
                        File.WriteAllText(ExPath + ExpFileName, Text, Encoding.UTF8);
                    }
                    else
                    {
                        MessageBox.Show(
                            "Password is not currect or Select a user to export file.", "Export",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show(
                        "Con't Export user data file, File not found or user name and password or incurrect.",
                        "Export",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("Error occured in exporting. Might be becouse of bad data saved.", "Export",
              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void importButton_Click(object sender, EventArgs e)
        {

            OpenFileDialog OpenFile = new OpenFileDialog();
            OpenFile.InitialDirectory = "C:\\";
            OpenFile.Filter = "Phone Directory File (*.ph)| *.ph";
            string fileTextInts = "";
            string ImpPath = "";
            string FileName = "";
            if (OpenFile.ShowDialog() == DialogResult.OK)
            {
                ImpPath = Application.StartupPath + "\\idData\\";
                FileName = OpenFile.FileName.ToString();
                fileTextInts = File.ReadAllText(FileName);

                string FileText = "", str = "", cStr = "";
                foreach (char ch in fileTextInts)
                {
                    if (ch == '~')
                    {
                        FileText += cStr + '\r' + '\n';
                        cStr = "";
                        continue;
                    }
                    if (ch != ' ')
                    {
                        str += ch;
                    }
                    else
                    {
                        if (str != "")
                        {
                            int toGain = (((((((Int32.Parse(str)) / 17) / 12) / 8) / 5) / 3) / 2);
                            cStr += char.Parse(char.ConvertFromUtf32(toGain));
                            str = "";
                        }
                    }
                }
                int index = 0; string fileNam = "", fileExt = "";
                FileText = FileText.Trim();
                index = FileText.LastIndexOf('x');
                string fileComplete = "";
                try
                {
                    fileExt = FileText.Substring(index + 1).Trim();
                    FileText = FileText.Remove(index).Trim();
                    index = FileText.LastIndexOf('n');
                    fileNam = FileText.Substring(index + 1).Trim();
                    FileText = FileText.Remove(index).Trim();
                    fileComplete = ImpPath + fileNam + "." + fileExt;
                }
                catch { MessageBox.Show("Bad file, this not a \'Phone Directory\' file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

                if (!File.Exists(fileComplete))
                {
                    new FileInfo(fileComplete).Create().Close();
                    File.WriteAllText(fileComplete, FileText, Encoding.UTF8);
                }
                else
                {
                    if (MessageBox.Show(
                        "A User of this name also exists are you sure to override it? \r\nif you click no then it will add new data \r\nwith existing but It might make harm to file.", "Import",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        new FileInfo(fileComplete).Delete();
                        new FileInfo(fileComplete).Create().Close();
                        File.WriteAllText(fileComplete, FileText, Encoding.UTF8);
                    }
                    else
                    {
                        string temp = File.ReadAllText(ImpPath + fileNam + "." + fileExt).Trim();
                        temp += "\r\n" + FileText;
                        new FileInfo(ImpPath + fileNam + "." + fileExt).Delete();
                        new FileInfo(ImpPath + fileNam + "." + fileExt).Create().Close();
                        File.WriteAllText(ImpPath + fileNam + "." + fileExt, temp, Encoding.UTF8);
                    }
                }
            }
            NamesAndPass();
        }
    }
}