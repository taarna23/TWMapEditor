namespace MapEditor
{
    partial class frmEditor
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
            this.button1 = new System.Windows.Forms.Button();
            this.tabMapEditor = new System.Windows.Forms.TabControl();
            this.tabEditor = new System.Windows.Forms.TabPage();
            this.splitEditor = new System.Windows.Forms.SplitContainer();
            this.picMap = new System.Windows.Forms.PictureBox();
            this.tabEdit = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnSaveMD3 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.trackRenderlayer = new System.Windows.Forms.TrackBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.trackZoom = new System.Windows.Forms.TrackBar();
            this.tabMapEditor.SuspendLayout();
            this.tabEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitEditor)).BeginInit();
            this.splitEditor.Panel1.SuspendLayout();
            this.splitEditor.Panel2.SuspendLayout();
            this.splitEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMap)).BeginInit();
            this.tabEdit.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackRenderlayer)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackZoom)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(972, 123);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabMapEditor
            // 
            this.tabMapEditor.Controls.Add(this.tabEditor);
            this.tabMapEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMapEditor.Location = new System.Drawing.Point(0, 0);
            this.tabMapEditor.Margin = new System.Windows.Forms.Padding(2);
            this.tabMapEditor.Name = "tabMapEditor";
            this.tabMapEditor.SelectedIndex = 0;
            this.tabMapEditor.Size = new System.Drawing.Size(1455, 974);
            this.tabMapEditor.TabIndex = 1;
            // 
            // tabEditor
            // 
            this.tabEditor.Controls.Add(this.splitEditor);
            this.tabEditor.Location = new System.Drawing.Point(4, 22);
            this.tabEditor.Margin = new System.Windows.Forms.Padding(2);
            this.tabEditor.Name = "tabEditor";
            this.tabEditor.Padding = new System.Windows.Forms.Padding(2);
            this.tabEditor.Size = new System.Drawing.Size(1447, 948);
            this.tabEditor.TabIndex = 0;
            this.tabEditor.Text = "Editor";
            this.tabEditor.UseVisualStyleBackColor = true;
            // 
            // splitEditor
            // 
            this.splitEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitEditor.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitEditor.Location = new System.Drawing.Point(2, 2);
            this.splitEditor.Margin = new System.Windows.Forms.Padding(2);
            this.splitEditor.Name = "splitEditor";
            // 
            // splitEditor.Panel1
            // 
            this.splitEditor.Panel1.AutoScroll = true;
            this.splitEditor.Panel1.AutoScrollMinSize = new System.Drawing.Size(10, 10);
            this.splitEditor.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.splitEditor.Panel1.Controls.Add(this.picMap);
            // 
            // splitEditor.Panel2
            // 
            this.splitEditor.Panel2.Controls.Add(this.tabEdit);
            this.splitEditor.Size = new System.Drawing.Size(1443, 944);
            this.splitEditor.SplitterDistance = 1141;
            this.splitEditor.SplitterWidth = 3;
            this.splitEditor.TabIndex = 0;
            // 
            // picMap
            // 
            this.picMap.Location = new System.Drawing.Point(0, 0);
            this.picMap.Margin = new System.Windows.Forms.Padding(2);
            this.picMap.Name = "picMap";
            this.picMap.Size = new System.Drawing.Size(227, 155);
            this.picMap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picMap.TabIndex = 1;
            this.picMap.TabStop = false;
            this.picMap.Paint += new System.Windows.Forms.PaintEventHandler(this.picMap_Paint);
            // 
            // tabEdit
            // 
            this.tabEdit.Controls.Add(this.tabPage1);
            this.tabEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabEdit.Location = new System.Drawing.Point(0, 0);
            this.tabEdit.Name = "tabEdit";
            this.tabEdit.SelectedIndex = 0;
            this.tabEdit.Size = new System.Drawing.Size(299, 944);
            this.tabEdit.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnSaveMD3);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(291, 918);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Editor";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnSaveMD3
            // 
            this.btnSaveMD3.Location = new System.Drawing.Point(7, 168);
            this.btnSaveMD3.Name = "btnSaveMD3";
            this.btnSaveMD3.Size = new System.Drawing.Size(75, 23);
            this.btnSaveMD3.TabIndex = 4;
            this.btnSaveMD3.Text = "Convert";
            this.btnSaveMD3.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.trackRenderlayer);
            this.groupBox2.Location = new System.Drawing.Point(7, 87);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(388, 75);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Layer";
            // 
            // trackRenderlayer
            // 
            this.trackRenderlayer.Location = new System.Drawing.Point(6, 19);
            this.trackRenderlayer.Name = "trackRenderlayer";
            this.trackRenderlayer.Size = new System.Drawing.Size(267, 45);
            this.trackRenderlayer.TabIndex = 0;
            this.trackRenderlayer.Value = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.trackZoom);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(388, 75);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Zoom";
            // 
            // trackZoom
            // 
            this.trackZoom.Location = new System.Drawing.Point(6, 19);
            this.trackZoom.Maximum = 100;
            this.trackZoom.Minimum = 5;
            this.trackZoom.Name = "trackZoom";
            this.trackZoom.Size = new System.Drawing.Size(268, 45);
            this.trackZoom.TabIndex = 0;
            this.trackZoom.Value = 100;
            this.trackZoom.Scroll += new System.EventHandler(this.trackZoom_Scroll);
            this.trackZoom.MouseDown += new System.Windows.Forms.MouseEventHandler(this.trackZoom_MouseDown);
            this.trackZoom.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trackZoom_MouseUp);
            // 
            // frmEditor
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1455, 974);
            this.Controls.Add(this.tabMapEditor);
            this.Controls.Add(this.button1);
            this.Name = "frmEditor";
            this.Text = "Talesweaver Map Stealer";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.frmEditor_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.frmEditor_DragEnter);
            this.tabMapEditor.ResumeLayout(false);
            this.tabEditor.ResumeLayout(false);
            this.splitEditor.Panel1.ResumeLayout(false);
            this.splitEditor.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitEditor)).EndInit();
            this.splitEditor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picMap)).EndInit();
            this.tabEdit.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackRenderlayer)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackZoom)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabControl tabMapEditor;
        private System.Windows.Forms.TabPage tabEditor;
        private System.Windows.Forms.SplitContainer splitEditor;
        private System.Windows.Forms.PictureBox picMap;
        private System.Windows.Forms.TabControl tabEdit;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnSaveMD3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TrackBar trackRenderlayer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TrackBar trackZoom;
    }
}

