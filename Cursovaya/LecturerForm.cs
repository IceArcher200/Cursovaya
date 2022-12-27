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
    public partial class LecturerForm : Form
    {
        DataBase dataStore = DataBase.GetInstance();
        private Panel buttonPanel = new Panel();
        private DataGridView eventsDataGridView = new DataGridView();
        private Button showExams = new Button();
        private Button deleteRowButton = new Button();
        private Button addEventButton = new Button();
        private ListBox personBox = new ListBox();

        public LecturerForm()
        {
            InitializeComponent();
            this.Text = "Расписание сессии";
            this.Load += new EventHandler(LecturerForm_Load);
        }
        private void LecturerForm_Load(System.Object sender, System.EventArgs e)
        {
            SetupLayout();
            SetupDataGridView();
            PopulateDataGridView();
        }


        private void showExams_Click(object sender, EventArgs e)
        {
            this.eventsDataGridView.Rows.Clear();
            string group1 = "";
            List<Event> events = dataStore.Get();
            if (events.Count != 0)
            {
                Lecturer lecturer = (Lecturer)personBox.SelectedItem;
                for (int i = 0; i < events.Count; i++)
                {
                    if (events[i].FullName == lecturer.FullName)
                    {
                        group1 = "";
                        foreach (string group in events[i].Groups)
                            group1 += group + " ";
                        this.eventsDataGridView.Rows.Add(events[i].Subject, events[i].FullName, events[i].Date, group1, events[i].Room);
                    }
                }
            }
        }

        private void deleteRowButton_Click(object sender, EventArgs e)
        {
            if (this.eventsDataGridView.SelectedRows.Count > 0 &&
                this.eventsDataGridView.SelectedRows[0].Index !=
                this.eventsDataGridView.Rows.Count - 1)
            {
                Lecturer lecturer = (Lecturer)personBox.SelectedItem;

                string date = this.eventsDataGridView.CurrentRow.Cells[2].Value.ToString();
                string subject = this.eventsDataGridView.CurrentRow.Cells[1].Value.ToString();
                string groups = this.eventsDataGridView.CurrentRow.Cells[3].Value.ToString();
                List<string> groupList = new List<string>();
                foreach (string group in groups.Split(' '))
                {
                    groupList.Add(group);
                }
                lecturer.RemoveExam(date, subject, groupList);
                this.eventsDataGridView.Rows.RemoveAt(
                    this.eventsDataGridView.SelectedRows[0].Index);
            }
        }

        private void addEventButton_Click(object sender, EventArgs e)
        {
            EventCreationForm eventCreation = new EventCreationForm((Lecturer)personBox.SelectedItem);
            eventCreation.ShowDialog();


        }

        private void SetupLayout()
        {
            this.Size = new Size(700, 500);

            showExams.Text = "Show exams";
            showExams.Location = new Point(10, 10);
            showExams.Click += new EventHandler(showExams_Click);

            deleteRowButton.Text = "Delete Row";
            deleteRowButton.Location = new Point(100, 10);
            deleteRowButton.Click += new EventHandler(deleteRowButton_Click);

            addEventButton.Text = "Add Event";
            addEventButton.Location = new Point(190, 10);
            addEventButton.Click += new EventHandler(addEventButton_Click);


            List<string> groups1 = new List<string> { "ABT-111", "ABT-222", "ABT-333", "ABT-444", "ABT-555", "ABT-666", "ABT-777" };
            List<string> groups2 = new List<string> { "ABT-110", "ABT-220", "ABT-330", "ABT-440", "ABT-550", "ABT-660", "ABT-770" };
            List<string> subjects1 = new List<string> { "Линейная алгебра", "Математический анализ", "Высшая математика" };
            List<string> subjects2 = new List<string> { "Что-то там", "Ерунда", "Компьютерная графика" };

            Lecturer Eugene = new Lecturer("Eugene", groups1, subjects1);
            Lecturer Mark = new Lecturer("Mark", groups2, subjects2);
            List<Lecturer> lecturers = new List<Lecturer> { Eugene, Mark };
            personBox.Location = new Point(510, 0);
            personBox.DisplayMember = "FullName";
            personBox.DataSource = lecturers;

            buttonPanel.Controls.Add(showExams);
            buttonPanel.Controls.Add(deleteRowButton);
            buttonPanel.Controls.Add(addEventButton);
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
            eventsDataGridView.ColumnHeadersBorderStyle =
                DataGridViewHeaderBorderStyle.Single;
            eventsDataGridView.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            eventsDataGridView.GridColor = Color.Black;
            eventsDataGridView.RowHeadersVisible = false;

            eventsDataGridView.Columns[0].Name = "Предмет";
            eventsDataGridView.Columns[1].Name = "Преподаватель";
            eventsDataGridView.Columns[2].Name = "Дата";
            eventsDataGridView.Columns[3].Name = "Группы";
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
            eventsDataGridView.Columns[3].DisplayIndex = 3;
        }


    }
}

