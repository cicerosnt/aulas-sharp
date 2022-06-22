using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace crud_xml
{
    
    public partial class frmCrudXml : Form
    {
        int plate=0, positionTable= -1;
        string firstName, lastName, cpf, celphone, phone, mail, company, sector, inCherge, admissionAt;

       

        DataTable dataTable;
        const string fileName = "PhoneBook.xml";

        public frmCrudXml()
        {
            InitializeComponent();

            initializePhoneBook();

            cleanFields();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (isvalidateFields())
            {
                DataRow dataRow = dataTable.Rows[positionTable];

                dataRow["Matricula"] = plate;
                dataRow["Nome"] = firstName;
                dataRow["Sobrenome"] = lastName;
                dataRow["CPF"] = cpf;
                dataRow["Celular"] = celphone;
                dataRow["Fixo"] = phone;
                dataRow["E-mail"] = mail;
                dataRow["Empresa"] = company;
                dataRow["Setor"] = sector;
                dataRow["Encarregado"] = inCherge;
                dataRow["Admissão"] = admissionAt;
                
                dataTable.AcceptChanges();
                dataTable.WriteXml(fileName, XmlWriteMode.WriteSchema);

                Msgs.alertaSucesso("Registro realizado com sucesso!");
                cleanFields();
                return;
            }
        }

        private void btnPrinter_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (positionTable >= 0)
            {
                dataTable.Rows.RemoveAt(positionTable);
                dataTable.AcceptChanges();
                dataTable.WriteXml(fileName, XmlWriteMode.WriteSchema);

                Msgs.alertaSucesso("Registro removido com sucesso!");
                cleanFields();
                return;
            }
            else
            {
                Msgs.alertaAlerta("Por favor, selecione um registro para Excluir.");
                cleanFields();
                return;
            }
        }

        private void txtPlate_Leave(object sender, EventArgs e)
        {
            if (isvalidateFieldsPlate())
            {
                if (isExistRegister())
                {
                    DialogResult dr = MessageBox.Show("Matrcula já cadastrada, deseja carregar os dados.", "Registro encontrado", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);

                    if (dr == DialogResult.Yes)
                    {
                        isExistRegister(true);
                        enableDisable(true);
                        btnDelete.Enabled = true;
                        btnEdit.Enabled = true;
                        btnSave.Enabled = false;
                        btnPrinter.Enabled = true;

                        return;
                    }
                    else
                    {
                        cleanFields();
                        txtName.Focus();
                        return;
                    }
                }
                enableDisable(true);
                btnSave.Enabled = true;

            }
            return;
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            cleanFields();

            positionTable = -1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (isvalidateFieldsPlate())
            {
                if (!isExistRegister())
                {
                    if (isvalidateFields())
                    {
                        DataRow dataRow = dataTable.NewRow();

                        dataRow["Matricula"] = plate;
                        dataRow["Nome"] = firstName;
                        dataRow["Sobrenome"] = lastName;
                        dataRow["CPF"] = cpf;
                        dataRow["Celular"] = celphone;
                        dataRow["Fixo"] = phone;
                        dataRow["E-mail"] = mail;
                        dataRow["Empresa"] = company;
                        dataRow["Setor"] = sector;
                        dataRow["Encarregado"] = inCherge;
                        dataRow["Admissão"] = admissionAt;

                        dataTable.Rows.Add(dataRow);
                        dataTable.AcceptChanges();
                        dataTable.WriteXml(fileName, XmlWriteMode.WriteSchema);

                        Msgs.alertaSucesso("Registro realizado com sucesso!");
                        cleanFields();
                        return;
                    }
                }
                else
                {
                    DialogResult dr = MessageBox.Show("Matrcula já cadastrada, deseja carregar os dados.", "Registro encontrado", MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Information);

                    if (dr == DialogResult.Yes)
                    {
                        isExistRegister(true);
                    }
                }
            }

            return;
        }

        private bool isExistRegister(bool load = false)
        {
            bool isTrue = false;
            try
            {
                for(int i=0; i<dataTable.Rows.Count; i++)
                {
                    if ((int)dataTable.Rows[i]["Matricula"] == plate)
                    {
                        isTrue = true;
                        if(load == true)
                        {
                            txtPlate.Text = dataTable.Rows[i]["Matricula"].ToString();
                            txtName.Text = dataTable.Rows[i]["Nome"].ToString();
                            txtLastName.Text = dataTable.Rows[i]["Sobrenome"].ToString();
                            txtCpf.Text = dataTable.Rows[i]["CPF"].ToString();
                            txtCelphone.Text = dataTable.Rows[i]["Celular"].ToString();
                            txtPhone.Text = dataTable.Rows[i]["Fixo"].ToString();
                            txtMail.Text = dataTable.Rows[i]["E-mail"].ToString();
                            cbxCompany.SelectedItem = dataTable.Rows[i]["Empresa"];
                            cbxSector.SelectedItem = dataTable.Rows[i]["Setor"];
                            txtInCharge.Text = dataTable.Rows[i]["Encarregado"].ToString();
                            txtAdmissionAt.Text = dataTable.Rows[i]["Admissão"].ToString();

                            positionTable = i;
                          
                        }
                    }
                }

                return isTrue;
            }
            catch(Exception ex)
            {
                return isTrue;
            }
            
        }

        private void initializePhoneBook()
        {
            dataTable = new DataTable("PhoneBook");

            if (System.IO.File.Exists(fileName))
            {
                dataTable.ReadXml(fileName);
            }
            else
            {
                dataTable.Columns.Add("Matricula", typeof(int));
                dataTable.Columns.Add("Nome", typeof(string));
                dataTable.Columns.Add("Sobrenome", typeof(string));
                dataTable.Columns.Add("CPF", typeof(string));
                dataTable.Columns.Add("Celular", typeof(string));
                dataTable.Columns.Add("Fixo", typeof(string));
                dataTable.Columns.Add("E-mail", typeof(string));
                dataTable.Columns.Add("Empresa", typeof(string));
                dataTable.Columns.Add("Setor", typeof(string));
                dataTable.Columns.Add("Encarregado", typeof(string));
                dataTable.Columns.Add("Admissão", typeof(string));

            }
        }

        private bool isvalidateFields()
        {
            firstName = txtName.Text;
            lastName = txtLastName.Text;
            cpf = txtCpf.Text;
            celphone = txtCelphone.Text;
            phone = txtPhone.Text;
            mail = txtMail.Text;
            company = cbxCompany.SelectedItem.ToString();
            sector = cbxSector.SelectedItem.ToString();
            inCherge = txtInCharge.Text;
            admissionAt = txtAdmissionAt.Text;
            //Msgs.alertaBox(
            //    "firstName: " + firstName+
            //    "\nlastName: " + lastName +
            //    "\ncpf: " + cpf+
            //    "\ncelphone: " + celphone+
            //    "\nphone: " + phone+
            //    "\nmail: " + mail+
            //    "\ncompany: " + company+
            //    "\nsector: " + sector+
            //    "\nadmissionAt: " + admissionAt

            //    );
            
            if(
                   firstName.Equals("")
                || lastName.Equals("")
                || cpf.Equals("")
                || celphone.Equals("")
                || mail.Equals("")
                || company.Equals("")
                || sector.Equals("")
                || inCherge.Equals(""))
            {
                Msgs.alertaAlerta("Os campos com * são abrigatorio.");
                return false;
                
            }
            return true;
        }


        private bool isvalidateFieldsPlate()
        {
            if (int.TryParse(txtPlate.Text, out plate) == false)
            {
                Msgs.alertaAlerta("Valor infromado para a matricula, é invalido.");
                txtPlate.Focus();
                return false;
            }

            return true;
        }

        private void cleanFields()
        {
            txtPlate.Clear();
            txtName.Clear();
            txtLastName.Clear();
            txtCpf.Clear();
            txtCelphone.Clear();
            txtPhone.Clear();
            txtMail.Clear();
            cbxCompany.SelectedItem = "";
            cbxSector.SelectedItem = "";
            txtInCharge.Clear();
            txtAdmissionAt.Clear();

            enableDisable(false);
            btnSave.Enabled = false;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            btnPrinter.Enabled = false;
        }

        private void enableDisable(bool enable = false)
        {
            txtPlate.Enabled = !enable;

            txtName.Enabled = enable;
            txtLastName.Enabled = enable;
            txtCpf.Enabled = enable;
            txtCelphone.Enabled = enable;
            txtPhone.Enabled = enable;
            txtMail.Enabled = enable;
            cbxCompany.Enabled = enable;
            cbxSector.Enabled = enable;
            txtInCharge.Enabled = enable;
            txtAdmissionAt.Enabled = enable;
        }
    }
}
