using UserService;

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
    }
}
