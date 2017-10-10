using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace twozerofoureight
{
    public partial class TwoZeroFourEightView : Form, View
    {
        Model model;
        Controller controller;
       
        public TwoZeroFourEightView()
        {
            InitializeComponent();
            model = new TwoZeroFourEightModel();
            model.AttachObserver(this);
            controller = new TwoZeroFourEightController();
            controller.AddModel(model);
            controller.ActionPerformed(TwoZeroFourEightController.LEFT);
            //test
        }

        public void Notify(Model m)
        {
            UpdateBoard(((TwoZeroFourEightModel) m).GetBoard());
            UpdateScore(((TwoZeroFourEightModel)m).GetScore());
            IsGameOver(((TwoZeroFourEightModel)m).IsGameOver());
        }

        private void UpdateTile(Label l, int i)
        {
            if (i != 0)
            {
                l.Text = Convert.ToString(i);
            } else {
                l.Text = "";
            }
            switch (i)
            {
                case 0:
                    l.BackColor = ColorTranslator.FromHtml("#ccc0b3");
                    break;
                case 2:
                    l.BackColor = ColorTranslator.FromHtml("#eee4da");
                    //change text black color
                    l.ForeColor = ColorTranslator.FromHtml("#776e65");
                    break;
                case 4:
                    l.BackColor = ColorTranslator.FromHtml("#ede0c8");
                    //change text black color
                    l.ForeColor = ColorTranslator.FromHtml("#776e65");
                    break;
                case 8:
                    l.BackColor = ColorTranslator.FromHtml("#f3b179");
                    //change text white color
                    l.ForeColor = ColorTranslator.FromHtml("#f9f6f2");
                    break;
                case 16:
                    l.BackColor = ColorTranslator.FromHtml("#f59563");
                    //change text white color
                    l.ForeColor = ColorTranslator.FromHtml("#f9f6f2");
                    break;
                case 32:
                    l.BackColor = ColorTranslator.FromHtml("#f67c5f");
                    //change text white color
                    l.ForeColor = ColorTranslator.FromHtml("#f9f6f2");
                    break;
                case 64:
                    l.BackColor = ColorTranslator.FromHtml("#f65e3b");
                    //change text white color
                    l.ForeColor = ColorTranslator.FromHtml("#f9f6f2");
                    break;
                case 128:
                    l.BackColor = ColorTranslator.FromHtml("#ecce70");
                    //change text white color
                    l.ForeColor = ColorTranslator.FromHtml("#f9f6f2");
                    break;
                case 256:
                    l.BackColor = ColorTranslator.FromHtml("#edcb61");
                    //change text white color
                    l.ForeColor = ColorTranslator.FromHtml("#f9f6f2");
                    break;
                case 512:
                    l.BackColor = ColorTranslator.FromHtml("#ecc750");
                    //change text white color
                    l.ForeColor = ColorTranslator.FromHtml("#f9f6f2");
                    break;
                case 1024:
                    l.BackColor = ColorTranslator.FromHtml("#edc440");
                    //change text white color
                    l.ForeColor = ColorTranslator.FromHtml("#f9f6f2");
                    break;
                case 2048:
                    l.BackColor = ColorTranslator.FromHtml("#ecc12e");
                    //change text white color
                    l.ForeColor = ColorTranslator.FromHtml("#f9f6f2");
                    break;
                case 4096:
                    l.BackColor = ColorTranslator.FromHtml("#ff3d3d");
                    //change text white color
                    l.ForeColor = ColorTranslator.FromHtml("#f9f6f2");
                    break;
                case 8192:
                    l.BackColor = Color.MediumBlue;
                    break;
                case 16384:
                    l.BackColor = Color.Blue;
                    break;
                case 32768:
                    l.BackColor = Color.Green;
                    break;
                case 65536:
                    l.BackColor = Color.LightGreen;
                    break;
                default:
                    l.BackColor = ColorTranslator.FromHtml("#ff1e1e");
                    //change text white color
                    l.ForeColor = ColorTranslator.FromHtml("#f9f6f2");
                    break;
            }
            switch (i / 1000)
            {
                case 0:
                    changeFontsize(l, 20.25F);
                    break;
                case 1:
                    changeFontsize(l, 16);
                    break;
                case 10:
                    changeFontsize(l, 14);
                    break;
                case 100:
                    changeFontsize(l, 12);
                    break;
                /*default:
                    changeFontsize(l, 8);
                    break;*/
            }
        }

        private void changeFontsize(Label l, float size)
        {
            l.Font = new Font("Consolas", size, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
        }

        private void UpdateBoard(int[,] board)
        {
            UpdateTile(lbl00,board[0, 0]);
            UpdateTile(lbl01,board[0, 1]);
            UpdateTile(lbl02,board[0, 2]);
            UpdateTile(lbl03,board[0, 3]);
            UpdateTile(lbl10,board[1, 0]);
            UpdateTile(lbl11,board[1, 1]);
            UpdateTile(lbl12,board[1, 2]);
            UpdateTile(lbl13,board[1, 3]);
            UpdateTile(lbl20,board[2, 0]);
            UpdateTile(lbl21,board[2, 1]);
            UpdateTile(lbl22,board[2, 2]);
            UpdateTile(lbl23,board[2, 3]);
            UpdateTile(lbl30,board[3, 0]);
            UpdateTile(lbl31,board[3, 1]);
            UpdateTile(lbl32,board[3, 2]);
            UpdateTile(lbl33,board[3, 3]);
        }

        private void UpdateScore(int score)
        {
            lblScore.Text = Convert.ToString(score);
        }

        private void IsGameOver(bool isOver)
        {
            lblGameOver.BackColor = System.Drawing.Color.Transparent;
            lblGameOver.Visible = isOver;
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            controller.ActionPerformed(TwoZeroFourEightController.LEFT);
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            controller.ActionPerformed(TwoZeroFourEightController.RIGHT);
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            controller.ActionPerformed(TwoZeroFourEightController.UP);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            controller.ActionPerformed(TwoZeroFourEightController.DOWN);
        }

        private void actionKey(Keys keys)
        {
            switch (keys)
            {
                case Keys.W:
                    controller.ActionPerformed(TwoZeroFourEightController.UP);
                    break;
                case Keys.A:
                    controller.ActionPerformed(TwoZeroFourEightController.LEFT);
                    break;
                case Keys.S:
                    controller.ActionPerformed(TwoZeroFourEightController.DOWN);
                    break;
                case Keys.D:
                    controller.ActionPerformed(TwoZeroFourEightController.RIGHT);
                    break;
                case Keys.Up:
                    controller.ActionPerformed(TwoZeroFourEightController.UP);
                    break;
                case Keys.Left:
                    controller.ActionPerformed(TwoZeroFourEightController.LEFT);
                    break;
                case Keys.Down:
                    controller.ActionPerformed(TwoZeroFourEightController.DOWN);
                    break;
                case Keys.Right:
                    controller.ActionPerformed(TwoZeroFourEightController.RIGHT);
                    break;
            }
        }

        private void btnLeft_KeyDown(object sender, KeyEventArgs e)
        {
            actionKey(e.KeyData);
        }

        private void btnUp_KeyDown(object sender, KeyEventArgs e)
        {
            actionKey(e.KeyData);
        }

        private void btnRight_KeyDown(object sender, KeyEventArgs e)
        {
            actionKey(e.KeyData);
        }

        private void btnDown_KeyDown(object sender, KeyEventArgs e)
        {
            actionKey(e.KeyData);
        }
        
        private void btnNewG_KeyDown(object sender, KeyEventArgs e)
        {
            actionKey(e.KeyData);
        }

        private void btnNewGLeft_Click(object sender, EventArgs e)
        {
            //New model
            model = new TwoZeroFourEightModel();
            model.AttachObserver(this);
            controller = new TwoZeroFourEightController();
            controller.AddModel(model);
            controller.ActionPerformed(TwoZeroFourEightController.LEFT);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            actionKey(keyData);
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
