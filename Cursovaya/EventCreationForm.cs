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
        private Button addEventButton = new Button();
        private DateTimePicker dateTimePicker = new DateTimePicker();
        private CheckedListBox groupBox = new CheckedListBox();
        private Lecturer lecturer;

        public EventCreationForm(object l)
        {
            InitializeComponent();
            lecturer = (Lecturer)l;
            SetupLayout();
        }

        private void SetupLayout()
        {
            this.Size = new Size(500, 500);

            subjectLabel.Location = new Point(200, 320);
            subjectLabel.TextAlign = ContentAlignment.MiddleCenter;
            subjectLabel.Text = "Предмет";

            groupBox.Location = new Point(300, 150);
            groupBox.CheckOnClick = true;
            foreach (string group in lecturer.Groups)
                groupBox.Items.Add(group);
            

            subjectBox.Location = new Point(300, 300);
            foreach (string subject in lecturer.Subjects)
                subjectBox.Items.Add(subject);
            //subjectBox.CheckOnClick = true;

            addEventButton.Text = "Add Event";
            addEventButton.Location = new Point(190, 400);
            addEventButton.Click += new EventHandler(addEventButton_Click);

            dateTimePicker.Location = new Point(50, 50);

            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.addEventButton);
            this.Controls.Add(this.subjectLabel);
            this.Controls.Add(this.subjectBox);
            this.Controls.Add(this.dateTimePicker);
        }

        private void addEventButton_Click(object sender, EventArgs e)
        {
            if (/*fioBox.Text == "" || */subjectBox.Text == "" || groupBox.CheckedItems.Count == 0) ; // Сделать предупреждение
            else
            {
                List<string> groups = new List<string>();
                foreach (string item in groupBox.CheckedItems)
                {
                    groups.Add(item);
                }

                lecturer.SetExam(dateTimePicker.Text, subjectBox.Text, groups);
               
            }
        }

    }
}
