
namespace UI
{
    partial class FormBoard 
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
            this.components = new System.ComponentModel.Container();
            this.boardLayOut = new System.Windows.Forms.FlowLayoutPanel();
            this.labelPlayer1 = new System.Windows.Forms.Label();
            this.labelScore1 = new System.Windows.Forms.Label();
            this.labelPlayer2 = new System.Windows.Forms.Label();
            this.labelScore2 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // boardLayOut
            // 
            this.boardLayOut.Location = new System.Drawing.Point(24, 27);
            this.boardLayOut.Name = "boardLayOut";
            this.boardLayOut.Size = new System.Drawing.Size(443, 352);
            this.boardLayOut.TabIndex = 0;
            this.boardLayOut.Paint += new System.Windows.Forms.PaintEventHandler(this.boardLayOut_Paint);
            // 
            // labelPlayer1
            // 
            this.labelPlayer1.AutoEllipsis = true;
            this.labelPlayer1.AutoSize = true;
            this.labelPlayer1.Location = new System.Drawing.Point(163, 405);
            this.labelPlayer1.Name = "labelPlayer1";
            this.labelPlayer1.Size = new System.Drawing.Size(48, 13);
            //this.labelPlayer1.Width = 0;
            this.labelPlayer1.TabIndex = 0;
            this.labelPlayer1.Text = "Player 1:";
            this.labelPlayer1.Click += new System.EventHandler(this.labelPlayer1_Click);
            // 
            // labelScore1
            // 
            this.labelScore1.AutoSize = true;
            this.labelScore1.Location = new System.Drawing.Point(211, 405);
            this.labelScore1.Name = "labelScore1";
            this.labelScore1.Size = new System.Drawing.Size(13, 13);
            this.labelScore1.TabIndex = 1;
            this.labelScore1.Text = "0";
            this.labelScore1.Click += new System.EventHandler(this.score1_Click);
            // 
            // labelPlayer2
            // 
            this.labelPlayer2.AutoSize = true;
            this.labelPlayer2.Location = new System.Drawing.Point(256, 405);
            this.labelPlayer2.Name = "labelPlayer2";
            this.labelPlayer2.Size = new System.Drawing.Size(48, 13);
            this.labelPlayer2.TabIndex = 2;
            this.labelPlayer2.Text = "Player 2:";
            this.labelPlayer2.Click += new System.EventHandler(this.labelPlayer2_Click);
            // 
            // labelScore2
            // 
            this.labelScore2.AutoSize = true;
            this.labelScore2.Location = new System.Drawing.Point(310, 405);
            this.labelScore2.Name = "labelScore2";
            this.labelScore2.Size = new System.Drawing.Size(13, 13);
            this.labelScore2.TabIndex = 3;
            this.labelScore2.Text = "0";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FormBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 450);
            this.Controls.Add(this.labelScore2);
            this.Controls.Add(this.labelPlayer2);
            this.Controls.Add(this.labelScore1);
            this.Controls.Add(this.labelPlayer1);
            this.Controls.Add(this.boardLayOut);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormBoard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TicTacToe";
            this.Load += new System.EventHandler(this.FormBoard_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.FlowLayoutPanel boardLayOut;
        private System.Windows.Forms.Label labelPlayer1;
        private System.Windows.Forms.Label labelScore1;
        private System.Windows.Forms.Label labelPlayer2;
        private System.Windows.Forms.Label labelScore2;
        private System.Windows.Forms.Timer timer1;
    }
}