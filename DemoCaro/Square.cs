using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DemoCaro
{
    [Serializable()]
    public class Square
    {
        //Hàm dựng
        public Square()
        {
            Row = 0;
            Column = 0;
            Location = new Point(0, 0);
            Owner = 0;
        }

        public Square(int iRow, int iColumn, Point pLocation, int iOnwer)
        {
            this._Row = iRow;
            this._Column = iColumn;
            this._Location = pLocation;
            this._Owner = iOnwer;
        }


        //Khai báo biến
        public const int _Width = 25;
        public const int _Height = 25;

        private int _Row;
        private int _Column;
        private Point _Location;


        private int _Owner;

        //Đóng gói để tham chiếu dữ liệu từ ngoài class
        public int Row
        {
            get { return _Row; }
            set { _Row = value; }
        }
        public int Column
        {
            get { return _Column; }
            set { _Column = value; }
        }
        public Point Location
        {
            get { return _Location; }
            set { _Location = value; }
        }

        public int Owner
        {
            get { return _Owner; }
            set { _Owner = value; }
        }
    }
}
