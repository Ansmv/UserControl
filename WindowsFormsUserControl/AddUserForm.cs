using UserService;
using WindowsFormsUserControl.Validators;

namespace WindowsFormsUserControl
{
    public partial class AddUserForm : Form
    {
        MainForm mainForm;

        public AddUserForm()
        {
            InitializeComponent();
        }

        private async void btnAddUser_Click(object sender, EventArgs e)
        {
            UserDTO user = new UserDTO();
            user.FullName = txtFullName.Text;
            user.DRFO = txtDRFO.Text;
            user.PhoneNumber = txtPhone.Text;
            user.Email = txtEmail.Text;
            UserServiceClient client = new UserServiceClient();
            if (await client.AddUserAsync(user) == 1)
            {
                txtFullName.Text = "";
                txtDRFO.Text = "";
                txtPhone.Text = "";
                txtEmail.Text = "";
                MessageBox.Show("Користувача успішно додано");
            }

        }

        private void btnCansel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtDRFO_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!DrfoValidator.ValidateDrfo(txtDRFO.Text))
            {
                e.Cancel = true;
                txtDRFO.Focus();
                errorProvider1.SetError(txtDRFO, "Неправильно введений ДРФО");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtDRFO, "");
            }
        }

        private void txtEmail_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!EmailValidator.ValidateEmail(txtEmail.Text))
            {
                e.Cancel = true;
                txtEmail.Focus();
                errorProvider1.SetError(txtEmail, "Невірний формат електронної пошти");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtEmail, "");
            }
        }
    }
}
