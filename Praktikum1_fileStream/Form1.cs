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

namespace Praktikum1_fileStream
{
    public partial class Form1 : Form
    {
        const int kapasitasAwal = 50;
        string[] arrCustomer = new string[kapasitasAwal];
        int jmlMax = kapasitasAwal;
        int idx = -1;
        int jmlCustomer = 0;
        char pemisah = ',';

        public Form1()
        {
            InitializeComponent();

            aktifkanTextbox(false);
            totalRecord();
        }

        private void pisahDataCustomer(string customer)
        {
            char[] pisah = { pemisah };
            string[] dataCustomer = customer.Split(pisah);

            txtId.Text = dataCustomer[0];
            txtNama.Text = dataCustomer[1];
            txtAlamat.Text = dataCustomer[2];
        }

        private void aktifkanTextbox(bool status)
        {
            txtId.Enabled = status;
            txtNama.Enabled = status;
            txtAlamat.Enabled = status;
        }

        private void bersih()
        {
            txtId.Clear();
            txtNama.Clear();
            txtAlamat.Clear();
        }

        private void totalRecord()
        {
            lblTotal.Text = "Total Record = " + jmlCustomer.ToString();
        }

        private void updateDataArray()
        {
            if (jmlCustomer > 0)
            {
                string customer = "";
                customer += txtId.Text + pemisah;
                customer += txtNama.Text + pemisah;
                customer += txtAlamat.Text + pemisah;

                arrCustomer[idx] = customer;
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            DialogResult pilih = dlgOpen.ShowDialog();

            if (pilih == DialogResult.OK)
            {
                arrCustomer = File.ReadAllLines(dlgOpen.FileName);
                jmlCustomer = arrCustomer.Length;
                idx = 0;

                jmlMax = jmlCustomer * 2;
                Array.Resize(ref arrCustomer, jmlMax);

                string customer = arrCustomer[idx];
                pisahDataCustomer(customer);
                aktifkanTextbox(true);
                totalRecord();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            updateDataArray();
            DialogResult pilih = dlgSave.ShowDialog();

            if (pilih == DialogResult.OK)
            {
                string[] arrBantuan = new string[jmlCustomer];
                Array.Copy(arrCustomer, arrBantuan, jmlCustomer);
                File.WriteAllLines(dlgSave.FileName, arrBantuan);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            updateDataArray();

            if (jmlCustomer > 0)
            {
                idx = 0;
                string customer = arrCustomer[idx];
                pisahDataCustomer(customer);
                totalRecord();
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            updateDataArray();

            if (jmlCustomer > 0)
            {
                idx = jmlCustomer - 1;
                string customer = arrCustomer[idx];
                pisahDataCustomer(customer);
                totalRecord();
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            updateDataArray();

            if (jmlCustomer > 0)
            {
                idx--;

                if (idx < 0)
                    idx = 0;

                string customer = arrCustomer[idx];
                pisahDataCustomer(customer);
                totalRecord();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            updateDataArray();

            if (jmlCustomer > 0)
            {
                idx++;

                if (idx >= jmlCustomer)
                    idx = jmlCustomer - 1;

                string customer = arrCustomer[idx];
                pisahDataCustomer(customer);
                totalRecord();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            aktifkanTextbox(true);
            updateDataArray();

            if (jmlCustomer == jmlMax)
            {
                jmlMax *= 2;
                Array.Resize(ref arrCustomer, jmlMax);
            }

            bersih();
            txtId.Focus();
            idx = jmlCustomer;
            jmlCustomer++;
            totalRecord();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            updateDataArray();

            if (jmlCustomer > 0)
            {
                if (idx == jmlCustomer - 1)
                    idx--;
                else
                    for (int i = idx; i < jmlCustomer; i++)
                        arrCustomer[i] = arrCustomer[i + 1];

                jmlCustomer--;

                if(jmlCustomer > 0) {
                    string customer = arrCustomer[idx];
                    pisahDataCustomer(customer);
                } else {
                    bersih();
                    aktifkanTextbox(false);
                }

                totalRecord();
            }
        }
    }
}
