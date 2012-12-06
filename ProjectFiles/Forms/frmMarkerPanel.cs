using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;

namespace NppPluginNET
{
    partial class frmMarkerPanel : Form
    {
		//Line numbers for search results go here
        List<int> lineResults = new List<int>();
		
		//The line count of the document is stored here
        int lineCount = 0;
		
		//Used for drawing the markers
        Pen linePen = new Pen(Color.FromArgb(229,192,0), 2);
        Pen fadelinePen = new Pen(Color.FromArgb(236, 224, 167), 1);

        public frmMarkerPanel()
        {
            InitializeComponent();
        }

        public void doSearch()
        {
			//Get a pointer to the Scintilla control in Notepad++
            IntPtr curScintilla = PluginBase.GetCurrentScintilla();
            List<int> tempLines = new List<int>();

			//Clear any previous markers showing in the document
            Win32.SendMessage(curScintilla, SciMsg.SCI_MARKERDELETEALL, 1, 0);
			//Set up the marker color
            Win32.SendMessage(curScintilla, SciMsg.SCI_MARKERSETBACK, 1, 0xdddddd);

			//Get line count of document
            lineCount = (int)Win32.SendMessage(curScintilla, SciMsg.SCI_GETLINECOUNT, 0, 0);
            int lastCharPos = 0;
            int docCharCount = (int)Win32.SendMessage(curScintilla, SciMsg.SCI_GETLENGTH, 0, 0);

            //Do first search from start of document, get character and line position
            //Loop through rest of file until a returned character position is -1
            while (true)
            {
                //Prepare search content. This includes char to start at, char to end at, text to find
                Sci_TextToFind theTTF = new Sci_TextToFind(new Sci_CharacterRange(lastCharPos, docCharCount), tbxSearchString.Text);

				//Perform the search
                int theCharPos = (int)(Win32.SendMessage(curScintilla, SciMsg.SCI_FINDTEXT, 0, theTTF.NativePointer));

                if(theCharPos != -1)
                {
                    //Note: Line indexes start at 0, not 1! Therefore if a word is found at "line 2" it actually means line 3 visually
                    int theLine = (int)Win32.SendMessage(curScintilla, SciMsg.SCI_LINEFROMPOSITION, theCharPos, 0);
                    int charLineStarts = (int)Win32.SendMessage(curScintilla, SciMsg.SCI_POSITIONFROMLINE, theLine, 0);
                    int charLineEnds = charLineStarts + (int)Win32.SendMessage(curScintilla, SciMsg.SCI_LINELENGTH, theLine, 0);
                    //MessageBox.Show(
                    //      "found at char: " + theCharPos.ToString()
                    //    + ", char is on line: " + (theLine + 1).ToString()
                    //    + ", line starts at char: " + charLineStarts.ToString()
                    //    + ", line ends at char: " + charLineEnds.ToString()
                    //    );
					
					//Add highlighting showing which line it is on
                    Win32.SendMessage(curScintilla, SciMsg.SCI_MARKERADD, theLine, 1);
                    tempLines.Add(theLine + 1);
                }
                lastCharPos = theCharPos + tbxSearchString.Text.Length;
                if(theCharPos == -1)
                {
					//All results have been found, so exit the loop
                    break;
                }

            }
            lineResults = tempLines;
            this.Refresh();
        }
        
        void FrmGoToLineVisibleChanged(object sender, EventArgs e)
        {
        	if (!Visible)
        	{
                Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_SETMENUITEMCHECK,
                                  PluginBase._funcItems.Items[PluginBase.idFrmGotToLine]._cmdID, 0);

				//Clear all of the line highlighting
                Win32.SendMessage(PluginBase.GetCurrentScintilla(), SciMsg.SCI_MARKERDELETEALL, 1, 0);
                lineResults = new List<int>();
        	}
        }

        private void frmGoToLine_Paint(object sender, PaintEventArgs e)
        {
			//If there are line results, cycle through them and draw orange markers
            if (lineResults.Count > 0)
            {
                for (int i = 0; i < lineResults.Count; i++)
                {
                    int linePos = ((int)(((decimal)lineResults[i] / (decimal)lineCount) * (this.Size.Height - 34))) + 8;

                    //Draw the marker
                    e.Graphics.DrawLine(fadelinePen, new Point(0, linePos - 1), new Point(this.Size.Width, linePos - 1));
                    e.Graphics.DrawLine(linePen, new Point(0, linePos), new Point(this.Size.Width, linePos));
                    e.Graphics.DrawLine(fadelinePen, new Point(0, linePos + 2), new Point(this.Size.Width, linePos + 2));
                }
            }
        }

        private void frmGoToLine_SizeChanged(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void tbxSearchString_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                doSearch();
            }
        }

        private void tbxSearchString_KeyUp(object sender, KeyEventArgs e)
        {
			//Remove previous highligting
            Win32.SendMessage(PluginBase.GetCurrentScintilla(), SciMsg.SCI_MARKERDELETEALL, 1, 0);
            lineResults = new List<int>();

			//Perform a search of the string length is larger than 1
            if(tbxSearchString.Text.Length > 1)
            {
                doSearch();
            }
        }
    }
}
