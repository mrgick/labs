namespace paint.Forms
{
    partial class EditForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listView = new System.Windows.Forms.ListView();
            this.columnNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnStartFigure = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.DeleteBtn = new System.Windows.Forms.Button();
            this.AddBtn = new System.Windows.Forms.Button();
            this.SaveBtn = new System.Windows.Forms.Button();
            this.OKBtn = new System.Windows.Forms.Button();
            this.ToUpBtn = new System.Windows.Forms.Button();
            this.ToDownBtn = new System.Windows.Forms.Button();
            this.ToBeginBtn = new System.Windows.Forms.Button();
            this.ToEndBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listView
            // 
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnNumber,
            this.columnType,
            this.columnStartFigure});
            this.listView.FullRowSelect = true;
            this.listView.HideSelection = false;
            this.listView.Location = new System.Drawing.Point(12, 12);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(288, 426);
            this.listView.TabIndex = 0;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
            // 
            // columnNumber
            // 
            this.columnNumber.Text = "Номер";
            // 
            // columnType
            // 
            this.columnType.Text = "Тип фигуры";
            this.columnType.Width = 100;
            // 
            // columnStartFigure
            // 
            this.columnStartFigure.Text = "Координаты начала";
            this.columnStartFigure.Width = 120;
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.Location = new System.Drawing.Point(306, 12);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(250, 426);
            this.flowLayoutPanel.TabIndex = 1;
            // 
            // DeleteBtn
            // 
            this.DeleteBtn.Enabled = false;
            this.DeleteBtn.Location = new System.Drawing.Point(562, 15);
            this.DeleteBtn.Name = "DeleteBtn";
            this.DeleteBtn.Size = new System.Drawing.Size(105, 33);
            this.DeleteBtn.TabIndex = 2;
            this.DeleteBtn.Text = "Удалить";
            this.DeleteBtn.UseVisualStyleBackColor = true;
            this.DeleteBtn.Click += new System.EventHandler(this.DeleteBtn_Click);
            // 
            // AddBtn
            // 
            this.AddBtn.Location = new System.Drawing.Point(562, 54);
            this.AddBtn.Name = "AddBtn";
            this.AddBtn.Size = new System.Drawing.Size(105, 33);
            this.AddBtn.TabIndex = 3;
            this.AddBtn.Text = "Добавить";
            this.AddBtn.UseVisualStyleBackColor = true;
            this.AddBtn.Click += new System.EventHandler(this.AddBtn_Click);
            // 
            // SaveBtn
            // 
            this.SaveBtn.Enabled = false;
            this.SaveBtn.Location = new System.Drawing.Point(562, 93);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(105, 33);
            this.SaveBtn.TabIndex = 4;
            this.SaveBtn.Text = "Сохранить";
            this.SaveBtn.UseVisualStyleBackColor = true;
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // OKBtn
            // 
            this.OKBtn.Location = new System.Drawing.Point(562, 132);
            this.OKBtn.Name = "OKBtn";
            this.OKBtn.Size = new System.Drawing.Size(105, 33);
            this.OKBtn.TabIndex = 5;
            this.OKBtn.Text = "ОК";
            this.OKBtn.UseVisualStyleBackColor = true;
            this.OKBtn.Click += new System.EventHandler(this.OKBtn_Click);
            // 
            // ToUpBtn
            // 
            this.ToUpBtn.Enabled = false;
            this.ToUpBtn.Location = new System.Drawing.Point(562, 238);
            this.ToUpBtn.Name = "ToUpBtn";
            this.ToUpBtn.Size = new System.Drawing.Size(105, 33);
            this.ToUpBtn.TabIndex = 6;
            this.ToUpBtn.Text = "На 1 вверх";
            this.ToUpBtn.UseVisualStyleBackColor = true;
            this.ToUpBtn.Click += new System.EventHandler(this.ToUpBtn_Click);
            // 
            // ToDownBtn
            // 
            this.ToDownBtn.Enabled = false;
            this.ToDownBtn.Location = new System.Drawing.Point(562, 277);
            this.ToDownBtn.Name = "ToDownBtn";
            this.ToDownBtn.Size = new System.Drawing.Size(105, 33);
            this.ToDownBtn.TabIndex = 7;
            this.ToDownBtn.Text = "На 1 вниз";
            this.ToDownBtn.UseVisualStyleBackColor = true;
            this.ToDownBtn.Click += new System.EventHandler(this.ToDownBtn_Click);
            // 
            // ToBeginBtn
            // 
            this.ToBeginBtn.Enabled = false;
            this.ToBeginBtn.Location = new System.Drawing.Point(562, 360);
            this.ToBeginBtn.Name = "ToBeginBtn";
            this.ToBeginBtn.Size = new System.Drawing.Size(105, 33);
            this.ToBeginBtn.TabIndex = 3;
            this.ToBeginBtn.Text = "В начало";
            this.ToBeginBtn.UseVisualStyleBackColor = true;
            this.ToBeginBtn.Click += new System.EventHandler(this.ToBeginBtn_Click);
            // 
            // ToEndBtn
            // 
            this.ToEndBtn.Enabled = false;
            this.ToEndBtn.Location = new System.Drawing.Point(562, 399);
            this.ToEndBtn.Name = "ToEndBtn";
            this.ToEndBtn.Size = new System.Drawing.Size(105, 33);
            this.ToEndBtn.TabIndex = 8;
            this.ToEndBtn.Text = "В конец";
            this.ToEndBtn.UseVisualStyleBackColor = true;
            this.ToEndBtn.Click += new System.EventHandler(this.ToEndBtn_Click);
            // 
            // EditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 450);
            this.Controls.Add(this.ToEndBtn);
            this.Controls.Add(this.ToBeginBtn);
            this.Controls.Add(this.ToDownBtn);
            this.Controls.Add(this.ToUpBtn);
            this.Controls.Add(this.OKBtn);
            this.Controls.Add(this.SaveBtn);
            this.Controls.Add(this.AddBtn);
            this.Controls.Add(this.DeleteBtn);
            this.Controls.Add(this.flowLayoutPanel);
            this.Controls.Add(this.listView);
            this.Name = "EditForm";
            this.Text = "Окно редактирования";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ColumnHeader columnNumber;
        private System.Windows.Forms.ColumnHeader columnType;
        private System.Windows.Forms.ColumnHeader columnStartFigure;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.Button DeleteBtn;
        private System.Windows.Forms.Button AddBtn;
        private System.Windows.Forms.Button SaveBtn;
        private System.Windows.Forms.Button OKBtn;
        private System.Windows.Forms.Button ToUpBtn;
        private System.Windows.Forms.Button ToDownBtn;
        private System.Windows.Forms.Button ToBeginBtn;
        private System.Windows.Forms.Button ToEndBtn;
    }
}