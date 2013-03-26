#define _debug_documentfile
using System.Windows.Forms;
using System;
using System.Drawing;

namespace Carbinet
{
    public class DocumentFile : IComparable<DocumentFile>
    {
        #region
        //public string referenceID;
        public event EventHandler Click;
        public string name;
        string text;
        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                this.doc.Text = value;
            }
        }
        public int columnNumber;
        public int floorNumber;//物资在所在层的位置索引
        public MetroFramework.Controls.MetroTile doc;
        public string indexBase;//比较大小的根据，通常是在同一行中的排序，比如列号
        public int carbinetIndex;//柜子编号
        private int width = 16;

        public int Width
        {
            get { return this.doc.Width; }
            set { this.doc.Width = value; }
        }
        private int height = 92;

        public int Height
        {
            get { return this.doc.Height; }
            set { this.doc.Height = value; }
        }

        public int Left
        {
            get { return this.doc.Left; }
            set { this.doc.Left = value; }
        }

        public int Top
        {
            get { return this.doc.Top; }
            set { this.doc.Top = value; }
        }
        //public DocumentChair myChair;


        #endregion

        public DocumentFile(string name, int floor)
        {
            //this.doc = new Button();
            //this.doc.Width = this.width;
            //this.doc.Height = this.height;
            //this.doc.Name = name;
            //this.doc.UseVisualStyleBackColor = false;
            //this.doc.TabStop = false;
            //this.doc.Text = this.text;
            //this.doc.BackColor = Color.White;
            //this.doc.BackgroundImage = global::Carbinet.Properties.Resources.grey;
            //this.doc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            //this.doc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            //this.doc.FlatAppearance.BorderSize = 0;

            this.doc = new MetroFramework.Controls.MetroTile();
            doc.Width = this.width;
            doc.Height = this.height;
            doc.ActiveControl = null;
            doc.MainText = this.text;
            doc.Name = name;
            doc.Style = MetroFramework.MetroColorStyle.Blue;
            doc.StyleManager = null;
            doc.Text = string.Empty;
            doc.Theme = MetroFramework.MetroThemeStyle.Light;
            doc.TileCount = 0;


            this.name = name;
            this.floorNumber = floor;
            this.doc.Click += new EventHandler(doc_Click);
        }
        public void resetDocState()
        {
            this.doc.Style = MetroFramework.MetroColorStyle.Blue;
            this.doc.MainText = string.Empty;
            this.doc.Text = string.Empty;
        }
        void doc_Click(object sender, EventArgs e)
        {
            if (this.Click != null)
            {
                this.Click(this, e);
            }
        }
        public void setDocPosition(int left, int top)
        {
            this.doc.Left = left;
            this.doc.Top = top;
        }
        public void setColorStyle(DocumentFileState _state)
        {
            MetroFramework.MetroColorStyle style = MetroFramework.MetroColorStyle.Blue;
            switch (_state) { 
                case DocumentFileState.InitialState:
                    style = MetroFramework.MetroColorStyle.Blue;
                    break;
                case DocumentFileState.Green:
                    style = MetroFramework.MetroColorStyle.Green;
                    break;
                case DocumentFileState.Orange:
                    style = MetroFramework.MetroColorStyle.Orange;
                    break;
                case DocumentFileState.Red:
                    style = MetroFramework.MetroColorStyle.Red;
                    break;
            }
            this.doc.Style = style;
        }
        public void setBackgroundColor(Color color)
        {
            this.doc.BackColor = color;
        }
        public void setBackgroundImage(Image image)
        {
            this.doc.BackgroundImage = image;
        }
        public void setText(string text)
        {
            //this.doc.Text = text;
            this.doc.MainText = text;
        }
        public int CompareTo(DocumentFile other)
        {
            // The temperature comparison depends on the comparison of the
            // the underlying Double values. Because the CompareTo method is
            // strongly typed, it is not necessary to test for the correct
            // object type.
            //return (string.CompareOrdinal(other.indexBase, this.indexBase));
            return (string.CompareOrdinal(this.indexBase, other.indexBase));
        }

        public static int operator >(DocumentFile df1, DocumentFile df2)
        {
            return string.CompareOrdinal(df1.indexBase, df2.indexBase);
            //return 0;
        }
        public static int operator <(DocumentFile df1, DocumentFile df2)
        {
            return string.CompareOrdinal(df1.indexBase, df2.indexBase);
            //return 0;
        }

        //public void setChair(DocumentChair dc)
        //{
        //    //this.myChair = dc;
        //}
    }
}
