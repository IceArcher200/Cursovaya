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
    public partial class EventCreationForm : Form
    {

        private ListBox subjectBox = new ListBox();
        private ListBox typeBox = new ListBox();
        private Label subjectLabel = new Label();
        private Label roomLabel = new Label();
        private Label typeLabel = new Label();
        private Label dateTimeLabel = new Label();
        private Label groupLabel = new Label();
        private Button addEventButton = new Button();
        private DateTimePicker dateTimePicker = new DateTimePicker();
        private CheckedListBox groupBox = new CheckedListBox();
        private ListBox roomBox = new ListBox();
        private Lecturer lecturer;

        public EventCreationForm(object l)
        {
            InitializeComponent();
            this.Text = "Добавление экзамена";
            lecturer = (Lecturer)l;
            SetupLayout();
        }

        private void SetupLayout()
        {
            this.Size = new Size(550, 300);

            subjectLabel.Location = new Point(5, 0);
            subjectLabel.TextAlign = ContentAlignment.MiddleCenter;
            subjectLabel.Text = "Предмет:";

            subjectBox.Location = new Point(5, 30);
            foreach (string subject in lecturer.Subjects)
                subjectBox.Items.Add(subject);

            groupLabel.Location = new Point(130, 0);
            groupLabel.TextAlign = ContentAlignment.MiddleCenter;
            groupLabel.Text = "Группы:";

            groupBox.Location = new Point(130, 30);
            groupBox.CheckOnClick = true;
            foreach (string group in lecturer.Groups)
                groupBox.Items.Add(group);

            roomLabel.Location = new Point(380, 0);
            roomLabel.TextAlign = ContentAlignment.MiddleCenter;
            roomLabel.Text = "Аудитория:";

            typeLabel.Location = new Point(380, 0);
            typeLabel.TextAlign = ContentAlignment.MiddleCenter;
            typeLabel.Text = "Тип:";

            typeBox.Location = new Point(380, 30);
            typeBox.Items.Add("Консультация");
            typeBox.Items.Add("Экзамен");

            dateTimePicker.Location = new Point(5, 170);
            dateTimePicker.Format = DateTimePickerFormat.Custom;
            dateTimePicker.CustomFormat = "dd.MM.yyyy HH:mm";

            dateTimeLabel.Location = new Point(5, 145);
            dateTimeLabel.Text = "Дата и время экзамена:";

            roomLabel.Location = new Point(255, 0);
            roomLabel.TextAlign = ContentAlignment.MiddleCenter;
            roomLabel.Text = "Аудитория:";

            List<string> rooms = new List<string> { "1001", "1002", "1003","1004","1005" };
            roomBox.Location = new Point(255, 30);
            foreach (string room in rooms)
                roomBox.Items.Add(room);

            addEventButton.Text = "Добавить событие";
            addEventButton.Location = new Point(250, 170);
            addEventButton.Click += new EventHandler(addEventButton_Click);


            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.addEventButton);
            this.Controls.Add(this.subjectLabel);
            this.Controls.Add(this.subjectBox);
            this.Controls.Add(this.dateTimeLabel);
            this.Controls.Add(this.dateTimePicker);
            this.Controls.Add(this.groupLabel);
            this.Controls.Add(this.typeLabel);
            this.Controls.Add(this.roomLabel);
            this.Controls.Add(this.roomBox);
            this.Controls.Add(this.typeBox);
        }

        private void addEventButton_Click(object sender, EventArgs e)
        {
            DateTime datetime = dateTimePicker.Value;
            if (groupBox.CheckedItems.Count == 0)
                MessageBox.Show("Ни одна из групп не выбрана");
            else if (subjectBox.SelectedItems.Count == 0)
                MessageBox.Show("Не выбран предмет экзамена");
            else if (dateTimePicker.Value.Hour >= 19 || dateTimePicker.Value.Hour < 8)
                MessageBox.Show("Выберите другое время. Экзамен может проводиться с 8:00 до 19:00");
            else if (roomBox.SelectedItems.Count == 0)
                MessageBox.Show("Не выбрана аудитория");
            else if (typeBox.SelectedItems.Count == 0)
                MessageBox.Show("Не выбран тип события");
            else
            {
                List<string> groups = new List<string>();
                foreach (string item in groupBox.CheckedItems)
                {
                    groups.Add(item);
                }
                try

                {
                    if (typeBox.SelectedItem.Equals("Экзамен"))
                        lecturer.SetExam(datetime, subjectBox.Text, groups, roomBox.Text);
                    else lecturer.SetConsult(datetime, subjectBox.Text, groups, roomBox.Text);
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }

            }
        }

    }
}
