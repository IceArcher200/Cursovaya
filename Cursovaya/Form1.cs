﻿using System;
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
    public partial class Form1 : Form
    {
        DataBase dataStore = DataBase.GetInstance();
        private Panel buttonPanel = new Panel();
        private DataGridView eventsDataGridView = new DataGridView();
        private Button addNewRowButton = new Button();
        private Button deleteRowButton = new Button();
        private Button addEventButton = new Button();
        private TextBox fioBox = new TextBox();
        private TextBox subjectBox = new TextBox();
        private Label fioLabel = new Label();
        private Label subjectLabel = new Label();
        private DateTimePicker dateTimePicker = new DateTimePicker();
        private CheckedListBox groupBox = new CheckedListBox();

        public Form1()
        {
            InitializeComponent();
            this.Load += new EventHandler(Form1_Load);
        }
        private void Form1_Load(System.Object sender, System.EventArgs e)
        {
            SetupLayout();
            SetupDataGridView();
            PopulateDataGridView();
        }


        private void addNewRowButton_Click(object sender, EventArgs e)
        {
            this.eventsDataGridView.Rows.Clear();
            List<Event> events = dataStore.Get();
            if (events.Count != 0)
            {
                List<string> groups = new List<string>();
                foreach (string item in groupBox.CheckedItems)
                {
                    groups.Add(item);
                }
                Lecturer Eugene = new Lecturer(fioBox.Text, groups);
                for(int i = 0; i < events.Count; i++)
                {
                    if (events[i].FullName == Eugene.FullName)
                        this.eventsDataGridView.Rows.Add(events[i].Subject, events[i].FullName, events[i].Date, events[i].Groups);
                }
                
            }
        }

        private void deleteRowButton_Click(object sender, EventArgs e)
        {
            if (this.eventsDataGridView.SelectedRows.Count > 0 &&
                this.eventsDataGridView.SelectedRows[0].Index !=
                this.eventsDataGridView.Rows.Count - 1)
            {
                this.eventsDataGridView.Rows.RemoveAt(
                    this.eventsDataGridView.SelectedRows[0].Index);
            }
        }

        private void addEventButton_Click(object sender, EventArgs e)
        {
            if (fioBox.Text == "" || subjectBox.Text == "" || groupBox.CheckedItems.Count == 0); // Сделать предупреждение
            else
            {
                List<string> groups = new List<string>();
                foreach(string item in groupBox.CheckedItems)
                {
                    groups.Add(item);
                }
                
                Lecturer Eugene = new Lecturer(fioBox.Text, groups);
                Eugene.SetExam(dateTimePicker.Text, subjectBox.Text, groups);
            }
        }

        private void SetupLayout()
        {
            this.Size = new Size(900, 500);

            addNewRowButton.Text = "Show exams";
            addNewRowButton.Location = new Point(10, 10);
            addNewRowButton.Click += new EventHandler(addNewRowButton_Click);

            deleteRowButton.Text = "Delete Row";
            deleteRowButton.Location = new Point(100, 10);
            deleteRowButton.Click += new EventHandler(deleteRowButton_Click);

            addEventButton.Text = "Add Event";
            addEventButton.Location = new Point(190, 10);
            addEventButton.Click += new EventHandler(addEventButton_Click);
            
            fioBox.Location = new Point(10, 350);
            subjectBox.Location = new Point(210, 350);

            fioLabel.Location = new Point(10, 320);
            fioLabel.TextAlign = ContentAlignment.MiddleCenter;
            fioLabel.Text = "ФИО";

            subjectLabel.Location = new Point(210, 320);
            subjectLabel.TextAlign = ContentAlignment.MiddleCenter;
            subjectLabel.Text = "Предмет";

            groupBox.Location = new Point(410, 300);
            groupBox.CheckOnClick = true;
            
            groupBox.Items.AddRange(new string[] {"ABT-111","ABT-222","ABT-333","ABT-444","ABT-555", "ABT-666", "ABT-777" });

            buttonPanel.Controls.Add(addNewRowButton);
            buttonPanel.Controls.Add(deleteRowButton);
            buttonPanel.Controls.Add(addEventButton);
            buttonPanel.Height = 50;
            buttonPanel.Dock = DockStyle.Bottom;

            dateTimePicker.Location = new Point(60, 380);

            this.Controls.Add(this.buttonPanel);
            this.Controls.Add(this.fioBox);
            this.Controls.Add(this.subjectBox);
            this.Controls.Add(this.fioLabel);
            this.Controls.Add(this.subjectLabel);
            this.Controls.Add(this.dateTimePicker);
            this.Controls.Add(this.groupBox);
        }

        private void SetupDataGridView()
        {
            this.Controls.Add(eventsDataGridView);

            eventsDataGridView.ColumnCount = 4;

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

