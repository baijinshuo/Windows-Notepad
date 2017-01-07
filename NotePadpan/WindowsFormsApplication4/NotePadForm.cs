using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication4
{
    public partial class NotePadForm : Form
    {

        private bool dirtyBoolean = false;
        private string titleString = "无标题";
        /*private int rowInteger = 0;
        private int columnInteger = 0;*/
        private Boolean isnewBoolean = true;
        public static int startPositonInteger = 0;
        public String interString = "";

        StreamWriter notePadStreamWriter;
        StreamReader notePadStreamReader;



        public NotePadForm()
        {
            InitializeComponent();
        }

        private void 新建NToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Create a new txt project

            //Detect whether there's already a changed note
            if (dirtyBoolean)//if there is
            {
                //ask whether to save the current text and open a new project
                DialogResult responseDialogResult = MessageBox.Show("文件 " + titleString + " 的文字已经改变。\n 想保存文件吗？",
                    "记事本", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                if (responseDialogResult == DialogResult.Yes)
                {
                    //save the current notepad text
                    保存SToolStripMenuItem_Click(sender, e);
                    //create a new form
                    textBox1.Text = "";
                    this.Text = "无标题-记事本";
                    dirtyBoolean = false;
                }
                else if (responseDialogResult == DialogResult.No)
                {
                    //create a new form
                    textBox1.Text = "";
                    this.Text = "无标题-记事本";
                    dirtyBoolean = false;
                }
            }

            //if the current text is unchanged, open a new project
            else
            {
                textBox1.Text = "";
                this.Text = "无标题-记事本";
            }

        }

        private void 打开OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Open an existing file
            DialogResult responseDialogResult;

            //Detect whether there's already a changed note
            if (dirtyBoolean)
            {
                //ask whether to save the current text and open a new project
                responseDialogResult = MessageBox.Show("文件 " + titleString + " 的文字已经改变。\n 想保存文件吗？",
                   "记事本", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                if (responseDialogResult == DialogResult.Yes)
                {
                    另存为AToolStripMenuItem_Click(sender, e);
                }
                else if (responseDialogResult == DialogResult.Cancel)
                {
                    //Cancel the open operation
                    return;
                }
            }
            if (notePadStreamWriter != null)
            {
                //Close the notePadStreamWriter if the previous one has been changed.
                notePadStreamWriter.Close();
            }

            //Provoke the openFileDialog and fetch the result
            openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
            responseDialogResult = openFileDialog1.ShowDialog();

            if (responseDialogResult != DialogResult.Cancel)
            {
                //Open file, read in the text
                notePadStreamReader = new StreamReader(openFileDialog1.FileName);
                isnewBoolean = false;
                titleString = openFileDialog1.FileName;
                if (notePadStreamReader.Peek() != -1)
                {
                    textBox1.Text = notePadStreamReader.ReadLine();
                }
                notePadStreamReader.Close();
            }
        }

        private void 保存SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Detect whether the txt file has already existed
            if (!isnewBoolean)
            {
                writeLine();
                dirtyBoolean = false;//After saving the file, set the dirtyBoolean false
            }
            //if the txt file is a new one, provoke 另存为 method
            else 另存为AToolStripMenuItem_Click(sender, e);

        }

        private void 另存为AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult responseDialogResult;
            //Provoke the saveFileDialog and fetch the result
            saveFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
            responseDialogResult = saveFileDialog1.ShowDialog();

            if (responseDialogResult == DialogResult.Yes)
            {
                dirtyBoolean = false;//After saving the file, set the dirtyBoolean false
                isnewBoolean = false;//After saving the file, set the isnewBoolean false
                titleString = saveFileDialog1.FileName;//Change the title
                
                writeLine(); 
            }
        }

        private void writeLine()
        {
            //Write text to the target file, used to save a file
            if (notePadStreamWriter != null)
            {
                //Close the notePadStreamWriter if the previous one has been changed.
                notePadStreamWriter.Close();
            }
            notePadStreamWriter = new StreamWriter(titleString);
            if (notePadStreamWriter != null)
            {
     
                notePadStreamWriter.Write(textBox1.Text);
            }
            notePadStreamWriter.Close();
        }

        private void NotePadForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //When the closing "x" is clicked

            DialogResult responseDialogResult;

            //Detect whether the notepad note has been changed
            if (dirtyBoolean)
            {
                //ask the user whether to save it
                responseDialogResult = MessageBox.Show("文件 " + titleString + " 的文字已经改变。\n 想保存文件吗？",
                   "记事本", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                if (responseDialogResult == DialogResult.Yes)
                    保存SToolStripMenuItem_Click(sender, e);
                if (responseDialogResult == DialogResult.Cancel){
                    e.Cancel=true;
                    this.Activate();
                }
            }
            
        }

        private void 退出XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 撤销VToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Undo();
        }

        private void 剪切TToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Cut();
        }

        private void 复制CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Copy();
        }

        private void 粘贴PToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Paste();
        }

        private void 全选AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }

        private void 删除LToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Delete the seleced text
            textBox1.SelectedText = "";
        }

        private void 时间日期DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Display current date and add it to where the cursor is
            textBox1.Text += DateTime.Now.ToString("yyyy-M-d");
        }




        private void 查找FToolStripMenuItem_Click(object sender, EventArgs e)
        {
            searchForm searchBox = new searchForm(this);
            searchBox.Show();
            
        }




        private void 字体FToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Provoke the fontDialog and let the user to change the font
            fontDialog1.Font = textBox1.Font;
            fontDialog1.ShowDialog();
            textBox1.SelectionFont = fontDialog1.Font; 
        }

        private void 自动换行WToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Change the property of auto linewrap
            if (自动换行WToolStripMenuItem.Checked) textBox1.WordWrap = true;
            else textBox1.WordWrap = false;
        }

        private void 状态栏SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Change the visibility of the status bar
            if (状态栏SToolStripMenuItem.Checked) statusStrip1.Visible = true;
            else statusStrip1.Visible = false;
        }

        private void 关于记事本AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Create a new aboutBox and display it
            AboutBox1 aboutBox = new AboutBox1();
            aboutBox.ShowDialog();
        }

        

        private int GetStringLen(string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                int len = s.Length;
                for (int i = 0; i < s.Length; i++)
                {
                    if (s[i] > 255) len++;
                }
                return len;
            }
            return 0;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //if the text of the notepad is changed, set the dirtyBoolean true.
            dirtyBoolean = true;
            
        }
        

        private void 打印预览PToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        int indexInteger=0;

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font printFont = fontDialog1.Font;
            float horizontalPrintLocationFloat=e.MarginBounds.Left;
            float verticalPrintLocationFloat=e.MarginBounds.Top;
            float lineHeightFloat = printFont.GetHeight();
            float pageWidthFloat = e.MarginBounds.Right;

            char[] inputTextChar = textBox1.Text.ToArray();

            for (int i = indexInteger; i < inputTextChar.Length; i++)
            {
                e.Graphics.DrawString(inputTextChar[i].ToString(), printFont, Brushes.Black,
                    horizontalPrintLocationFloat, verticalPrintLocationFloat);

                string formattedOutputString = inputTextChar[i].ToString();
                SizeF fontSize = e.Graphics.MeasureString(formattedOutputString, printFont);
                if(inputTextChar[i]!=' ')
                    horizontalPrintLocationFloat = horizontalPrintLocationFloat + fontSize.Width / 2;
                else
                    horizontalPrintLocationFloat = horizontalPrintLocationFloat + fontSize.Width;
                if (horizontalPrintLocationFloat >= pageWidthFloat||inputTextChar[i]=='\n')
                {
                    verticalPrintLocationFloat = verticalPrintLocationFloat + lineHeightFloat;
                    if (verticalPrintLocationFloat > e.MarginBounds.Bottom)
                    {
                        indexInteger = i + 1;
                        e.HasMorePages = true;
                        break;
                    }
                    else
                    {
                        e.HasMorePages = false;
                    }
                    horizontalPrintLocationFloat = e.MarginBounds.Left;
                    
                }
                if (i == inputTextChar.Length-1)
                {
                    indexInteger = 0;
                }
            }
            
        }

        private void textBox1_SelectionChanged(object sender, EventArgs e)
        {
            int row = textBox1.GetLineFromCharIndex(textBox1.SelectionStart) + 1;
            int start = textBox1.GetFirstCharIndexOfCurrentLine();
            string s = textBox1.Text.Substring(start, textBox1.SelectionStart - start);
            int col = GetStringLen(s) + 1;
            lineToolStripStatusLabel.Text = "Ln " + row;
            columnToolStripStatusLabel.Text = " Col " + col;
        }

        

        
    }
}