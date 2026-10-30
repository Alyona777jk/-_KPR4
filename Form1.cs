using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Surname_KPR4
{
    public partial class Form1 : Form
    {
        private DataTable dataTable;
        private DataSet dataSet;

        private MenuStrip menuStrip;
        private ToolStrip toolStrip;
        private ContextMenuStrip contextMenuStrip;
        private DataGridView dataGridView1;
        private Chart chart1;
        private ComboBox comboBoxChartType;
        private Label labelChartType;

        public Form1()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            this.Text = "Практична робота №8 - Прізвище_КПР4";
            this.Width = 900;
            this.Height = 650;
            this.StartPosition = FormStartPosition.CenterScreen;

            // 1. MenuStrip
            menuStrip = new MenuStrip();
            ToolStripMenuItem fileMenu = new ToolStripMenuItem("Файл");
            ToolStripMenuItem calcMenu = new ToolStripMenuItem("Обчислення");

            ToolStripMenuItem itemCalculate = new ToolStripMenuItem("Розрахувати", null, MenuCalculate_Click);
            ToolStripMenuItem itemSave = new ToolStripMenuItem("Зберегти в XML...", null, MenuSave_Click);
            ToolStripMenuItem itemOpen = new ToolStripMenuItem("Відкрити з XML...", null, MenuOpen_Click);
            ToolStripMenuItem itemExit = new ToolStripMenuItem("Вихід", null, (s, e) => Application.Exit());

            fileMenu.DropDownItems.Add(itemOpen);
            fileMenu.DropDownItems.Add(itemSave);
            fileMenu.DropDownItems.Add(new ToolStripSeparator());
            fileMenu.DropDownItems.Add(itemExit);

            calcMenu.DropDownItems.Add(itemCalculate);

            menuStrip.Items.Add(fileMenu);
            menuStrip.Items.Add(calcMenu);
            this.MainMenuStrip = menuStrip;
            this.Controls.Add(menuStrip);

            // 2. ToolStrip
            toolStrip = new ToolStrip();
            ToolStripButton btnCalculate = new ToolStripButton("Обчислити", null, MenuCalculate_Click);
            ToolStripButton btnOpen = new ToolStripButton("Відкрити XML", null, MenuOpen_Click);
            ToolStripButton btnSave = new ToolStripButton("Зберегти XML", null, MenuSave_Click);

            toolStrip.Items.Add(btnCalculate);
            toolStrip.Items.Add(btnOpen);
            toolStrip.Items.Add(btnSave);
            this.Controls.Add(toolStrip);

            // 3. ContextMenuStrip
            contextMenuStrip = new ContextMenuStrip();
            contextMenuStrip.Items.Add("Обчислити дані", null, MenuCalculate_Click);
            contextMenuStrip.Items.Add("Зберегти у XML", null, MenuSave_Click);
            contextMenuStrip.Items.Add("Відкрити з XML", null, MenuOpen_Click);
            this.ContextMenuStrip = contextMenuStrip;

            // 4. ComboBox
            labelChartType = new Label() { Text = "Тип:", Top = 60, Left = 12, Width = 40 };
            comboBoxChartType = new ComboBox() { Top = 57, Left = 55, Width = 150, DropDownStyle = ComboBoxStyle.DropDownList };
            comboBoxChartType.Items.AddRange(new string[] { "Точкова", "Графік", "Згладжений графік", "Стовпчаста", "Гістограма" });
            comboBoxChartType.SelectedIndex = 1;
            comboBoxChartType.SelectedIndexChanged += ComboBoxChartType_SelectedIndexChanged;

            this.Controls.Add(labelChartType);
            this.Controls.Add(comboBoxChartType);

            // 5. DataGridView
            dataGridView1 = new DataGridView();
            dataGridView1.Left = 12;
            dataGridView1.Top = 90;
            dataGridView1.Width = 380;
            dataGridView1.Height = 480;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            this.Controls.Add(dataGridView1);

            // 6. Chart
            chart1 = new Chart();
            chart1.Left = 410;
            chart1.Top = 90;
            chart1.Width = 460;
            chart1.Height = 480;
            chart1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            ChartArea chartArea = new ChartArea("MainArea");
            chart1.ChartAreas.Add(chartArea);

            Series series = new Series("Series1");
            series.ChartType = SeriesChartType.Line;
            series.BorderWidth = 2;
            series.Color = Color.Blue;
            chart1.Series.Add(series);

            Title title = new Title("Залежність z_i = F(x_i)");
            chart1.Titles.Add(title);

            this.Controls.Add(chart1);

            InitDataSet();
        }

        private void InitDataSet()
        {
            dataTable = new DataTable("ResultTable");
            dataTable.Columns.Add("i", typeof(int));
            dataTable.Columns.Add("X", typeof(double));
            dataTable.Columns.Add("Y", typeof(double));
            dataTable.Columns.Add("Z", typeof(double));

            dataSet = new DataSet("CalculationDataSet");
            dataSet.Tables.Add(dataTable);

            dataGridView1.DataSource = dataTable;
        }

        private void MenuCalculate_Click(object sender, EventArgs e)
        {
            dataTable.Rows.Clear();

            int n = 14;
            double x = 2.8;
            double dx = 2.1;
            double y = 1.7;
            double dy = 0.3;

            double sumZ = 0;

            for (int i = 1; i <= n; i++)
            {
                double sinX = Math.Sin(x);
                double cosY = Math.Cos(y);
                double cosX = Math.Cos(x);

                double z = (Math.Abs(cosX) > 0.00001) ? (Math.Pow(sinX, 2) - Math.Pow(cosY, 2)) / cosX : 0;

                dataTable.Rows.Add(i, Math.Round(x, 4), Math.Round(y, 4), Math.Round(z, 4));

                sumZ += z;

                x += dx;
                y += dy;
            }

            UpdateChart();

            MessageBox.Show($"Розрахунок завершено!\nСума всіх значень Z: {Math.Round(sumZ, 4)}", 
                            "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void UpdateChart()
        {
            try
            {
                chart1.Series["Series1"].Points.Clear();
                
                foreach (DataRow row in dataTable.Rows)
                {
                    if (row["X"] != DBNull.Value && row["Z"] != DBNull.Value)
                    {
                        double xVal = Convert.ToDouble(row["X"]);
                        double zVal = Convert.ToDouble(row["Z"]);
                        chart1.Series["Series1"].Points.AddXY(xVal, zVal);
                    }
                }
                chart1.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка побудови графіка: " + ex.Message);
            }
        }

        private void ComboBoxChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chart1 == null || chart1.Series.Count == 0) return;

            switch (comboBoxChartType.SelectedIndex)
            {
                case 0:
                    chart1.Series["Series1"].ChartType = SeriesChartType.Point;
                    break;
                case 1:
                    chart1.Series["Series1"].ChartType = SeriesChartType.Line;
                    break;
                case 2:
                    chart1.Series["Series1"].ChartType = SeriesChartType.Spline;
                    break;
                case 3:
                    chart1.Series["Series1"].ChartType = SeriesChartType.Column;
                    break;
                case 4:
                    chart1.Series["Series1"].ChartType = SeriesChartType.Bar;
                    break;
            }
        }

        private void MenuSave_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "XML файли (*.xml)|*.xml|Всі файли (*.*)|*.*";
                saveFileDialog.Title = "Зберегти таблицю у форматі XML";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    dataSet.WriteXml(saveFileDialog.FileName);
                    MessageBox.Show("Файл успішно збережено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void MenuOpen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "XML файли (*.xml)|*.xml|Всі файли (*.*)|*.*";
                openFileDialog.Title = "Відкрити XML файл";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    dataSet.Clear();
                    dataSet.ReadXml(openFileDialog.FileName);

                    if (dataSet.Tables.Count > 0)
                    {
                        dataTable = dataSet.Tables[0];
                        dataGridView1.DataSource = dataTable;
                        UpdateChart();
                    }

                    MessageBox.Show("Дані успішно завантажено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
