using UserService;
using WindowsFormsUserControl.Validators;

namespace WindowsFormsUserControl
{
    public partial class EditUserForm : Form
    {
        private uint modifiedUserId;
        public EditUserForm()
        {
            InitializeComponent();
        }

        internal void LoadUser(UserDTO selectedUser)
        {
            modifiedUserId = selectedUser.Id;
            txtFullName.Text = selectedUser.FullName;
            txtDRFO.Text = selectedUser.DRFO;
            txtEmail.Text = selectedUser.Email;
            txtPhone.Text = selectedUser.PhoneNumber;
            txtCreationDate.Text = selectedUser.Created.ToString();
            txtModificationDate.Text = selectedUser.LastUpdated.ToString();
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                UserDTO user = new UserDTO();
                user.FullName = txtFullName.Text;
                user.DRFO = txtDRFO.Text;
                user.PhoneNumber = txtPhone.Text;
                user.Email = txtEmail.Text;
                UserServiceClient client = new UserServiceClient();
                if (await client.UpdateUserAsync(modifiedUserId, user) == 1)
                {
                    MessageBox.Show("Дані користувача змінено");
                }
            }
            else
            {
                MessageBox.Show("Будь ласка, виправте помилки у введених даних.");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
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

