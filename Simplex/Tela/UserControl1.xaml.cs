using Simplex.Calculo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Windows.Threading;

namespace Simplex
{
    /// <summary>
    /// Interaction logic for MainScreen.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        private UserControl1Controller controller;
        public UserControl1()
        {
            InitializeComponent();
            controller = new UserControl1Controller();
            this.DataContext = controller;
        }

        private void Button_Click_Limpar(object sender, RoutedEventArgs e)
        {
            controller = new UserControl1Controller();
            this.DataContext = controller;
        }

        private void Button_Click_Travar(object sender, RoutedEventArgs e)
        {
            controller.Travar = true;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox tb && string.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "0";
            }
        }

        private void Button_Click_Adicionar_Inequacao(object sender, RoutedEventArgs e)
        {
            controller.CurrentInequacao.Ordem = controller.ListaInequacao.Count();
            controller.ListaInequacao.Add(controller.CurrentInequacao.DuplicarObjeto<Inequacao>());
            controller.CurrentInequacao = new Inequacao();
            ColocarFocus(x1Inequacao);
        }

        private void ColocarFocus(IInputElement focus)
        {
            _ = Dispatcher.BeginInvoke((Action)delegate
            {
                _ = Keyboard.Focus(focus);
            }, DispatcherPriority.ContextIdle);
        }

        private void Button_Click_Calcular(object sender, RoutedEventArgs e)
        {
            if (controller.FuncaoObjetiva.X1 == 0 && controller.FuncaoObjetiva.X2 == 0 && controller.FuncaoObjetiva.X3 == 0)
            {
                MessageBox.Show("É necessário informar uma função OBJ");
            }
            else if (!controller.ListaInequacao.Any())
            {
                MessageBox.Show("É necessário adicionar ao menos uma inequação");
            }

            controller.Calcular();
        }
    }
}
