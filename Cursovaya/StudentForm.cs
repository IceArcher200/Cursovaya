using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cursovaya
{
    public partial class StudentForm : Form
    {
        DataBase dataStore = DataBase.GetInstance();
        private Panel buttonPanel = new Panel();
        private DataGridView eventsDataGridView = new DataGridView();
        private Button showExams = new Button();
        private ListBox personBox = new ListBox();
        public StudentForm()
        {
            InitializeComponent();
            this.Text = "Расписание сессии";
            this.Load += new EventHandler(StudentForm_Load);
            
        }
        private void StudentForm_Load(System.Object sender, System.EventArgs e)
        {
            SetupLayout();
            SetupDataGridView();
            PopulateDataGridView();
        }

        private void showExams_Click(object sender, EventArgs e)
        {
            this.eventsDataGridView.Rows.Clear();
            List<Event> events = dataStore.Get();
            if (events.Count != 0)
            {
                Student student = (Student)personBox.SelectedItem;
                for (int i = 0; i < events.Count; i++)
                {
                    foreach (Group groupE in events[i].Groups)
                        if (groupE.Name == student.Group)
                        this.eventsDataGridView.Rows.Add(events[i].GetName(),events[i].Subject, events[i].FullName, events[i].Date,events[i].Room);
                }
            }
        }

        private void SetupLayout()
        {
            this.Size = new Size(700, 500);

            showExams.Text = "Show exams";
            showExams.Location = new Point(10, 10);
            showExams.Click += new EventHandler(showExams_Click);

            personBox.Location = new Point(510, 0);
            List<Student> students = new List<Student> { new Student("Владимир Ут", "ABT-111"), new Student("Уауа", "АВТ-333")};
            personBox.DisplayMember = "FullName";
            personBox.DataSource = students;

            buttonPanel.Controls.Add(showExams);
            buttonPanel.Height = 50;
            buttonPanel.Dock = DockStyle.Bottom;

            this.Controls.Add(this.buttonPanel);
            this.Controls.Add(this.personBox);
        }

        private void SetupDataGridView()
        {
            this.Controls.Add(eventsDataGridView);

            eventsDataGridView.ColumnCount = 5;

            eventsDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            eventsDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            eventsDataGridView.ColumnHeadersDefaultCellStyle.Font =
                new Font(eventsDataGridView.Font, FontStyle.Bold);

            eventsDataGridView.Name = "eventsDataGridView";
            eventsDataGridView.Location = new Point(8, 8);
            eventsDataGridView.Size = new Size(500, 250);
            eventsDataGridView.AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            eventsDataGridView.AutoSizeColumnsMode =
        DataGridViewAutoSizeColumnsMode.AllCells;
            eventsDataGridView.ColumnHeadersBorderStyle =
                DataGridViewHeaderBorderStyle.Single;
            eventsDataGridView.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            eventsDataGridView.GridColor = Color.Black;
            eventsDataGridView.RowHeadersVisible = false;

            eventsDataGridView.Columns[0].Name = "";
            eventsDataGridView.Columns[1].Name = "Предмет";
            eventsDataGridView.Columns[2].Name = "Преподаватель";
            eventsDataGridView.Columns[3].Name = "Дата";
            eventsDataGridView.Columns[4].Name = "Кабинет";


            eventsDataGridView.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            eventsDataGridView.MultiSelect = false;
            eventsDataGridView.Dock = DockStyle.Fill;
            eventsDataGridView.ReadOnly = true;
        }

        private void PopulateDataGridView()
        {
            eventsDataGridView.Columns[0].DisplayIndex = 0;
            eventsDataGridView.Columns[1].DisplayIndex = 1;
            eventsDataGridView.Columns[2].DisplayIndex = 2;
        }
    }
}
