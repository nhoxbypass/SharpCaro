using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace DemoCaro
{
    class Board
    {
        //Hàm dựng
        public Board()
        {
            _Rows = 0;
            _Columns = 0;
        }

        public Board(int Rows, int Collumns)
        {
            this._Columns = Collumns;
            this._Rows = Rows;
        }


        //Khai báo biến
        private int _Rows;
        private int _Columns;

        //Đóng gói
        public int Rows
        {
            get { return _Rows; }
        }
        public int Columns
        {
            get { return _Columns; }
        }


        //Vẽ bàn cờ
        public void DrawBoard(Graphics g)
        {
            //Vẽ cột
            for(int i = 0; i <= _Columns; i++)
            {
                g.DrawLine(CaroChess.pen, i * Square._Width, 0, i * Square._Width, _Rows*Square._Height);
            }

            //Vẽ dòng
            for(int j = 0; j <= _Rows; j++)
            {
                g.DrawLine(CaroChess.pen, 0, j * Square._Height, _Columns * Square._Width, j * Square._Height);
            }
        }

        //Vẽ ô cờ đại diện cho hành động đánh cờ
        public void DrawSquare(Point point, SolidBrush slb ,Graphics g)
        {
            //Tô màu ô cờ 
            g.FillEllipse(slb, point.X + 2, point.Y + 2, Square._Width - 4, Square._Height - 4);    
        }

        //Xóa ô cờ với chức năng Undo
        public void EraseSquare(Point point, SolidBrush slb ,Graphics g)
        {
            //Tô màu hình vuông cùng màu bàn cờ lấp lại ô cờ đã đi
            g.FillRectangle(slb, point.X + 1, point.Y + 1, Square._Width - 2, Square._Height - 2);
        }
    }
}
