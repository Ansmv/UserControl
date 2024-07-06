using UserService;

namespace WindowsFormsUserControl
{
    public partial class MainForm : Form
    {
        private AddUserForm addUserForm;
        private EditUserForm editUserForm;

        public MainForm()
        {
            InitializeComponent();
            LoadDataAsync();
        }

        private void btnLoadUsers_Click(object sender, EventArgs e)
        {
            LoadDataAsync();
        }

        private async void LoadDataAsync()
        {
            UserServiceClient service = new UserServiceClient();
            List<UserDTO> users = await service.GetAllUsersAsync();

            if (users != null && users.Count > 0)
            {
                // Clear existing columns
                dataGridViewUsers.Columns.Clear();

                // Define columns in the desired order
                dataGridViewUsers.AutoGenerateColumns = false;

                AddColumn("Id", "ID");
                AddColumn("FullName", "Full Name");
                AddColumn("DRFO", "DRFO");
                AddColumn("Email", "Email");
                AddColumn("PhoneNumber", "Phone Number");
                AddColumn("Created", "Created");
                AddColumn("LastUpdated", "Last Updated");

                // Bind the data source
                dataGridViewUsers.DataSource = users;
            }
            else
            {
                MessageBox.Show("Failed to fetch users");
            }
        }

        private void AddColumn(string dataPropertyName, string headerText)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn
            {
                DataPropertyName = dataPropertyName,
                HeaderText = headerText
            };
            dataGridViewUsers.Columns.Add(column);
        }

        private void dataGridViewUsers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                UserDTO user = (UserDTO)dataGridViewUsers.Rows[e.RowIndex].DataBoundItem;
                EditUser(user);
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewUsers.SelectedCells.Count > 0)
            {
                int rowIndex = dataGridViewUsers.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridViewUsers.Rows[rowIndex];
                UserDTO selectedUser = (UserDTO)selectedRow.DataBoundItem;
                var id = selectedUser.Id;
                UserServiceClient service = new UserServiceClient();
                if (await service.DeleteUserAsync(id) == 1)
                {
                    MessageBox.Show("Користувача успішно видалено");
                }
            }
            else
            {
                MessageBox.Show("Будь ласка оберіть рядок");
            }
            LoadDataAsync();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            if (addUserForm == null)
            {
                addUserForm = new AddUserForm();
                addUserForm.FormClosed += addUserForm_FormClosed;
            }

            addUserForm.Show(this);
        }

        void addUserForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            addUserForm = null;
            Show();
            LoadDataAsync();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridViewUsers.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridViewUsers.Rows[rowIndex];
            UserDTO selectedUser = (UserDTO)selectedRow.DataBoundItem;
            EditUser(selectedUser);
        }

        private void EditUser(UserDTO selectedUser)
        {
            if (editUserForm == null)
            {
                editUserForm = new EditUserForm();
            }
            editUserForm.LoadUser(selectedUser);
            editUserForm.ShowDialog(this);

            editUserForm.Dispose();
            editUserForm = null;
            LoadDataAsync();
        }
    }
}
