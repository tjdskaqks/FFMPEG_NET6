
namespace MP4VideoEncoding
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
            this.cb_AutoStart = new System.Windows.Forms.CheckBox();
            this.lv_DirectoryList = new System.Windows.Forms.ListView();
            this.btn_StartJob = new System.Windows.Forms.Button();
            this.btn_EndJob = new System.Windows.Forms.Button();
            this.tb_InputDirecotyPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_AddDirectoryPath = new System.Windows.Forms.Button();
            this.btn_SaveDirectories = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label23 = new System.Windows.Forms.Label();
            this.lbl_JobCount = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.lbl_AllJobCount = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.lbl_ConvEstimatedTime = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lbl_ConvSpeed = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lbl_ConvFrame = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.lbl_ConvDuration = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lbl_ConvFileSize = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lbl_OriDuration = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lbl_OriFileSize = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbl_Filename = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cb_AutoStart
            // 
            this.cb_AutoStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_AutoStart.AutoSize = true;
            this.cb_AutoStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cb_AutoStart.Location = new System.Drawing.Point(539, 14);
            this.cb_AutoStart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cb_AutoStart.Name = "cb_AutoStart";
            this.cb_AutoStart.Size = new System.Drawing.Size(100, 27);
            this.cb_AutoStart.TabIndex = 0;
            this.cb_AutoStart.Text = "자동시작";
            this.cb_AutoStart.UseVisualStyleBackColor = true;
            // 
            // lv_DirectoryList
            // 
            this.lv_DirectoryList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lv_DirectoryList.HideSelection = false;
            this.lv_DirectoryList.HoverSelection = true;
            this.lv_DirectoryList.Location = new System.Drawing.Point(14, 51);
            this.lv_DirectoryList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lv_DirectoryList.MultiSelect = false;
            this.lv_DirectoryList.Name = "lv_DirectoryList";
            this.lv_DirectoryList.Size = new System.Drawing.Size(629, 302);
            this.lv_DirectoryList.TabIndex = 1;
            this.lv_DirectoryList.UseCompatibleStateImageBehavior = false;
            // 
            // btn_StartJob
            // 
            this.btn_StartJob.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_StartJob.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_StartJob.Location = new System.Drawing.Point(533, 360);
            this.btn_StartJob.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_StartJob.Name = "btn_StartJob";
            this.btn_StartJob.Size = new System.Drawing.Size(104, 33);
            this.btn_StartJob.TabIndex = 2;
            this.btn_StartJob.Text = "시작";
            this.btn_StartJob.UseVisualStyleBackColor = true;
            // 
            // btn_EndJob
            // 
            this.btn_EndJob.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_EndJob.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_EndJob.Location = new System.Drawing.Point(533, 400);
            this.btn_EndJob.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_EndJob.Name = "btn_EndJob";
            this.btn_EndJob.Size = new System.Drawing.Size(104, 33);
            this.btn_EndJob.TabIndex = 3;
            this.btn_EndJob.Text = "종료";
            this.btn_EndJob.UseVisualStyleBackColor = true;
            // 
            // tb_InputDirecotyPath
            // 
            this.tb_InputDirecotyPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_InputDirecotyPath.Location = new System.Drawing.Point(98, 14);
            this.tb_InputDirecotyPath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tb_InputDirecotyPath.Name = "tb_InputDirecotyPath";
            this.tb_InputDirecotyPath.Size = new System.Drawing.Size(343, 30);
            this.tb_InputDirecotyPath.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 23);
            this.label1.TabIndex = 5;
            this.label1.Text = "폴더추가";
            // 
            // btn_AddDirectoryPath
            // 
            this.btn_AddDirectoryPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_AddDirectoryPath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_AddDirectoryPath.Location = new System.Drawing.Point(447, 13);
            this.btn_AddDirectoryPath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_AddDirectoryPath.Name = "btn_AddDirectoryPath";
            this.btn_AddDirectoryPath.Size = new System.Drawing.Size(80, 33);
            this.btn_AddDirectoryPath.TabIndex = 6;
            this.btn_AddDirectoryPath.Text = "추가";
            this.btn_AddDirectoryPath.UseVisualStyleBackColor = true;
            // 
            // btn_SaveDirectories
            // 
            this.btn_SaveDirectories.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_SaveDirectories.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_SaveDirectories.Location = new System.Drawing.Point(539, 577);
            this.btn_SaveDirectories.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_SaveDirectories.Name = "btn_SaveDirectories";
            this.btn_SaveDirectories.Size = new System.Drawing.Size(104, 33);
            this.btn_SaveDirectories.TabIndex = 7;
            this.btn_SaveDirectories.Text = "저장";
            this.btn_SaveDirectories.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.label23);
            this.panel1.Controls.Add(this.lbl_JobCount);
            this.panel1.Controls.Add(this.label22);
            this.panel1.Controls.Add(this.lbl_AllJobCount);
            this.panel1.Controls.Add(this.label20);
            this.panel1.Controls.Add(this.lbl_ConvEstimatedTime);
            this.panel1.Controls.Add(this.label19);
            this.panel1.Controls.Add(this.lbl_ConvSpeed);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.lbl_ConvFrame);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.lbl_ConvDuration);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.lbl_ConvFileSize);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.lbl_OriDuration);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.lbl_OriFileSize);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.lbl_Filename);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(14, 360);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(513, 251);
            this.panel1.TabIndex = 26;
            // 
            // label23
            // 
            this.label23.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label23.Location = new System.Drawing.Point(18, 147);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(84, 23);
            this.label23.TabIndex = 41;
            this.label23.Text = "작업 현황";
            // 
            // lbl_JobCount
            // 
            this.lbl_JobCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_JobCount.AutoSize = true;
            this.lbl_JobCount.Location = new System.Drawing.Point(123, 211);
            this.lbl_JobCount.Name = "lbl_JobCount";
            this.lbl_JobCount.Size = new System.Drawing.Size(19, 23);
            this.lbl_JobCount.TabIndex = 40;
            this.lbl_JobCount.Text = "0";
            // 
            // label22
            // 
            this.label22.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(18, 211);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(100, 23);
            this.label22.TabIndex = 39;
            this.label22.Text = "완료 개수 : ";
            // 
            // lbl_AllJobCount
            // 
            this.lbl_AllJobCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_AllJobCount.AutoSize = true;
            this.lbl_AllJobCount.Location = new System.Drawing.Point(120, 179);
            this.lbl_AllJobCount.Name = "lbl_AllJobCount";
            this.lbl_AllJobCount.Size = new System.Drawing.Size(37, 23);
            this.lbl_AllJobCount.TabIndex = 38;
            this.lbl_AllJobCount.Text = "100";
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(15, 179);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(83, 23);
            this.label20.TabIndex = 37;
            this.label20.Text = "총 개수 : ";
            // 
            // lbl_ConvEstimatedTime
            // 
            this.lbl_ConvEstimatedTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_ConvEstimatedTime.AutoSize = true;
            this.lbl_ConvEstimatedTime.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lbl_ConvEstimatedTime.ForeColor = System.Drawing.Color.Red;
            this.lbl_ConvEstimatedTime.Location = new System.Drawing.Point(325, 211);
            this.lbl_ConvEstimatedTime.Name = "lbl_ConvEstimatedTime";
            this.lbl_ConvEstimatedTime.Size = new System.Drawing.Size(78, 23);
            this.lbl_ConvEstimatedTime.TabIndex = 36;
            this.lbl_ConvEstimatedTime.Text = "00:01:22";
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(219, 211);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(100, 23);
            this.label19.TabIndex = 35;
            this.label19.Text = "예상 시간 : ";
            // 
            // lbl_ConvSpeed
            // 
            this.lbl_ConvSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_ConvSpeed.AutoSize = true;
            this.lbl_ConvSpeed.Location = new System.Drawing.Point(325, 179);
            this.lbl_ConvSpeed.Name = "lbl_ConvSpeed";
            this.lbl_ConvSpeed.Size = new System.Drawing.Size(49, 23);
            this.lbl_ConvSpeed.TabIndex = 34;
            this.lbl_ConvSpeed.Text = "23.0x";
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(219, 179);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(100, 23);
            this.label15.TabIndex = 33;
            this.label15.Text = "변환 속도 : ";
            // 
            // lbl_ConvFrame
            // 
            this.lbl_ConvFrame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_ConvFrame.AutoSize = true;
            this.lbl_ConvFrame.Location = new System.Drawing.Point(324, 147);
            this.lbl_ConvFrame.Name = "lbl_ConvFrame";
            this.lbl_ConvFrame.Size = new System.Drawing.Size(28, 23);
            this.lbl_ConvFrame.TabIndex = 32;
            this.lbl_ConvFrame.Text = "10";
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(219, 147);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(77, 23);
            this.label17.TabIndex = 31;
            this.label17.Text = "프레임 : ";
            // 
            // lbl_ConvDuration
            // 
            this.lbl_ConvDuration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_ConvDuration.AutoSize = true;
            this.lbl_ConvDuration.Location = new System.Drawing.Point(324, 112);
            this.lbl_ConvDuration.Name = "lbl_ConvDuration";
            this.lbl_ConvDuration.Size = new System.Drawing.Size(72, 23);
            this.lbl_ConvDuration.TabIndex = 30;
            this.lbl_ConvDuration.Text = "00:01:22";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(219, 112);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(60, 23);
            this.label11.TabIndex = 29;
            this.label11.Text = "길이 : ";
            // 
            // lbl_ConvFileSize
            // 
            this.lbl_ConvFileSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_ConvFileSize.AutoSize = true;
            this.lbl_ConvFileSize.Location = new System.Drawing.Point(324, 79);
            this.lbl_ConvFileSize.Name = "lbl_ConvFileSize";
            this.lbl_ConvFileSize.Size = new System.Drawing.Size(54, 23);
            this.lbl_ConvFileSize.TabIndex = 28;
            this.lbl_ConvFileSize.Text = "10MB";
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(219, 79);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(111, 23);
            this.label13.TabIndex = 27;
            this.label13.Text = "파일사이즈 : ";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label9.Location = new System.Drawing.Point(219, 10);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(124, 23);
            this.label9.TabIndex = 26;
            this.label9.Text = "변환 파일 정보";
            // 
            // lbl_OriDuration
            // 
            this.lbl_OriDuration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_OriDuration.AutoSize = true;
            this.lbl_OriDuration.Location = new System.Drawing.Point(120, 112);
            this.lbl_OriDuration.Name = "lbl_OriDuration";
            this.lbl_OriDuration.Size = new System.Drawing.Size(72, 23);
            this.lbl_OriDuration.TabIndex = 21;
            this.lbl_OriDuration.Text = "00:01:22";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 112);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 23);
            this.label8.TabIndex = 20;
            this.label8.Text = "길이 : ";
            // 
            // lbl_OriFileSize
            // 
            this.lbl_OriFileSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_OriFileSize.AutoSize = true;
            this.lbl_OriFileSize.Location = new System.Drawing.Point(120, 79);
            this.lbl_OriFileSize.Name = "lbl_OriFileSize";
            this.lbl_OriFileSize.Size = new System.Drawing.Size(54, 23);
            this.lbl_OriFileSize.TabIndex = 19;
            this.lbl_OriFileSize.Text = "10MB";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 79);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(111, 23);
            this.label6.TabIndex = 18;
            this.label6.Text = "파일사이즈 : ";
            // 
            // lbl_Filename
            // 
            this.lbl_Filename.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_Filename.AutoSize = true;
            this.lbl_Filename.Location = new System.Drawing.Point(120, 46);
            this.lbl_Filename.Name = "lbl_Filename";
            this.lbl_Filename.Size = new System.Drawing.Size(72, 23);
            this.lbl_Filename.TabIndex = 17;
            this.lbl_Filename.Text = "xxx.mp4";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 23);
            this.label3.TabIndex = 16;
            this.label3.Text = "파일명 : ";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(15, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 23);
            this.label2.TabIndex = 15;
            this.label2.Text = "파일 정보";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 621);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btn_SaveDirectories);
            this.Controls.Add(this.btn_AddDirectoryPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_InputDirecotyPath);
            this.Controls.Add(this.btn_EndJob);
            this.Controls.Add(this.btn_StartJob);
            this.Controls.Add(this.lv_DirectoryList);
            this.Controls.Add(this.cb_AutoStart);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(640, 540);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "장기 녹화 인코딩";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cb_AutoStart;
        private System.Windows.Forms.ListView lv_DirectoryList;
        private System.Windows.Forms.Button btn_StartJob;
        private System.Windows.Forms.Button btn_EndJob;
        private System.Windows.Forms.TextBox tb_InputDirecotyPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_AddDirectoryPath;
        private System.Windows.Forms.Button btn_SaveDirectories;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_ConvEstimatedTime;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lbl_ConvSpeed;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lbl_ConvFrame;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lbl_ConvDuration;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lbl_ConvFileSize;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lbl_OriDuration;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lbl_OriFileSize;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbl_Filename;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_JobCount;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label lbl_AllJobCount;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label23;
    }
}

