﻿namespace OOP_LABA_6
{
    public partial class Main
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.paint_box = new System.Windows.Forms.Panel();
            this.label_paintbox = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.groupButton = new System.Windows.Forms.Button();
            this.input = new System.Windows.Forms.Button();
            this.output = new System.Windows.Forms.Button();
            this.unGroupButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.paint_box.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // paint_box
            // 
            this.paint_box.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.paint_box.BackColor = System.Drawing.SystemColors.Info;
            this.paint_box.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.paint_box.Controls.Add(this.label_paintbox);
            this.paint_box.Location = new System.Drawing.Point(9, 10);
            this.paint_box.Margin = new System.Windows.Forms.Padding(2);
            this.paint_box.Name = "paint_box";
            this.paint_box.Size = new System.Drawing.Size(695, 601);
            this.paint_box.TabIndex = 0;
            this.paint_box.MouseClick += new System.Windows.Forms.MouseEventHandler(this.paint_box_MouseClick);
            // 
            // label_paintbox
            // 
            this.label_paintbox.AutoSize = true;
            this.label_paintbox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_paintbox.Location = new System.Drawing.Point(3, 3);
            this.label_paintbox.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_paintbox.Name = "label_paintbox";
            this.label_paintbox.Size = new System.Drawing.Size(157, 21);
            this.label_paintbox.TabIndex = 0;
            this.label_paintbox.Text = "Окно для рисования";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButton1.Location = new System.Drawing.Point(3, 12);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(60, 24);
            this.radioButton1.TabIndex = 7;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Круг";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButton2.Location = new System.Drawing.Point(4, 42);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(75, 24);
            this.radioButton2.TabIndex = 8;
            this.radioButton2.Text = "Линия";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.radioButton1);
            this.panel1.Controls.Add(this.radioButton2);
            this.panel1.Location = new System.Drawing.Point(721, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(87, 75);
            this.panel1.TabIndex = 10;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BackColor = System.Drawing.Color.Green;
            this.textBox1.Location = new System.Drawing.Point(806, 56);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(123, 20);
            this.textBox1.TabIndex = 12;
            this.textBox1.Click += new System.EventHandler(this.textBox3_Click);
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.BackColor = System.Drawing.Color.Aqua;
            this.textBox2.Location = new System.Drawing.Point(806, 20);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(123, 20);
            this.textBox2.TabIndex = 13;
            this.textBox2.Click += new System.EventHandler(this.textBox3_Click);
            // 
            // textBox3
            // 
            this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox3.BackColor = System.Drawing.SystemColors.Window;
            this.textBox3.Location = new System.Drawing.Point(806, 39);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(123, 20);
            this.textBox3.TabIndex = 14;
            this.textBox3.Click += new System.EventHandler(this.textBox3_Click);
            // 
            // groupButton
            // 
            this.groupButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupButton.Location = new System.Drawing.Point(721, 81);
            this.groupButton.Name = "groupButton";
            this.groupButton.Size = new System.Drawing.Size(208, 48);
            this.groupButton.TabIndex = 16;
            this.groupButton.Text = "Группировка ";
            this.groupButton.UseVisualStyleBackColor = true;
            this.groupButton.Click += new System.EventHandler(this.groupButton_Click);
            // 
            // input
            // 
            this.input.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.input.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.input.Location = new System.Drawing.Point(721, 243);
            this.input.Name = "input";
            this.input.Size = new System.Drawing.Size(208, 48);
            this.input.TabIndex = 17;
            this.input.Text = "Считывание";
            this.input.UseVisualStyleBackColor = true;
            this.input.Click += new System.EventHandler(this.input_Click);
            // 
            // output
            // 
            this.output.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.output.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.output.Location = new System.Drawing.Point(721, 189);
            this.output.Name = "output";
            this.output.Size = new System.Drawing.Size(208, 48);
            this.output.TabIndex = 18;
            this.output.Text = "Запись";
            this.output.UseVisualStyleBackColor = true;
            this.output.Click += new System.EventHandler(this.output_Click);
            // 
            // unGroupButton
            // 
            this.unGroupButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.unGroupButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.unGroupButton.Location = new System.Drawing.Point(721, 135);
            this.unGroupButton.Name = "unGroupButton";
            this.unGroupButton.Size = new System.Drawing.Size(208, 48);
            this.unGroupButton.TabIndex = 19;
            this.unGroupButton.Text = "Разгруппировка ";
            this.unGroupButton.UseVisualStyleBackColor = true;
            this.unGroupButton.Click += new System.EventHandler(this.unGroupButton_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(467, 254);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(208, 37);
            this.button1.TabIndex = 20;
            this.button1.Text = "Липкий объект";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(941, 620);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupButton);
            this.Controls.Add(this.unGroupButton);
            this.Controls.Add(this.output);
            this.Controls.Add(this.input);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.paint_box);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Main";
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Main_KeyDown);
            this.paint_box.ResumeLayout(false);
            this.paint_box.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.Label label_paintbox;
        #endregion

        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button groupButton;
        private System.Windows.Forms.Button input;
        private System.Windows.Forms.Button output;
        private System.Windows.Forms.Button unGroupButton;
        public System.Windows.Forms.Panel paint_box;
        private System.Windows.Forms.Button button1;
    }
}

