using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityTest
{
    public partial class LoginForm : Form
    {
        bool captchaRequired = false;
        bool canLogin = true;
        public Users user;
        public LoginForm()
        {
            InitializeComponent();
        }
        string captchaText = "";
        string alphabetAndNumbers = "abcdefghijklmnopqrstuvwxyz0123456789";
        private void GenerateCaptcha()
        {
            int iWidth = pictureBox1.Width;
            int iHeight = pictureBox1.Height;
            Random oRandom = new Random();

            int[] aBackgroundNoiseColor = new int[] { 150, 150, 150 };
            int[] aTextColor = new int[] { 0, 0, 0 };
            int[] aFontEmSizes = new int[] { 15, 20, 25, 30, 35 };

            string[] aFontNames = new string[]
            {
                 "Comic Sans MS",
                 "Arial",
                 "Times New Roman",
                 "Georgia",
                 "Verdana",
                 "Geneva"
            };
            FontStyle[] aFontStyles = new FontStyle[]
            {
                 FontStyle.Bold,
                 FontStyle.Italic,
                 FontStyle.Regular,
                 FontStyle.Strikeout,
                 FontStyle.Underline
            };
            HatchStyle[] aHatchStyles = new HatchStyle[]
            {
                 HatchStyle.BackwardDiagonal, HatchStyle.Cross,
                    HatchStyle.DashedDownwardDiagonal, HatchStyle.DashedHorizontal,
                 HatchStyle.DashedUpwardDiagonal, HatchStyle.DashedVertical,
                    HatchStyle.DiagonalBrick, HatchStyle.DiagonalCross,
                 HatchStyle.Divot, HatchStyle.DottedDiamond, HatchStyle.DottedGrid,
                    HatchStyle.ForwardDiagonal, HatchStyle.Horizontal,
                 HatchStyle.HorizontalBrick, HatchStyle.LargeCheckerBoard,
                    HatchStyle.LargeConfetti, HatchStyle.LargeGrid,
                 HatchStyle.LightDownwardDiagonal, HatchStyle.LightHorizontal,
                    HatchStyle.LightUpwardDiagonal, HatchStyle.LightVertical,
                 HatchStyle.Max, HatchStyle.Min, HatchStyle.NarrowHorizontal,
                    HatchStyle.NarrowVertical, HatchStyle.OutlinedDiamond,
                 HatchStyle.Plaid, HatchStyle.Shingle, HatchStyle.SmallCheckerBoard,
                    HatchStyle.SmallConfetti, HatchStyle.SmallGrid,
                 HatchStyle.SolidDiamond, HatchStyle.Sphere, HatchStyle.Trellis,
                    HatchStyle.Vertical, HatchStyle.Wave, HatchStyle.Weave,
                 HatchStyle.WideDownwardDiagonal, HatchStyle.WideUpwardDiagonal, HatchStyle.ZigZag
            };

            //Create captchaText
            captchaText = "";

            for (int k = 0; k < 4; k++)
            {
                captchaText += alphabetAndNumbers[oRandom.Next(0, alphabetAndNumbers.Length)];
            }
            //Creates an output Bitmap
            Bitmap image = new Bitmap(iWidth, iHeight);
            Graphics oGraphics = Graphics.FromImage(image);
            oGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;

            //Create a Drawing area
            RectangleF oRectangleF = new RectangleF(0, 0, iWidth, iHeight);
            Brush oBrush = default(Brush);

            //Draw background (Lighter colors RGB 100 to 255)
            oBrush = new HatchBrush(aHatchStyles[oRandom.Next
                (aHatchStyles.Length - 1)], Color.FromArgb((oRandom.Next(100, 255)),
                (oRandom.Next(100, 255)), (oRandom.Next(100, 255))), Color.White);
            oGraphics.FillRectangle(oBrush, oRectangleF);

            System.Drawing.Drawing2D.Matrix oMatrix = new System.Drawing.Drawing2D.Matrix();
            int i = 0;
            for (i = 0; i <= captchaText.Length - 1; i++)
            {
                oMatrix.Reset();
                int iChars = captchaText.Length;
                int x = iWidth / (iChars + 1) * i;
                int y = iHeight / 2;

                //Rotate text Random
                oMatrix.RotateAt(oRandom.Next(-40, 40), new PointF(x, y));
                oGraphics.Transform = oMatrix;

                //Draw the letters with Random Font Type, Size and Color
                oGraphics.DrawString
                (
                //Text
                captchaText.Substring(i, 1),
                //Random Font Name and Style
                new Font(aFontNames[oRandom.Next(aFontNames.Length - 1)],
                   aFontEmSizes[oRandom.Next(aFontEmSizes.Length - 1)],
                   aFontStyles[oRandom.Next(aFontStyles.Length - 1)]),
                //Random Color (Darker colors RGB 0 to 100)
                new SolidBrush(Color.FromArgb(oRandom.Next(0, 100),
                   oRandom.Next(0, 100), oRandom.Next(0, 100))),
                x,
                oRandom.Next(10, 40)
                );
                oGraphics.ResetTransform();
            }
            pictureBox1.Image = image;
          
        }
        private bool SuccesfulLogin(string login, string password)
        {
            using (SvetEntities context = new SvetEntities())
            {
                user = context.Users.FirstOrDefault(x => x.Login == login && x.Password==password);
                if (user == null) return false;
                else if (user != null) return true;
            }
            return false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (!captchaRequired)
            {
                if (SuccesfulLogin(textBox1.Text,textBox2.Text))
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Неправильный логин или пароль");
                    GenerateCaptcha();
                    captchaRequired = true;
                    pictureBox1.Visible = true;
                    textBox3.Visible = true;
                    button3.Visible = true;
                }
            }
            else if(canLogin)
            {     
                if (SuccesfulLogin(textBox1.Text, textBox2.Text) && textBox3.Text == captchaText.ToString())
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    if(!SuccesfulLogin(textBox1.Text, textBox2.Text)) MessageBox.Show("Неправильный логин или пароль, следующая попытка будет доступна через 10 секунд");
                    if (textBox3.Text != captchaText.ToString()) MessageBox.Show("Неверно введена каптча, следующая попытка будет доступна через 10 секунд");
                    GenerateCaptcha();
                    canLogin = false;
                    Task.Delay(TimeSpan.FromMilliseconds(10000))
                    .ContinueWith(task => canLogin=true);
                }
            }
            else if(!canLogin)
            {
                MessageBox.Show("Подождите перед следующей попыткой входа");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GenerateCaptcha();
        }
    }
}
