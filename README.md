# Practical Work #8: Windows Forms Data Management & Visualization

A C# Windows Forms application developed as part of Practical Work #8 (Computer Practice #4). The program demonstrates techniques for handling tabular data using `DataGridView`, managing memory caching via `DataTable` and `DataSet`, persisting data to `XML` files, and dynamic data visualization using the `Chart` component.

---

##  Features

* **Tabular Data Entry & Editing:** Interactive grid (`DataGridView`) for viewing and modifying dataset rows in real time.
* **Mathematical Calculation:** Automatically calculates function values $z_i = F(x_i, y_i)$ for $n = 14$ iterations according to Variant 1:
  $$F(x_i, y_i) = \frac{\sin^2(x_i) - \cos^2(y_i)}{\cos(x_i)}$$
  *Parameters:* $x_0 = 2.8, \Delta x = 2.1, y_0 = 1.7, \Delta y = 0.3$.
* **XML Data Persistence:** Save and open dataset files (`.xml`) seamlessly using `DataSet.WriteXml()` and `DataSet.ReadXml()` via standard dialogs (`SaveFileDialog`, `OpenFileDialog`).
* **Dynamic Chart Visualization:** Render $z_i = F(x_i)$ plots using `System.Windows.Forms.DataVisualization.Charting`.
* **Chart Type Switcher:** Instant switching between chart types via `ComboBox`:
  * Point / Scatter
  * Line
  * Spline
  * Bar (Horizontal)
  * Column (Vertical)
* **User Interface:** Full navigation setup including `MenuStrip`, `ToolStrip`, and `ContextMenuStrip`.

---

##  Tech Stack & Requirements

* **Language:** C#
* **Framework:** .NET 6.0+ (Windows Forms)
* **Packages:**
  * `System.Windows.Forms.DataVisualization` (Pre-release)
  * `System.Data.SqlClient`

---

##  Installation & Running via Git Bash

1. **Clone or navigate to the project directory:**
   ```bash
   cd Surname_KPR4
Restore dependencies:

Bash
dotnet restore

Bash
dotnet run
