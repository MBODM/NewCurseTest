namespace NewCurseTest
{
    partial class MainForm
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
            buttonFetch = new Button();
            richTextBoxJson = new RichTextBox();
            panelWebView = new Panel();
            webView = new Microsoft.Web.WebView2.WinForms.WebView2();
            buttonLoad = new Button();
            buttonTest = new Button();
            textBoxUrl = new TextBox();
            buttonClose = new Button();
            panelWebView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webView).BeginInit();
            SuspendLayout();
            // 
            // buttonFetch
            // 
            buttonFetch.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonFetch.Location = new Point(1015, 724);
            buttonFetch.Name = "buttonFetch";
            buttonFetch.Size = new Size(75, 25);
            buttonFetch.TabIndex = 1;
            buttonFetch.Text = "Fetch";
            buttonFetch.UseVisualStyleBackColor = true;
            buttonFetch.Click += ButtonFetch_Click;
            // 
            // richTextBoxJson
            // 
            richTextBoxJson.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            richTextBoxJson.Location = new Point(12, 323);
            richTextBoxJson.Name = "richTextBoxJson";
            richTextBoxJson.ReadOnly = true;
            richTextBoxJson.Size = new Size(1240, 395);
            richTextBoxJson.TabIndex = 7;
            richTextBoxJson.Text = "";
            // 
            // panelWebView
            // 
            panelWebView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelWebView.BorderStyle = BorderStyle.FixedSingle;
            panelWebView.Controls.Add(webView);
            panelWebView.Location = new Point(12, 41);
            panelWebView.Name = "panelWebView";
            panelWebView.Size = new Size(1240, 276);
            panelWebView.TabIndex = 5;
            // 
            // webView
            // 
            webView.AllowExternalDrop = true;
            webView.CreationProperties = null;
            webView.DefaultBackgroundColor = Color.White;
            webView.Location = new Point(600, 150);
            webView.Name = "webView";
            webView.Size = new Size(75, 23);
            webView.TabIndex = 6;
            webView.ZoomFactor = 1D;
            // 
            // buttonLoad
            // 
            buttonLoad.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonLoad.Location = new Point(934, 724);
            buttonLoad.Name = "buttonLoad";
            buttonLoad.Size = new Size(75, 25);
            buttonLoad.TabIndex = 0;
            buttonLoad.Text = "Load";
            buttonLoad.UseVisualStyleBackColor = true;
            buttonLoad.Click += ButtonLoad_Click;
            // 
            // buttonTest
            // 
            buttonTest.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonTest.Location = new Point(1096, 724);
            buttonTest.Name = "buttonTest";
            buttonTest.Size = new Size(75, 25);
            buttonTest.TabIndex = 2;
            buttonTest.Text = "Test";
            buttonTest.UseVisualStyleBackColor = true;
            buttonTest.Click += ButtonTest_Click;
            // 
            // textBoxUrl
            // 
            textBoxUrl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxUrl.Location = new Point(12, 12);
            textBoxUrl.Name = "textBoxUrl";
            textBoxUrl.ReadOnly = true;
            textBoxUrl.Size = new Size(1240, 23);
            textBoxUrl.TabIndex = 4;
            // 
            // buttonClose
            // 
            buttonClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonClose.Location = new Point(1177, 724);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new Size(75, 25);
            buttonClose.TabIndex = 3;
            buttonClose.Text = "Close";
            buttonClose.UseVisualStyleBackColor = true;
            buttonClose.Click += ButtonClose_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1264, 761);
            Controls.Add(buttonClose);
            Controls.Add(textBoxUrl);
            Controls.Add(buttonTest);
            Controls.Add(buttonLoad);
            Controls.Add(panelWebView);
            Controls.Add(richTextBoxJson);
            Controls.Add(buttonFetch);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "NewCurseTest";
            panelWebView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)webView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonFetch;
        private RichTextBox richTextBoxJson;
        private Panel panelWebView;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView;
        private Button buttonLoad;
        private Button buttonTest;
        private TextBox textBoxUrl;
        private Button buttonClose;
    }
}