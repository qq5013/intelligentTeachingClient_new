#define _debug_documentchair
using System.Diagnostics;

namespace Carbinet
{
    //  档案柜每层的位置
    public class DocumentChair
    {
        bool IsEmpty = true;
        int top;

        public int Top
        {
            get { return top; }
            set { top = value; }
        }
        int left;
        public DocumentFile docmentFile = null;
        public CarbinetFloor carbinetFloor = null;
        public int Left
        {
            //序列号 与 前面档案数目加间隔的乘积
            get
            {
                int i = 0;
                //if (this.docmentFile != null)
                //{
                //Debug.WriteLine(string.Format("DocumentChair -> file epc = "));
                //return this.index * (CarbinetFloor.DOCUMENT_GAP + this.docmentFile.documentWidth);
                i = this.carbinetFloor.getDocumentsTotalWidth(this.index) + this.index * CarbinetFloor.DOCUMENT_GAP;
                return i;
                //}
                //else
                //{
                //return 0;
                //}
            }
            set { left = value; }
        }
        public int index = 0;//
        public int floor = 0;

        public DocumentChair(DocumentFile df, CarbinetFloor cf)
        {
            this.docmentFile = df;
            this.carbinetFloor = cf;
        }
        public void setChairEmpty()
        {
            this.IsEmpty = true;
            this.docmentFile = null;
        }
        public void setChairNotEmpty(DocumentFile df)
        {
            this.IsEmpty = false;
            this.docmentFile = df;
        }
        public void recaculateDocPosition()
        {
            if (this.docmentFile != null && this.carbinetFloor != null)
            {
                this.docmentFile.Left = this.Left+this.carbinetFloor.Left;
#if _debug_documentchair

                Debug.WriteLine(string.Format("recaculate ->   index = {0}  left = {1}", this.index, this.docmentFile.Left));
#endif
                //this.docmentFile.Top = this.Top;
            }
        }
        public bool getEmptyState()
        {
            return this.IsEmpty;

        }

    }
}
