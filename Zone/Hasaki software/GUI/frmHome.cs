using Intech_software.BUS;
using Intech_software.External;
using Intech_software.Models.WmsModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Intech_software.GUI
{
    public partial class frmHome : Form
    {
        AccountsBus accountsBus = new AccountsBus();
        Wms _wmsApi = new Wms();
        public frmHome()
        {
            InitializeComponent();
            btnZoneStatus.Click += BtnZoneStatus_Click;
        }

        private void BtnZoneStatus_Click(object sender, EventArgs e)
        {
            OpenFrom(_zoneStatus);
        }

        private void frmHome_Load(object sender, EventArgs e)
        {
            btnInBound.Enabled = false;
            btnUser.Enabled = false;
            btnDvvc.Enabled = false;
        }

        private Form currentForm;
        frmInBound _frmInBound = new frmInBound();
        FrmSetting _frmSetting = new FrmSetting();
        frmDvvc _frmDvvc = new frmDvvc();
        ZoneStatus _zoneStatus = new ZoneStatus();

        private void OpenFrom(Form form)
        {
            if (currentForm != null)
            {
                currentForm.Visible = false;
            }
            currentForm = form;
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            panelBody.Controls.Add(form);
            panelBody.Tag = form;
            form.BringToFront();
            form.Show();
        }

        #region menu navigation

        private void btnInBound_Click(object sender, EventArgs e)
        {
            btnInBound.BackColor = Color.LimeGreen;
            btnDvvc.BackColor = Color.SeaGreen;
            btnUser.BackColor = Color.SeaGreen;
            FormCollection fc = Application.OpenForms;

            List<Form> openForm = new List<Form>();
            foreach (Form frm in fc)
            {
                if (frm.Name == "frmInBound")
                {
                    return;
                }
                if (frm.Name == "frmDvvc")
                {
                    openForm.Add(frm);
                }
            }
            foreach (var item in openForm)
            {
                item.Close();
                item.Dispose();
            }

            _frmInBound = new frmInBound();
            OpenFrom(_frmInBound);
        }
        private void buttonDvvC_Click(object sender, EventArgs e)
        {
            btnInBound.BackColor = Color.SeaGreen;
            btnUser.BackColor = Color.SeaGreen;
            _frmInBound.Close();
        }
        private void btnSetting_Click(object sender, EventArgs e)
        {
            _frmSetting.ShowDialog();
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            frmUser fu = new frmUser();
            fu.ShowDialog();
        }
        #endregion
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (currentForm != null)
            {
                currentForm.Visible = false;
            }
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Do you want to logout system?", "Notification", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dg == DialogResult.OK)
            {
                if (currentForm != null)
                {
                    currentForm.Visible = false;
                }
                btnInBound.Enabled = false;
                btnUser.Enabled = false;
                btnDvvc.Enabled = false;
            }
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUserName.Text == string.Empty)
                {
                    MessageBox.Show("Tài khoản không được để trống.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtPassword.Text == string.Empty)
                {
                    MessageBox.Show("Mật khẩu không được để trống.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                HandlerLogin();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Do you want to exit program?", "Notification", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dg == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void frmHome_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
            Environment.Exit(0);
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                txtPassword.UseSystemPasswordChar = true;
            else
                txtPassword.UseSystemPasswordChar = false;
        }


        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                txtPassword.Text = txtPassword.Text.Trim();
                HandlerLogin();
                txtPassword.Text = string.Empty;
            }
        }

        private async void HandlerLogin()
        {
            if (checkBoxDebug.Checked)
            {
                DataTable dt = accountsBus.GetLoginInfo("admin", "admin");
                if (dt.Rows.Count > 0)
                {
                    frmInBound.revAccountName = dt.Rows[0][1].ToString();

                    btnInBound.Enabled = true;
                    btnDvvc.Enabled = true;
                    btnUser.Enabled = true;
                }
                else
                    MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                // login using hasaki account

                var resLogin = await _wmsApi.Login(new Models.WmsModel.LoginRequest()
                {
                    Email = txtUserName.Text,
                    Password = txtPassword.Text,
                });
                if (!string.IsNullOrEmpty(resLogin.Message))
                {
                    MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác");
                    return;
                }
                if (string.IsNullOrEmpty(resLogin.Token))
                {
                    MessageBox.Show("Không lấy đc token");
                    return;
                }
                frmInBound.revAccountName = resLogin.User.Email;
                UserLogin.Token = resLogin.Token;
                UserLogin.Email = txtUserName.Text;
                UserLogin.Password = txtPassword.Text;
                UserLogin.UserId = resLogin.User.UserId;
                btnInBound.Enabled = true;
                btnDvvc.Enabled = true;
                btnUser.Enabled = true;
                MessageBox.Show("Đăng nhập thành công");
            }


        }

        private void btnDvvc_Click(object sender, EventArgs e)
        {
            btnDvvc.BackColor = Color.LimeGreen;
            btnInBound.BackColor = Color.SeaGreen;
            btnUser.BackColor = Color.SeaGreen;

            FormCollection fc = Application.OpenForms;
            List<Form> openForm = new List<Form>();
            foreach (Form frm in fc)
            {
                if (frm.Name == "frmDvvc")
                {
                    return;
                }

                if (frm.Name == "frmInBound")
                {
                    openForm.Add(frm);
                }
            }
            foreach (var item in openForm)
            {
                item.Close();
                item.Dispose();
            }

            _frmDvvc = new frmDvvc();
            OpenFrom(_frmDvvc);
        }
    }
}
