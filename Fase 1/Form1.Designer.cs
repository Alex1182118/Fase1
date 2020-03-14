namespace Fase_1
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.Follow = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.List_First = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.List_Last = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SETS = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.Follow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SETS)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(23, 295);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 50);
            this.button1.TabIndex = 0;
            this.button1.Text = "ARCHIVO";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Follow
            // 
            this.Follow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Follow.Location = new System.Drawing.Point(152, 129);
            this.Follow.Name = "Follow";
            this.Follow.RowHeadersWidth = 51;
            this.Follow.RowTemplate.Height = 24;
            this.Follow.Size = new System.Drawing.Size(277, 425);
            this.Follow.TabIndex = 2;
            this.Follow.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Follow_CellContentClick);
            // 
            // label1
            // 
            this.label1.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(66, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(451, 39);
            this.label1.TabIndex = 3;
            this.label1.Text = "TABLA DE TRANSICIONES";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(659, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 39);
            this.label2.TabIndex = 5;
            this.label2.Text = "FIRST";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // List_First
            // 
            this.List_First.FormattingEnabled = true;
            this.List_First.ItemHeight = 16;
            this.List_First.Location = new System.Drawing.Point(584, 129);
            this.List_First.Name = "List_First";
            this.List_First.Size = new System.Drawing.Size(293, 420);
            this.List_First.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1046, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 39);
            this.label3.TabIndex = 7;
            this.label3.Text = "LAST";
            // 
            // List_Last
            // 
            this.List_Last.FormattingEnabled = true;
            this.List_Last.ItemHeight = 16;
            this.List_Last.Location = new System.Drawing.Point(961, 129);
            this.List_Last.Name = "List_Last";
            this.List_Last.Size = new System.Drawing.Size(276, 420);
            this.List_Last.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1391, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 39);
            this.label4.TabIndex = 9;
            this.label4.Text = "SETS";
            // 
            // SETS
            // 
            this.SETS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SETS.Location = new System.Drawing.Point(1323, 129);
            this.SETS.Name = "SETS";
            this.SETS.RowHeadersWidth = 51;
            this.SETS.RowTemplate.Height = 24;
            this.SETS.Size = new System.Drawing.Size(261, 425);
            this.SETS.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1644, 603);
            this.Controls.Add(this.SETS);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.List_Last);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.List_First);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Follow);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Follow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SETS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView Follow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox List_First;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox List_Last;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView SETS;
    }
}

