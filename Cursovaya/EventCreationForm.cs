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
        private Label subjectLabel = new Label();
        private Label dateTimeLabel = new Label();
        private Label groupLabel = new Label();
        private Button addEventButton = new Button();
        private DateTimePicker dateTimePicker = new DateTimePicker();
        private CheckedListBox groupBox = new CheckedListBox();
        private ComboBox roomBox = new ComboBox();
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
            this.Size = new Size(500, 700);

            subjectLabel.Location = new Point(100, 200);
            subjectLabel.TextAlign = ContentAlignment.MiddleCenter;
            subjectLabel.Text = "Предмет:";

            subjectBox.Location = new Point(200, 200);
            foreach (string subject in lecturer.Subjects)
                subjectBox.Items.Add(subject);

            groupLabel.Location = new Point(100, 100);
            groupLabel.TextAlign = ContentAlignment.MiddleCenter;
            groupLabel.Text = "Группы:";

            groupBox.Location = new Point(200, 100);
            groupBox.CheckOnClick = true;
            foreach (string group in lecturer.Groups)
                groupBox.Items.Add(group);

            dateTimePicker.Location = new Point(200, 50);
            dateTimePicker.Format = DateTimePickerFormat.Custom;
            dateTimePicker.CustomFormat = "dd.MM.yyyy HH:mm";

            dateTimeLabel.Location = new Point(100, 50);
            dateTimeLabel.TextAlign = ContentAlignment.MiddleCenter;
            dateTimeLabel.Text = "Дата и время экзамена:";

            roomBox.Location = new Point(100, 500);

            addEventButton.Text = "Add Event";
            addEventButton.Location = new Point(190, 400);
            addEventButton.Click += new EventHandler(addEventButton_Click);


            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.addEventButton);
            this.Controls.Add(this.subjectLabel);
            this.Controls.Add(this.subjectBox);
            this.Controls.Add(this.dateTimeLabel);
            this.Controls.Add(this.dateTimePicker);
            this.Controls.Add(this.groupLabel);
            this.Controls.Add(this.roomBox);
        }

        private void addEventButton_Click(object sender, EventArgs e)
        {
            if (groupBox.CheckedItems.Count == 0)
                MessageBox.Show("Ни одна из групп не выбрана");
            else if (subjectBox.SelectedItems.Count == 0)
                MessageBox.Show("Не выбран предмет экзамена");
            else if (dateTimePicker.Value.Hour >= 19 || dateTimePicker.Value.Hour < 8)
                MessageBox.Show("Выберите другое время. Экзамен может проводиться с 8:00 до 19:00");
            else
            {
                List<string> groups = new List<string>();
                foreach (string item in groupBox.CheckedItems)
                {
                    groups.Add(item);
                }
                try
                { lecturer.SetExam(dateTimePicker.Text, subjectBox.Text, groups); }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }

            }
        }

    }
}
