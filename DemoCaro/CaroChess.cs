using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace DemoCaro
{
    //Khai báo các biến enum
    enum ENDGAME
    {
        DRAW,
        PLAYER1,
        PLAYER2,
        COM
    }

    enum MODE
    {
        PLAYER,
        EASYCOM,
        NORMALCOM
    }

    class CaroChess
    {
        //Khai báo biến
        #region DeclareVariables
        //Các bút vẽ bàn cờ và tô quân cờ
        public static Pen pen;
        public static SolidBrush slb1;
        public static SolidBrush slb2;
        public static SolidBrush eraseSlb;

        //Đối tượng bàn cờ
        private Board _board;
        //Mảng chứa thông tin các ô cờ
        private Square[,] SqrArr;
        //Stack chứa các ô đã đánh và các ô đã Undo
        private Stack<Square> stack_Squares;
        private Stack<Square> stack_Undo;
        //Mảng chứa giá trị điểm tấn công và phòng thủ cho A.I
        private long[] AtkArr;
        private long[] DefArr;
        //Lượt đi
        private int _Turn;
        //Biến sẵn sàng chơi game
        public bool isReadyPvP;
        public bool isReadyPvsCom;
        //Chế đồ game và thông tin người thắng
        private ENDGAME _endGame;
        private MODE _modeGame;

        public MODE modeGame
        {
            get { return _modeGame; }
        }

        #endregion DeclareVariables


        //Hàm dựng - khởi tạo các đối tượng
        public CaroChess()
        {
            pen = new Pen(Color.Black);
            slb1 = new SolidBrush(Color.Blue);
            slb2 = new SolidBrush(Color.Red);
            eraseSlb = new SolidBrush(SystemColors.Control);
            //Bàn cờ 20 ô có thể thay đổi nếu thay đổi kích cỡ panel
            _board = new Board(20, 20);
            SqrArr = new Square[_board.Rows,_board.Columns];
            stack_Squares = new Stack<Square>();
            stack_Undo = new Stack<Square>();
            //Mảng điểm của A.I
            AtkArr = new long[7] { 0, 9, 54, 162, 1458, 13112, 118008 };
            DefArr = new long[7] { 0, 3, 27, 99, 729, 6561, 59049 };
            //Lượt đi mặc định là 1
            _Turn = 1;
            isReadyPvP = false;
            isReadyPvsCom = false;
        }


        //Khởi tạo các đối tượng cần thiết - vẽ bàn cờ...
        #region Game Init
        //Vẽ bàn cờ
        public void DrawChessBoard(Graphics g)
        {
            _board.DrawBoard(g);
        }

        //Khởi tạo mảng quân cờ rỗng với Owner = 0
        public void SquareArrInit()
        {
            for(int i = 0; i < _board.Rows; i++)
            {
                for (int j = 0; j < _board.Columns; j++)
                {
                    SqrArr[i, j] = new Square(i, j, new Point(j*Square._Width,i*Square._Height), 0);
                }    
            }
            
        }

        //Bắt đầu chơi với máy dễ
        public void StartPvsEasyCom(Graphics g)
        {
            //Làm mới thông tin
            stack_Squares.Clear();
            stack_Undo.Clear();

            //Khởi tạo lại mảng ô cờ
            SquareArrInit();

            //Vẽ lại bàn cờ
            DrawChessBoard(g);

            //Khởi tạo các thứ cần thiết
            _Turn = 1;
            _modeGame = MODE.EASYCOM;
            StartComputer(g);
            isReadyPvsCom = true;
        }

        //Bắt đầu chơi với người
        public void StartPvsP(Graphics g)
        {
            stack_Squares.Clear();
            stack_Undo.Clear();
            SquareArrInit();
            DrawChessBoard(g);
            _Turn = 1;
            _modeGame = MODE.PLAYER;
            isReadyPvP = true;
        }

        //Repaint quân cờ
        public void ReDrawChess(Graphics g)
        {
            foreach (Square sqr in stack_Squares)
            {
                if (sqr.Owner == 1)
                {
                    _board.DrawSquare(sqr.Location, slb1, g);
                }
                else if (sqr.Owner == 2)
                {
                    _board.DrawSquare(sqr.Location, slb2, g);
                }
            }
        }
        #endregion Game Init


        //Chơi game
        #region Play Game
        //Xử lý đánh cờ với tọa độ của Click chuột
        public bool PlayChess(int MouseX, int MouseY, Graphics g)
        {
            //Xử lý click tràn biên
            if ((MouseX % Square._Width == 0) || (MouseY % Square._Height == 0) || (MouseX > _board.Columns * Square._Width) || (MouseY > _board.Rows * Square._Height))
            {
                return false;
            }

            //Lấy dòng/cột theo tọa độ
            int _Row = MouseY / Square._Height;
            int _Col = MouseX / Square._Width;

            //Nếu ô đã có người đánh thì ko đánh thêm đc
            if (SqrArr[_Row, _Col].Owner != 0)
                return false;

            //Đánh cờ thành công, lưu vị trí, sở hữu, vẽ quân cờ
            switch (_Turn)
            {
                case 1:
                    SqrArr[_Row, _Col].Owner = _Turn;
                    _board.DrawSquare(SqrArr[_Row, _Col].Location, slb1, g);
                    _Turn = 2;
                    break;

                case 2:
                    SqrArr[_Row, _Col].Owner = _Turn;
                    _board.DrawSquare(SqrArr[_Row, _Col].Location, slb2, g);
                    _Turn = 1;
                    break;

                default:
                    System.Windows.Forms.MessageBox.Show("Có lỗi!", "Thông báo");
                    break;
            }

            //Lưu vào stack những nước cỡ đã đánh
            stack_Squares.Push(new Square(SqrArr[_Row, _Col].Row, SqrArr[_Row, _Col].Column, SqrArr[_Row, _Col].Location, SqrArr[_Row, _Col].Owner));
            
            //Không thể Redo nếu đã đánh thêm 1 nước mới
            stack_Undo.Clear();

            return true;

        }
        #endregion Play Game


        //Chức năng Undo - Redo
        #region Undo-Redo Functions
        public void UndoFunc(Graphics g)
        {
            //Nếu chưa có quân nào thì ko thể undo
           if(stack_Squares.Count == 0)
           {
               return;
           }

           //Lấy 1 quân cờ ra khỏi stack các nước đã đánh
            Square sqr = stack_Squares.Pop();

            //Cho vào stack Undo để có thể Redo
            stack_Undo.Push(new Square(sqr.Row,sqr.Column,sqr.Location,sqr.Owner));

            //Thiết lập lại lượt đi
            if(sqr.Owner == 1)
            {
                _Turn = 1;
            }
            else
            {
                _Turn = 2;
            }

            //Gán vị trí hiện tại vừa undo thành ô cỡ rỗng
            SqrArr[sqr.Row, sqr.Column].Owner = 0;

            //Xóa quân cờ đã vẽ
            _board.EraseSquare(sqr.Location,eraseSlb,g);
        }

        public void RedoFunc(Graphics g)
        {
            if (stack_Undo.Count == 0)
            {
                return;
            }

            Square sqr = stack_Undo.Pop();
            stack_Squares.Push(new Square(sqr.Row, sqr.Column, sqr.Location, sqr.Owner));

            if (sqr.Owner == 1)
            {
                _Turn = 2;
            }
            else
            {
                _Turn = 1;
            }

            SqrArr[sqr.Row, sqr.Column].Owner = sqr.Owner;
            _board.DrawSquare(sqr.Location, (sqr.Owner == 2) ? slb2 : slb1, g);
        }
        #endregion Undo-Redo Functions


        //Xử lý điều kiện kết thúc game, và đưa ra thông báo
        #region End Game
        //Thông báo người thắng cuộc
        public void OnEndGame()
        {
            switch(_endGame)
            {
                case ENDGAME.DRAW:
                    System.Windows.Forms.MessageBox.Show("Hòa cờ!", "Icetea Việt");
                    break;

                case ENDGAME.PLAYER1:
                    System.Windows.Forms.MessageBox.Show("Người chơi 1 thắng!", "Icetea Việt");
                    break;

                case ENDGAME.PLAYER2:
                    System.Windows.Forms.MessageBox.Show("Người chơi 2 thắng!", "Icetea Việt");
                    
                    break;

                case ENDGAME.COM:
                    System.Windows.Forms.MessageBox.Show("Computer thắng!", "Icetea Việt");
                    break;
            }

            //Nhưng không cho đánh tiếp
            isReadyPvP = false;
            isReadyPvsCom = false;
        }

        //Xử lý điều kiện kết thúc game
        public bool IsEndGame()
        {
            //Đánh hết bàn cờ thì hòa cờ
            if(stack_Squares.Count >= _board.Columns*_board.Rows)
            {
                _endGame = ENDGAME.DRAW;
                return true;
            }

            //Xét các ô trên bàn cờ
            foreach(Square sqr in stack_Squares)
            {
                //Ô nào đã đánh thì duyệt 4 hướng tìm xem đã đủ đk thắng chưa và trả về giá trị
                if (sqr.Owner != 0)
                {
                    if ((ConsiderCol(sqr.Row, sqr.Column, sqr.Owner)) || (ConsiderRow(sqr.Row, sqr.Column, sqr.Owner)) || (ConsiderLeftDiagonal(sqr.Row, sqr.Column, sqr.Owner)) || (ConsiderRightDiagonal(sqr.Row, sqr.Column, sqr.Owner)))
                    {
                        if (_modeGame == MODE.EASYCOM)
                        {
                            _endGame = sqr.Owner == 1 ? ENDGAME.PLAYER1 : ENDGAME.COM;
                        }
                        else
                            _endGame = sqr.Owner == 1 ? ENDGAME.PLAYER1 : ENDGAME.PLAYER2;
                        return true;
                    }
                }
            }

            return false;
        }

        //Duyệt hàng dọc từ trên xuống
        private bool ConsiderCol(int currRow, int currCol, int currOwner)
        {
            //Lưu ý currRow và currCol ơ đây chưa hẳn là số dòng, chỉ là chỉ số index dòng và côt thôi
            //Nếu vị trí bắt đầu thấp hơn dòng thứ 15 - nghĩa là k đủ chứa 5 quân cờ phía dưới thì ko duyệt nữa
            if (currRow > _board.Rows - 5)
                return false;

            //Duyệt 4 dòng phía dưới nó nếu gặp quân địch hoặc ô trống thì ko duyệt nữa vì k đủ 5 quân
            for (int i = 1; i < 5; i++)
            {
                if (SqrArr[currRow + i, currCol].Owner != currOwner)
                    return false;
            }

            //Qua các dòng trên thì đã đủ 5 quân kế nhau cùng 1 cột - cần xử lý chặn 2 đầu
            //Nếu quân đầu ở sát biên thì ko thể bị chặn 2 đầu - đủ 5 quân nên -> thắng
            if ((currRow == 0) || (currRow + 5 == _board.Rows))
            {
                return true;
            }

            //Nếu k gần biên thì xem 2 đầu có bị chặn hay ko 
            if ((SqrArr[currRow - 1, currCol].Owner == 0) || (SqrArr[currRow + 5, currCol].Owner == 0))
            {
                return true;
            }

            //Nếu có chặn thì return false - chưa thắng
            return false;
        }

        //Duyệt dòng từ trái qua
        private bool ConsiderRow(int currRow, int currCol, int currOwner)
        {
            if (currCol > _board.Columns - 5)
                return false;

            for (int i = 1; i < 5; i++)
            {
                if (SqrArr[currRow, currCol + i].Owner != currOwner)
                    return false;
            }

            if ((currCol == 0) || (currCol + 5 == _board.Columns))
            {
                return true;
            }

            if ((SqrArr[currRow, currCol - 1].Owner == 0) || (SqrArr[currRow, currCol + 5].Owner == 0))
            {
                return true;
            }

            return false;
        }

        //Duyệt đường chéo xuôi từ trên xuống
        private bool ConsiderLeftDiagonal(int currRow, int currCol, int currOwner)
        {
            //Lưu ý currRow và currCol ơ đây chưa hẳn là số dòng, chỉ là chỉ số index dòng và côt thôi
            if ((currRow > _board.Rows - 5) || (currCol > _board.Columns - 5))
                return false;

            for (int i = 1; i < 5; i++)
            {
                if (SqrArr[currRow + i, currCol + i].Owner != currOwner)
                    return false;
            }

            if ((currRow == 0) || (currRow + 5 == _board.Rows) || (currCol == 0) || (currCol + 5 == _board.Columns))
            {
                return true;
            }

            if ((SqrArr[currRow - 1, currCol - 1].Owner == 0) || (SqrArr[currRow + 5, currCol + 5].Owner == 0))
            {
                return true;
            }

            return false;
        }

        //Duyệt đường chéo ngược từ dưới lên
        private bool ConsiderRightDiagonal(int currRow, int currCol, int currOwner)
        {
            if ((currRow < 4) || (currCol > _board.Columns - 5))
                return false;

            for (int i = 1; i < 5; i++)
            {
                if (SqrArr[currRow - i, currCol + i].Owner != currOwner)
                    return false;
            }

            if ((currRow == 4) || (currRow + 1 == _board.Rows) || (currCol == 0) || (currCol + 5 == _board.Columns))
            {
                return true;
            }

            if ((SqrArr[currRow + 1, currCol - 1].Owner == 0) || (SqrArr[currRow - 5, currCol + 5].Owner == 0))
            {
                return true;
            }

            return false;
        }
        #endregion End Game


        //Xử lý A.I và Heuristic để máy đánh cờ
        #region Play Vs Computer
        //Khởi động máy
        public void StartComputer(Graphics g)
        {
            //Chờ nước đi đầu của người, ta có thể lập trình cho máy đánh trước, nhưng phải thay đổi 1 chút trong hàm A.I
            if(stack_Squares.Count != 0)
            {
                Square Sqr = FindWayToMove();
                PlayChess(Sqr.Location.X + 1, Sqr.Location.Y + 1, g);
            }
        }

        //Tìm nước đi tối ưu theo điểm tấn công và phòng ngự
        private  Square FindWayToMove()
        {
            Square sqrResult = new Square();
            long MaxPts = 0;

            //Duyệt cả bàn cờ
            for(int i = 0; i < _board.Rows; i++)
            {
                for(int j = 0; j < _board.Columns; j++)
                {
                    //Nếu ô nào còn trống thì tính xem ô đó có tối ưu hay ko
                    if (SqrArr[i, j].Owner == 0)
                    {
                        //Tính điểm tấn công và phòng ngự
                        long AtkPts = AtkPts_InCol(i, j) + AtkPts_InRow(i, j) + AtkPts_InRightDiagonal(i, j) + AtkPts_InLeftDiagonal(i, j);
                        long DefPts = DefPts_InCol(i, j) + DefPts_InRow(i, j) + DefPts_InRightDiagonal(i, j) + DefPts_InLeftDiagonal(i, j);

                        //Lấy điểm lớn nhất
                        long temp = AtkPts < DefPts ? DefPts : AtkPts;
                        if (temp > MaxPts)
                        {
                            MaxPts = temp;
                            sqrResult = new Square(SqrArr[i, j].Row, SqrArr[i, j].Column, SqrArr[i, j].Location, SqrArr[i, j].Owner);
                        }
                    }
                }
            }

            return sqrResult;
        }

        //Xử lý suy nghĩ duyệt điểm của ô cờ
        #region A.I Heuristic
        //Duyệt tấn công
        private long AtkPts_InCol(int currRow, int currCol)
        {
            //Người luôn đánh trc nên Owner của máy sẽ là 2
            long SumPts = 0;
            int NumbOfUs = 0;
            int NumbOfEnemy = 0;

            //Duyệt từ trên xuống
            for (int i = 1; i < 6 && currRow + i < _board.Rows;i++)
            {
                //Nếu gặp quân ta thì cộng số lượng vào
                if (SqrArr[currRow + i, currCol].Owner == 2)
                {
                    NumbOfUs++;
                }
                else if (SqrArr[currRow + i, currCol].Owner == 1) //Gặp quân địch thì cộng số lượng, trừ điểm và break
                {
                    SumPts -= 9;
                    NumbOfEnemy++;
                    break;
                }
                else
                    break;
            }

            //Duyệt từ dưới lên
            for (int i = 1; i < 6 && currRow - i >= 0; i++)
            {
                if (SqrArr[currRow - i, currCol].Owner == 2)
                {
                    NumbOfUs++;
                }
                else if (SqrArr[currRow - i, currCol].Owner == 1)
                {
                    SumPts -= 9;
                    NumbOfEnemy++;
                    break;
                }
                else
                    break;
            }

            //Nếu bị chặn 2 đầu thì điểm tấn công = 0
            if(NumbOfEnemy == 2)
            {
                return 0;
            }

            //Tính điểm tổng
            SumPts -= AtkArr[NumbOfEnemy];
            SumPts += AtkArr[NumbOfUs];

                return SumPts;
        }

        private long AtkPts_InRow(int currRow, int currCol)
        {
            //Người luôn đánh trc nên Owner của máy sẽ là 2
            long SumPts = 0;
            int NumbOfUs = 0;
            int NumbOfEnemy = 0;

            for (int i = 1; i < 6 && currCol + i < _board.Columns; i++)
            {
                if (SqrArr[currRow, currCol + i].Owner == 2)
                {
                    NumbOfUs++;
                }
                else if (SqrArr[currRow, currCol + i].Owner == 1)
                {
                    SumPts -= 9;
                    NumbOfEnemy++;
                    break;
                }
                else
                    break;
            }

            for (int i = 1; i < 6 && currCol - i >= 0; i++)
            {
                if (SqrArr[currRow, currCol - i].Owner == 2)
                {
                    NumbOfUs++;
                }
                else if (SqrArr[currRow, currCol - i].Owner == 1)
                {
                    SumPts -= 9;
                    NumbOfEnemy++;
                    break;
                }
                else
                    break;
            }

            if (NumbOfEnemy == 2)
            {
                return 0;
            }

            //SumPts -= DefArr[NumbOfEnemy + 1];
            SumPts -= AtkArr[NumbOfEnemy];
            SumPts += AtkArr[NumbOfUs];

            return SumPts;
        }

        private long AtkPts_InRightDiagonal(int currRow, int currCol)
        {
            //Người luôn đánh trc nên Owner sẽ là 2
            long SumPts = 0;
            int NumbOfUs = 0;
            int NumbOfEnemy = 0;

            //Duyệt chéo ngược lên từ trái sang phải
            for (int i = 1; i < 6 && currCol + i < _board.Columns && currRow - i >= 0; i++)
            {
                if (SqrArr[currRow - i, currCol + i].Owner == 2)
                {
                    NumbOfUs++;
                }
                else if (SqrArr[currRow - i, currCol + i].Owner == 1)
                {
                    SumPts -= 9;
                    NumbOfEnemy++;
                    break;
                }
                else
                    break;
            }

            //Duyệt chéo ngược xuống từ phải sang trái
            for (int i = 1; i < 6 && currRow + i < _board.Rows && currCol - i >= 0; i++)
            {
                if (SqrArr[currRow + i, currCol - i].Owner == 2)
                {
                    NumbOfUs++;
                }
                else if (SqrArr[currRow + i, currCol - i].Owner == 1)
                {
                    SumPts -= 9;
                    NumbOfEnemy++;
                    break;
                }
                else
                    break;
            }

            if (NumbOfEnemy == 2)
            {
                return 0;
            }

            
            SumPts -= AtkArr[NumbOfEnemy];
            SumPts += AtkArr[NumbOfUs];

            return SumPts;
        }

        private long AtkPts_InLeftDiagonal(int currRow, int currCol)
        {
            //Người luôn đánh trc nên Owner của máy sẽ là 2
            long SumPts = 0;
            int NumbOfUs = 0;
            int NumbOfEnemy = 0;

            for (int i = 1; i < 6 && currCol - i >= 0 && currRow - i >= 0; i++)
            {
                if (SqrArr[currRow - i, currCol - i].Owner == 2)
                {
                    NumbOfUs++;
                }
                else if (SqrArr[currRow - i, currCol - i].Owner == 1)
                {
                    SumPts -= 9;
                    NumbOfEnemy++;
                    break;
                }
                else
                    break;
            }

            for (int i = 1; i < 6 && currRow + i < _board.Rows && currCol + i < _board.Columns; i++)
            {
                if (SqrArr[currRow + i, currCol + i].Owner == 2)
                {
                    NumbOfUs++;
                }
                else if (SqrArr[currRow + i, currCol + i].Owner == 1)
                {
                    SumPts -= 9;
                    NumbOfEnemy++;
                    break;
                }
                else
                    break;
            }

            if (NumbOfEnemy == 2)
            {
                return 0;
            }

            SumPts -= AtkArr[NumbOfEnemy];
            SumPts += AtkArr[NumbOfUs];

            return SumPts;
        }

        //Duyệt phòng thủ
        private long DefPts_InCol(int currRow, int currCol)
        {
            //Người luôn đánh trc nên Owner sẽ là 2
            long SumPts = 0;
            int NumbOfUs = 0;
            int NumbOfEnemy = 0;

            for (int i = 1; i < 6 && currRow + i < _board.Rows; i++)
            {
                if (SqrArr[currRow + i, currCol].Owner == 2)
                {
                    NumbOfUs++;
                    break;
                }
                else if (SqrArr[currRow + i, currCol].Owner == 1)
                {
                    NumbOfEnemy++;
                }
                else
                    break;
            }

            for (int i = 1; i < 6 && currRow - i >= 0; i++)
            {
                if (SqrArr[currRow - i, currCol].Owner == 2)
                {
                    NumbOfUs++;
                    break;
                }
                else if (SqrArr[currRow - i, currCol].Owner == 1)
                {
                    NumbOfEnemy++;
                }
                else
                    break;
            }

            if (NumbOfUs == 2)
            {
                return 0;
            }

            //Tính điểm phòng thủ
            SumPts += DefArr[NumbOfEnemy];
            if(NumbOfEnemy > 0)
                SumPts -= AtkArr[NumbOfUs] * 2;

            return SumPts;
        }

        private long DefPts_InRow(int currRow, int currCol)
        {
            //Người luôn đánh trc nên Owner sẽ là 2
            long SumPts = 0;
            int NumbOfUs = 0;
            int NumbOfEnemy = 0;

            for (int i = 1; i < 6 && currCol + i < _board.Columns; i++)
            {
                if (SqrArr[currRow, currCol + i].Owner == 2)
                {
                    NumbOfUs++;
                    break;
                }
                else if (SqrArr[currRow, currCol + i].Owner == 1)
                {
                    NumbOfEnemy++;
                }
                else
                    break;
            }

            for (int i = 1; i < 6 && currCol - i >= 0; i++)
            {
                if (SqrArr[currRow, currCol - i].Owner == 2)
                {
                    NumbOfUs++;
                    break;
                }
                else if (SqrArr[currRow, currCol - i].Owner == 1)
                {
                    NumbOfEnemy++;
                }
                else
                    break;
            }

            if (NumbOfUs == 2)
            {
                return 0;
            }

           
            SumPts += DefArr[NumbOfEnemy];
            if (NumbOfEnemy > 0)
                SumPts -= AtkArr[NumbOfUs] * 2;

            return SumPts;
        }

        private long DefPts_InRightDiagonal(int currRow, int currCol)
        {
            //Người luôn đánh trc nên Owner sẽ là 2
            long SumPts = 0;
            int NumbOfUs = 0;
            int NumbOfEnemy = 0;

            for (int i = 1; i < 6 && currCol + i < _board.Columns && currRow - i >= 0; i++)
            {
                if (SqrArr[currRow - i, currCol + i].Owner == 2)
                {
                    NumbOfUs++;
                    break;
                }
                else if (SqrArr[currRow - i, currCol + i].Owner == 1)
                {
                    NumbOfEnemy++;
                }
                else
                    break;
            }

            for (int i = 1; i < 6 && currRow + i < _board.Rows && currCol - i >= 0; i++)
            {
                if (SqrArr[currRow + i, currCol - i].Owner == 2)
                {
                    NumbOfUs++;
                    break;
                }
                else if (SqrArr[currRow + i, currCol - i].Owner == 1)
                {
                    NumbOfEnemy++;
                }
                else
                    break;
            }

            if (NumbOfUs == 2)
            {
                return 0;
            }

            
            SumPts += DefArr[NumbOfEnemy];
            if (NumbOfEnemy > 0)
                SumPts -= AtkArr[NumbOfUs] * 2;

            return SumPts;
        }

        private long DefPts_InLeftDiagonal(int currRow, int currCol)
        {
            //Người luôn đánh trc nên Owner sẽ là 2
            long SumPts = 0;
            int NumbOfUs = 0;
            int NumbOfEnemy = 0;

            for (int i = 1; i < 6 && currCol - i >= 0 && currRow - i >= 0; i++)
            {
                if (SqrArr[currRow - i, currCol - i].Owner == 2)
                {
                    NumbOfUs++;
                    break;
                }
                else if (SqrArr[currRow - i, currCol - i].Owner == 1)
                {
                    NumbOfEnemy++;
                }
                else
                    break;
            }

            for (int i = 1; i < 6 && currRow + i < _board.Rows && currCol + i < _board.Columns; i++)
            {
                if (SqrArr[currRow + i, currCol + i].Owner == 2)
                {
                    NumbOfUs++;
                    break;
                }
                else if (SqrArr[currRow + i, currCol + i].Owner == 1)
                {
                    NumbOfEnemy++;
                }
                else
                    break;
            }

            if (NumbOfUs == 2)
            {
                return 0;
            }

            
            SumPts += DefArr[NumbOfEnemy];
            if (NumbOfEnemy > 0)
                SumPts -= AtkArr[NumbOfUs] * 2;

            return SumPts;
        }
        #endregion A.I Heuristic
        #endregion Play Vs Computer

        //Save-Load Game
        public void SaveGame(string path)
        {
            //Let try XML Serialize
            //XML is quite old, so i don't waste my time to learn it, just use JSON
            //Lưu stack đã đi và mảng ô cờ bằng 2 cách cho đa dạng

            //Dùng StreamWriter tạo một file với tên là path
            using (StreamWriter file = File.CreateText(@path + ".json"))
            {
                //Ghi vào file Json trực tiếp
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, SqrArr);
            }

            //Đảo thứ tự của stack thông qua mảng thường, convert qua String. Vì khi load lên stack sẽ load ngược
            Square[] tmp = stack_Squares.ToArray();
            Array.Reverse(tmp);
            string json = JsonConvert.SerializeObject(tmp);

            //Ghi vào file text
            System.IO.File.WriteAllText(@path + ".txt", json);

            //Thông báo thành công
            MessageBox.Show("Đã lưu!", "IceTea Việt", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        public bool LoadGame(string path)
        {
            //Let try XML Serialize
            try
            {
                using (StreamReader file = File.OpenText(@path + ".json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    SqrArr = (Square[,])serializer.Deserialize(file, typeof(Square[,]));
                }

                //Đọc trực tiếp string từ text lên và convert ngược lại stack
                string json;
                json = System.IO.File.ReadAllText(@path + ".txt");
                stack_Squares = JsonConvert.DeserializeObject<Stack<Square>>(json);

                //Xử lý lượt đi
                if (stack_Squares.Peek().Owner == 1)
                {
                    _Turn = 2;
                }
                else if (stack_Squares.Peek().Owner == 2)
                {
                    _Turn = 1;
                }

                MessageBox.Show("Load thành công!", "IceTea Việt", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không tìm thấy ván cờ đã lưu!", "IceTea Việt", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
