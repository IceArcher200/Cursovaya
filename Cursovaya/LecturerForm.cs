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
        private Label personLabel = new Label();

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
            string groupTemp = "";
            List<Event> events = dataStore.Get();
            if (events.Count != 0)
            {
                Lecturer lecturer = (Lecturer)personBox.SelectedItem;
                for (int i = 0; i < events.Count; i++)
                {
                    if (events[i].FullName == lecturer.FullName)
                    {
                        groupTemp = "";
                        foreach (Group group in events[i].Groups)
                            groupTemp += group.Name + " ";
                        this.eventsDataGridView.Rows.Add(events[i].GetName(),events[i].Subject, events[i].FullName, events[i].Date + " - " + events[i].GetEndTime(), groupTemp, events[i].Room.Number);
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

                string date = this.eventsDataGridView.CurrentRow.Cells[3].Value.ToString();
                DateTime d1 = DateTime.ParseExact(date.Split('-')[0], "dd.MM.yyyy HH:mm:ss ",
                                       System.Globalization.CultureInfo.InvariantCulture);
                string subject = this.eventsDataGridView.CurrentRow.Cells[2].Value.ToString();
                string groups = this.eventsDataGridView.CurrentRow.Cells[4].Value.ToString();
                List<string> groupList = new List<string>();
                foreach (string group in groups.Split(' '))
                {
                    groupList.Add(group);
                }
                groupList.Remove(groupList.Last());
                lecturer.RemoveEvent(d1, subject, groupList);
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
            this.Size = new Size(800, 500);

            showExams.Text = "Показать рассписание";
            showExams.Location = new Point(10, 10);
            showExams.Click += new EventHandler(showExams_Click);

            deleteRowButton.Text = "Удалить выбранное событие";
            deleteRowButton.Location = new Point(100, 10);
            deleteRowButton.Click += new EventHandler(deleteRowButton_Click);

            addEventButton.Text = "Добавить событие";
            addEventButton.Location = new Point(190, 10);
            addEventButton.Click += new EventHandler(addEventButton_Click);
            List<Group> groups = new List<Group> { new Group("ABT-111",30), new Group("ABT-122", 30),
                new Group("ABT-133", 30),new Group("ABT-144",30),new Group("ABT-155",30),new Group("ABT-166",30) };

            List<string> subjects1 = new List<string> { "Линейная алгебра", "Математический анализ", "Высшая математика" };
            List<string> subjects2 = new List<string> { "ОПД", "ОИБ", "Компьютерная графика" };
            List<string> subjects3 = new List<string> { "Правоведение", "Философия", "Физика" };

            Lecturer Eugene = new Lecturer("Иванов Иван", groups, subjects1);
            Lecturer Mark = new Lecturer("Марков Марк", groups, subjects2);
            groups.Remove(groups.Last());
            Lecturer Andrey = new Lecturer("Андронов Андрей", groups, subjects3);
            List<Lecturer> lecturers = new List<Lecturer> { Eugene, Mark, Andrey };

            personBox.Location = new Point(550, 300);
            personBox.DisplayMember = "FullName";
            personBox.DataSource = lecturers;

            personLabel.Location = new Point(550, 270);
            personLabel.Text = "Преподаватель";
            personLabel.TextAlign = ContentAlignment.MiddleCenter;

            buttonPanel.Controls.Add(showExams);
            buttonPanel.Controls.Add(deleteRowButton);
            buttonPanel.Controls.Add(addEventButton);
            buttonPanel.Height = 50;
            buttonPanel.Dock = DockStyle.Bottom;

            this.Controls.Add(this.buttonPanel);
            this.Controls.Add(this.personBox);
            this.Controls.Add(this.personLabel);
        }

        private void SetupDataGridView()
        {
            this.Controls.Add(eventsDataGridView);

            eventsDataGridView.ColumnCount = 6;

            eventsDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            eventsDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            eventsDataGridView.ColumnHeadersDefaultCellStyle.Font =
                new Font(eventsDataGridView.Font, FontStyle.Bold);

            eventsDataGridView.Name = "Расписание сессии";
            eventsDataGridView.Location = new Point(8, 8);
            eventsDataGridView.Size = new Size(500, 250);
            eventsDataGridView.AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            eventsDataGridView.ColumnHeadersBorderStyle =
                DataGridViewHeaderBorderStyle.Single;
            eventsDataGridView.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            eventsDataGridView.GridColor = Color.Black;
            eventsDataGridView.RowHeadersVisible = false;

            eventsDataGridView.Columns[0].Name = "";
            eventsDataGridView.Columns[1].Name = "Предмет";
            eventsDataGridView.Columns[2].Name = "Преподаватель";
            eventsDataGridView.Columns[3].Name = "Дата. Время начала - время окончания";
            eventsDataGridView.Columns[4].Name = "Группы";
            eventsDataGridView.Columns[5].Name = "Кабинет";

            eventsDataGridView.AutoSizeColumnsMode =
        DataGridViewAutoSizeColumnsMode.AllCells;
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
            eventsDataGridView.Columns[4].DisplayIndex = 4;
        }


    }
}

