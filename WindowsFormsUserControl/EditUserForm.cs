using UserService;

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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

