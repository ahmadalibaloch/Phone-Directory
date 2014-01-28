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
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();


        }
        public string UserFileName = "";
        public string UserFilePass = "";
        string fullFileName = "";
        /// <summary>
        /// /////////////////////////////////////////////////////////////
        /// </summary>
        ///     
        bool SaveEdit;
        int editId;
        bool fieldCon;
        public int loadAndManip(int index)
        {
            ///////////////STARTING TEMP LOAD STREAM HERE FOR COUNTING ARRAYS LENGTH///////
            int rang = 0;
            string temp;
            StreamReader idStreamLT = new StreamReader(Application.StartupPath + "\\IdData\\" + fullFileName);
            while (!idStreamLT.EndOfStream)
            {
                temp = idStreamLT.ReadLine();
                if (temp != "ETM") { temp = ""; }
                else { rang += 1; }
            }
            idStreamLT.Close();
            /////////////////ENDING////AND INTIELIZING VARIABLES AND ARRAYS/////////////////
            string idLoad;
            int saver = 0;
            int counter = 0;
            string[] names = new string[rang];
            //STARTING MAIN LOAD STREAM HERE//////////////////////////////////////////////
            StreamReader idStreamL = new StreamReader(Application.StartupPath + "\\IdData\\" + fullFileName);
            while (!idStreamL.EndOfStream)
            {
                idLoad = idStreamL.ReadLine();
                //if (idLoad == "") { MessageBox.Show("Data File is empity."); goto startHere; }
                if (idLoad != "ETM")
                {
                    if (counter == 0)
                    {
                        names[saver] = idLoad;
                    }
                    counter += 1;
                }
                else
                {
                    saver += 1;
                    counter = 0;
                }
            }
            try
            {
                idStreamL.Close();
                nameCombo.Items.Clear();
                nameCombo.Items.AddRange(names);
                nameCombo.SelectedIndex = index;
                //btnDelete.Enabled = true;
            }
            catch
            {
            }
            // btnDelete.Enabled = true;
            return saver;


        }
        public void clearFields()
        {
            firstName.Text = "";
            lastName.Text = "";
            mobileNo.Text = "";
            cityName.Text = "";
            countryName.Text = "";
        }
        public void fieldControl()
        {
            if (fieldCon)
            {
                firstName.ReadOnly = true;
                lastName.ReadOnly = true;
                mobileNo.ReadOnly = true;
                cityName.ReadOnly = true;
                countryName.ReadOnly = true;
                fieldCon = false;
            }
            else
            {
                firstName.ReadOnly = false;
                lastName.ReadOnly = false;
                mobileNo.ReadOnly = false;
                cityName.ReadOnly = false;
                countryName.ReadOnly = false;
                fieldCon = true;
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (firstName.Text == "" || lastName.Text == "" || mobileNo.Text == "" || cityName.Text == "" || countryName.Text == "")
            {
                MessageBox.Show("One or more data fields or empty or no record selected to edit. Complete fields or select a record from Names list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SaveEdit = false;
                editId = nameCombo.SelectedIndex;
                btnEdit.Enabled = false;
                btnSave.Enabled = true;
                btnNew.Enabled = false;
                btnCancel.Enabled = true;
                btnLoad.Enabled = false;
                btnDelete.Enabled = false;
                nameCombo.Enabled = false;
                fieldCon = false;
                fieldControl();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            btnEdit.Enabled = true;
            btnNew.Enabled = true;
            btnCancel.Enabled = false;
            btnLoad.Enabled = true;
            btnDelete.Enabled = true;
            nameCombo.Enabled = true;
            btnNew.Focus();
            loadAndManip(nameCombo.SelectedIndex);
            fieldCon = true;
            fieldControl();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("The record will be deleted. Are you sure to delete this record?.", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    /////////////////////////INITELIZING VARIABES AND READING EXISTING DATA//////
                    int ARrayRang = 0;
                    string TEmpStr;
                    StreamReader idStreamLD = new StreamReader(Application.StartupPath + "\\IdData\\" + fullFileName);
                    while (!idStreamLD.EndOfStream)
                    {
                        TEmpStr = idStreamLD.ReadLine();
                        if (TEmpStr != "ETM") { TEmpStr = ""; }
                        else { ARrayRang += 1; }
                    }
                    idStreamLD.Close();
                    ////////////////////////////ENDING////AND INTIELIZING VARIABLES AND ARRAYS/////////////////
                    string[] namedf = new string[ARrayRang];
                    string[] namedl = new string[ARrayRang];
                    string[] mobiled = new string[ARrayRang];
                    string[] cityd = new string[ARrayRang];
                    string[] countryd = new string[ARrayRang];

                    int delId = nameCombo.SelectedIndex;
                    int delLinC = 0;
                    int delSC = 0;
                    int delEn = 0;
                    string LTI;
                    /////////////////////////READING EXISTING DATA TILL DELID///////////////////
                    StreamReader bidE = new StreamReader(Application.StartupPath + "\\IdData\\" + fullFileName);
                    while (!bidE.EndOfStream)
                    {
                        LTI = bidE.ReadLine();
                        if (LTI != "ETM")
                        {
                            if (delSC >= delId)
                            {
                                break;
                            }
                            switch (delLinC)
                            {
                                case 0:
                                    namedf[delSC] = LTI;
                                    break;
                                case 1:
                                    namedl[delSC] = LTI;
                                    break;
                                case 2:
                                    mobiled[delSC] = LTI;
                                    break;
                                case 3:
                                    cityd[delSC] = LTI;
                                    break;
                                case 4:
                                    countryd[delSC] = LTI;
                                    break;
                            }
                            delLinC += 1;
                        }
                        else
                        {
                            if (delSC >= delId)
                            {
                                break;
                            }

                            delSC += 1;
                            delLinC = 0;
                        }
                    }
                    bidE.Close();
                    //////////////READ EXISTING DATA AND NOW STARTING AFTER DELID DATA READING///
                    /////////////////////////READING DATA AFTER DELID///////////////////////////
                    //delSC = 0;
                    delEn = 0;
                    bool FIREADING = false;
                    StreamReader bidA = new StreamReader(Application.StartupPath + "\\IdData\\" + fullFileName);
                    while (!bidA.EndOfStream)
                    {
                        LTI = bidA.ReadLine();

                        if (LTI != "ETM")
                        {
                            if (delEn > delId)
                            {
                                switch (delLinC)
                                {
                                    case 0:
                                        namedf[delSC] = LTI;
                                        break;
                                    case 1:
                                        namedl[delSC] = LTI;
                                        break;
                                    case 2:
                                        mobiled[delSC] = LTI;
                                        break;
                                    case 3:
                                        cityd[delSC] = LTI;
                                        break;
                                    case 4:
                                        countryd[delSC] = LTI;
                                        break;
                                }
                            }
                            delLinC += 1;


                        }
                        else
                        {
                            if (delEn > delId)
                            {
                                FIREADING = true;
                            }
                            if (FIREADING == false)
                            {
                                delEn += 1;
                            }
                            if (FIREADING)
                            {
                                delSC += 1;
                            }
                            delLinC = 0;
                        }
                    }
                    bidA.Close();
                    /////////////////SAVING ALL EXISTING + AFTER DELID DATA TO DATA FILE////////
                    string[] Namf = new string[namedf.Length - 1];
                    string[] Naml = new string[namedf.Length - 1];
                    string[] Mobil = new string[namedf.Length - 1];
                    string[] Cit = new string[namedf.Length - 1];
                    string[] Countr = new string[namedf.Length - 1];

                    for (int z = 0; z < Countr.Length; z++)
                    {
                        Namf[z] = namedf[z].ToString();
                        Naml[z] = namedl[z].ToString();
                        Mobil[z] = mobiled[z].ToString();
                        Cit[z] = cityd[z].ToString();
                        Countr[z] = countryd[z].ToString();
                    }

                    StreamWriter bidS = new StreamWriter(Application.StartupPath + "\\IdData\\" + fullFileName);
                    string strSavings = "";
                    int d = 0;
                    try
                    {
                        do
                        {
                            strSavings += Namf[d].ToString() + "\r\n" + Naml[d].ToString() + "\r\n" + Mobil[d].ToString() + "\r\n" + Cit[d].ToString() + "\r\n" + Countr[d].ToString() + "\r\nETM\r\n";
                            d++;
                        } while (d < Namf.Length - 1);
                        //int j = Namf.Length-1;
                        strSavings += Namf[d].ToString() + "\r\n" + Naml[d].ToString() + "\r\n" + Mobil[d].ToString() + "\r\n" + Cit[d].ToString() + "\r\n" + Countr[d].ToString() + "\r\nETM";
                        bidS.WriteLine(strSavings);
                    }
                    catch (Exception excep)
                    {
                        MessageBox.Show(excep.Message);
                        MessageBox.Show("If it is last or second last record then don't try to delete please edit.");

                    }
                    bidS.Close();
                    loadAndManip(editId + 1);
                    // btnDelete.Enabled = false ;
                }
            }
            catch (Exception fd)
            {
                btnDelete.Enabled = false;
                MessageBox.Show("If it is last or second last record then don't try to delete please edit.");
                MessageBox.Show(fd.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            fullFileName = UserFileName + "." + UserFilePass;
            ///////////////////////////////////////////////
            btnSave.Enabled = false;
            btnEdit.Enabled = true;
            btnNew.Enabled = true;
            btnCancel.Enabled = false;
            btnDelete.Enabled = false;
            firstName.MaxLength = 20;
            lastName.MaxLength = 20;
            mobileNo.MaxLength = 15;
            cityName.MaxLength = 20;
            countryName.MaxLength = 25;
            SaveEdit = true;
            fieldCon = true;
            fieldControl();
            loadAndManip(0);
            MessageBox.Show("Welcom!, " + UserFileName + "." + " There are " + loadAndManip(0).ToString() + " records.", "Welcom!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //THIS IS NOT WORKING===================this.GetNextControl(btnNew, true);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {

            SaveEdit = true;
            btnNew.Enabled = false;
            btnSave.Enabled = true;
            btnEdit.Enabled = false;
            btnCancel.Enabled = true;
            btnLoad.Enabled = false;
            btnDelete.Enabled = false;
            nameCombo.Enabled = false;
            clearFields();
            fieldControl();
            firstName.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveEdit)
            {
                try
                {
                    //////INITIALIZING VARIABLES AND STARTING STREAM FOR EXISTING DATA///////////
                    string idSave = "";
                    string idLFS = "";
                    StreamReader fSave = new StreamReader(Application.StartupPath + "\\IdData\\" + fullFileName);
                    while (!fSave.EndOfStream)
                    {
                        idLFS += fSave.ReadLine() + "\r\n";
                    }
                    fSave.Close();
                    //////////////STARTING STREAM WRITER HERE/////////////////////////////////////
                    long newInteger = Convert.ToInt64(mobileNo.Text);
                    if (firstName.Text == "" || lastName.Text == "" || mobileNo.Text == "" || cityName.Text == "" || countryName.Text == "")
                    {
                        MessageBox.Show("One or more data fields or empty please first complete them.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        StreamWriter idStreamW = new StreamWriter(Application.StartupPath + "\\IdData\\" + fullFileName);
                        idSave = firstName.Text + "\r\n" + lastName.Text + "\r\n" + mobileNo.Text + "\r\n" + cityName.Text + "\r\n" + countryName.Text + "\r\nETM";
                        idStreamW.WriteLine(idLFS + idSave);
                        idStreamW.Close();
                        btnSave.Enabled = false;
                        btnEdit.Enabled = true;
                        btnNew.Enabled = true;
                        btnDelete.Enabled = true;
                        clearFields();
                        fieldCon = true;
                        fieldControl();
                        btnLoad.Enabled = true;
                        btnCancel.Enabled = false;
                        nameCombo.Enabled = true;
                    }
                    SaveEdit = true;
                    loadAndManip(nameCombo.Items.Count);
                }
                catch
                {
                    MessageBox.Show("Please enter valid format in all fields.", "Format Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                /////////////////ENDING///////////////////////////////////////////////////////
            }
            else//========================================================================
            {
                try
                {
                    long newEnter = Convert.ToInt64(mobileNo.Text);
                    ///////////////STARTING TEMP LOAD STREAM HERE FOR COUNTING ARRAYS LENGTH///////
                    int arrayRang = 0;
                    string tempstr;
                    StreamReader idStreamLt = new StreamReader(Application.StartupPath + "\\IdData\\" + fullFileName);
                    while (!idStreamLt.EndOfStream)
                    {
                        tempstr = idStreamLt.ReadLine();
                        if (tempstr != "ETM") { tempstr = ""; }
                        else { arrayRang += 1; }
                    }
                    idStreamLt.Close();
                    /////////////////ENDING////AND INTIELIZING VARIABLES AND ARRAYS/////////////////
                    string idLFE;
                    int SaveCount = 0;
                    int LineCounter = 0;
                    string[] Namef = new string[arrayRang];
                    string[] Namel = new string[arrayRang];
                    string[] Mobile = new string[arrayRang];
                    string[] City = new string[arrayRang];
                    string[] Country = new string[arrayRang];
                    //STARTING MAIN LOAD STREAM HERE//////////////////////////////////////////////

                    StreamReader idStreamE = new StreamReader(Application.StartupPath + "\\IdData\\" + fullFileName);
                    while (!idStreamE.EndOfStream)
                    {
                        idLFE = idStreamE.ReadLine();
                        if (idLFE != "ETM")
                        {
                            switch (LineCounter)
                            {
                                case 0:
                                    Namef[SaveCount] = idLFE;
                                    break;
                                case 1:
                                    Namel[SaveCount] = idLFE;
                                    break;
                                case 2:
                                    Mobile[SaveCount] = idLFE;
                                    break;
                                case 3:
                                    City[SaveCount] = idLFE;
                                    break;
                                case 4:
                                    Country[SaveCount] = idLFE;
                                    break;
                            }
                            LineCounter += 1;
                        }
                        else
                        {
                            SaveCount += 1;
                            LineCounter = 0;
                        }
                    }
                    idStreamE.Close();
                    ////////////////STARTING HERE TO EDIT ARRAYS////////////////////////////

                    Namef[editId] = firstName.Text;
                    Namel[editId] = lastName.Text;
                    Mobile[editId] = mobileNo.Text;
                    City[editId] = cityName.Text;
                    Country[editId] = countryName.Text;


                    firstName.Text = "";
                    lastName.Text = "";
                    mobileNo.Text = "";
                    cityName.Text = "";
                    countryName.Text = "";
                    nameCombo.Text = "";

                    /////////////////STARTING WRITING STREAM FROM HERE//////////////////////
                    StreamWriter streamEditing = new StreamWriter(Application.StartupPath + "\\IdData\\" + fullFileName);
                    string strString = "";
                    int i = 0;

                    do
                    {
                        strString += Namef[i].ToString() + "\r\n" + Namel[i].ToString() + "\r\n" + Mobile[i].ToString() + "\r\n" + City[i].ToString() + "\r\n" + Country[i].ToString() + "\r\nETM\r\n";
                        i++;
                    } while (i < Namef.Length - 1);
                    int r = Namef.Length - 1;
                    strString += Namef[r].ToString() + "\r\n" + Namel[r].ToString() + "\r\n" + Mobile[r].ToString() + "\r\n" + City[r].ToString() + "\r\n" + Country[r].ToString() + "\r\nETM";
                    streamEditing.WriteLine(strString);

                    streamEditing.Close();
                    SaveEdit = true;
                    btnEdit.Enabled = true;
                    btnSave.Enabled = false;
                    btnNew.Enabled = true;
                    btnLoad.Enabled = true;
                    btnDelete.Enabled = true;
                    nameCombo.Enabled = true;
                    fieldCon = true;
                    fieldControl();
                    loadAndManip(editId);
                }
                catch
                {
                    MessageBox.Show("Please enter valid format in fields.", "Format Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void nameCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ///////////////STARTING TEMP LOAD STREAM HERE FOR COUNTING ARRAYS LENGTH///////
                int ArrayRang = 0;
                string tempStr;
                StreamReader idStreamLT = new StreamReader(Application.StartupPath + "\\IdData\\" + fullFileName);
                while (!idStreamLT.EndOfStream)
                {
                    tempStr = idStreamLT.ReadLine();
                    if (tempStr != "ETM") { tempStr = ""; }
                    else { ArrayRang += 1; }
                }
                idStreamLT.Close();
                /////////////////ENDING////AND INTIELIZING VARIABLES AND ARRAYS/////////////////
                string idLFC;
                int saveCount = 0;
                int lineCounter = 0;
                string[] namesf = new string[ArrayRang];
                string[] namesl = new string[ArrayRang];
                string[] mobile = new string[ArrayRang];
                string[] city = new string[ArrayRang];
                string[] country = new string[ArrayRang];
                //STARTING MAIN LOAD STREAM HERE//////////////////////////////////////////////

                StreamReader idStreamL = new StreamReader(Application.StartupPath + "\\IdData\\" + fullFileName);
                while (!idStreamL.EndOfStream)
                {
                    idLFC = idStreamL.ReadLine();
                    if (idLFC != "ETM")
                    {
                        switch (lineCounter)
                        {
                            case 0:
                                namesf[saveCount] = idLFC;
                                break;
                            case 1:
                                namesl[saveCount] = idLFC;
                                break;
                            case 2:
                                mobile[saveCount] = idLFC;
                                break;
                            case 3:
                                city[saveCount] = idLFC;
                                break;
                            case 4:
                                country[saveCount] = idLFC;
                                break;
                        }
                        lineCounter += 1;
                    }
                    else
                    {
                        saveCount += 1;
                        lineCounter = 0;
                    }
                }
                idStreamL.Close();
                firstName.Text = namesf[nameCombo.SelectedIndex];
                lastName.Text = namesl[nameCombo.SelectedIndex];
                mobileNo.Text = mobile[nameCombo.SelectedIndex];
                cityName.Text = city[nameCombo.SelectedIndex];
                countryName.Text = country[nameCombo.SelectedIndex];
                btnDelete.Enabled = true;
            }
            catch (Exception hj)
            {
                MessageBox.Show(hj.Message);
            }
        }
        private void btnLoad_Click(object sender, EventArgs e)
        {

            loadAndManip(0);
            MessageBox.Show("There are " + loadAndManip(0).ToString() + " Records found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to exit program?", "Application", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void TextBoxes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSave_Click(sender, e);
            }
        }
    }
}