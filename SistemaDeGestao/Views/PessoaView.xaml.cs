using System;
using System.Collections.Generic;
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

namespace SistemaDeGestao.Views
{
    public partial class PessoaView : UserControl
    {
        public PessoaView()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty CpfMaskedTextProperty =
            DependencyProperty.RegisterAttached("CpfMaskedText", typeof(string), typeof(PessoaView), new PropertyMetadata(string.Empty, OnCpfMaskedTextChanged));

        public static string GetCpfMaskedText(DependencyObject obj)
        {
            return (string)obj.GetValue(CpfMaskedTextProperty);
        }

        public static void SetCpfMaskedText(DependencyObject obj, string value)
        {
            obj.SetValue(CpfMaskedTextProperty, value);
        }

        private static void OnCpfMaskedTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var textBox = d as TextBox;
            if (textBox == null) return;

            textBox.TextChanged -= TextBox_TextChanged;
            textBox.TextChanged += TextBox_TextChanged;
        }

        private static void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null) return;

            var unmaskedText = new string(textBox.Text.Where(char.IsDigit).ToArray());

            if (unmaskedText.Length > 11)
            {
                unmaskedText = unmaskedText.Substring(0, 11);
            }

            var maskedText = unmaskedText;

            if (maskedText.Length > 3)
            {
                maskedText = maskedText.Insert(3, ".");
            }
            if (maskedText.Length > 7)
            {
                maskedText = maskedText.Insert(7, ".");
            }
            if (maskedText.Length > 11)
            {
                maskedText = maskedText.Insert(11, "-");
            }

            textBox.TextChanged -= TextBox_TextChanged;
            textBox.Text = maskedText;
            textBox.TextChanged += TextBox_TextChanged;

            textBox.CaretIndex = textBox.Text.Length;
        }
    }
}
