namespace QuokkaDevVSIXTemplates
{
    partial class NewAggregateForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkIncludeIRepo;
        private System.Windows.Forms.CheckBox chkIncludeEvents;
        private System.Windows.Forms.CheckBox chkIncludeFactory;
        private System.Windows.Forms.CheckBox chkIncludeSpec;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewAggregateForm));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkIncludeIRepo = new System.Windows.Forms.CheckBox();
            this.chkIncludeEvents = new System.Windows.Forms.CheckBox();
            this.chkIncludeFactory = new System.Windows.Forms.CheckBox();
            this.chkIncludeSpec = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.Indigo;
            this.btnOK.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnOK.FlatAppearance.BorderSize = 0;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOK.Location = new System.Drawing.Point(200, 165);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.DimGray;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(281, 165);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chkIncludeIRepo
            // 
            this.chkIncludeIRepo.AutoSize = true;
            this.chkIncludeIRepo.Checked = true;
            this.chkIncludeIRepo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIncludeIRepo.Location = new System.Drawing.Point(12, 12);
            this.chkIncludeIRepo.Name = "chkIncludeIRepo";
            this.chkIncludeIRepo.Size = new System.Drawing.Size(180, 20);
            this.chkIncludeIRepo.TabIndex = 2;
            this.chkIncludeIRepo.Text = "Add IRepository interface";
            this.chkIncludeIRepo.UseVisualStyleBackColor = true;
            // 
            // chkIncludeEvents
            // 
            this.chkIncludeEvents.AutoSize = true;
            this.chkIncludeEvents.Checked = true;
            this.chkIncludeEvents.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIncludeEvents.Location = new System.Drawing.Point(12, 35);
            this.chkIncludeEvents.Name = "chkIncludeEvents";
            this.chkIncludeEvents.Size = new System.Drawing.Size(153, 20);
            this.chkIncludeEvents.TabIndex = 3;
            this.chkIncludeEvents.Text = "Include an events file";
            this.chkIncludeEvents.UseVisualStyleBackColor = true;
            // 
            // chkIncludeFactory
            // 
            this.chkIncludeFactory.AutoSize = true;
            this.chkIncludeFactory.Checked = true;
            this.chkIncludeFactory.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIncludeFactory.Location = new System.Drawing.Point(12, 58);
            this.chkIncludeFactory.Name = "chkIncludeFactory";
            this.chkIncludeFactory.Size = new System.Drawing.Size(261, 20);
            this.chkIncludeFactory.TabIndex = 3;
            this.chkIncludeFactory.Text = "Include a factory for aggregate creation";
            this.chkIncludeFactory.UseVisualStyleBackColor = true;
            // 
            // chkIncludeSpec
            // 
            this.chkIncludeSpec.AutoSize = true;
            this.chkIncludeSpec.Checked = true;
            this.chkIncludeSpec.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIncludeSpec.Location = new System.Drawing.Point(12, 81);
            this.chkIncludeSpec.Name = "chkIncludeSpec";
            this.chkIncludeSpec.Size = new System.Drawing.Size(188, 20);
            this.chkIncludeSpec.TabIndex = 3;
            this.chkIncludeSpec.Text = "Include a specifications file";
            this.chkIncludeSpec.UseVisualStyleBackColor = true;
            // 
            // NewAggregateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(304, 160);
            this.Controls.Add(this.chkIncludeIRepo);
            this.Controls.Add(this.chkIncludeEvents);
            this.Controls.Add(this.chkIncludeFactory);
            this.Controls.Add(this.chkIncludeSpec);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NewAggregateForm";
            this.Text = "New Aggregate";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}