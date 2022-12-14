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
    public partial class ChooseForm : Form
    {

        private Label infoLabel = new Label();
        private Button lectureButton = new Button();
        private Button studentButton = new Button();
        public ChooseForm()
        {
            InitializeComponent();
            this.Text = "";
            SetupLayout();
        }

        private void SetupLayout()
        {
            this.Size = new Size(200, 200);
            infoLabel.Location = new Point(5, 5);
            infoLabel.TextAlign = ContentAlignment.MiddleCenter;
            infoLabel.AutoSize = true;
            infoLabel.Text = "Выберите свою категорию";

            lectureButton.Text = "Преподаватель";
            lectureButton.Location = new Point(5, 40);
            lectureButton.Click += new EventHandler(lectureButton_Click);
            lectureButton.AutoSize = true;

            studentButton.Text = "Студент";
            studentButton.Location = new Point(5, 70);
            studentButton.Click += new EventHandler(studentButton_Click);
            studentButton.AutoSize = true;

            this.Controls.Add(this.infoLabel);
            this.Controls.Add(this.lectureButton);
            this.Controls.Add(this.studentButton);
        }

        private void lectureButton_Click(object sender, EventArgs e)
        {
            LecturerForm lectureForm = new LecturerForm();
            this.Hide();
            lectureForm.FormClosed += (s, args) => this.Close();
            lectureForm.Show();
        }

        private void studentButton_Click(object sender, EventArgs e)
        {
            StudentForm studentForm = new StudentForm();
            this.Hide();
            studentForm.FormClosed += (s, args) => this.Close();
            studentForm.Show();
        }
    }
}
