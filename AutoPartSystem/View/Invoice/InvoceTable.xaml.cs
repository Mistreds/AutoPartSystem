using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutoPartSystem.View.Invoice
{
    /// <summary>
    /// Логика взаимодействия для InvoceTable.xaml
    /// </summary>
    public partial class InvoceTable : UserControl
    {
        public InvoceTable()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog1.Filter = "Excel (*.jpeg)|*.jpeg";
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                return;
            var newFile = new FileInfo(saveFileDialog1.FileName);
            SaveToPNG(InvoiceGrid, new Size { Height = this.ActualHeight, Width = this.ActualWidth }, saveFileDialog1.FileName);
        }
        public  void SaveToPNG(FrameworkElement frameworkElement, Size size, string fileName)
        {
            using (FileStream stream = new FileStream(string.Format("{0}", fileName), FileMode.Create))
            {
               
                SaveToPNG(frameworkElement, size, stream);
            }
        }

        public  void SaveToPNG(FrameworkElement frameworkElement, Size size, Stream stream)
        {
            Transform transform = frameworkElement.LayoutTransform;
            frameworkElement.LayoutTransform = null;
            Thickness margin = frameworkElement.Margin;
            frameworkElement.Margin = new Thickness(0, 0, margin.Right - margin.Left, margin.Bottom - margin.Top);
            frameworkElement.Measure(size);
            frameworkElement.Arrange(new Rect(size));
            RenderTargetBitmap bmp = new RenderTargetBitmap((int)size.Width, (int)size.Height, 96, 96, PixelFormats.Pbgra32);
            bmp.Render(frameworkElement);
            frameworkElement.LayoutTransform = transform;
            frameworkElement.Margin = margin;
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Interlace = PngInterlaceOption.On;
            encoder.Frames.Add(BitmapFrame.Create(bmp));
            encoder.Save(stream);
        }
    }
}
