using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoCaro
{
    public partial class Form1 : Form
    {
        //Khai báo biến
        CaroChess caro = new CaroChess();
        Graphics grs;
        string str_GameGuide;


        public Form1()
        {
            InitializeComponent();
            //Tạo đôi tượng Graphics trên panel chơi game

            grs = pnl_CaroChess.CreateGraphics();
            caro.SquareArrInit();
            str_GameGuide = "- Hai bên lần lượt đánh vào\n từng ô.\n\n- Bên nào đạt 5 con trên 1\n hàng ngang, hàng dọc, chéo\n xuôi, chéo ngược mà không\n bị chặn 2 đầu là người chiến\n thắng. \n\n- Nếu bàn cờ đầy thì hòa cờ .";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Xử lý chữ chạy panel hướng dẫn
            timer_GameGuide.Enabled = true;
            lblGameGuide.Text = str_GameGuide;
        }


        private void timer_GameGuide_Tick(object sender, EventArgs e)
        {
            //Xử lý vòng lặp chữ chạy
            lblGameGuide.Location = new Point(lblGameGuide.Location.X, lblGameGuide.Location.Y - 1);
            if(lblGameGuide.Location.Y + lblGameGuide.Height < 0)
            {
                lblGameGuide.Location = new Point(lblGameGuide.Location.X, pnl_GameGuide.Location.Y);
            }
        }


        //Xử lý sự kiện repaint - Vẽ lại bàn cờ và quân cờ
        private void pnl_CaroChess_Paint(object sender, PaintEventArgs e)
        {
            caro.DrawChessBoard(grs);
            caro.ReDrawChess(grs);
        }

        //Sự kiện đánh cờ
        private void pnl_CaroChess_MouseClick(object sender, MouseEventArgs e)
        {
            //Bàn cờ sẵn sàng khi đã chọn chế độ chơi
            if ((!caro.isReadyPvP) && (!caro.isReadyPvsCom))
            {
                MessageBox.Show("Chưa chọn chế độ chơi", "IceTea Việt",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }

            //Nếu đánh cờ thành công sẽ xét xem đã thắng chưa
            if (caro.PlayChess(e.Location.X, e.Location.Y, grs))
            {
                if (caro.IsEndGame())
                {
                    caro.OnEndGame();
                    return;
                }
                else if (caro.modeGame == MODE.EASYCOM) //Nếu mode là Computer mới đi vào cho Com đánh cờ
                {
                    //Khởi động máy
                    caro.StartComputer(grs);
                    if (caro.IsEndGame())
                    {
                        caro.OnEndGame();
                        return;
                    }
                }
            }
        }

        //Bắt đầu chơi với người
        private void PvsP(object sender, EventArgs e)
        {
            grs.Clear(pnl_CaroChess.BackColor);
            caro.StartPvsP(grs);
        }

        //Chức năng Undo
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            caro.UndoFunc(grs);
        }

        //Chức năng Redo
        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            caro.RedoFunc(grs);
        }

        //Bắt đầu chơi với máy dễ
        private void PvsEasyCom(object sender, EventArgs e)
        {
            grs.Clear(pnl_CaroChess.BackColor);
            caro.StartPvsEasyCom(grs);
        }

        //Xử lý thoát game
        private void bt_Exit_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Bạn có muốn thoát?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dlr == DialogResult.OK)
            {
                Application.Exit();

            }
        }

        //Khung giới thiệu
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Demo game Caro v1", "IceTea Việt");
        }

        private void normalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng này đang được hoàn thiện!", "IceTea Việt");
        }

        //Lưu bàn cờ
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str_Path;
            
            if(caro.modeGame == MODE.PLAYER)
            {
                str_Path = @"PlayerSave";
            }
            else
            {
                str_Path = @"ComputerSave";
            }

            caro.SaveGame(str_Path);
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((caro.isReadyPvP) || (caro.isReadyPvsCom))
            {
                string str_Path;

                if (caro.modeGame == MODE.PLAYER)
                {
                    str_Path = @"PlayerSave";
                }
                else
                {
                    str_Path = @"ComputerSave";
                }

                if (caro.LoadGame(str_Path))
                {
                    grs.Clear(pnl_CaroChess.BackColor);
                    caro.DrawChessBoard(grs);
                    caro.ReDrawChess(grs);
                }
            }
            else
            {
                MessageBox.Show("Chưa chọn chế độ chơi", "IceTea Việt", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
               
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
        }

       

        
    }
}
