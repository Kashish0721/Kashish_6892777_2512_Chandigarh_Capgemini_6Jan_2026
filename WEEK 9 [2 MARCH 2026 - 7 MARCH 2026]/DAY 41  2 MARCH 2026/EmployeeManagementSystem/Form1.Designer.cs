namespace EmployeeManagementSystem
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtEmpId = new TextBox();
            txtEmpSal = new TextBox();
            txtEmpDOJ = new TextBox();
            txtEmpDesig = new TextBox();
            txtEmpName = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            btnInsert = new Button();
            btnUpdate = new Button();
            btnClear = new Button();
            btnFind = new Button();
            btnClose = new Button();
            btnDelete = new Button();
            dataGridView1 = new DataGridView();
            label6 = new Label();
            txtDeptNo = new TextBox();
            btnUpdateDB = new Button();
            cmbDepartment = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // txtEmpId
            // 
            txtEmpId.Location = new Point(156, 34);
            txtEmpId.Name = "txtEmpId";
            txtEmpId.Size = new Size(125, 27);
            txtEmpId.TabIndex = 0;
            // 
            // txtEmpSal
            // 
            txtEmpSal.Location = new Point(156, 227);
            txtEmpSal.Name = "txtEmpSal";
            txtEmpSal.Size = new Size(125, 27);
            txtEmpSal.TabIndex = 1;
            // 
            // txtEmpDOJ
            // 
            txtEmpDOJ.Location = new Point(156, 179);
            txtEmpDOJ.Name = "txtEmpDOJ";
            txtEmpDOJ.Size = new Size(125, 27);
            txtEmpDOJ.TabIndex = 2;
            // 
            // txtEmpDesig
            // 
            txtEmpDesig.Location = new Point(156, 129);
            txtEmpDesig.Name = "txtEmpDesig";
            txtEmpDesig.Size = new Size(125, 27);
            txtEmpDesig.TabIndex = 3;
            // 
            // txtEmpName
            // 
            txtEmpName.Location = new Point(156, 78);
            txtEmpName.Name = "txtEmpName";
            txtEmpName.Size = new Size(125, 27);
            txtEmpName.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(51, 41);
            label1.Name = "label1";
            label1.Size = new Size(58, 20);
            label1.TabIndex = 5;
            label1.Text = "Emp ID";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(49, 85);
            label2.Name = "label2";
            label2.Size = new Size(83, 20);
            label2.TabIndex = 6;
            label2.Text = "Emp Name";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(51, 136);
            label3.Name = "label3";
            label3.Size = new Size(81, 20);
            label3.TabIndex = 7;
            label3.Text = "Emp Desig";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(51, 186);
            label4.Name = "label4";
            label4.Size = new Size(70, 20);
            label4.TabIndex = 8;
            label4.Text = "Emp DOJ";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(51, 234);
            label5.Name = "label5";
            label5.Size = new Size(83, 20);
            label5.TabIndex = 9;
            label5.Text = "Emp Salary";
            // 
            // btnInsert
            // 
            btnInsert.Location = new Point(82, 327);
            btnInsert.Name = "btnInsert";
            btnInsert.Size = new Size(94, 29);
            btnInsert.TabIndex = 10;
            btnInsert.Text = "Insert";
            btnInsert.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(82, 387);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(94, 29);
            btnUpdate.TabIndex = 11;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(287, 327);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(94, 29);
            btnClear.TabIndex = 12;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            // 
            // btnFind
            // 
            btnFind.Location = new Point(187, 327);
            btnFind.Name = "btnFind";
            btnFind.Size = new Size(94, 29);
            btnFind.TabIndex = 13;
            btnFind.Text = "Find";
            btnFind.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            btnClose.Location = new Point(287, 387);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(94, 29);
            btnClose.TabIndex = 14;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(187, 387);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(94, 29);
            btnDelete.TabIndex = 15;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(361, 54);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(300, 188);
            dataGridView1.TabIndex = 17;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(51, 282);
            label6.Name = "label6";
            label6.Size = new Size(96, 20);
            label6.TabIndex = 18;
            label6.Text = "Emp DeptNo";
            // 
            // txtDeptNo
            // 
            txtDeptNo.Location = new Point(156, 275);
            txtDeptNo.Name = "txtDeptNo";
            txtDeptNo.Size = new Size(125, 27);
            txtDeptNo.TabIndex = 19;
            // 
            // btnUpdateDB
            // 
            btnUpdateDB.Location = new Point(401, 352);
            btnUpdateDB.Name = "btnUpdateDB";
            btnUpdateDB.Size = new Size(200, 29);
            btnUpdateDB.TabIndex = 20;
            btnUpdateDB.Text = "Update into Database";
            btnUpdateDB.UseVisualStyleBackColor = true;
            // 
            // cmbDepartment
            // 
            cmbDepartment.FormattingEnabled = true;
            cmbDepartment.Location = new Point(401, 264);
            cmbDepartment.Name = "cmbDepartment";
            cmbDepartment.Size = new Size(151, 28);
            cmbDepartment.TabIndex = 21;
           
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(cmbDepartment);
            Controls.Add(btnUpdateDB);
            Controls.Add(txtDeptNo);
            Controls.Add(label6);
            Controls.Add(dataGridView1);
            Controls.Add(btnDelete);
            Controls.Add(btnClose);
            Controls.Add(btnFind);
            Controls.Add(btnClear);
            Controls.Add(btnUpdate);
            Controls.Add(btnInsert);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtEmpName);
            Controls.Add(txtEmpDesig);
            Controls.Add(txtEmpDOJ);
            Controls.Add(txtEmpSal);
            Controls.Add(txtEmpId);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtEmpId;
        private TextBox txtEmpSal;
        private TextBox txtEmpDOJ;
        private TextBox txtEmpDesig;
        private TextBox txtEmpName;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Button btnInsert;
        private Button btnUpdate;
        private Button btnClear;
        private Button btnFind;
        private Button btnClose;
        private Button btnDelete;
        private DataGridView dataGridView1;
        private Label label6;
        private TextBox txtDeptNo;
        private Button btnUpdateDB;
        private ComboBox cmbDepartment;
    }
}
